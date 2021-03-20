using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCryptoSystems
{
    public interface IEncoder
    {
        string EncryptText(string openText);
        string DecryptText(string cipherText);
    }
}
