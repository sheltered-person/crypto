using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Elgamal
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

        public static BigInteger Generator(this BigInteger p, List<BigInteger> fact, BigInteger R)
        {
            BigInteger n = R;

            for (int i = 2; i * i <= n; i++) 
            {
                if (n % i == 0) 
                {
                    fact.Add(i);
                    while (n % i == 0)
                        n /= i;
                }
            }

            if (n > 1)
                fact.Add(n);

            for (BigInteger res = 2; res <= p; res++) 
            {
                bool ok = true;

                for (int i = 0; i < fact.Count && ok; i++)
                    ok &= BigInteger.ModPow(res, (p - 1) / fact[i], p) != 1;

                if (ok)
                    return res;
            }

            return -1;
        }
    }
}
