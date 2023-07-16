using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearnMoq.Day4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Collections;

namespace LearnMoq.Day4.Tests
{
    [TestClass()]
    public class FakePropertyBySetupTests
    {
        [TestMethod()]
        public void FakePropertyBySetupStubTests()
        {
            // 上一章講了如何使用Setup偽造方法
            // 這一章我們將偽造屬性和事件
            //________________________________________
            // Setup:偽造屬性
            // 上一章我們說過setup系列不僅可以偽造方法，也可以偽造屬性
            // （畢竟屬性本質也是方法，┓( ´∀` )┏）。這裡依然用上一次的moq官方文檔中的介面為例 (產品程式碼)

            // 這次不同的是，我們偽造的東西變成了Name方法。
            // 記得小時候老師教我們做了好事，別人問你叫什麼，你就要說“我叫紅領巾”。
            // 所以期望IFoo的Name方法返回值是”紅領巾”.那麼就可以寫
            // 我只記得台灣的老師很喜歡教"最高品質"，要回"靜悄悄"  <(￣ c￣)y▂ξ 
            var fakeFoo = new Mock<IFoo>();
            fakeFoo.Setup(fake => fake.Name).Returns("靜悄悄");

            // 是不是很簡單，但是，重點來了，我們還能在API中看到有一個SetupSet這個方法。
            // 此時，有了經驗的童鞋們會說，這不是很簡單麼，偽造屬性的Set方法。
            // 大錯特錯
            // 思考下，我們如果偽造了一個屬性的Set方法後，能夠幹什麼呢？只能是驗證這個偽物件的屬性是否被賦值了。
            // 此時，我們的偽物件作用發生了變化，由Stub變成了Mock ,
            // 因此，這個命名是非常失敗的命名，正確的叫法應該是VerifySet ,當然Moq也有VerifySet ，他們做的事情也“幾乎”一樣。

            // expects an invocation to set the value to "foo"
            // 期望調用將值設置為“foo” ???
            // 看起來跟 var expected = "foo"; 有點像
            // 但是這裡的foo是一個屬性，而不是一個變數
            // 然後再 Assert.AreEqual(expected, foo.Name); 這樣比較嗎? 不懂
            // Git Copilot 曰：我們可以偽造一個屬性，然後讓他自動跟蹤他的值，也就是說，我們可以在偽物件上設置一個屬性，然後這個屬性的值會被記錄下來。
            // 這個功能在測試中非常有用，因為我們可以在測試中設置一個屬性，然後在測試結束後，驗證這個屬性的值是否符合我們的預期。
            fakeFoo.SetupSet(fake => fake.Name = "靜悄悄");

            // or verify the setter directly
            // 或者直接驗證setter
            //使用VerifySet驗證
            fakeFoo.VerifySet(fake => fake.Name = "靜悄悄");
            //fakeFoo.VerifyGet(fake => fake.Name);
            fakeFoo.VerifyAll();


            // 繼續往前推進
            // 遞迴偽造
            // 舉例說明，你現在期望偽造IFoo介面的屬性Bar的子屬性Baz的Name
            // 這時候，你可以這樣做
            // 這裡的Baz是一個IFoo類型的屬性，所以我們可以繼續偽造他的子屬性Name
            // 這裡的Name是一個string類型的屬性，所以我們可以偽造他的返回值
            // 這裡的返回值是一個string類型的屬性，所以我們可以偽造他的返回值
            




            //Assert.AreEqual("靜悄悄", fakeFoo.Object.Name);

            //var mock = new Mock<IFoo>();

            //mock.Setup(foo => foo.Name).Returns("bar");

            //// auto-mocking hierarchies (a.k.a. recursive mocks)
            //mock.Setup(foo => foo.Bar.Baz.Name).Returns("baz");

            //// expects an invocation to set the value to "foo"
            //mock.SetupSet(foo => foo.Name = "foo");

            //// or verify the setter directly
            //mock.VerifySet(foo => foo.Name = "foo");

            //// Setup a property so that it will automatically start tracking its value(also known as Stub):
            //// start "tracking" sets/gets to this property
            //mock.SetupProperty(f => f.Name);

            //// alternatively, provide a default value for the stubbed property
            //mock.SetupProperty(f => f.Name, "foo");


            //// Now you can do:
            //IFoo foo = mock.Object;
            //// Initial value was stored
            //Assert.AreEqual("foo", foo.Name);

            //// New value set which changes the initial value
            //foo.Name = "bar";
            //Assert.AreEqual("bar", foo.Name);
            //// Stub all properties on a mock:
            //mock.SetupAllProperties();


        }

