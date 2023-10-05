using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textanalyse
{
    class CountingClass<T>
    {
        private T countedObject;
        private int objectCount;

        public CountingClass(T countedObject)
        {
            this.countedObject = countedObject;
            objectCount = 1;
        }

        public void increaseNumberCount()
        {
            objectCount++;
        }

        public T getCountedObject()
        {
            return countedObject;
        }
        public int getObjectCount()
        {
            return objectCount;
        }
    }
}
