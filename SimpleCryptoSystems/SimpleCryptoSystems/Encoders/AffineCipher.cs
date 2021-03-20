using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCryptoSystems
{
    public class AffineCipher : IEncoder
    {
        private int a, b;

        private static int M = 26;


        public AffineCipher(int a, int b)
        {
            if (SimpleMaths.GCD(a, M) != 1)
                throw new ArgumentException("Error:" 
                    + " \"a\" parameter and modulus M should be coprime.");

            this.a = a;
            this.b = b;
        }

        public string EncryptText(string openText)
        {
            StringBuilder encryptedText = new StringBuilder();

            foreach (char c in openText) 
            {
                if (c == ' ')
                {
                    encryptedText.Append(' ');
                    continue;
                }

                int code = (c == ' ') ? 26 : c - 'A';
                code = (a * code + b) % M;

                encryptedText.Append((char)(code + 'A'));
            }

            return encryptedText.ToString();
        }


        public string DecryptText(string cipherText)
        {
            int revA = SimpleMaths.ReverseElement(a, M);

            StringBuilder decryptedText = new StringBuilder();

            foreach (char c in cipherText)
            {
                if (c == ' ')
                {
                    decryptedText.Append(' ');
                    continue;
                }

                int 
                    code = c - 'A',
                    x = (revA * (code - b)) % M;

                if (x < 0)
                    x += M;

                char next = (x == 26) ? ' ' : (char)(x + 'A');
                decryptedText.Append(next);
            }

            return decryptedText.ToString();
        }
    }
}
