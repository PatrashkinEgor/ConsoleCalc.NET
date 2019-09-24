using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    public class NotEnoughArgsException : System.Exception
    {
        public NotEnoughArgsException() : base("Not enough arguments to execute math operation") { }
    }
}
