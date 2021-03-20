using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCryptoSystems
{
    public class SimpleSubstitutionCipher : IEncoder
    {
        private Dictionary<char, char> cipherTable;

        private static int aSize = 27;


        public SimpleSubstitutionCipher(List<char> alphabet, List<char> keys)
        {
            if (alphabet.Count != aSize - 1 || keys.Count != aSize - 1)
                throw new ArgumentException("Error: " 
                    + "chiper table should contains" + aSize.ToString() 
                    + " pairs of symbols.");

            cipherTable = new Dictionary<char, char>(aSize);

            for (int i = 0; i < aSize - 1; i++)
                cipherTable.Add(alphabet[i], keys[i]);

            cipherTable.Add(' ', ' ');
        }


        public SimpleSubstitutionCipher(Dictionary<char, char> chiperTable)
        {
            if (chiperTable.Count != aSize)
                throw new ArgumentException("Error: "
                    + "chiper table should contains" + aSize.ToString()
                    + " pairs of symbols.");

            this.cipherTable = chiperTable;
        }


        public string EncryptText(string openText)
        {
            StringBuilder encryptedText = new StringBuilder();

            foreach (char c in openText)
                encryptedText.Append(cipherTable[c]);

            return encryptedText.ToString();
        }


        public string DecryptText(string chiperText)
        {
            StringBuilder decryptedText = new StringBuilder();

            foreach (char c in chiperText)
            {
                Func<KeyValuePair<Char, Char>, bool> func = pair 
                    => { return pair.Value == c; };

                IEnumerable<KeyValuePair<Char, Char>> match = cipherTable.Where(func);
                decryptedText.Append(match.ElementAt(0).Key);
            }

            return decryptedText.ToString();
        }
    }
}
