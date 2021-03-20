using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleCryptoSystems
{
    public class AffineAnalyst : IAnalyst
    {
        private static readonly int M = 26;

        private string cipherText;

        private double rate;

        private string decryptedText;


        public AffineAnalyst(string cipherText)
        {
            this.cipherText = cipherText;
            this.rate = double.MinValue;
        }

        public string Analyze()
        {
            for (int a = 1; a < M; a++) 
            {
                if (SimpleMaths.GCD(a, M) != 1)
                    continue;

                for (int b = 0; b < M; b++) 
                {
                    AffineCipher affine = new AffineCipher(a, b);
                    string text = affine.DecryptText(cipherText);

                    double score = NgramStatistics.CountTextScore(text);

                    if (score > rate)
                    {
                        rate = score;
                        this.decryptedText = text;
                    }
                }
            }

            return decryptedText;
        }
    }
}
