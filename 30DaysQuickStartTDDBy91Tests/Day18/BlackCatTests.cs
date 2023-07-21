using Microsoft.VisualStudio.TestTools.UnitTesting;
using _30DaysQuickStartTDDBy91.Day18;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30DaysQuickStartTDDBy91.Day18.Tests
{   
    [TestClass()]
    public class FactoryPattern_Step9
    {
        /// <summary>
        ///  GetILogistics 的測試
        /// </summary>
        [TestMethod()]
        public void GetILogisticsTests_GetBlackCat()
        {
            // arrange
            string company = "1";
            Product product = new Product();
            ILogistics expected = new BlackCat();

            // act
            ILogistics actual = FactoryRepository.GetILogistics(company, product);

            // assert
            Assert.AreEqual(expected.GetType(),actual.GetType());
        }

        [TestMethod()]
        public void GetILogisticsTests_GetHsinchu()
        {
            // arrange 
            string company = "2";
            Product product = new Product();
            ILogistics expected = new Hsinchu();

            // act
            ILogistics actual = FactoryRepository.GetILogistics(company, product);

            // assert
            Assert.AreEqual(expected.GetType(), actual.GetType());
        }

        [TestMethod()]
        public void GetILogisticsTests_GetPostOffice()
        {
            // arrange
            string company = "3";
            Product product = new Product();
            ILogistics expected = new PostOffice();

            // act
            ILogistics actual = FactoryRepository.GetILogistics(company, product);

            // assert
            Assert.AreEqual(expected.GetType(), actual.GetType());
        }
    }
}