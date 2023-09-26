using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textanalyse
{
    class NumberCount
    {
        private int number;
        private int numberCount;

        public NumberCount(int zahl)
        {
            this.number = zahl;
            numberCount = 1;
        }

        public void increaseNumberCount()
        {
            numberCount++;
        }

        public int getNumber()
        {
            return number;
        }
        public int getNumberCount()
        {
            return numberCount;
        }
    }
}
