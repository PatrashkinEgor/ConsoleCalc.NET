using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    class Summation : MathOperation
    {
        public Summation() : base(Priority.LOW, NumberOfArgs.TWO){}

        override
        public double Execute(params double[] arg)
        {
            return arg[0] + arg[1];
        }
    }
}
