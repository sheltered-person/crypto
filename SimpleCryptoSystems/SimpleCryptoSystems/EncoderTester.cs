using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleCryptoSystems
{
    public static class EncoderTester
    {
        public static bool TestEncoder(IEncoder encoder, 
            string openText, out string decryptedText)
        {
            string cipherText = encoder.EncryptText(openText);
            decryptedText = encoder.DecryptText(cipherText);

            return openText.CompareTo(decryptedText) == 0;
        }
    }
}
