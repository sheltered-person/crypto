using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;

namespace RSA
{
    public class BIGenerator
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();


        private static int RandomSecureInt32(int lowerBound, int upperBound)
        {
            if (lowerBound > upperBound)
                throw new ArgumentOutOfRangeException(nameof(lowerBound));

            if (lowerBound == upperBound)
                return lowerBound;

            var data = new byte[4];
            rngCsp.GetBytes(data);

            int 
                generatedValue = Math.Abs(BitConverter.ToInt32(data, startIndex: 0)), 
                diff = upperBound - lowerBound,
                mod = generatedValue % diff,
                normalizedNumber = lowerBound + mod;

            return normalizedNumber;
        }


        public static BigInteger GenerateBigInteger()
        {
            int bytesCount = RandomSecureInt32(20, 100);
            var data = new byte[bytesCount];

            rngCsp.GetBytes(data);
            return new BigInteger(data);
        }


        public static BigInteger GenerateBigInteger(BigInteger upperBound)
        {
            byte[]
                upperInteger = upperBound.ToByteArray(),
                resultInteger = new byte[upperInteger.Length];

            int size = upperInteger.Length - 1;

            resultInteger[size] = (byte)RandomSecureInt32(0, upperInteger[size]);

            //Если сгенерированная первая цифра совпала с первой цифрой верхней границы.
            if (resultInteger[size] == upperInteger[size])
            {
                for (int i = size - 1; i >= 0; i--)
                {
                    resultInteger[i] = (byte)RandomSecureInt32(0, upperInteger[i]);
                }
            }
            else
            {
                for (int i = size - 1; i >= 0; i--)
                {
                    resultInteger[i] = (byte)RandomSecureInt32(0, 255);
                }
            }

            return new BigInteger(resultInteger);
        }


        //Генератор случайного простого числа заданного порядка.
        public static BigInteger GeneratePrimeBigInteger(int bitSize)
        {
            if (bitSize <= 1)
                throw new ArgumentException("Error: num of bit can't be 1 or less.");

            if (bitSize <= 20)
            {
                BigInteger tempResult;
                bool isPrime;

                do
                {
                    tempResult = GenerateBigInteger(BigInteger.One << bitSize) - 1;
                    isPrime = IsPrimeClass.BasicPrimaryTest(tempResult);

                } while (!isPrime || tempResult == 0);

                return tempResult;
            }

            BigInteger N, R, F = 2;

            while (F == 2 || F < 0)
                F = GeneratePrimeBigInteger(bitSize / 2 - 1);

            while (true)
            {
                R = GenerateBigInteger(2 * F - 1) + 1;

                if (BigInteger.GreatestCommonDivisor(F, 2 * R) != 1)
                    continue;

                N = 2 * R * F + 1;

                if (N < 0)
                    continue;

                bool hasDivisors = false;

                for (int i = 2; i < 1000 && i * i <= N; i++)
                {
                    if (N % i == 0)
                    {
                        hasDivisors = true;
                        break;
                    }
                }

                if (hasDivisors)
                    continue;

                //a = (1, N - 1)
                //BigInteger a = GenerateBigInteger(N - 2) + 1;
                BigInteger a = 2;

                //a^(N-1) = 1 (mod N)
                if (BigInteger.ModPow(a, N - 1, N) != 1)
                    continue;

                //a^((N-1)/F) (mod N) - 1
                BigInteger temp = BigInteger.ModPow(a, (N - 1) / F, N) - 1;

                // GCD (temp, N)
                if (BigInteger.GreatestCommonDivisor(temp, N) != 1)
                    continue;

                return N;
            }
        }
    }
}
