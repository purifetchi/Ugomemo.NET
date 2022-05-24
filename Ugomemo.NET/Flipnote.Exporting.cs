using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;

namespace Ugomemo.NET
{
    public sealed partial class Flipnote
    {
        /// <summary>
        /// Export this Flipnote to a GIF file.
        /// </summary>
        /// <param name="filename">The path to save the exported file.</param>
        public void ExportToGIF(string filename)
        {
            var gif = new Image<Rgb24>(Animation.Frame.WIDTH, Animation.Frame.HEIGHT);
            foreach (var frame in Frames)
                gif.Frames.AddFrame(frame.Image.Frames[0]);

            foreach (var frame in gif.Frames)
                frame.Metadata.GetGifMetadata().FrameDelay = (int)(AnimationInfo.PlaybackSpeed * 100);

            gif.Metadata.GetGifMetadata().RepeatCount = (ushort)(AnimationInfo.Looping ? 0 : 1);
            gif.Metadata.GetGifMetadata().ColorTableMode = GifColorTableMode.Local;

            gif.SaveAsGif(filename);
        }
    }
}
