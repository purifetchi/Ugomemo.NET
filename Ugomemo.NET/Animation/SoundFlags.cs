namespace Ugomemo.NET.Animation
{
    /// <summary>
    /// The sound effect flags for a given animation frame.
    /// </summary>
    public sealed class SoundFlags
    {
        /// <summary>
        /// The flag for the first sound effect.
        /// </summary>
        public bool Sound1 { get; internal set; }

        /// <summary>
        /// The flag for the second sound effect.
        /// </summary>
        public bool Sound2 { get; internal set; }

        /// <summary>
        /// The flag for the third sound effect.
        /// </summary>
        public bool Sound3 { get; internal set; }
    }
}
