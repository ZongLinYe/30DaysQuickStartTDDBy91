using Microsoft.VisualStudio.TestTools.UnitTesting;
using FirstUnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstUnitTest.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method)]
        [DataTestMethod()]
        public void AddTest(int firstNumber,int secondNumber,int expected)
        {
            var operation = new Calculator();
            var actual = operation.Add(firstNumber, secondNumber);
            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<object[]> GetTestData()
        {
            yield return new object[] { 1, 2, 3 };
            yield return new object[] { 2, 2, 4 };
            yield return new object[] { 3, 2, 5 };
            yield return new object[] { 4, 2, 6 };
        }
    }
}