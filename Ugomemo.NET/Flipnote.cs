using System;
using Ugomemo.NET.Animation;

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

        /// <summary>
        /// The animation frames of this flipnote.
        /// </summary>
        public Frame[] Frames { get; private set; }

        /// <summary>
        /// Load a flipnote and generate images for all frames.
        /// </summary>
        /// <param name="filename">The filename of the flipnote.</param>
        public Flipnote(string filename)
        {
            Parse(filename, true);
        }

        /// <summary>
        /// Load a flipnote.
        /// </summary>
        /// <param name="filename">The filename of the flipnote.</param>
        /// <param name="generateFrameImages">Whether Ugomemo.NET should generate images for all frames.</param>
        public Flipnote(string filename, bool generateFrameImages)
        {
            Parse(filename, generateFrameImages);
        }
    }
}
