using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCryptoSystems
{
    public class FreqComparer : IComparer<char>
    {
        private int[] frequencies;


        public FreqComparer(int[] frequencies)
        {
            this.frequencies = frequencies;
        }


        public int Compare(char x, char y)
        {
            if (frequencies[x - 'A'] > frequencies[y - 'A'])
                return -1;
            else if (frequencies[x - 'A'] == frequencies[y - 'A'])
                return 0;
            else
                return 1;
        }
    }
}
