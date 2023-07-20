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
    public class PostOfficeTests
    {
        [TestMethod()]
        public void GetsComapanyNameTest()
        {
            var target = new PostOffice();
            var expected = "郵局";
            var actual = target.GetsCompanyName();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetsFeeTest()
        {
            var target = new PostOffice();
            var expected = 0.0;
            var actual = target.GetsFee();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CalculateTest()
        {
            // arrange
            var target = new PostOffice()
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
            // act
            target.Calculate();

            var expectedName = "郵局";
            var expectedFee = 200.0;
            var actualName = target.GetsCompanyName();
            var actualFee = target.GetsFee();
            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedFee, actualFee);
        }
    }
}