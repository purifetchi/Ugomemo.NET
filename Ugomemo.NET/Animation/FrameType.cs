namespace Ugomemo.NET.Animation
{
    /// <summary>
    /// The type of a frame. Either a 'Keyframe' which doesn't get affected by the previous frame,
    /// or an 'Interframe' which gets its values by being diffed with the previous frame.
    /// </summary>
    public enum FrameType
    {
        Interframe,
        Keyframe
    }
}
