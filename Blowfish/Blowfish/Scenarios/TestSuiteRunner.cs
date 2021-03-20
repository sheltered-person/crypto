using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blowfish.Scenarios
{
    public class TestSuiteRunner
    {
        private Tests.Test[] suite;
        private Model[] models;

        private int testCount = 9;

        public TestSuiteRunner()
        {
            models = new Model[testCount];
            suite = new Tests.Test[testCount];
        }

        private void InitSuite(List<int> bits)
        {
            for (int i = 0; i < testCount; i++)
                models[i] = new Model(bits);

            suite[0] = new Tests.Frequency(bits.Count, ref models[0]);
            suite[1] = new Tests.Runs(bits.Count, ref models[1]);
            suite[2] = new Tests.Serial(16, bits.Count, ref models[2]);
            suite[3] = new Tests.Rank(bits.Count, ref models[3]);
            suite[4] = new Tests.RandomExcursions(bits.Count, ref models[4]);
            suite[5] = new Tests.LinearComplexity(1024, bits.Count, ref models[5]);
            suite[6] = new Tests.LongestRunOfOnes(bits.Count, ref models[6]);
            suite[7] = new Tests.Universal(bits.Count, ref models[7]);
            suite[8] = new Tests.CumulativeSums(true, bits.Count, ref models[8]);
        }

        public void RunSuite(byte[] cipherText)
        {
            List<int> a = BinaryConverter.ToBinaryList(cipherText);
            InitSuite(a);

            List<double[]> results = new List<double[]>(testCount);

            foreach (var test in suite)
                results.Add(test.run(true));

            foreach (var model in models) 
            {
                foreach (var report in model.reports)
                {
                    Console.WriteLine(report.Key);
                    Console.WriteLine(report.Value.title);
                    Console.WriteLine(report.Value.body);
                }
            }
        }
    }
}
