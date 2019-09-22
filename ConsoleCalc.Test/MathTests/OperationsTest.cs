using System;
using NUnit.Framework;

namespace ConsoleCalc.Test
{
    [TestFixture]
    public class OperationsTest
    {
        [TestCase('+')]
        [TestCase('-')]
        [TestCase('*')]
        [TestCase('/')]
        public void ContainsCmdTest(char cmd)
        {
            Operations operations = new Operations();
            Assert.IsTrue(operations.ContainsCmd(cmd));
        }

        [Test]
        public void ContainsNotValidCmdTest()
        {
            Operations operations = new Operations();
            Assert.IsTrue(!operations.ContainsCmd('#'));
        }

        [TestCase(typeof(Summation), '+')]
        [TestCase(typeof(Subtraction), '-')]
        [TestCase(typeof(Multiplication), '*')]
        [TestCase(typeof(Division), '/')]
        public void GetOperationTest(Type expected, char cmd)
        {
            Operations operations = new Operations();
            Assert.IsInstanceOf(expected, operations.GetOperation(cmd));
        }

        [Test]
        public void GetNotValidOperationTest()
        {
            Operations operations = new Operations();
            Assert.IsNull(operations.GetOperation('&'));
        }
    }
}
