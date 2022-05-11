using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp.PixelFormats;
using Ugomemo.NET.Exceptions;

namespace Ugomemo.NET.Tests
{
    [TestClass]
    public class ParsingTests
    {
        [TestMethod]
        [DeploymentItem("TestFiles/not_a_flipnote.ppm")]
        [DeploymentItem("TestFiles/pekira_beach.ppm")]
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
                var badFlipnote = new Flipnote("pekira_beach.ppm");
            }
            catch (NotAFlipnoteException)
            {
                Assert.Fail("Failed to parse good known flipnote!");
            }
        }

        [TestMethod]
        [DeploymentItem("TestFiles/pekira_beach.ppm")]
        public void EnsureCorrectFrameCount()
        {
            var flipnote = new Flipnote("pekira_beach.ppm");
            Assert.IsTrue(flipnote.FrameCount == 186);
        }

        [TestMethod]
        [DeploymentItem("TestFiles/pekira_beach.ppm")]
        public void EnsureCorrectLocked()
        {
            var flipnote = new Flipnote("pekira_beach.ppm");
            Assert.IsTrue(flipnote.Locked);
        }

        [TestMethod]
        [DeploymentItem("TestFiles/pekira_beach.ppm")]
        public void EnsureCorrectCreatedOn()
        {
            var flipnote = new Flipnote("pekira_beach.ppm");
            Assert.IsTrue(flipnote.CreatedOn.Year == 2011);
            Assert.IsTrue(flipnote.CreatedOn.Month == 7);
            Assert.IsTrue(flipnote.CreatedOn.Day == 30);
        }

        [TestMethod]
        [DeploymentItem("TestFiles/pekira_beach.ppm")]
        public void EnsureThumbnailPixelsMatch()
        {
            var flipnote = new Flipnote("pekira_beach.ppm");
            Assert.IsTrue(flipnote.Thumbnail.Image[0, 0] == new Rgb24(173, 171, 255));
            Assert.IsTrue(flipnote.Thumbnail.Image[1, 0] == new Rgb24(173, 171, 255));
            Assert.IsTrue(flipnote.Thumbnail.Image[2, 0] == new Rgb24(173, 171, 255));
        }
    }
}
