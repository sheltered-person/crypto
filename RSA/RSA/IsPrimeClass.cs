using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RSA
{
    public class IsPrimeClass
    {
        //Простейшая проверка на простоту делением до корня.
        public static bool BasicPrimaryTest(BigInteger num)
        {
            if (num == 1)
                return false;

            for (BigInteger i = 2; i * i <= num; i++)
            {
                if (num % i == 0)
                    return false;
            }

            return true;
        }


        //Вероятностный тест Ферма, основанный на малой теореме Ферма.
        public static bool FermatTest(BigInteger num, int secureParam)
        {
            bool isPrime = true;

            Parallel.For(0, secureParam, (i, pls) =>
            {
                BigInteger a = BIGenerator.GenerateBigInteger(num - 4) + 2;

                //a^(num - 1) = 1 (mod num)
                if (BigInteger.ModPow(a, num - 1, num) == 1)
                    return;

                isPrime = false;
                pls.Break();
            });

            return isPrime;
        }


        //Вычисление символа Якоби.
        public static BigInteger JacobiSymbol(BigInteger a, BigInteger n)
        {
            if (a < 0)
            {
                BigInteger tempBI = (n - 1) / 2;
                int exponent = (tempBI.IsEven) ? 1 : -1;

                return exponent * JacobiSymbol(-a, n);
            }

            if (a.IsEven)
            {
                BigInteger tempBI = (n * n - 1) / 8;
                int exponent = (tempBI.IsEven) ? 1 : -1;

                return exponent * JacobiSymbol(a / 2, n);
            }

            if (a == 1)
                return 1;

            if (a < n)
            {
                BigInteger tempBI = (a - 1) * (n - 1) / 4;
                int exponent = (tempBI.IsEven) ? 1 : -1;

                return exponent * JacobiSymbol(n, a);
            }

            return JacobiSymbol(a % n, n);
        }


        //Тест Соловея-Штрассена. У тебя явно здесь проблемы.
        public static bool SolovayStrassenTest(BigInteger num, int secureParam)
        {
            bool isPrime = true;

            Parallel.For(0, secureParam, (i, pls) =>
            {
                BigInteger a = BIGenerator.GenerateBigInteger(num - 3) + 2;

                if (BigInteger.GreatestCommonDivisor(a, num) <= 1)
                {
                    BigInteger
                        mod = BigInteger.ModPow(a, (num - 1) / 2, num),
                        jacobi = JacobiSymbol(a, num);

                    //a^((num - 1)/2) = jacobi(a, num) (mod num)

                    if (jacobi < 0)
                        jacobi += num;

                    if (mod == jacobi % num)
                        return;

                    isPrime = false;
                    pls.Break();
                }

            });

            return isPrime;
        }


        //Тест Миллера-Рабина.
        public static bool MillerRabinTest(BigInteger num, int secureParam)
        {
            // num = 2^s * t
            BigInteger t = num - 1;
            long s = 0;

            while (t % 2 == 0) 
            {
                t /= 2;
                s++;
            }

            bool isPrime = true;

            Parallel.For(0, secureParam, (i, pls) =>
            {
                BigInteger
                    a = BIGenerator.GenerateBigInteger(num - 4) + 2,
                    x = BigInteger.ModPow(a, t, num);

                //a = (2, num - 2)
                //x = a^t (mod num)

                if (x == 1 || x == num - 1)
                    return;
                
                for (long j = 1; j < s; j++)
                {
                    //x = x^2 (mod num)
                    x = BigInteger.ModPow(x, 2, num);

                    if (x == num - 1)
                        return;
                }

                isPrime = false;
                pls.Break();
            });

            return isPrime;
        }
    }
}
