using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blowfish.Scenarios
{
    public class TextCorrelationScenario : IScenario
    {
        private int keySize = 56;
        private int textSize = 1048576;

        private int blockLength = 8;

        public string Description => "Исследование корреляции открытого " +
            "и зашифрованного текста в режиме простой замены.";

        public void RunScenario(IEncoder encoder)
        {
            byte[] text = BlockProvider.ZeroBlock(textSize);
            byte[] key = BlockProvider.RandomBlock(keySize);

            encoder.SetupKey(key);

            int rounds = textSize / blockLength;

            for (int i = 0; i < rounds; i++)
            {
                byte[] X = BlockProvider.RandomBlock(blockLength);
                BlowfishEncoder.XOR(ref X, encoder.Encrypt(X));
                Buffer.BlockCopy(X, 0, text, i * blockLength, blockLength);
            }

            TestSuiteRunner runner = new TestSuiteRunner();
            runner.RunSuite(text);
        }
    }
}
