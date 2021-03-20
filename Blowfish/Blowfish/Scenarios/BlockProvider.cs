using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blowfish.Scenarios
{
    public class BlockProvider
    {
        private static Random random;

        static BlockProvider()
        {
            random = new Random();
        }

        public static byte[] RandomBlock(int size)
        {
            byte[] sequence = new byte[size];
            random.NextBytes(sequence);

            return sequence;
        }

        public enum HemmingMode
        {
            Low,
            High
        }

        public static byte[] HemmingBlock(int size, HemmingMode mode)
        {
            string filler;
            char bit;

            if (mode == HemmingMode.High)
            {
                filler = "11111111";
                bit = '0';
            }
            else
            {
                filler = "00000000";
                bit = '1';
            }

            byte[] blocks = new byte[size];

            StringBuilder[] binaryBlocks = new StringBuilder[size];

            for (int i = 0; i < size; i++)
                binaryBlocks[i] = new StringBuilder(filler);

            int bitsCount = random.Next(10) % 2;

            if (bitsCount != 0)
            {
                int[] blockNum = new int[bitsCount];
                int[] positions = new int[bitsCount];

                for (int k = 0; k < bitsCount; k++)
                {
                    blockNum[k] = random.Next(size);
                    positions[k] = random.Next(8);
                }

                for (int i = 0; i < bitsCount; i++)
                    binaryBlocks[blockNum[i]][positions[i]] = bit;
            }

            for (int i = 0; i < size; i++)
                blocks[i] = Convert.ToByte(binaryBlocks[i].ToString(), 2);

            return blocks;
        }

        public static byte[] ZeroBlock(int size)
        {
            byte[] block = new byte[size];
            return block;
        }
    }
}
