namespace Ugomemo.NET
{
    /// <summary>
    /// Animation information for a flipnote.
    /// </summary>
    public sealed class AnimationInfo
    {
        /// <summary>
        /// Whether this animation is looping.
        /// </summary>
        public bool Looping { get; internal set; }

        /// <summary>
        /// Should the first layer be hidden?
        /// </summary>
        public bool HideLayer1 { get; internal set; }

        /// <summary>
        /// Should the second layer be hidden?
        /// </summary>
        public bool HideLayer2 { get; internal set; }
    }
}
