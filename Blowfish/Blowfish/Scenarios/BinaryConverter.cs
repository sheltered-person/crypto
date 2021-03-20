using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blowfish.Scenarios
{
    public class BinaryConverter
    {
        public static List<int> ToBinaryList(byte[] bytes)
        {
            List<int> bits = new List<int>(bytes.Length * 8);

            foreach (byte b in bytes)
            {
                string binary = Convert.ToString(b, 2).PadLeft(8, '0');

                foreach (char bit in binary)
                    bits.Add(bit - '0');
            }

            return bits;
        }

        public static byte[] ToByteArray(List<int> bits)
        {
            byte[] bytes = new byte[bits.Count / 8];

            for (int i = 0; i < bits.Count; i += 8)
            {
                StringBuilder binary = new StringBuilder();

                for (int j = i; j < i + 8; j++)
                    binary.Append(bits[j]);

                bytes[i / 8] = Convert.ToByte(binary.ToString(), 2);
            }

            return bytes;
        }
    }
}
