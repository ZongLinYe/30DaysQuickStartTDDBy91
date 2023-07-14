using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace _30DaysQuickStartTDDBy91.Day5.Tests
{
    [TestClass()]
    public class ValidationTests
    {
        [TestMethod()]
        public void CheckAuthenticationTest()
        {
            Validation target = new Validation(); // TODO: 初始化為適當值
            string id = string.Empty; // TODO: 初始化為適當值
            string password = string.Empty; // TODO: 初始化為適當值
            bool expected = false; // TODO: 初始化為適當值
            bool actual;
            //target.CheckAuthenticationCheckAuthentication() 會直接與 class AccountDao 以及 class Hash 相依，所以無法單元測試。
            actual = target.CheckAuthentication(id, password);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("驗證這個測試方法的正確性。");
        }
    }
}