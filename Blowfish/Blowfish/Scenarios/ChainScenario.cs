using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blowfish.Scenarios
{
    public class ChainScenario : IScenario
    {
        private int keySize = 56;
        private int textSize = 1048576;

        private int blockLength = 8;

        public string Description => "Исследование вероятностных " +
            "свойств выходной последовательности в режиме цепочной обработки.";

        public void RunScenario(IEncoder encoder)
        {
            byte[] key = BlockProvider.RandomBlock(keySize);
            byte[] text = BlockProvider.ZeroBlock(textSize);

            byte[] Y = BlockProvider.ZeroBlock(blockLength);

            int rounds = textSize / blockLength;
            encoder.SetupKey(key);

            for (int i = 0; i < rounds; i++) 
            {
                Y = encoder.Encrypt(Y);
                Buffer.BlockCopy(Y, 0, text, i * blockLength, blockLength);
            }

            TestSuiteRunner runner = new TestSuiteRunner();
            runner.RunSuite(text);
        }
    }
}
