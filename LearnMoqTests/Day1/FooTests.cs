using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearnMoq.Day1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace LearnMoq.Day1.Tests
{
    [TestClass()]
    public class FooTests
    {
        [TestMethod()]
        public void DoATest()
        {
            // 傳統做法，手動建立 FakeLog物件，並傳入 Foo 的建構子。
            var fakeLog = new FakeLog();
            var foo = new Foo(fakeLog);
            foo.DoA();
            Assert.AreEqual("Finish A", fakeLog.Log);
        }

        [TestMethod()]
        public void DoATestMoq()
        {
            //使用Moq的自動創建代碼來替換我們手動創建的FakeLog。
            var fakeLog = new Moq.Mock<ILog>();
            var foo = new Foo(fakeLog.Object);
            foo.DoA();
            fakeLog.Verify(x => x.Write("Finish A"));
        }


    }
    // 我們要測試的只有日誌寫入，因此Read方法不需要實現，
    // 另外我們還需要一個簡單的方式能夠把寫入的內容暴露出來。
    // 所以這裡還定義了一個屬性Log用於展示寫入的日誌內容。
    public class FakeLog : ILog
    {
        public string Log { get; private set; }

        public void Write(string text)
        {
            Log = text;
        }

        public string Read()
        {
            throw new NotImplementedException();
        }
    }
}