using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        // TDD的起手式，紅燈->綠燈->重構循環的第一步：紅燈。
        [TestMethod()]
        public void Minus_Input_First_3_Second_2_Return_1()
        {
            //arrange
            int expected = 1;
            Calculator target = new Calculator();
            int firstNumber = 3;
            int secondNumber = 2;

            //act
            int actual;
            actual = target.Minus(firstNumber, secondNumber);

            //assert
            Assert.AreEqual(expected, actual);
        }


        // Mixed Bill 叔 Class By DynamicData
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
            // 測試案例要測試邊界 & 中間值
            yield return new object[] { 1, 2, 3 };
            yield return new object[] { 2, 2, 4 };
            yield return new object[] { 3, 2, 5 };
            yield return new object[] { 4, 2, 6 };
        }

        /// <summary>
        ///Add 的測試，初學者寫法
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            Calculator target = new Calculator(); // TODO: 初始化為適當值
            int firstNumber = 0; // TODO: 初始化為適當值
            int secondNumber = 0; // TODO: 初始化為適當值
            int expected = 0; // TODO: 初始化為適當值
            int actual;
            actual = target.Add(firstNumber, secondNumber);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("驗證這個測試方法的正確性。");
        }

        // By GWT 命名 By Bill 叔
        [TestMethod()]
        public void Given_Calculator_FirstNumber_1_SecondNumber_2_When_Add_Then_3()
        {
            //arrange
            Calculator target = new Calculator();
            int firstNumber = 1;
            int secondNumber = 2;
            int expected = 3;

            //act
            int actual;
            actual = target.Add(firstNumber, secondNumber);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}