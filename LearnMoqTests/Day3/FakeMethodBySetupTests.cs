using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearnMoq.Day3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace LearnMoq.Day3.Tests
{
    [TestClass()]
    public class FakeMethodBySetupTests
    {
        [TestMethod()]
        public void FakeMethodBySetupStubTests()
        {
            // 我期望IFoo的GetCount方法返回值為 3 那麼就可以寫
            var fakeFoo = new Mock<IFoo>();
            fakeFoo.Setup(fake => fake.GetCount()).Returns(3);           
            // 或者我期望 DoSomethingStringy 方法傳入"fitness"是返回"slim",那麼我可以寫
            fakeFoo.Setup(fake => fake.DoSomethingStringy("fitness")).Returns("slim");
            // 例如我們有時候會期望返回值和輸入參數有關，
            // 例如 DoSomethingStringy 方法傳入"fitness"是返回"fitness makes me slim",那麼可以用如下寫法               
            fakeFoo.Setup(fake => fake.DoSomethingStringy("fitness")).Returns((string value) => value + "makes  me slim");
            // 那有同學說了，我不能只"fitness"啊，我還可以"swimming","running",那怎麼辦呢？
            // 沒問題，把這些輸入情況也偽造了
            fakeFoo.Setup(fake => fake.DoSomethingStringy("swimming")).Returns((string value) => value + "makes  me slim");
            fakeFoo.Setup(fake => fake.DoSomethingStringy("running")).Returns((string value) => value + "makes  me slim");

            // 有人覺得這麼寫太累，因為三個運動的Returns部分的內容是一樣的。
            // OK呀，我們可以使用參數匹配It.IsAny<string>()，是任意字串輸入都被偽造
            fakeFoo.Setup(fake => fake.DoSomethingStringy(It.IsAny<string>())).Returns((string value) => value + "makes  me slim");

            // 那還有人喜歡啥都不做，啥都不做肯定不會"slim",
            // 那這裡我們要返回一個ArgumentException("you must do something to make you slim")。
            // OK呀，不過這裡我們不能用Return，這裡我們引入一個新格式，拋出異常
            // setup偽造方法拋出異常的格式為偽物件.Setup(fake => fake.方法名).Throws(異常物件)
            // 或者偽物件.Setup(fake => fake.方法名).Throws < T exception >
            // 因此就可以寫成
            fakeFoo.Setup(fake => fake.DoSomethingStringy("")).Throws(new ArgumentException("you must do something to make you slim"));


            // 此外我們還常常遇到連續調用同一方法，返回值不同的情況，
            // 例如第一次"fitness"是返回"fitness makes me slim x1",
            // 第二次就會返回"fitness makes me slim x2",
            // 這時我們就需要另外一個函數CallBack，CallBack可以讓你在方法調用的時候執行一個回呼函數。
            // 對於上面的情況我們就可以寫成
            int count = 1;
            string results = "makes me slim";
            fakeFoo.Setup(fake => fake.DoSomethingStringy("fitness"))
                .Returns((string value) => $"{value} {results} x{count}")
                .Callback(() => count++);


            //OK以上就是Moq偽造方法的基本用法，總結一下
            //偽造無參數方法
            fakeFoo.Setup(fake => fake.GetCount()).Returns(3);
            //偽造指定參數方法
            fakeFoo.Setup(fake => fake.DoSomethingStringy("fitness")).Returns("slim");
            //偽造方法返回值和參數相關
            fakeFoo.Setup(fake => fake.DoSomethingStringy("fitness")).Returns((string value) => value + "makes  me slim");
            //偽造方法參數匹配
            fakeFoo.Setup(fake => fake.DoSomethingStringy(It.IsAny<string>())).Returns((string value) => value + "makes  me slim");
            //偽造方法拋出異常
            fakeFoo.Setup(fake => fake.DoSomethingStringy("")).Throws(new ArgumentException("you must do something to make you slim"));
            //偽造方法回檔
            fakeFoo.Setup(fake => fake.DoSomethingStringy("fitness"))
                .Returns((string value) => $"{value} {results} x{count}")
                .Callback(() => count++);


        }
    }
}