using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearnMoq.Day6.MockBehaviorAndCallBaseAndSetupSequenceAndProtectedAndInternal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Moq;


namespace LearnMoq.Day6.MockBehaviorAndCallBaseAndSetupSequenceAndProtectedAndInternal.Tests
{
    [TestClass()]
    public class MockBehaviorAndCallBaseAndSetupSequenceAndProtectedAndInternalTests
    {
        [TestMethod()]
        public void MockBehaviorTest()
        {
            // MockBehavior: 偽對象行為
            //在使用Moq創建偽物件時，可以在構造函數裡傳入MockBehavior
            //MockBehavior有了兩種：
            //•	Loose：預設行為，任何未顯示偽造的方法和屬性都會返回預設值，且不會拋出異常。
            //•	Strict： 任何調用都需要顯式Setup,並使用VerifyAll驗證。
            //什麼意思呢？如下圖所示，你要驗證公共方法A,A中做了T.B()和T.C()兩件事。
            //public void A()
            // {
            //      Name = T.B();
            //      Age = T.C();
            // }
            // Loose允許你測試A時只偽造方法B，並驗證Name狀態，
            //Strict要求必須同時偽造方法B和C,否則會拋出異常。
            //同樣，另一方面，如果後期方法A又調用了一個T.D()，那麼前者的測試會過，後者會失敗，提醒用戶修改測試。
            //至於選擇，我個人是沒有什麼偏好，大家自己喜歡就好。

        }
    [TestMethod()]
        public void CallBaseTest()
        {
            // CallBase: 調用基類方法
            //如果你期望某些方法調用原類型虛方法的默認實現，可以使用
            var mock = new Mock<IFoo> { CallBase = true };
            //這個在測試有一大堆虛方法的基類時十分有效，不用為了測一個方法，偽造過多其他方法。

        }
        [TestMethod()]
        public void SetupSequenceTest()
        {
            // SetupSequence: 偽造序列
            // 如果你期望，一個方法每次調用返回值都不同，那麼可以試試下面的寫法。
            var mock = new Mock<IFoo>();
            mock.SetupSequence(f => f.GetCount())
                .Returns(3)  // will be returned on 1st invocation
                .Returns(2)  // will be returned on 2nd invocation
                .Returns(1)  // will be returned on 3rd invocation
                .Returns(0)  // will be returned on 4th invocation
                .Throws(new InvalidOperationException());  // will be thrown on 5th invocation
            // 值得注意的是如果你期望，該方法被調用4次，那麼一定要在第5次（最後一句）
            // Throws(new InvalidOperationException()) 中斷測試，否則會返回Null

        }
        [TestMethod()]
        public void ProtectedTest()
        {
            // Protected():偽造Protected成員
            // 如果需要測試Protected成員的行為，你可以使用下面的方式（不過到了這一步，可能已經意味著你的代碼需要再審查一遍結構是否合理了）
            // 無參數
            var mock = new Mock<IFoo>();
            mock.Protected()
                 .Setup<int>("Execute")
                 .Returns(5);
            //帶參數
            mock.Protected()
                .Setup<string>("Execute",
                    ItExpr.IsAny<string>())
                .Returns(true);
            //值得注意的是，因為Protected成員“不可見”，因此只能使用字串進行處理

        }
        [TestMethod()]
        public void InternalTest()
        {
            // Internal程式集可見
            //有時候我們會需要測試一些Internal的類和方法，此時我們不僅需要對測試專案可見，還要對測試框架的生成器可見。
            //因此需要在AssemblyInfo.cs添加
            //[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
            //[assembly: InternalsVisibleTo("YourTestProject")]
            //________________________________________
            //ok, 以上是Moq基礎的全部知識性內容。
            //下面說說對Moq的看法。
            //Moq作為一個受限的單元測試框架，做到了免費，簡單，易用。
            //應該說能夠滿足大部分的應用需求。
            //對於一個項目來說，如果Moq能夠滿足使用需求，那這個項目一定是SOLID的
            //當然，對於一些遺留代碼來說，通常需要非受限框架對他進行支持（例如typemock isolator，MS Fakes）
            //不足之處是，Moq的概念存在一些混淆，往往容易將初學者帶偏，一些API設計也有待商榷。
            //但是，Moq只是我們書寫單元測試的工具，他真正的威力在於使用者。
            //兵無常勢，水無常形。框架總會更新，創建測試的能力才是需要保留的。

        }

    }
}