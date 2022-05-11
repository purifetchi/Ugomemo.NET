namespace Ugomemo.NET
{
    public sealed partial class Flipnote
    {
        /// <summary>
        /// The amount of animation frames this flipnote has.
        /// </summary>
        public uint FrameCount { get; set; }

        private uint AnimationDataSize { get; set; }
        private uint SoundDataSize { get; set; }

        public Flipnote(string filename)
        {
            Parse(filename);
        }
    }
}
