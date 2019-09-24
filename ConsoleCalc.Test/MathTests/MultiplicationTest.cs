using System;
using NUnit.Framework;

namespace ConsoleCalc.Test
{
    [TestFixture]
    public class MultiplicationTest
    {
        private Multiplication mul;

        [SetUp]
        public void Init()
        {
            mul = new Multiplication();
        }


        [Test]
        public void BaseFunctionTest()
        {
            double expected = 8;
            double actual = mul.Execute(4, 2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NullArgsTest()
        {
            Assert.Catch<NotEnoughArgsException>(() => mul.Execute());
        }

        [Test]
        public void PropertiesTest()
        {
            Priority expectedPriority = Priority.HIGHT;
            NumberOfArgs expectedNumberOfArgs = NumberOfArgs.TWO;
            Assert.AreEqual(expectedPriority, mul.GetPriority());
            Assert.AreEqual(expectedNumberOfArgs, mul.GetNumberOfArgs());
        }
    }
}
