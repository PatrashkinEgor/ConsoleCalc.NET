using System;
using NUnit.Framework;

namespace ConsoleCalc.Test
{
    [TestFixture]
    public class DivisionTest
    {
        private Division div;

        [SetUp]
        public void Init()
        {
            div = new Division();
        }

        [Test]
        public void BaseFunctionTest()
        {
            double expected = 2;
            double actual = div.Execute(4, 2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DividedByZeroTest()
        {
            Assert.Catch<DivideByZeroException>(() => div.Execute(0, 0));
        }

        [Test]
        public void NullArgsTest()
        {
            Assert.Catch<IndexOutOfRangeException>(() => div.Execute());
        }
        [Test]
        public void PropertiesTest()
        {
            Priority expectedPriority = Priority.HIGHT;
            NumberOfArgs expectedNumberOfArgs = NumberOfArgs.TWO;
            Assert.AreEqual(expectedPriority, div.GetPriority());
            Assert.AreEqual(expectedNumberOfArgs, div.GetNumberOfArgs());
        }
    }



}
