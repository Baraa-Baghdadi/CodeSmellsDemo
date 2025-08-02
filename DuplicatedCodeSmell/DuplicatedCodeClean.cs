using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.DuplicatedCodeSmell
{
    public   class DuplicatedCodeClean
    {
        private void Log(string phase)
        {
            Console.WriteLine(phase);
        }

        public void CreateOrder()
        {
            //Log("Started");
            //Log("Finished");
        }
        public void CancelOrder()
        {
            //Log("Started");
            //Log("Finished");
        }
    }
}
