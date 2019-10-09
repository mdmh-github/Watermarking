using System;
using System.Drawing;

namespace Watermarking
{
    public class WatermarkExtractor : Watermark
    {
        public WatermarkExtractor(string inputImagePath)
        {
            Image = new Bitmap(inputImagePath);
        }

        public string ExtractMessage()
        {
            var bitsOutput = "";
            var counter = 0;
            var bitTextParser = new BitTextParser();
            var bitsDelimiter = bitTextParser.TextToBits(delimiter);
            var widthRange = range(Image.Width);
            var heigthRange = range(Image.Height);

            try
            {
                foreach (int w in widthRange)
                {
                    foreach (int h in heigthRange)
                    {
                        if (w % 2 == 0 && h % 2 == 0)
                        {
                            bitsOutput += (Image.GetPixel(w, h).R & 1).ToString();
                            if (++counter % 8 == 0 && bitsOutput.EndsWith(bitsDelimiter))
                                throw new Exception("delimter reached");
                        }
                    }
                }
            }
            catch
            {
            }

            return bitTextParser.BitsToText(bitsOutput.Remove(bitsOutput.Length - 8));
        }
    }
}