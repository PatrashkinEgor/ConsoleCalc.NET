using System;
using NUnit.Framework;


namespace ConsoleCalc.Test
{
    [TestFixture]
    public class SummationTest
    {
        private Summation sum;

        [SetUp]
        public void Init()
        {
            sum = new Summation();
        }

        [Test]
        public void BaseFunctionTest()
        {
            Summation sum = new Summation();
            double expected = 5;
            double actual = sum.Execute(2, 3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NullArgsTest()
        {
            Summation sum = new Summation();
            Assert.Catch<IndexOutOfRangeException>(() => sum.Execute());
        }

        [Test]
        public void PropertiesTest()
        {
            Summation sum = new Summation();
            Priority expectedPriority = Priority.LOW;
            NumberOfArgs expectedNumberOfArgs = NumberOfArgs.TWO;
            Assert.AreEqual(expectedPriority, sum.GetPriority());
            Assert.AreEqual(expectedNumberOfArgs, sum.GetNumberOfArgs());
        }
    }
}
