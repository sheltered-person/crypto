using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blowfish.Scenarios
{
    public class SpecialDataScenario : IScenario
    {
        private int keySize = 56;
        private int textSize = 1048576;

        private int blockLength = 8;

        private Mode mode;
        private BlockProvider.HemmingMode hemming;

        public enum Mode
        {
            Key,
            Text
        }

        public SpecialDataScenario(Mode mode, BlockProvider.HemmingMode hemming)
        {
            this.mode = mode;
            this.hemming = hemming;
        }

        public string Description => "Исследование вероятностных свойств выходной " +
            "последовательности режима простой замены при специальном выборе открытого текста и ключа " +
            "(" + mode.ToString() + ", " + hemming.ToString() + ").";

        public void RunScenario(IEncoder encoder)
        {
            byte[] key;
            byte[] text = BlockProvider.ZeroBlock(textSize);

            if (mode == Mode.Key)
            {
                key = BlockProvider.HemmingBlock(keySize, hemming);
                text = BlockProvider.RandomBlock(textSize);
            }
            else
            {
                key = BlockProvider.RandomBlock(keySize);

                byte[] X;

                int rounds = textSize / blockLength;

                for (int i=0;i<rounds;i++)
                {
                    X = BlockProvider.HemmingBlock(blockLength, hemming);
                    Buffer.BlockCopy(X, 0, text, i * blockLength, blockLength);
                }

                text = BlockProvider.HemmingBlock(textSize, hemming);
            }

            encoder.SetupKey(key);
            byte[] cipherText = encoder.Encrypt(text);

            TestSuiteRunner runner = new TestSuiteRunner();
            runner.RunSuite(cipherText);
        }
    }
}
