using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RSA
{
    public static class Extentions
    {
        public static BigInteger ReverseElement(this BigInteger a, BigInteger M)
        {
            BigInteger x, y, d;

            d = GCDExtended(a, M, out x, out y);

            if (d != 1)
                return 1;

            return x;
        }

        public static BigInteger GCDExtended(BigInteger a, BigInteger b,
            out BigInteger x, out BigInteger y)
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }

            BigInteger x1, y1;

            BigInteger d1 = GCDExtended(b, a % b, out x1, out y1);

            x = y1;
            y = x1 - (a / b) * y1;

            return d1;
        }
    }
}
