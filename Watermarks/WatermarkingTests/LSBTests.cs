using Microsoft.VisualStudio.TestTools.UnitTesting;
using Watermarking;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
namespace UnitTestProject1
{
    [TestClass]
    public class LSBTests
    {
        string rootPath => @"c:\users\diego munoz\documents\visual studio 2015\Projects\Watermarks\WatermarkingTests\";
        string inputPath => $@"{rootPath}input_images\";
        string outputPath => $@"{rootPath}output_images\";
        string tempPath => $@"{rootPath}temp_images\";
        string inputImagePath => $"{inputPath}TestImage.tif";
        string outputImagePath => $"{outputPath}output_TestImage.tif";

        [TestMethod]
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
