using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RSA
{
    public class RSAEncoder
    {
        private BigInteger p;
        private BigInteger q;

        private BigInteger n;

        private BigInteger e;
        private BigInteger d;

        public struct PublicKey
        {
            public BigInteger E;
            public BigInteger N;

            public PublicKey(BigInteger e, BigInteger n)
            {
                E = e;
                N = n;
            }
        }

        public PublicKey Key => new PublicKey(e, n);

        public RSAEncoder()
        {
            p = BIGenerator.GeneratePrimeBigInteger(1024);
            q = BIGenerator.GeneratePrimeBigInteger(1024);

            n = p * q;
            BigInteger phi = (p - 1) * (q - 1);

            do
            {
                e = BIGenerator.GeneratePrimeBigInteger(1024);
            } while (BigInteger.GreatestCommonDivisor(phi, e) != 1 
            || e >= phi);

            d = e.ReverseElement(phi);

            if (d < 0)
                d += phi;
        }

        public BigInteger Encrypt(string message, PublicKey key)
        {
            byte[] text = Encoding.ASCII.GetBytes(message);
            return Encrypt(new BigInteger(text), key);
        }

        public BigInteger Encrypt(BigInteger message, PublicKey key)
        {
            return BigInteger.ModPow(message, key.E, key.N);
        }

        public BigInteger Decrypt(BigInteger message, out string text)
        {
            BigInteger result = Decrypt(message);
            text = new string(Encoding.ASCII.GetChars(result.ToByteArray()));
            return result;
        }

        public BigInteger Decrypt(BigInteger message)
        {
            return BigInteger.ModPow(message, d, n);
        }
    }
}
