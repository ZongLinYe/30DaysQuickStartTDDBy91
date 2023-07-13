using _30DaysQuickStartTDDBy91.Day5.InterfaceDesign;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _30DaysQuickStartTDDBy91.Day5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _30DaysQuickStartTDDBy91.Day5.InterfaceDesign.Tests
{
    [TestClass()]
    public class ValidationByInterfaceDesignTests
    {
        // Step1
        [TestMethod()]
        public void ValidationByInterfaceDesignTestStep1()
        {
            IAccountDao dao = null; // TODO: 初始化為適當值
            IHash hash = null; // TODO: 初始化為適當值
            var target = new ValidationByInterfaceDesign(dao, hash); // TODO: 初始化為適當值
            string id = string.Empty; // TODO: 初始化為適當值
            string password = string.Empty; // TODO: 初始化為適當值
            bool expected = false; // TODO: 初始化為適當值
            bool actual;
            actual = target.CheckAuthentication(id, password);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("驗證這個測試方法的正確性。");
        }

        // Step2 Magic
        // 為什麼這樣的設計方式，就可以幫助我們只獨立的測試 Validation 的 CheckAuthentication方法呢？

        // 接下來要用到「手動設計」的stub。

        // 大家回過頭看一下，CheckAuthentication 方法中，使用到了 IAccountDao 的 GetPassword 方法，取得 id 對應密碼。
        // 也使用到了 IHash的GetHashResult 方法，取得 hash 運算結果。接著才是比對兩者是否相同。
        // 寫單元測試的 3A pattern，單元測試程式碼如下：
        [TestMethod()]
        public void ValidationByInterfaceDesignTestStep2()
        {

            //arrange
            // 初始化 StubAccountDao，來當作 IAccountDao 的執行個體
            IAccountDao dao = new StubAccountDao();

            //初始化 StubHash，來當作 IHash 的執行個體
            IHash hash = new StubHash();

            // 將自訂的兩個 stub object，注入到目標物件中，也就是 Validation 物件
            var target = new ValidationByInterfaceDesign(dao, hash);

            // 因為不管如何 CheckAuthentication() 裡面的 GetPassword() 與 GetHashResult() 都會回傳 "91"，所以這邊隨便填
            string id = "id隨便啦";
            string password = "密碼也沒關係";    

            // 期望為 true，因為預期 hash 後的結果是 "91"，而 IAccountDao 回來的結果也是 "91"，所以為 true
            bool expected = true;

            //act
            bool actual;
            actual = target.CheckAuthentication(id, password);

            //assert
            Assert.AreEqual(expected, actual);
        }
        //如此一來，就可以讓我們的測試目標物件：Validation，不直接相依於 AccountDao 與 Hash 物件，
        //透過 stub 物件來模擬，以驗證 Validation 物件本身的 CheckAuthentication 方法邏輯，是否符合預期。

    }

    // 透過介面可進行擴充，多型與覆寫（如果是繼承父類或抽象類別，而非實作介面時）的特性，
    // 我們這邊舉 IAccountDao 為例，建立一個 StubAccountDao 的類別，來實作 IAccountDao。
    // 並且，在 GetPassword 方法中，不管傳入參數為何，都固定回傳 "91"，代表 Dao 回來的密碼。程式碼如下所示：
    internal class StubAccountDao : IAccountDao
    {
        public string GetPassword(string id)
        {
            return "91";
        }
    }
    // 接著用同樣的方式，讓 StubHash 的 GetHashResult，也回傳 "91"，代表 hash 後的結果。程式碼如下：
    internal class StubHash : IHash
    {
        public string GetHashResult(string password)
        {
            return "91";
        }
    }
}