using System;

namespace Ugomemo.NET
{
    /// <summary>
    /// A Flipnote Studio flipnote.
    /// </summary>
    public sealed partial class Flipnote
    {
        /// <summary>
        /// The amount of animation frames this flipnote has.
        /// </summary>
        public uint FrameCount { get; private set; }

        /// <summary>
        /// Whether this flipnote has been locked.
        /// </summary>
        public bool Locked { get; private set; }

        /// <summary>
        /// The date & time this flipnote has been created.
        /// </summary>
        public DateTimeOffset CreatedOn { get; private set; }

        /// <summary>
        /// The tiny (64x48) thumbnail associated with this flipnote.
        /// </summary>
        public Thumbnail Thumbnail { get; private set; }

        /// <summary>
        /// The meta information about the animation.
        /// </summary>
        public AnimationInfo AnimationInfo { get; private set; }

        private uint AnimationDataSize { get; set; }
        private uint SoundDataSize { get; set; }

        public Flipnote(string filename)
        {
            Parse(filename);
        }
    }
}