        [TestMethod()]
        public void FakePropertyByRecursiveMocks()
        {
            // 遞迴偽造
            // 舉例說明，你現在期望偽造IFoo介面的屬性Bar的子屬性Baz的Name
            //偽造對象
            var fakeFoo = new Mock<IFoo>();
            var fakeBar = new Mock<Bar>();
            var fakeBaz = new Mock<Baz>();
            //組裝
            fakeBaz.Setup(fake => fake.Name).Returns("靜悄悄");
            fakeBar.Setup(fake => fake.Baz).Returns(fakeBaz.Object);
            fakeFoo.Setup(fake => fake.Bar).Returns(fakeBar.Object);


            // auto-mocking hierarchies (a.k.a. recursive mocks)
            //mock.Setup(foo => foo.Bar.Baz.Name).Returns("baz");
            // 可累了是吧，沒事有更加簡單的寫法
            var fakeFoo2 = new Mock<IFoo>();
            fakeFoo2.Setup(fake => fake.Bar.Baz.Name).Returns("靜悄悄");
            //  一步到位。遞迴偽造會將調用路徑上的所有物件自動偽造。
            //因此，這也是區別普通框架和好框架的標準之一。
            //當然，我們有時候也僅希望偽造一個屬性實現，使這個偽造物件可用，
            //那麼就可以使用SetupProperty添加自動實現
            fakeFoo2.SetupProperty(fake => fake.Name);
           // 當然也可以設置初始值
            fakeFoo2.SetupProperty(fake => fake.Name,"靜悄悄");



            // 好總結下偽造屬性的方法。
            // 偽造屬性返回值
            fakeFoo.Setup(fake => fake.Name).Returns("靜悄悄");
            // 遞迴偽造
            fakeFoo.Setup(fake => fake.Bar.Baz.Name).Returns("靜悄悄");
            // 自動屬性實現
            fakeFoo.SetupProperty(fake => fake.Name, "靜悄悄");
            // 當然還有要重點區分的
            // 使用VerifySet驗證
            fakeFoo.VerifySet(fake => fake.Name = "靜悄悄");
            // 使用SetupSet驗證
            fakeFoo.SetupSet(fake => fake.Name = "靜悄悄");
            fakeFoo.VerifyAll();


            //// Raise: 偽造事件
            //// 事件也是一種常見的依賴，我們常常需要驗證在發生某些事件時，被測物件能否順利回應。
            //// 這裡的行為偏向於Act,而之前的那些屬於Arrange
            //// 至於Arrange - Act - Assert的三A結構，可以參考其他的單元測試書籍。
            //fakeFoo.Object.MyEvent += OnMyEvent;
            //fakeFoo.Raise(fack => fack.MyEvent += null, new EventArgs());
            //// 很簡單，第一個參數請保持為null，因為這個事件永遠不會觸發，應該監聽的是fakeFoo.Object.MyEvent
            //// 另外Moq也支持自訂的EventHandler類，
            //// 而針對泛型版本的EventHandler < T >，格式會稍稍不同，需要添加sender
            //fakeFoo.Raise(fack => fack.MyEvent += null, fack, new EventArgs());

            //// Setting up an event's `add` and `remove` accessors (requires Moq 4.13 or later):
            //fakeFoo.SetupAdd(m => m.FooEvent += It.IsAny<EventHandler>())...;
            //fakeFoo.SetupRemove(m => m.FooEvent -= It.IsAny<EventHandler>())...;

            //// Raising an event on the mock
            //fakeFoo.Raise(m => m.FooEvent += null, new FooEventArgs(fooValue));

            //// Raising an event on the mock that has sender in handler parameters
            //fakeFoo.Raise(m => m.FooEvent += null, this, new FooEventArgs(fooValue));

            //// Raising an event on a descendant down the hierarchy
            //fakeFoo.Raise(m => m.Child.First.FooEvent += null, new FooEventArgs(fooValue));

            //// Causing an event to raise automatically when Submit is invoked
            //fakeFoo.Setup(foo => foo.Submit()).Raises(f => f.Sent += null, EventArgs.Empty);
            //// The raised event would trigger behavior on the object under test, which 
            //// you would make assertions about later (how its state changed as a consequence, typically)

       
            //var mock = new Mock<IFoo>();

            //// Raise passing the custom arguments expected by the event delegate
            //mock.Raise(foo => foo.MyEvent += null, 25, true);


        }

        //// Raising a custom event which does not adhere to the EventHandler pattern
        //public delegate void MyEventHandler(int i, bool b);


        private void OnMyEvent(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    //public interface IFoo
    //{
    //    event MyEventHandler MyEvent;
    //}

}