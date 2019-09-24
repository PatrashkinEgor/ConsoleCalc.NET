using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    public class Division : MathOperation
    {
        public Division() : base(Priority.HIGHT, NumberOfArgs.TWO) { }
        override public double Execute(params double[] arg)
        {
            this.EnoughArgsToExecute(arg.Length);

            if (arg[1] == 0)
                throw new DivideByZeroException("Division by zero is prohibited.");
            return arg[0] / arg[1];
        }
    }
}
