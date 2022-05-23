using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Ugomemo.NET.Animation
{
    public sealed partial class Frame
    {
        /// <summary>
        /// Generate image from this frame's data.
        /// </summary>
        internal void GenerateImage(AnimationInfo animationInfo)
        {
            Image = new Image<Rgb24>(WIDTH, HEIGHT);

            DrawPaper();

            if (!animationInfo.HideLayer2)
                DrawLayer(Layer2Bitmap, FrameInfo.Layer2Color);

            if (!animationInfo.HideLayer1)
                DrawLayer(Layer1Bitmap, FrameInfo.Layer1Color);
        }

        /// <summary>
        /// Draw the paper of this frame.
        /// </summary>
        private void DrawPaper()
        {
            var paperColor = GetPaperColor(FrameInfo.PaperColor);

            for (var y = 0; y < HEIGHT; y++)
            {
                for (var x = 0; x < WIDTH; x++)
                {
                    Image[x, y] = paperColor;
                }
            }
        }

        /// <summary>
        /// Draws one layer into the final image.
        /// </summary>
        private void DrawLayer(byte[,] bitmap, PenColor penColor)
        {
            var color = GetPenColor(penColor);

            for (var y = 0; y < HEIGHT; y++)
            {
                for (var x = 0; x < WIDTH; x++)
                {
                    if (bitmap[y, x] > 0)
                        Image[x, y] = color;
                }
            }
        }
    }
}
