using System;
using NUnit.Framework;


namespace ConsoleCalc.Test
{
    [TestFixture]
    public class SubtractionTest 
    {
        Subtraction sub;

        [SetUp]
        public void Init()
        {
            sub = new Subtraction();
        }


        [Test]
        public void BaseFunctionTest()
        {
            Subtraction sub = new Subtraction();
            double expected = 2;
            double actual = sub.Execute(4, 2);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NullArgsTest()
        {
            Subtraction sub = new Subtraction();
            Assert.Catch<IndexOutOfRangeException>(() => sub.Execute());
        }

        [Test]
        public void PropertiesTest()
        {
            Subtraction sub = new Subtraction();
            Priority expectedPriority = Priority.LOW;
            NumberOfArgs expectedNumberOfArgs = NumberOfArgs.TWO;
            Assert.AreEqual(expectedPriority, sub.GetPriority());
            Assert.AreEqual(expectedNumberOfArgs, sub.GetNumberOfArgs());
        }
    }
}
