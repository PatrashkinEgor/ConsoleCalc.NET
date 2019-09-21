using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    abstract class MathOperation : IExecutable
    {
        private Priority priority;
        private NumberOfArgs numberOfArgs;
        public MathOperation(Priority priority, NumberOfArgs numberOfArgs)
        {
            this.priority = priority;
            this.numberOfArgs = numberOfArgs;
        }
        public Priority GetPriority()
        {
            return priority;
        }
        public NumberOfArgs GetNumberOfArgs() {
            return numberOfArgs;
        }

        public abstract double Execute(params double[] arg);
    }
    enum Priority
    {
        HIGHT,
        LOW
    }
    enum NumberOfArgs
    {
        ONE,
        TWO
    }
}
