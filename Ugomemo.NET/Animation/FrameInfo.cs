namespace Ugomemo.NET.Animation
{
    public sealed class FrameInfo
    {
        /// <summary>
        /// The type of this frame.
        /// </summary>
        public FrameType Type { get; internal set; }

        /// <summary>
        /// Whether this frame is translated in regards to the previous frame or not.
        /// </summary>
        public bool Translated { get; internal set; }

        /// <summary>
        /// The color of the first layer's pen.
        /// </summary>
        public PenColor Layer1Color { get; internal set; }

        /// <summary>
        /// The color of the second layer's pen.
        /// </summary>
        public PenColor Layer2Color { get; internal set; }

        /// <summary>
        /// The color of the paper.
        /// </summary>
        public PaperColor PaperColor { get; internal set; }

        /// <summary>
        /// The X value this frame has been translated by.
        /// </summary>
        public int TranslateX { get; internal set; }

        /// <summary>
        /// The Y value this frame has been translated by.
        /// </summary>
        public int TranslateY { get; internal set; }
    }
}
