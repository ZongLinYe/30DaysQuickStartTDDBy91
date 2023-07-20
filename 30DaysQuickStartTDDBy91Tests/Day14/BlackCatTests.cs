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
    public class BlackCatTests
    {
        [TestMethod()]
        public void GetsCompanyNameTest()
        {
            BlackCat blackCat = new BlackCat();
            string expected = "黑貓";
            string actual = blackCat.GetsCompanyName();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetsFeeTest()
        {
            BlackCat blackCat = new BlackCat();
            double expected = 0.0;
            double actual = blackCat.GetsFee();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CalculateTest()
        {
            // 從整合測試的 Test case，來當作單元測試的 test case
            // Arrange
            BlackCat target = new BlackCat() 
            { 
                ShipProduct = new Product()
                {
                    IsNeedCool=true,
                    Name = "筆電", 
                    Size = new Size
                    {
                        Height = 10,
                        Length=30,
                        Width=20
                    },
                    Weight = 10
                }            
            };

            // Act
            target.Calculate();

            var expectedName = "黑貓";
            var expectedFee = 200.0;

            var actualName = target.GetsCompanyName();
            var actualFee = target.GetsFee();

            // Assert
            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedFee, actualFee);

        }

    }

   
}