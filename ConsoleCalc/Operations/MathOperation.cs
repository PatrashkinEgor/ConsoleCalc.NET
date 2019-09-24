using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    public abstract class MathOperation : IExecutable
    {
        private readonly Priority priority;
        protected readonly NumberOfArgs numberOfArgs;
        public MathOperation(Priority priority, NumberOfArgs numberOfArgs)
        {
            this.priority = priority;
            this.numberOfArgs = numberOfArgs;
        }
        public Priority GetPriority()
        {
            return priority;
        }
        public NumberOfArgs GetNumberOfArgs()
        {
            return numberOfArgs;
        }

        protected bool EnoughArgsToExecute(int length)
        {
            if (length < (int)numberOfArgs)
                throw new NotEnoughArgsException();
            return true;
        }


        public abstract double Execute(params double[] arg);
    }
    public enum Priority
    {
        HIGHT,
        LOW
    }
    public enum NumberOfArgs
    {
        ONE = 1,
        TWO
    }
}
