using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            flipnote = new Flipnote("pekira_beach.ppm");
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
        public void EnsureThumbnailPixelsMatch()
        {
            Assert.IsTrue(flipnote.Thumbnail.Image[0, 0] == new Rgb24(173, 171, 255));
            Assert.IsTrue(flipnote.Thumbnail.Image[1, 0] == new Rgb24(173, 171, 255));
            Assert.IsTrue(flipnote.Thumbnail.Image[2, 0] == new Rgb24(173, 171, 255));
        }

        [TestMethod]
        public void EnsureFlipnoteAnimationInfoIsCorrect()
        {
            Assert.IsTrue(flipnote.AnimationInfo.Looping);
            Assert.IsFalse(flipnote.AnimationInfo.HideLayer1);
            Assert.IsFalse(flipnote.AnimationInfo.HideLayer2);
        }
    }
}
