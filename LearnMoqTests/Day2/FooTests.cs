using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace LearnMoq.Day2.Tests
{
    [TestClass()]
    public class FooTests
    {
        [TestMethod()]
        public void DoBTest()
        {
            // 請注意這裡的Mock和Moq框架中的Mock<T>不是一個概念！！
            // 這裡的Mock是一個自定義的類，用於模擬ILog的行為。
            // 而Moq框架中的Mock<T>是一個泛型類，用於模擬任意類型的行為。
           
            var fakeLog1 = new Mock<ILog>();
            var fakeLog2 = new Mock<ILog>();
            // Stub 與 Mock 在 Moq 的差異
            // Stub: 用來設定當呼叫某個方法時，要回傳什麼值。
            // Mock: 用來驗證某個方法是否有被呼叫。
            // Stub 對應方法的是 Setup, Mock 對應的方法是 Verify。
            fakeLog1.Setup(log => log.Read()).Returns("I'm slim");
            var foo = new Foo(fakeLog1.Object, fakeLog2.Object);
            foo.DoB();
            fakeLog2.Verify(log => log.Write("I'm slim"));

            // 解釋一下fakeLog1.Setup(log => log.Read()).Returns("I'm slim");
            // 是設置fakeLog1在調用Read方法時，一定會返回"I'm slim",
            // 這樣就可以保證在DoB方法中，讀取到的內容一定是"I'm slim"。
            // 而 fakeLog2.Verify(log => log.Write("I'm slim"));
            // 則是驗證fakeLog2是否被調用了Write方法，並且傳入的參數是"I'm slim"
            // 這裡的fakeLog1是Stub，而fakeLog2是Mock。       

        }
    }
}