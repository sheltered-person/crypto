using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RSA
{
    public class Program
    {
        static void Main(string[] args)
        {
            RSAEncoder Alisa = new RSAEncoder();
            RSAEncoder Bob = new RSAEncoder();

            //BigInteger message = BIGenerator.GenerateBigInteger();
            string message = "attack at the east dawn of the castle";
            Console.WriteLine("Alisa's message:\n\t" + message);

            BigInteger cipherText = Bob.Encrypt(message, Alisa.Key);
            Console.WriteLine("\nBob's encrypt message:\n\t" + cipherText);

            message = "kuku";

            cipherText = Alisa.Decrypt(cipherText, out message);
            Console.WriteLine("\nAlisa's decrypt message:\n\t" + message);

            Console.ReadKey();
        }
    }
}
