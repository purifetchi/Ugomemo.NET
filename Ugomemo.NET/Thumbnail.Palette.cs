using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Ugomemo.NET
{
    public sealed partial class Thumbnail
    {
        private static readonly Color[] PALETTE =
        {
            // WHITE (unused)
            new Color(new Rgb24(255, 255, 255)),

            // DARK GREY
            new Color(new Rgb24(82, 82, 82)),

            // WHITE 
            new Color(new Rgb24(255, 255, 255)),

            // LIGHT GREY
            new Color(new Rgb24(156, 156, 156)),

            // PURE RED
            new Color(new Rgb24(255, 72, 68)),

            // DARK RED
            new Color(new Rgb24(200, 81, 79)),

            // LIGHT RED / PINK
            new Color(new Rgb24(255, 173, 172)),

            // PURE GREEN
            new Color(new Rgb24(0, 255, 0)),

            // PURE BLUE
            new Color(new Rgb24(72, 64, 255)),

            // DARK BLUE
            new Color(new Rgb24(81, 79, 184)),

            // LIGHT BLUE
            new Color(new Rgb24(173, 171, 255)),

            // PURE GREEN
            new Color(new Rgb24(0, 255, 0)),

            // MAGENTA / PURPLE
            new Color(new Rgb24(182, 87, 183)),

            // PURE GREEN
            new Color(new Rgb24(0, 255, 0)),

            // PURE GREEN
            new Color(new Rgb24(0, 255, 0)),

            // PURE GREEN
            new Color(new Rgb24(0, 255, 0))
        };
    }
}
