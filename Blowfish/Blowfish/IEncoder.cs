using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blowfish
{
    public interface IEncoder
    {
        void SetupKey(byte[] key);
        byte[] Encrypt(byte[] openText);
    }
}
