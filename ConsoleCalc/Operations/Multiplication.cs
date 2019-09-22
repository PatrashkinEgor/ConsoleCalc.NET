﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc
{
    public class Multiplication : MathOperation
    {
        public Multiplication() : base(Priority.HIGHT, NumberOfArgs.TWO) { }
        override public double Execute(params double[] arg)
        {
            if (arg.Length < (int)numberOfArgs)
                throw new IndexOutOfRangeException("Not enough arguments to multiply");
            return arg[0] * arg[1];
        }
    }
}