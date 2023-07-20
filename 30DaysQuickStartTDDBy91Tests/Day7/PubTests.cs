using Microsoft.VisualStudio.TestTools.UnitTesting;
using _30DaysQuickStartTDDBy91.Day7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
//using Microsoft.VisualStudio.CodeCoverage;


namespace _30DaysQuickStartTDDBy91.Day7.Tests
{
    // Stub
    [TestClass()]
    public class PubTests
    {
        [TestMethod()]
        public void Test_Charge_Customer_Count()
        {
            // arrange
            var stubCheckInFee = new Mock<ICheckInFee>();
            stubCheckInFee.Setup(x => x.GetFee(It.IsAny<Customer>())).Returns(100);
            //var target = new Pub(stubCheckInFee.Object);

            DateTime fixedDateTime = DateTime.Now;
            var target = new Pub(stubCheckInFee.Object,() => fixedDateTime);
            // User story 是男生才會收費
            var customers = new List<Customer>()
            {
                new Customer(){IsMale=true},
                new Customer(){IsMale=true},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
            };
            // 期望收費人數，上面的 customers 有 5 人，但是只有 2 人是男生，所以期望收費人數是 2 人
            var expected = 2;

            // act
            // 執行收費
            var actual = target.CheckIn(customers);

            // assert
            Assert.AreEqual(expected, actual);
        }

        // Stub
        [TestMethod()]
        public void Test_Income()
        {
            // arrange
            var stubCheckInFee = new Mock<ICheckInFee>();
            stubCheckInFee.Setup(x => x.GetFee(It.IsAny<Customer>())).Returns(100);
            //var target = new Pub(stubCheckInFee.Object);

            DateTime fixedDateTime = DateTime.Now;
            var target = new Pub(stubCheckInFee.Object, () => fixedDateTime);
            // User story 是男生才會收費
            var customers = new List<Customer>()
            {
                new Customer(){IsMale=true},
                new Customer(){IsMale=true},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
            };

            var inComeBeforeCheckIn = target.GetInCome();
            Assert.AreEqual(0, inComeBeforeCheckIn);

            // 期望收費總金額，上面的 customers 有 5 人，但是只有 2 人是男生，所以期望收費金額是 200 元
            var expected = 200m;
            var expectedCount = 2;

            // act
            // 執行收費
            var chargeCustomerCount = target.CheckIn(customers);
            var actual = target.GetInCome();

            // assert
            Assert.AreEqual(expectedCount, chargeCustomerCount);
            Assert.AreEqual(expected, actual);
        }

        // Stub
        [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method)]
        [DataTestMethod()]
        public void Test_Charge_Customer_Count_With_DynamicData(List<Customer> customers, int expected, decimal expectedTotalMoney)
        {
            // arrange
            var stubCheckInFee = new Mock<ICheckInFee>();
            stubCheckInFee.Setup(x => x.GetFee(It.IsAny<Customer>())).Returns(100);
            //var target = new Pub(stubCheckInFee.Object);

            DateTime fixedDateTime = DateTime.Now;
            var target = new Pub(stubCheckInFee.Object, () => fixedDateTime);

            var inComeBeforeCheckIn = target.GetInCome();
            Assert.AreEqual(0, inComeBeforeCheckIn);

            // act
            // 執行收費
            var actual = target.CheckIn(customers);
            var actualIncome = target.GetInCome();

            // assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedTotalMoney, actualIncome);
        }

        private static IEnumerable<object[]> GetTestData()
        {
            var customers1 = new List<Customer>()
            {
                new Customer(){IsMale=true},
                new Customer(){IsMale=true},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
            };

            var customers2 = new List<Customer>()
            {
                new Customer(){IsMale=true},
                new Customer(){IsMale=true},
                new Customer(){IsMale=true},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
            };

            var customers3 = new List<Customer>()
            {
                new Customer(){IsMale=true},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
            };

            var customers4 = new List<Customer>()
            {
                new Customer(){IsMale=true},
                new Customer(){IsMale=true},
            };

            var customers5 = new List<Customer>()
            {
                new Customer(){IsMale=true},
                new Customer(){IsMale=true},
                new Customer(){IsMale=true},
                new Customer(){IsMale=true},
                new Customer(){IsMale=true},
                new Customer(){IsMale=true},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
                new Customer(){IsMale=true},
            };

            yield return new object[] { customers1, 2, 200m };
            yield return new object[] { customers2, 3, 300m };
            yield return new object[] { customers3, 1, 100m };
            yield return new object[] { customers4, 2, 200m };
            yield return new object[] { customers5, 7, 700m };

        }

        // Mock
        [TestMethod()]
        public void Test_CheckIn_Charge_Only_Male()
        {
            // 想要驗證在 2男3女的測試案例中，是否呼叫 ICheckInFee 介面的 GetFee() 兩次
            var mockCheckInFee = new Mock<ICheckInFee>();
            //var target = new Pub(mockCheckInFee.Object);
            
            DateTime fixedDateTime = DateTime.Now;
            var target = new Pub(mockCheckInFee.Object,() => fixedDateTime);
            var customers = new List<Customer>()
            {
                new Customer(){IsMale=true},
                new Customer(){IsMale=true},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
            };
            // act
            target.CheckIn(customers);
            // assert
            // 驗證 GetFee 是否有被呼叫兩次
            mockCheckInFee.Verify(x => x.GetFee(It.IsAny<Customer>()), Times.Exactly(2));
        }

     

        // 以這例子來說，假設CheckIn的需求改變，從原本的「女生免費入場」變成「只有當天為星期五，女生才免費入場」，修改生產程式碼
        [TestMethod()]
        public void CheckInOnlyFridayWomanFree_Should_Charge_Male_And_Female_Customers_On_Friday_Test()
        {
            // arrange
            var mockCheckInFee = new Mock<ICheckInFee>();
            //var pub = new Pub(mockCheckInFee.Object);

            DateTime fixedDateTime = new DateTime(2023, 7, 21);
            var target = new Pub(mockCheckInFee.Object, () => fixedDateTime);

            var customers = new List<Customer>()
            {
                new Customer(){IsMale=true},
                new Customer(){IsMale=true},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
            };

            // Act
            var result = target.CheckInOnlyFridayWomanFree(customers);        

            // Assert
            mockCheckInFee.Verify(x => x.GetFee(It.IsAny<Customer>()), Times.Exactly(2));

            Assert.AreEqual(2, result);
        }

  
        // 以這例子來說，假設CheckIn的需求改變，從原本的「女生免費入場」變成「只有當天為星期五，女生才免費入場」，修改生產程式碼
        [TestMethod()]
        public void CheckInOnlyFridayWomanFree_Should_Charge_Male_Customers_On_Non_Friday_Test()
        {
            // arrange
            var mockCheckInFee = new Mock<ICheckInFee>();
            //var pub = new Pub(mockCheckInFee.Object);

            DateTime fixedDateTime = new DateTime(2023, 7, 22);
            var target = new Pub(mockCheckInFee.Object, () => fixedDateTime);

            var customers = new List<Customer>()
            {
                new Customer(){IsMale=true},
                new Customer(){IsMale=true},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
                new Customer(){IsMale=false},
            };

            // Act
            var result = target.CheckInOnlyFridayWomanFree(customers);

            // Assert
            mockCheckInFee.Verify(x => x.GetFee(It.IsAny<Customer>()), Times.Exactly(5));

            Assert.AreEqual(5, result);
        }


    }

    public static class FakeDateTime
    {
        public static Func<DateTime> Now { get; set; } = () => DateTime.Now;
    }
}