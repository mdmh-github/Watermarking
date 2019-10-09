
using NUnit.Framework;
using static NUnit.Framework.Assert;
using Watermarking;
namespace UnitTestProject1
{
    [TestFixture]
    public class LSBTests
    {
        private string rootPath => @"c:\users\diego munoz\documents\visual studio 2015\Projects\Watermarks\WatermarkingTests\";

        private string inputPath => $@"{rootPath}input_images\";
        private string outputPath => $@"{rootPath}output_images\";
        private string tempPath => $@"{rootPath}temp_images\";
        private string inputImagePath => $"{inputPath}TestImage.tif";
        private string outputImagePath => $"{outputPath}output_TestImage.tif";

        [Test]
        public void LSB_Insert_Test()
        {
            var originalMsg = "Eduard Snowden, CIA Operative";

            new WaterMarker(inputImagePath)
                .InsertMessage(originalMsg, outputImagePath);

            var extractedMessage =
                new WatermarkExtractor(outputImagePath)
                .ExtractMessage();

            AreEqual(originalMsg, extractedMessage);
        }
    }
}
