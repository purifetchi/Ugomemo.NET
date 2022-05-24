using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Ugomemo.NET
{
    /// <summary>
    /// The tiny flipnote thumbnail.
    /// </summary>
    public sealed partial class Thumbnail
    {
        public const int WIDTH = 64;
        public const int HEIGHT = 48;

        /// <summary>
        /// The image containing the small flipnote thumbnail.
        /// </summary>
        public Image<Rgb24> Image { get; private set; }

        internal Thumbnail(BinaryReader reader)
        {
            ParseThumbnail(reader);
        }

        private void ParseThumbnail(BinaryReader reader)
        {
            Image = new Image<Rgb24>(WIDTH, HEIGHT);

            for (var y = 0; y < HEIGHT; y += 8)
            {
                for (var x = 0; x < WIDTH; x += 8)
                {
                    for (var line = 0; line < 8; line += 1)
                    {
                        for (var pixel = 0; pixel < 8; pixel += 2)
                        {
                            var actualX = x + pixel;
                            var actualY = y + line;

                            // NOTE: One byte encodes two 4 bit palette indices, where the first one is stored in the
                            //       lowest significant slot, and the second in the most significant.
                            var pixelData = reader.ReadByte();
                            Image[actualX, actualY] = PALETTE[pixelData & 0x0F];
                            Image[actualX + 1, actualY] = PALETTE[(pixelData >> 4) & 0x0F];
                        }
                    }
                }
            }
        }
    }
}
