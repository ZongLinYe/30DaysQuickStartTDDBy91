using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _30DaysQuickStartTDDBy91.Day6.Tests
{
    [TestClass()]
    public class ValidationOverrideVirtualMethodTests
    {
        [TestMethod()]
        public void CheckAuthenticationTest()
        {
            // Magic 的時刻
            // MyValidation 繼承 ValidationOverrideVirtualMethod 之後，就可以 override GetAccountDao() 以及 GetHash()，並且回傳 Stub 物件。
            // Stub 物件也分別 override GetPassword() 以及 GetHashResult()，並且回傳預期的值。
            ValidationOverrideVirtualMethod target = new MyValidation();

            string id = "id隨便啦";
            string password = "密碼也沒關係";

            bool expected = true;

            bool actual;
            actual = target.CheckAuthentication(id, password);

            Assert.AreEqual(expected, actual);
        }
    }
    // 將上一篇文章的StubHash改繼承自Hash，StubAccountDao改繼承自AccountDao，
    // 並將原本public的方法，加上override關鍵字，覆寫其父類方法內容
    public class StubAccountDao : AccountDao
    {
        public override string GetPassword(string id)
        {
            return "91";
        }
    }

    public class StubHash : Hash
    {
        public override string GetHashResult(string password)
        {
            return "91";
        }
    }
    // 建立一個MyValidation的class，繼承自 ValidationOverrideVirtualMethod。
    // 並覆寫GetAccountDao()與GetHash()，使其回傳Stub Object。
    public class MyValidation : ValidationOverrideVirtualMethod
    {
        protected override AccountDao GetAccountDao()
        {
            return new StubAccountDao();
        }

        protected override Hash GetHash()
        {
            return new StubHash();
        }
    }
}