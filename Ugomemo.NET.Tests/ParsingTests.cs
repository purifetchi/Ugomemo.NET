using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Ugomemo.NET.Exceptions;

namespace Ugomemo.NET.Tests
{
    [TestClass]
    [DeploymentItem("TestFiles/pekira_beach.ppm")]
    public class ParsingTests
    {
        private Flipnote flipnote;

        [TestInitialize]
        public void InitializeParsingTests()
        {
            flipnote = new Flipnote("pekira_beach.ppm", false);
        }

        [TestMethod]
        [DeploymentItem("TestFiles/not_a_flipnote.ppm")]
        public void ParseValidFlipnote()
        {
            Assert.IsTrue(File.Exists("pekira_beach.ppm"));
            Assert.IsTrue(File.Exists("not_a_flipnote.ppm"));

            try
            {
                var badFlipnote = new Flipnote("not_a_flipnote.ppm");
                Assert.Fail("Parsed a bad flipnote!");
            }
            catch (NotAFlipnoteException)
            {

            }

            try
            {
                var goodFlipnote = new Flipnote("pekira_beach.ppm");
            }
            catch (NotAFlipnoteException)
            {
                Assert.Fail("Failed to parse good known flipnote!");
            }
        }

        [TestMethod]
        public void EnsureCorrectFrameCount()
        {
            Assert.IsTrue(flipnote.FrameCount == 186);
        }

        [TestMethod]
        public void EnsureCorrectLocked()
        {
            Assert.IsTrue(flipnote.Locked);
        }

        [TestMethod]
        public void EnsureCorrectCreatedOn()
        {
            Assert.IsTrue(flipnote.CreatedOn.Year == 2011);
            Assert.IsTrue(flipnote.CreatedOn.Month == 7);
            Assert.IsTrue(flipnote.CreatedOn.Day == 30);
        }

        [TestMethod]
        [DeploymentItem("TestFiles/sample_thumbnail.png")]
        public void EnsureThumbnailPixelsMatch()
        {
            Assert.IsTrue(File.Exists("sample_thumbnail.png"));
            using var image = Image.Load<Rgb24>("sample_thumbnail.png");

            for (var x = 0; x < Thumbnail.WIDTH; x++)
            {
                for (var y = 0; x < Thumbnail.HEIGHT; x++)
                {
                    Assert.IsTrue(flipnote.Thumbnail.Image[x, y] == image[x, y]);
                }
            }
        }

        [TestMethod]
        public void EnsureFlipnoteAnimationInfoIsCorrect()
        {
            Assert.IsTrue(flipnote.AnimationInfo.Looping);
            Assert.IsFalse(flipnote.AnimationInfo.HideLayer1);
            Assert.IsFalse(flipnote.AnimationInfo.HideLayer2);
        }

        [TestMethod]
        public void EnsureFrameInfoIsCorrect()
        {
            Assert.IsTrue(flipnote.Frames[0].FrameInfo.Type == Animation.FrameType.Keyframe);
            Assert.IsTrue(flipnote.Frames[0].FrameInfo.PaperColor == Animation.PaperColor.White);
            Assert.IsTrue(flipnote.Frames[0].FrameInfo.Layer1Color == Animation.PenColor.InverseOfPaper);
            Assert.IsTrue(flipnote.Frames[0].FrameInfo.Layer2Color == Animation.PenColor.Blue);

            Assert.IsTrue(flipnote.Frames[1].FrameInfo.Type == Animation.FrameType.Interframe);
            Assert.IsTrue(flipnote.Frames[1].FrameInfo.PaperColor == Animation.PaperColor.White);
            Assert.IsTrue(flipnote.Frames[1].FrameInfo.Layer1Color == Animation.PenColor.Red);
            Assert.IsTrue(flipnote.Frames[1].FrameInfo.Layer2Color == Animation.PenColor.Blue);
        }

        [TestMethod]
        [DeploymentItem("TestFiles/sample_frame.png")]
        public void EnsureFramesAreCorrect()
        {
            Assert.IsTrue(File.Exists("sample_frame.png"));

            var flipnote = new Flipnote("pekira_beach.ppm");
            using var image = Image.Load<Rgb24>("sample_frame.png");

            for (var x = 0; x < Animation.Frame.WIDTH; x++)
            {
                for (var y = 0; x < Animation.Frame.HEIGHT; x++)
                {
                    Assert.IsTrue(flipnote.Frames[85].Image[x, y] == image[x, y]);
                }
            }
        }
    }
}
