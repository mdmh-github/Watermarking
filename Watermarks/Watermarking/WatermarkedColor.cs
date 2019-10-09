using System.Drawing;

namespace Watermarking
{
    public class WatermarkedColor
    {
        private byte flipper0 => 254;
        private byte flipper1 => 1;

        public Color getWaterMarked(char bitToInsert, Color c)
        => Color.FromArgb(
            insertBit(bitToInsert, c.R),
            c.G,
            c.B);

        private byte insertBit(char bit, byte originalByte)
        =>
            (byte)(bit == '0' ?
            originalByte & flipper0 :
            originalByte | flipper1);
    }
}