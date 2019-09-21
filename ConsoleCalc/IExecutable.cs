using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    interface IExecutable
    {
        double Execute(params double[] arg);
    }
}
