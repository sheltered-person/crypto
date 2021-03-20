using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blowfish.Scenarios;

namespace Blowfish
{
    public class Program
    {
        static void Main(string[] args)
        {
            int scenariosCount = 9;
            IScenario[] scenarios = new IScenario[scenariosCount];

            //scenarios[0] = new RandomDataScenario();
            //scenarios[1] = new SpecialDataScenario(SpecialDataScenario.Mode.Text, BlockProvider.HemmingMode.Low);
            //scenarios[2] = new SpecialDataScenario(SpecialDataScenario.Mode.Text, BlockProvider.HemmingMode.High);
            //scenarios[3] = new SpecialDataScenario(SpecialDataScenario.Mode.Key, BlockProvider.HemmingMode.Low);
            //scenarios[4] = new SpecialDataScenario(SpecialDataScenario.Mode.Key, BlockProvider.HemmingMode.High);
            scenarios[5] = new KeyErrorPropagationScenario();
            scenarios[6] = new TextErrorPropagationScenario();
            scenarios[7] = new TextCorrelationScenario();
            scenarios[8] = new ChainScenario();

            BlowfishEncoder encoder = new BlowfishEncoder();

            for (int i = 5; i < 9; i++)
            {
                Console.WriteLine(scenarios[i].Description);
                scenarios[i].RunScenario(encoder);
            }

            //foreach (var scenario in scenarios)
            //{
            //    Console.WriteLine(scenario.Description);
            //    scenario.RunScenario(encoder);
            //}

            Console.ReadKey();
        }
    }
}
