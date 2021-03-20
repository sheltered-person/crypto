using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;

namespace Elgamal
{
    public class EGSAEncoder
    {
        private BigInteger q;
        private BigInteger p;
        private BigInteger g;

        private BigInteger x;
        private BigInteger y;

        public struct PublicKey
        {
            public BigInteger p;
            public BigInteger q;
            public BigInteger g;
            public BigInteger y;

            public PublicKey(BigInteger p, BigInteger q, BigInteger g, BigInteger y)
            {
                this.p = p;
                this.q = q;
                this.g = g;
                this.y = y;
            }
        }

        public struct Signature
        {
            public BigInteger r;
            public BigInteger s;

            public Signature(BigInteger r, BigInteger s)
            {
                this.s = s;
                this.r = r;
            }
        }

        public PublicKey Key { get; private set; }

        public EGSAEncoder()
        {
            BigInteger k;
            p = BIGenerator.GeneratePrimeBigInteger(1024, out q, out k);

            do
            {
                BigInteger h = BIGenerator.GenerateBigInteger(p - 3) + 1;
                g = BigInteger.ModPow(h, k, p);
            } while (g == 1);
            //g = p.Generator(new List<BigInteger>() { 2, q }, k / 2);

            x = BIGenerator.GenerateBigInteger(p - 2) + 1;
            y = BigInteger.ModPow(g, x, p);

            Key = new PublicKey(p, q, g, y);
        }

        public EGSAEncoder(BigInteger p, BigInteger q, BigInteger g, BigInteger x)
        {
            this.p = p;
            this.q = q;
            this.g = g;
            this.x = x;

            y = BigInteger.ModPow(g, x, p);
            Key = new PublicKey(p, q, g, y);
        }

        public EGSAEncoder(PublicKey key)
        {
            Key = key;
            p = key.p;
            q = key.q;
            g = key.g;
            y = key.y;
        }

        private BigInteger Hash(BigInteger M, BigInteger p)
        {
            using (SHA256 h = SHA256.Create())
            {
                BigInteger m = new BigInteger(h.ComputeHash(M.ToByteArray()));
                m %= p - 1;

                if (m < 0)
                    m += p - 1;

                return m;
            }
        }

        public Signature SignatureMessage(string M)
        {
            byte[] text = Encoding.ASCII.GetBytes(M);
            return SignatureMessage(new BigInteger(text));
        }

        public Signature SignatureMessage(BigInteger M)
        {
            BigInteger k;

            do
            {
                k = BIGenerator.GenerateBigInteger(q - 2) + 1;
            } while (BigInteger.GreatestCommonDivisor(k, p - 1) != 1);

            BigInteger r = BigInteger.ModPow(g, k, p);

            if (r < 0)
                r += p;

            BigInteger m = Hash(M, p);
            BigInteger s = (m - x * r) * k.ReverseElement(p - 1);
            s %= p - 1;

            if (s < 0)
                s += p - 1;

            return new Signature(r, s);
        }

        public Signature SignatureMessage(BigInteger M, BigInteger k)
        {
            BigInteger r = BigInteger.ModPow(g, k, p);

            if (r < 0)
                r += p;

            //BigInteger m = Hash(M, p);
            BigInteger m = M % p;
            BigInteger s = (m - x * r) * k.ReverseElement(p - 1);
            s %= q;

            if (s < 0)
                s += p - 1;

            return new Signature(r, s);
        }

        public bool VerifySignature(string M, Signature sign, PublicKey key)
        {
            byte[] text = Encoding.ASCII.GetBytes(M);
            return VerifySignature(new BigInteger(text), sign, key);
        }

        public bool VerifySignature(BigInteger M, Signature sign, PublicKey key)
        {
            if (sign.r > 0 && sign.s > 0)
            {
                if (sign.r < key.p && sign.s < key.p - 1)
                {
                    //BigInteger m = Hash(M, key.p);

                    BigInteger m = M % p;

                    BigInteger a = BigInteger.ModPow(key.y, sign.r, key.p)
                        * BigInteger.ModPow(sign.r, sign.s, key.p);
                    a %= key.p;

                    BigInteger b = BigInteger.ModPow(key.g, m, key.p);

                    if (a == b)
                        return true;
                } 
            }

            return false;
        }
    }
}
