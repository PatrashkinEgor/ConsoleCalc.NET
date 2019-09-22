using System;
using NUnit.Framework;

namespace ConsoleCalc.Test
{
    [TestFixture]
    public class ParsingCalculatorTest
    {
        private ParsingCalculator calc;

        [SetUp]
        public void Init()
        {
            calc = new ParsingCalculator();
        }


        [TestCase("w13,31", 0, "")]
        [TestCase("w13,31", 1, "13,31")]
        [TestCase(",31", 0, "")]
        [TestCase("3,3,1", 0, "3,3")]
        public void CutNumberFromStringTest(string input, int pt, string expected)
        {
            string actual = ParsingCalculator.CutNumberFromString(input, pt);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("")]
        [TestCase("2#2")]
        [TestCase("2*(2+3")]
        [TestCase("2*(2+")]
        [TestCase("2*(2++")]
        [TestCase("f*a*d*e")]
        public void CountNotValidStringTest(string input)
        {
            Assert.Catch<SyntaxException>(() => calc.CountExpressionFromString(""));
        }



        [TestCase("1+2-3", 0)]
        [TestCase("(2+2)*2", 8)]
        [TestCase("2+2*2", 6)]
        [TestCase("(-1)*2", -2)]
        [TestCase("3,14*0,5", 1.57)]
        public void CountExpTest(string input, double expected)
        {
            double actual = calc.CountExpressionFromString(input);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CountNullExpTest()
        {
            ParsingCalculator parser = new ParsingCalculator();
            string input = null;
            Assert.Catch<NullReferenceException>(() => parser.CountExpressionFromString(input));
        }
    }
}
