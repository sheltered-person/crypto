using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blowfish.Scenarios
{
    public class RandomDataScenario : IScenario
    {
        private int keySize = 56;
        private int textSize = 1048576;

        private int blockLength = 8;

        public string Description => "Исследование вероятностных свойств выходной " +
            "последовательности режима простой замены при произвольном выборе открытого текста и ключа.";

        public void RunScenario(IEncoder encoder)
        {
            byte[] key = BlockProvider.RandomBlock(keySize);
            byte[] text = BlockProvider.ZeroBlock(textSize);

            encoder.SetupKey(key);

            byte[] X;

            int rounds = textSize / blockLength;

            for (int i = 0; i < rounds; i++)
            {
                X = BlockProvider.RandomBlock(blockLength);
                Buffer.BlockCopy(X, 0, text, i * blockLength, blockLength);
            }

            byte[] cipherText = encoder.Encrypt(text);

            TestSuiteRunner runner = new TestSuiteRunner();
            runner.RunSuite(cipherText);
        }
    }
}
