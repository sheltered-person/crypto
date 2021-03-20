using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleCryptoSystems
{
    enum Mode
    {
        Affine,
        Simple,
        Vigenere
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Mode mode = Mode.Vigenere;

            Console.WriteLine("Welcome to encoder program!");
            Console.WriteLine("Select mode:");
            Console.WriteLine("\tAffine");
            Console.WriteLine("\tSimple");
            Console.WriteLine("\tVigenere\n");

            string s = Console.ReadLine();

            if (!Enum.TryParse(s, out mode))
                return;

            Console.WriteLine("Select working mode:");
            Console.WriteLine("\tOpen");
            Console.WriteLine("\tClose");

            string work = Console.ReadLine();

            if (work.ToLower() == "open")
                OpenMode(mode);
            else
                CloseMode(mode);

            Console.WriteLine("\nFiles are successfully writed!");
            Console.ReadKey();

        }


        static void OpenMode(Mode mode)
        {
            using (StreamReader inReader = new StreamReader("in.txt"))
            {
                string openText = inReader.ReadLine();

                using (StreamReader keyReader = new StreamReader(mode.ToString() + "\\" + "key.txt"))
                {
                    IEncoder encoder;

                    try
                    {
                        switch (mode)
                        {
                            case Mode.Affine:
                                int a, b;
                                ReadAffineKeys(keyReader, out a, out b);
                                encoder = new AffineCipher(a, b);

                                break;

                            case Mode.Simple:
                                Dictionary<char, char> alphabet = ReadTable(keyReader);
                                encoder = new SimpleSubstitutionCipher(alphabet);

                                break;

                            default:
                                string keyWord = ReadKeyWord(keyReader);
                                encoder = new VigenereCipher(keyWord);

                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }

                    string cipher = encoder.EncryptText(openText);

                    using (StreamWriter cryptWriter = new StreamWriter(mode.ToString() + "\\" + "crypt.txt"))
                    {
                        cryptWriter.WriteLine(cipher);
                    }

                    string decryptText = encoder.DecryptText(cipher);

                    using (StreamWriter decryptWriter = new StreamWriter(mode.ToString() + "\\" + "decrypt.txt"))
                    {
                        decryptWriter.WriteLine(decryptText);
                    }
                }
            }
        }


        static void CloseMode(Mode mode)
        {
            using (StreamReader reader = new StreamReader(mode.ToString() + "\\" + "crypt.txt"))
            {
                StringBuilder cipher = new StringBuilder();

                string s;

                while ((s = reader.ReadLine()) != null)
                    cipher.Append(s.ToUpper());

                string cipherText = cipher.ToString();

                Console.WriteLine("Cipher-text:\n");
                Console.WriteLine(cipherText);

                IAnalyst analyst;

                switch (mode)
                {
                    case Mode.Affine:
                        analyst = new AffineAnalyst(cipherText);
                        break;

                    case Mode.Simple:
                        analyst = new SimpleSubstitutionAnalyst(cipherText);
                        break;

                    default:
                        analyst = new VigenereAnalyst(cipherText);
                        break;
                }

                string openText = analyst.Analyze();

                Console.WriteLine("\nOpen text:\n");
                Console.WriteLine(openText);

                using (StreamWriter writer = new StreamWriter(mode.ToString() + "\\" + "decrypt.txt"))
                {
                    writer.WriteLine(openText);
                }
            }
        }


        static void ReadAffineKeys(StreamReader reader, out int a, out int b)
        {
            string s = reader.ReadLine();

            string[] subs = s.Split(' ');

            if (!int.TryParse(subs[0], out a))
                throw new ArgumentException();

            if (!int.TryParse(subs[1], out b))
                throw new ArgumentException();
        }


        static Dictionary<char, char> ReadTable(StreamReader reader)
        {
            Dictionary<char, char> dictionary = new Dictionary<char, char>();

            string 
                alphabet = reader.ReadLine(),
                code = reader.ReadLine();

            for (int i = 0; i < 27; i++)
                dictionary.Add(alphabet[i], code[i]);

            return dictionary;
        }


        static string ReadKeyWord(StreamReader reader)
        {
            return reader.ReadLine();
        }
    }
}
