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
            // 然後再 Assert.AreEqual(expected, foo.Name); 這樣比較
            fakeFoo.SetupSet(fake => fake.Name = "靜悄悄");

            // or verify the setter directly
            // 或者直接驗證setter
            //使用VerifySet驗證
            fakeFoo.VerifySet(fake => fake.Name = "靜悄悄");
            fakeFoo.VerifyGet(fake => fake.Name);
            fakeFoo.VerifyAll();

            Assert.AreEqual(fakeFoo, fakeFoo.Object.Name);

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
    }
}