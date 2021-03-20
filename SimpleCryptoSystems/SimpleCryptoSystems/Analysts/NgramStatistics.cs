using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleCryptoSystems
{
    public static class NgramStatistics
    {
        private static Dictionary<string, double> ngrams;

        private static int n;

        private static double floor;

        private static readonly string file = "english_quadgrams.txt";


        static NgramStatistics()
        {
            ngrams = new Dictionary<string, double>(400000);

            using (StreamReader reader = new StreamReader(file))
            {
                string ngram;

                while ((ngram = reader.ReadLine()) != null)
                {
                    string[] subs = ngram.Split(' ');
                    ngrams.Add(subs[0], double.Parse(subs[1]));
                }
            }

            List<string> keys = ngrams.Keys.ToList();

            n = keys[0].Length;

            foreach (string key in keys)
                ngrams[key] = Math.Log10(ngrams[key] / ngrams.Count);

            floor = Math.Log10(0.01 / ngrams.Count);
        }

        public static double CountTextScore(string text)
        {
            double score = 0;

            for (int i = 0; i + n < text.Length; i++)
            {
                string key = text.Substring(i, n);

                if (key.Contains(' '))
                    continue;

                if (ngrams.ContainsKey(key))
                    score += ngrams[key];
                else
                    score += floor;
            }

            return score;
        }
    }
}
