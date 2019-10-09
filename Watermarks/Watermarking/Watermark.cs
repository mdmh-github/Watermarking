using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Watermarking
{
    public abstract class Watermark
    {
        public string delimiter => "*";

        protected Bitmap Image;

        protected static IEnumerable<int> range(int leng)
          => Enumerable.Range(0, leng);
    }
}