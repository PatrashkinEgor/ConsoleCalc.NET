using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    class Multiplication : MathOperation
    {
        public Multiplication() : base(Priority.HIGHT, NumberOfArgs.TWO) { }
        override public double Execute(params double[] arg)
        {
            return arg[0] * arg[1];
        }
    }
}
