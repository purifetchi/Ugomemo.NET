using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Ugomemo.NET.Animation
{
    public sealed partial class Frame
    {
        /// <summary>
        /// The generic palette used in flipnotes.
        /// </summary>
        private static readonly Color[] PALETTE =
        {
            // WHITE
            new Color(new Rgb24(255, 255, 255)),

            // BLACK
            new Color(new Rgb24(14, 14, 14)),

            // RED
            new Color(new Rgb24(255, 42, 42)),

            // BLUE
            new Color(new Rgb24(10, 57, 255)),
        };

        /// <summary>
        /// Get the color for the given pen color.
        /// </summary>
        /// <param name="penColor">The pen color.</param>
        /// <returns>The color tied to that pen color.</returns>
        public Color GetPenColor(PenColor penColor)
        {
            switch (penColor)
            {
                case PenColor.InverseOfPaperUnused:
                case PenColor.InverseOfPaper:
                    if (FrameInfo.PaperColor == PaperColor.White)
                        return PALETTE[1];
                    return PALETTE[0];

                case PenColor.Red:
                    return PALETTE[2];

                case PenColor.Blue:
                    return PALETTE[3];
            }

            return PALETTE[0];
        }

        /// <summary>
        /// Get the color for the given paper color.
        /// </summary>
        /// <param name="paperColor">The paper color.</param>
        /// <returns>The color tied to that paper color.</returns>
        public Color GetPaperColor(PaperColor paperColor)
        {
            switch (paperColor)
            {
                case PaperColor.Black:
                    return PALETTE[1];

                case PaperColor.White:
                    return PALETTE[0];
            }

            return PALETTE[0];
        }
    }
}
