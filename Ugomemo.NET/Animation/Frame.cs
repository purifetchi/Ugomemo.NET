using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Ugomemo.NET.Animation
{
    /// <summary>
    /// A frame of a flipnote animation.
    /// </summary>
    public sealed partial class Frame
    {
        /// <summary>
        /// The width of a frame.
        /// </summary>
        public const int WIDTH = 256;

        /// <summary>
        /// The height of a frame.
        /// </summary>
        public const int HEIGHT = 192;

        /// <summary>
        /// This frame's information.
        /// </summary>
        public FrameInfo FrameInfo { get; private set; }

        /// <summary>
        /// The image representing this frame.
        /// </summary>
        public Image<Rgb24> Image { get; private set; }

        /// <summary>
        /// The sound flags for this frame.
        /// </summary>
        public SoundFlags SoundFlags { get; internal set; }

        /// <summary>
        /// The first layer's bitmap.
        /// </summary>
        public byte[,] Layer1Bitmap { get; private set; }

        /// <summary>
        /// The second layer's bitmap.
        /// </summary>
        public byte[,] Layer2Bitmap { get; private set; }
    }
}
