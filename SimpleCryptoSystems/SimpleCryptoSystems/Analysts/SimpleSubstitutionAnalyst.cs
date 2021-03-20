using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleCryptoSystems
{
    public class SimpleSubstitutionAnalyst : IAnalyst
    {
        private static List<char> alphabet;

        private static List<char> sortedAlphabet;

        private static string file = "english_alphabet.txt";

        private static string frequencyFile = "monograms.txt";

        private readonly int times = 100;

        private string cipherText;

        private List<char> parentKey;

        private double rate;


        static SimpleSubstitutionAnalyst()
        {
            alphabet = new List<char>(30);

            using (StreamReader reader = new StreamReader(file))
            {
                string s = reader.ReadLine();

                foreach (char c in s)
                    alphabet.Add(c);
            }

            int[] frequency = new int[alphabet.Count];

            using (StreamReader reader = new StreamReader(frequencyFile))
            {
                string s;
                int i = 0;

                while ((s = reader.ReadLine()) != null)
                    frequency[i++] = int.Parse(s);
            }

            FreqComparer comparer = new FreqComparer(frequency);

            sortedAlphabet = new List<char>(alphabet);
            sortedAlphabet.Sort(comparer);
        }


        public SimpleSubstitutionAnalyst(string cipherText)
        {
            this.cipherText = cipherText;
            this.rate = double.MinValue;
        }


        private int[] CountFrequencies(string text)
        {
            int[] frequency = new int[26];

            for (int i=0;i<text.Length;i++)
            {
                if (text[i]!=' ')
                    frequency[text[i] - 'A']++;
            }

            return frequency;
        }


        private List<char> FrequencySortedAlphabet(string text)
        {
            List<char> sorted = new List<char>(alphabet);
            int[] frequency = CountFrequencies(text);

            FreqComparer comparer = new FreqComparer(frequency);
            sorted.Sort(comparer);

            return sorted;
        }


        public string Analyze()
        {
            List<char> bestKey = FrequencySortedAlphabet(cipherText);
            SimpleSubstitutionCipher cipher = new SimpleSubstitutionCipher(sortedAlphabet, bestKey);

            string decryptedText = cipher.DecryptText(cipherText);
            double score = NgramStatistics.CountTextScore(decryptedText);

            for (int k = 0; k < 7; k++) 
            {
                Random random = new Random();
                parentKey = alphabet.Shuffle(random).ToList();

                cipher = new SimpleSubstitutionCipher(alphabet, parentKey);

                string text = cipher.DecryptText(cipherText);
                rate = NgramStatistics.CountTextScore(text);

                for (int t = 0; t < times; t++)
                {
                    List<char> key = alphabet.Shuffle(random).ToList();

                    cipher = new SimpleSubstitutionCipher(alphabet, key);

                    string nextText = cipher.DecryptText(cipherText);
                    double currentRate = NgramStatistics.CountTextScore(nextText);

                    if (currentRate > rate)
                    {
                        parentKey = key;
                        text = nextText;
                        rate = currentRate;

                        for (int fail = 0; fail < 10000;)
                        {
                            int i = random.Next(alphabet.Count),
                                j = random.Next(alphabet.Count);

                            List<char> newKey = parentKey.Swap(i, j).ToList();

                            cipher = new SimpleSubstitutionCipher(alphabet, newKey);

                            string newText = cipher.DecryptText(cipherText);
                            double newRate = NgramStatistics.CountTextScore(newText);

                            if (newRate > rate)
                            {
                                parentKey = newKey;
                                text = newText;
                                rate = newRate;

                                fail = 0;
                            }
                            else
                                fail++;
                        }
                    }
                }

                if (rate > score)
                {
                    score = rate;
                    decryptedText = text;
                    bestKey = parentKey;
                }
            }

            return decryptedText;
        }
    }
}
