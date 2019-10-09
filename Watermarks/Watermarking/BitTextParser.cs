using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Watermarking
{
        public class BitTextParser
        {
            public string BitsToText(string bits)
            => Encoding.ASCII.GetString(GetBytesFromBinaryString(bits).ToArray());

            private IEnumerable<byte> GetBytesFromBinaryString(string binary)
            {
                for (int i = 0; i < binary.Length; i += 8)
                    yield return Convert.ToByte(binary.Substring(i, 8), 2);
            }

            public string TextToBits(string text)
            => text
            .Select(x => Convert.ToString(x, 2).PadLeft(8, '0'))
            .Aggregate((x, y) => x + y);
        }
}