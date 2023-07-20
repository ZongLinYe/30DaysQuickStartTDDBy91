using Microsoft.VisualStudio.TestTools.UnitTesting;
using _30DaysQuickStartTDDBy91.Day14;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30DaysQuickStartTDDBy91.Day14.Tests
{
    [TestClass()]
    public class HsinchuTests
    {
        [TestMethod()]
        public void GetsComapanyNameTest()
        {
            var target = new Hsinchu();
            var expected = "新竹貨運";
            var actual = target.GetsComapanyName();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetsFeeTest()
        {
            var target = new Hsinchu();
            var expected = 0.0;
            var actual = target.GetsFee();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void CalculateTest()
        {
            // 從整合測試的 Test case，來當作單元測試的 test case
            // Arrange
            Hsinchu target = new Hsinchu()
            {
                ShipProduct = new Product()
                {
                    IsNeedCool = true,
                    Name = "筆電",
                    Size = new Size
                    {
                        Height = 10,
                        Length = 30,
                        Width = 20
                    },
                    Weight = 10
                }
            };

            // Act
            target.Calculate();

            var expectedName = "新竹貨運";
            var expectedFee = 200.0;

            var actualName = target.GetsComapanyName();
            var actualFee = target.GetsFee();

            // Assert
            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedFee, actualFee);
        }
    }
}