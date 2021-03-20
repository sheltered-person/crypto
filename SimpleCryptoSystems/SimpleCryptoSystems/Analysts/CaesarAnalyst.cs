using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleCryptoSystems
{
    public class CaesarAnalyst : IAnalyst
    {
        private static readonly int M = 26;

        private static readonly string file = "english_monograms.txt";

        private static List<double> letterScore;

        private StringBuilder cipherText;


        static CaesarAnalyst()
        {
            letterScore = new List<double>(M);

            using (StreamReader reader = new StreamReader(file))
            {
                string s;

                while ((s = reader.ReadLine()) != null)
                    letterScore.Add(double.Parse(s));
            }
        }


        public CaesarAnalyst(StringBuilder cipherText)
        {
            this.cipherText = cipherText;
        }


        private int[] CountFrequencies(StringBuilder text)
        {
            int[] frequency = new int[M];

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != ' ')
                    frequency[text[i] - 'A']++;
            }

            return frequency;
        }


        double CountHiSquaredScore(StringBuilder str)
        {
            double hiSqr = 0;
            int[] frequency = CountFrequencies(str);

            for (int i = 0; i < M; i++)
                hiSqr += (frequency[i] * 100 / str.Length - letterScore[i]) 
                    / letterScore[i];

            return hiSqr;
        }


        public string Analyze()
        {
            string decryptedText = "";
            double score = double.MaxValue;

            for (int i = 0; i < M; i++) 
            {
                StringBuilder text = new StringBuilder();

                for (int j = 0; j < cipherText.Length; j++) 
                {
                    if (cipherText[j] == ' ')
                        text.Append(' ');
                    else
                    {
                        int code = (cipherText[j] - i) % M;
                        text.Append((char)(code + 'A'));
                    }
                }

                double currentScore = CountHiSquaredScore(text);

                if (currentScore < score)
                {
                    score = currentScore;
                    decryptedText = text.ToString();
                }
            }

            return decryptedText;
        }
    }
}
