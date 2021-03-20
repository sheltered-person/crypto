using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blowfish.Scenarios
{
    public class TextErrorPropagationScenario : IScenario
    {
        private int keySize = 56;
        private int textSize = 1048576;

        private int blockLength = 8;

        public string Description => "Исследование размножения " +
            "ошибки при изменении открытого текста в режиме простой замены.";

        public void RunScenario(IEncoder encoder)
        {
            byte[] text = BlockProvider.ZeroBlock(textSize);
            byte[] key;

            byte[] X;

            int rounds = textSize / (8 * 8 * blockLength);
            int textPosition = 0;

            for (int i = 0; i < rounds; i++)
            {
                key = BlockProvider.ZeroBlock(keySize);
                X = BlockProvider.RandomBlock(blockLength);

                encoder.SetupKey(key);
                byte[] F = encoder.Encrypt(X);

                for (int j = 0; j < blockLength * 8; j++)
                {
                    List<int> bits = BinaryConverter.ToBinaryList(X);
                    bits[j] = bits[j] == 1 ? 0 : 1;

                    byte[] Xi = BinaryConverter.ToByteArray(bits);

                    byte[] Fi = encoder.Encrypt(Xi);
                    BlowfishEncoder.XOR(ref Fi, F);

                    Buffer.BlockCopy(Fi, 0, text, textPosition, blockLength);
                    textPosition += 8;
                }
            }

            TestSuiteRunner runner = new TestSuiteRunner();
            runner.RunSuite(text);
        }
    }
}
