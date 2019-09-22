using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    public class SyntaxException : System.Exception
    {
        public SyntaxException(string message, int pointer) : base("\n" + message + " at symbol " + pointer + ".") { }

    }
}
