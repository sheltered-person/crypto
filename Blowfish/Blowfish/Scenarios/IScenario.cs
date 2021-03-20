using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blowfish.Scenarios
{
    public interface IScenario
    {
        void RunScenario(IEncoder encoder);
        string Description { get; }
    }
}
