using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    class Division : MathOperation
    {
        public Division() : base(Priority.HIGHT, NumberOfArgs.TWO) { }
        override public double Execute(params double[] arg)
        {
            if (arg[1] == 0)
                throw new DividedByZeroException();
            return arg[0] / arg[1];
        }
    }
}
