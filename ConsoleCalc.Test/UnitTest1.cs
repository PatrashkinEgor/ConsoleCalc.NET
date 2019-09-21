using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ConsoleCalc.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SummationTest1()
        {
            Summation sum = new Summation();
            double expected = 5;
            double actual = sum.Execute(2, 3);
            Assert.AreEqual(expected, actual);
            Assert.
        }
    }
}
