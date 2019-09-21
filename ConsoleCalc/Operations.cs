using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    class Operations
    {
        private Dictionary<Char, MathOperation> mathOperations = new Dictionary<Char, MathOperation>();
        public Operations()
        {
        mathOperations.Add('+', new Summation());
        mathOperations.Add('-', new Subtraction());
        mathOperations.Add('*', new Multiplication());
        mathOperations.Add('/', new Division());

        }

        public bool containsCmd(char cmd)
        {
            return mathOperations.ContainsKey(cmd);
        }

        public MathOperation getOperation(char cmd)
        {
            try
            {
                return mathOperations[cmd];
            }
            catch (ArgumentException e)
            {
                e.ToString();
            }
            return null;
        }
    }
}
