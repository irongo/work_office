using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office
{
    public class Task
    {
        int remain;
        int function;
        public int Remain
        {
            get
            {
                return remain;
            }
            set
            {
                remain = value;
            }
        }
        public int Function
        {
            get
            {
                return function;
            }
            set
            {
                function = value;
            }
        }
        public Task(Random Randomizer)
        {
            remain = Randomizer.Next(1, 3);
            function = Randomizer.Next(1, 6);
        }
    }
}
