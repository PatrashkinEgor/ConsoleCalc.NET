using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    public class Summation : MathOperation
    {
        public Summation() : base(Priority.LOW, NumberOfArgs.TWO) { }

        override
        public double Execute(params double[] arg)
        {
            this.EnoughArgsToExecute(arg.Length);
            return arg[0] + arg[1];
        }
    }
}
