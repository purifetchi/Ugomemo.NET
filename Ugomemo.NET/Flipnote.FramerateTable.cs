namespace Ugomemo.NET
{
    public sealed partial class Flipnote
    {
        /// <summary>
        /// The framerate table mapping the 1-8 values to actual seconds.
        /// </summary>
        public static readonly float[] FRAMERATE_TABLE = 
        {
            0f,

            // 1 frame / 2 seconds
            2f,

            // 1 frame / 1 second
            1f,

            // 2 frames / 1 second
            1/2f,

            // 4 frames / 1 second
            1/4f,

            // 6 frames / 1 second
            1/6f,

            // 12 frames / 1 second
            1/12,

            // 20 frames / 1 second
            1/20f,

            // 30 frames / 1 second
            1/30f,
        };
    }
}
