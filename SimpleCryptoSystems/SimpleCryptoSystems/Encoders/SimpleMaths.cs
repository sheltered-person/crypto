using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCryptoSystems
{
    public class SimpleMaths
    {
        public static int GCD(int a, int b)
        {
            int x, y;
            return GCDExtended(a, b, out x, out y);
        }

        public static int GCDExtended(int a, int b, out int x, out int y)
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }

            int x1, y1;

            int d1 = GCDExtended(b, a % b, out x1, out y1);

            x = y1;
            y = x1 - (a / b) * y1;

            return d1;
        }


        public static int ReverseElement(int a, int M)
        {
            int x, y, d;

            d = GCDExtended(a, M, out x, out y);

            if (d != 1)
                return 1;

            return x;
        }
    }
}
