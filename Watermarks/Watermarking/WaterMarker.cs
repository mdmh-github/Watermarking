using System.Drawing;

namespace Watermarking
{
    public class WaterMarker : Watermark
    {
        public WaterMarker(string inputImgPath)
        {
            Image = new Bitmap(inputImgPath);
        }

        public void InsertMessage(string message, string outputImgPath)
        {
            int counter = 0;

            var bits = new BitTextParser().TextToBits(message + delimiter);
            var watermarkedColor = new WatermarkedColor();
            var widthRange = range(Image.Width);
            var heigthRange = range(Image.Height);

            foreach (int w in widthRange)
            {
                foreach (int h in heigthRange)
                {
                    if (w % 2 == 0 && h % 2 == 0 && counter < bits.Length)
                    {
                        var oldPixel = Image.GetPixel(w, h);
                        var newPixel = watermarkedColor.getWaterMarked(
                               bits[counter++], oldPixel);

                        Image.SetPixel(w, h, newPixel);
                    }
                }
            }

            Image.Save(outputImgPath);
        }
    }
}