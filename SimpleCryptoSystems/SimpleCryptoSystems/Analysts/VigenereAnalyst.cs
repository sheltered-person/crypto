using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleCryptoSystems
{
    public class VigenereAnalyst : IAnalyst
    {
        private static int aSize = 26;

        private string cipherText;

        private List<char> text;

        private SortedDictionary<double, int> lengths;


        public VigenereAnalyst(string cipherText)
        {
            this.cipherText = cipherText;

            lengths = new SortedDictionary<double, int>(
                Comparer<double>.Create((x,y)=> {
                    return y.CompareTo(x);
                }));

            this.text = cipherText.SkipAll(' ', Comparer<char>.Default).ToList();
        }


        private int[,] CountFrequencies(int period)
        {
            int[,] frequencies = new int[period, aSize];

            for (int i = 0; i < period; i++)
            {
                for (int j = i; j < text.Count; j += period)
                    frequencies[i, text[j] - 'A']++;
            }

            return frequencies;
        }


        private double CountCoincidence(int period)
        {
            double ic = 0;

            int[,] frequencies = CountFrequencies(period);

            int[] sizes = new int[period];

            for (int i = 0; i < period; i++)
            {
                double current = 0;

                for (int j = 0; j < aSize; j++)
                {
                    current += (double)frequencies[i, j] * (frequencies[i, j] - 1);
                    sizes[i] += frequencies[i, j];
                }

                current /= sizes[i] * (sizes[i] - 1);
                ic += current;
            }

            return ic / period;
        }


        public string Analyze()
        {
            int size = (int)Math.Sqrt(text.Count);

            for (int i = 0, period = 2; i < 10; i++, period++)
            {
                double ic = CountCoincidence(period);
                lengths.Add(ic, period);
            }

            List<string> results = new List<string>();

            string decryptedText = "";
            double score = double.MinValue;

            foreach (var length in lengths.Take(5))
            {
                int T = length.Value;

                StringBuilder[] subs = new StringBuilder[T];

                for (int i = 0; i < T; i++)
                    subs[i] = new StringBuilder();

                for (int i = 0; i < text.Count; i++)
                    subs[i % T].Append(text[i]);

                string[] decrypted = new string[T];

                for (int i = 0; i < T; i++) 
                {
                    CaesarAnalyst analyst = new CaesarAnalyst(subs[i]);
                    decrypted[i] = analyst.Analyze();
                }

                StringBuilder str = new StringBuilder();

                for (int i = 0; i < text.Count; i++)
                    str.Append(decrypted[i % T][i/T]);

                double currentScore = 
                    NgramStatistics.CountTextScore(str.ToString());

                if (currentScore > score)
                {
                    score = currentScore;
                    decryptedText = str.ToString();
                }
            }

            return decryptedText;
        }
    }
}
