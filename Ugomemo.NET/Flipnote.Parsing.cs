using System;
using System.IO;
using Ugomemo.NET.Animation;
using Ugomemo.NET.Exceptions;
using Ugomemo.NET.IO;

namespace Ugomemo.NET
{
    public sealed partial class Flipnote
    {
        private static readonly char[] MAGIC_STRING = { 'P', 'A', 'R', 'A' };
        private const uint FORMAT_VERSION = 0x24;

        /// <summary>
        /// Constant epoch, always at the 1st of January 2000.
        /// </summary>
        private static readonly DateTimeOffset EPOCH = DateTimeOffset.FromUnixTimeSeconds(946684800);

        /// <summary>
        /// Whether the flipnote parser should actually generate images for every frame.
        /// </summary>
        private bool ShouldGenerateImagesForFrames { get; set; }

        private void Parse(string filename, bool generateFrameImages)
        {
            ShouldGenerateImagesForFrames = generateFrameImages;

            using var fileStream = new FileStream(filename, FileMode.Open);
            using var bitReader = new BinaryBitReaderEx(fileStream, System.Text.Encoding.ASCII);

            ParseHeader(filename, bitReader);
            ParseMetadata(bitReader);

            Thumbnail = new Thumbnail(bitReader);

            ParseAnimationHeader(bitReader);
        }

        /// <summary>
        /// Parses the header of the flipnote.
        /// </summary>
        private void ParseHeader(string filename, BinaryBitReaderEx reader)
        {
            if (reader.ReadByte() != MAGIC_STRING[0] ||
                reader.ReadByte() != MAGIC_STRING[1] ||
                reader.ReadByte() != MAGIC_STRING[2] ||
                reader.ReadByte() != MAGIC_STRING[3])
            {
                throw new NotAFlipnoteException($"{filename} is not a flipnote - invalid magic!");
            }

            reader.ReadULong();
            FrameCount = reader.ReadUInt(16);

            var formatVersion = reader.ReadUInt(16);
            if (formatVersion != FORMAT_VERSION)
                throw new NotAFlipnoteException($"{filename} is not a flipnote - invalid format version!");
        }

        /// <summary>
        /// Parses the metadata of the flipnote.
        /// </summary>
        private void ParseMetadata(BinaryBitReaderEx reader)
        {
            Locked = reader.ReadUInt(16) == 1;

            // TODO: Parse all of the skipped data.
            //       - Author name
            //       - Author ID
            //       - Filename
            reader.ReadBytes(136);

            var timestamp = reader.ReadUInt();
            CreatedOn = EPOCH.AddSeconds(timestamp);

            // NOTE: The last 2 bytes of the metadata are always null and ignored.
            reader.ReadUInt(16);
        }

        /// <summary>
        /// Parses the animation header of the flipnote.
        /// </summary>
        private void ParseAnimationHeader(BinaryBitReaderEx reader)
        {
            var frameOffsetTableSize = reader.ReadUInt(16);

            // NOTE: The docs actually specify that this unknown value is supposed to be only
            //       16 bits, but from my own experimentation the value is actually 32 bits long.
            reader.ReadUInt(32);

            var flags = reader.ReadUInt(16);
            AnimationInfo = new AnimationInfo
            {
                Looping = (flags & 0x2) != 0,
                HideLayer1 = (flags & 0x10) != 0,
                HideLayer2 = (flags & 0x20) != 0
            };

            ParseAnimationFrameOffsetTable(reader, frameOffsetTableSize);
        }

        /// <summary>
        /// Parses the animation frame offset table of the flipnote.
        /// </summary>
        private void ParseAnimationFrameOffsetTable(BinaryBitReaderEx reader, uint size)
        {
            // Every frame offset is a 4 byte uint32.
            var frameOffsetCount = size / 4;
            var frameOffsetTable = new uint[frameOffsetCount];

            // Calculate all of the frame positions with the offset already applied, to make seeking easier.
            const int DEFAULT_FRAME_TABLE_POSITION = 0x06A8;
            for (var i = 0; i < frameOffsetCount; i++)
                frameOffsetTable[i] = DEFAULT_FRAME_TABLE_POSITION + size + reader.ReadUInt();

            ParseFrames(reader, frameOffsetTable);
        }

        /// <summary>
        /// Parses all of the frames this flipnote has.
        /// </summary>
        private void ParseFrames(BinaryBitReaderEx reader, uint[] offsets)
        {
            Frames = new Frame[offsets.Length];

            for (var i = 0; i < FrameCount + 1; i++)
            {
                reader.Seek(offsets[i]);

                var frame = new Frame(reader);
                if (frame.FrameInfo.Type == FrameType.Interframe)
                    frame.MergeWithFrame(Frames[i - 1]);

                if (ShouldGenerateImagesForFrames)
                    frame.GenerateImage(AnimationInfo);

                Frames[i] = frame;
            }
        }
    }
}
