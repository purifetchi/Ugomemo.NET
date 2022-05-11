using BinaryBitLib;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Ugomemo.NET
{
    public sealed partial class Thumbnail
    {
        public const int WIDTH = 64;
        public const int HEIGHT = 48;

        public Image<Rgb24> Image { get; private set; }

        internal Thumbnail(BinaryBitReader reader)
        {
            ParseThumbnail(reader);
        }

        private void ParseThumbnail(BinaryBitReader reader)
        {
            Image = new Image<Rgb24>(WIDTH, HEIGHT);

            for (var y = 0; y < HEIGHT; y += 8)
                for (var x = 0; x < WIDTH; x += 8)
                    for (var line = 0; line < 8; line += 1)
                        for (var pixel = 0; pixel < 8; pixel += 1)
                        {
                            var actualX = x + pixel;
                            var actualY = y + line;

                            Image[actualX, actualY] = PALETTE[reader.ReadUInt(4)];
                        }
        }
    }
}
