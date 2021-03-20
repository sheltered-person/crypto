using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;

namespace PrimeNumbers
{
    public class Program
    {
        static void Main(string[] args)
        {
            int bitSize = 256;
            int 
                secureParam = 100,
                amountOfTests;

            if (!int.TryParse(args[1], out amountOfTests))
            {
                Console.WriteLine("Please, use a file name string as the first argument " +
                    "and the uint amount of tests as the second.");
                return;
            }

            using (StreamWriter wstream = new StreamWriter(args[0], false))
            {
                long wrong = 0;

                DateTime start = DateTime.Now;

                for (uint i = 0; i < amountOfTests; i++)
                {
                    BigInteger bi = BIGenerator.GeneratePrimeBigInteger(bitSize);

                    bool
                        fermat = IsPrimeClass.FermatTest(bi, secureParam),
                        soloveyStrassen = IsPrimeClass.SolovayStrassenTest(bi, secureParam),
                        millerRabin = IsPrimeClass.MillerRabinTest(bi, secureParam);

                    if (!(fermat && soloveyStrassen && millerRabin))
                        wrong++;

                    StringBuilder report = new StringBuilder("\n" + (i + 1).ToString() 
                        + ". Generated integer:\t" + bi.ToString()
                        + "\n\tFermat prime test result:\t" + fermat.ToString()
                        + "\n\tSolovey-Strassen test result:\t" + soloveyStrassen.ToString()
                        + "\n\tMiller-Rabin test result:\t" + millerRabin.ToString());

                    wstream.WriteLine(report.ToString());
                    Console.WriteLine(report.ToString());
                }

                StringBuilder statistics = new StringBuilder("\nTest statistics:\n\tTotal:\t" + args[1]
                    + "\n\tCrrct:\t" + (amountOfTests - wrong).ToString()
                    + "\n\tWrong:\t" + wrong.ToString());

                wstream.WriteLine(statistics.ToString());
                Console.WriteLine(statistics.ToString());

                Console.WriteLine(DateTime.Now - start);
            }

            Console.ReadKey();
        }
    }
}
