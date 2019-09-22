using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    public class Operations
    {
        readonly private Dictionary<char, MathOperation> mathOperations = new Dictionary<char, MathOperation>();
        public Operations()
        {
            mathOperations.Add('+', new Summation());
            mathOperations.Add('-', new Subtraction());
            mathOperations.Add('*', new Multiplication());
            mathOperations.Add('/', new Division());

        }

        public bool ContainsCmd(char cmd)
        {
            return mathOperations.ContainsKey(cmd);
        }

        public MathOperation GetOperation(char cmd)
        {
            if (ContainsCmd(cmd))
            {
                return mathOperations[cmd];
            }
            else return null;

        }
    }
}
