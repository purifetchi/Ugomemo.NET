using System.IO;
using Ugomemo.NET.Util;

namespace Ugomemo.NET.Animation
{
    public sealed partial class Frame
    {
        /// <summary>
        /// Constructs a new frame from the reader.
        /// 
        /// NOTE: The reader must point to the beginning of this frame's data!
        /// </summary>
        /// <param name="reader">The bit reader that has the flipnote.</param>
        internal Frame(BinaryReader reader)
        {
            ParseHeader(reader);
            ParseFrame(reader);
        }

        /// <summary>
        /// Parses the header of this flipnote.
        /// </summary>
        private void ParseHeader(BinaryReader reader)
        {
            var flags = reader.ReadByte();
            FrameInfo = new FrameInfo
            {
                PaperColor = (PaperColor)(flags & 0x1),
                Layer1Color = (PenColor)((flags >> 1) & 0x3),
                Layer2Color = (PenColor)((flags >> 3) & 0x3),
                Translated = ((flags >> 5) & 0x3) > 0,
                Type = (FrameType)((flags >> 7) & 0x1)
            };

            if (FrameInfo.Translated)
            {
                FrameInfo.TranslateX = reader.ReadByte();
                FrameInfo.TranslateY = reader.ReadByte();
            }
        }

        /// <summary>
        /// Parses the line compression table for one layer.
        /// </summary>
        private LineCompressionMethod[] ParseLineCompressionTable(BinaryReader reader)
        {
            var lineMethods = new LineCompressionMethod[HEIGHT];
            var line = 0;

            for (var byteOffset = 0; byteOffset < 48; byteOffset++)
            {
                var thisByte = reader.ReadByte();
                for (var bitOffset = 0; bitOffset < 8; bitOffset += 2)
                {
                    lineMethods[line] = (LineCompressionMethod)((thisByte >> bitOffset) & 0x03);
                    line += 1;
                }
            }

            return lineMethods;
        }

        /// <summary>
        /// Parses one layer.
        /// </summary>
        private byte[,] ParseLayer(BinaryReader reader, LineCompressionMethod[] compressionTable)
        {
            var bitmap = new byte[HEIGHT, WIDTH];

            for (var line = 0; line < HEIGHT; line++)
            {
                switch (compressionTable[line])
                {
                    case LineCompressionMethod.Skip:
                        continue;

                    case LineCompressionMethod.Sparse:
                        ParseSparseLine(reader, line, bitmap);
                        continue;

                    case LineCompressionMethod.SparseInverted:
                        // All of the pixels must be inverted first before we parse this line.
                        for (var pixel = 0; pixel < WIDTH; pixel++)
                            bitmap[line, pixel] = 1;

                        ParseSparseLine(reader, line, bitmap);
                        continue;

                    case LineCompressionMethod.Full:
                        ParseFullLine(reader, line, bitmap);
                        continue;
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Parses one sparse line of a layer.
        /// </summary>
        private void ParseSparseLine(BinaryReader reader, int line, byte[,] bitmap)
        {
            // The endianness of the flags is swapped because it makes reading line data
            // much much easier.
            var flags = reader.ReadUInt32().SwapEndianness();
            var pixel = 0;

            // Iterate for as long as we still have chunks to read.
            while ((flags & 0xFFFFFFFF) > 0)
            {
                // If the leftmost bit isn't 0, it means the next 8 pixels actually are coded.
                // Decode them the same way we decode a single line with full compression.
                if ((flags & 0x80000000) != 0)
                {
                    var chunk = reader.ReadByte();
                    for (var bit = 0; bit < 8; bit++)
                    {
                        bitmap[line, pixel] = (byte)((chunk >> bit) & 0x1);
                        pixel++;
                    }
                }

                // If the rightmost bit is 0, we need to skip this chunk, skip the next 8 pixels.
                else
                {
                    pixel += 8;
                }

                flags <<= 1;
            }
        }

        /// <summary>
        /// Parses one full line of a layer.
        /// </summary>
        private void ParseFullLine(BinaryReader reader, int line, byte[,] bitmap)
        {
            var pixel = 0;
            while (pixel < WIDTH)
            {
                var chunk = reader.ReadByte();
                for (var bit = 0; bit < 8; bit++)
                {
                    bitmap[line, pixel] = (byte)((chunk >> bit) & 0x1);
                    pixel++;
                }    
            }
        }

        /// <summary>
        /// Parse the entire frame.
        /// </summary>
        private void ParseFrame(BinaryReader reader)
        {
            var layer1CompressionTable = ParseLineCompressionTable(reader);
            var layer2CompressionTable = ParseLineCompressionTable(reader);

            Layer1Bitmap = ParseLayer(reader, layer1CompressionTable);
            Layer2Bitmap = ParseLayer(reader, layer2CompressionTable);
        }

        /// <summary>
        /// Merge this frame with a different frame, using the diffing algorithm.
        /// </summary>
        internal void MergeWithFrame(Frame other)
        {
            for (var line = 0; line < HEIGHT; line++)
            {
                if (line - FrameInfo.TranslateY < 0)
                    continue;

                if (line - FrameInfo.TranslateY >= HEIGHT)
                    continue;

                for (var pixel = 0; pixel < WIDTH; pixel++)
                {
                    if (pixel - FrameInfo.TranslateX < 0)
                        continue;

                    if (pixel - FrameInfo.TranslateX >= WIDTH)
                        continue;

                    Layer1Bitmap[line, pixel] ^= other.Layer1Bitmap[line - FrameInfo.TranslateY, pixel - FrameInfo.TranslateX];
                    Layer2Bitmap[line, pixel] ^= other.Layer2Bitmap[line - FrameInfo.TranslateY, pixel - FrameInfo.TranslateX];
                }
            }
        }
    }
}
