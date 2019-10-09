using DiscreteCosineTransform;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace UnitTestProject1
{
    [TestFixture]
    public class DCTTests
    {
        private string rootPath => @"c:\users\diego munoz\documents\visual studio 2015\Projects\Watermarks\WatermarkingTests\";
        private string inputPath => $@"{rootPath}input_images\";
        private string outputPath => $@"{rootPath}output_images\";
        private string tempPath => $@"{rootPath}temp_images\";
        private string inputImagePath => $"{inputPath}smallImage.jpg";
        private string outputImagePath => $"{outputPath}output_smallImage.jpg";

        [Test]
        public void DCT_Back_Fort_Test()
        {
            var bmap = new Bitmap(inputImagePath);

            double[] pixelArray = getPixelArray(bmap);

            IDCT dct = DCT.CreateParallelDCT();

            var output = setPixelMatrix(bmap, dct.Backward(dct.Forward(pixelArray)));

            output.Save(outputImagePath);
        }

        [Test]
        public void DCT_Insertion_Test()
        {
            var bmap = new Bitmap(inputImagePath);

            double[] pixelArray = getPixelArray(bmap);

            IDCT dct = DCT.CreateParallelDCT();

            double[] forward = dct.Forward(pixelArray);

            double[] signal = Enumerable.Repeat(0d, 5000).ToArray();

            signal[3000] = 750;
            var insert_point = 30000;
            var fSignal = dct.Forward(signal);

            for (int i = insert_point; i < insert_point + fSignal.Length; i++)
                forward[i] += fSignal[i - insert_point];

            var output = setPixelMatrix(bmap, dct.Backward(forward));

            output.Save(outputImagePath);
        }

        [Test]
        public void DCT_detection_Test()
        {
            var bmap = new Bitmap(inputImagePath);

            double[] pixelArray = getPixelArray(bmap);

            IDCT dct = DCT.CreateParallelDCT();

            double[] forward = dct.Forward(pixelArray);
            const int signalSize = 5000;
            double[] signal = Enumerable.Repeat(0d, signalSize).ToArray();

            signal[3000] = 750;
            var insert_point = 30000;
            var fSignal = dct.Forward(signal);

            for (int i = insert_point; i < insert_point + fSignal.Length; i++)
                forward[i] += fSignal[i - insert_point];

            var output = setPixelMatrix(bmap, dct.Backward(forward));

            output.Save(outputImagePath);

            var bmapMarked = new Bitmap(outputImagePath);

            var forwardedMarked = dct.Forward(getPixelArray(bmapMarked));

            double[] extractedSignal = Enumerable.Repeat(0d, signalSize).ToArray();

            for (int i = insert_point; i < insert_point + fSignal.Length; i++)
            {
                extractedSignal[i - insert_point] = Math.Abs(forward[i] - forwardedMarked[i]);
            }

            var bacwardextractedSignal = dct.Backward(extractedSignal);

            var mean = bacwardextractedSignal.Sum() / bacwardextractedSignal.Length;

            Console.WriteLine(mean);
        }

        private double[] getPixelArray(Bitmap b) => getPixelArray2(b).ToArray();

        private static IEnumerable<double> getPixelArray2(Bitmap img)
        {
            for (int h = 0; h < img.Height; h++)
            {
                for (int w = 0; w < img.Width; w++)
                    yield return img.GetPixel(w, h).R;
            }
        }

        private static Bitmap setPixelMatrix(Bitmap image, double[] pixelMatrix)
        {
            var newImage = new Bitmap(image);

            for (int h = 0, counter = 0; h < image.Height; h++)
            {
                for (int w = 0; w < image.Width; w++)
                {
                    Color previousPixel = image.GetPixel(w, h);

                    Color newPixel =
                        Color.FromArgb(
                        (byte)pixelMatrix[counter++],
                        previousPixel.G,
                        previousPixel.B);

                    newImage.SetPixel(w, h, newPixel);
                }
            }

            return newImage;
        }

        private static string ToString(double[] a) => a.Select(t => t.ToString("0.00")).Aggregate((x, y) => $"{x}\t{y}");
    }
}
