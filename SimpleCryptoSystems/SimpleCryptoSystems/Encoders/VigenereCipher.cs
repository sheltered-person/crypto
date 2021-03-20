using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCryptoSystems
{
    public class VigenereCipher : IEncoder
    {
        private string key;

        private static List<List<char>> tabulaRecta;

        private static int M = 26;

        private int keySize;


        static VigenereCipher()
        {
            tabulaRecta = new List<List<char>>(M);
            tabulaRecta.Add(new List<char>(M));

            for (int i = 0; i < M; i++)
                tabulaRecta[0].Add((char)('A' + i));

            //tabulaRecta[0].Add(' ');

            for (int i = 1; i < M; i++)
            {
                tabulaRecta.Add(new List<char>(M));

                for (int j = 0; j < M; j++)
                    tabulaRecta[i].Add(tabulaRecta[i - 1][(j + 1) % M]);
            }
        }


        public VigenereCipher(string key)
        {
            this.key = key;
            this.keySize = key.Length;
        }


        public string EncryptText(string openText)
        {
            int 
                size = openText.Length,
                shift = 0;

            StringBuilder encryptedText = new StringBuilder();

            foreach (char c in openText) 
            {
                if (c == ' ')
                {
                    encryptedText.Append(' ');
                    continue;
                }

                int
                    x = (c == ' ') ? 26 : c - 'A',
                    y = key[shift % keySize];

                y = ((char)y == ' ') ? 26 : y - 'A';

                encryptedText.Append(tabulaRecta[x][y]);
                shift++;
            }

            return encryptedText.ToString();
        }


        public string DecryptText(string cipherText)
        {
            StringBuilder decryptedText = new StringBuilder();

            int shift = 0;

            foreach (char c in cipherText)
            {
                int
                    x = key[shift % keySize],
                    y = 0;

                x = ((char)x == ' ') ? 26 : x - 'A';

                foreach (char symb in tabulaRecta[x]) 
                {
                    if (symb == c)
                        break;

                    y++;
                }

                y = (y == 26) ? ' ' : y + 'A';
                decryptedText.Append((char)y);

                shift++;
            }

            return decryptedText.ToString();
        }
    }
}
