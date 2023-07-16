using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearnMoq.Day5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnMoq.Day1.Tests;
using Moq;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;

namespace LearnMoq.Day5.Tests
{
    [TestClass()]
    public class ItAndCallBackAndVerificationTests
    {
        [TestMethod()]
        public void ItTest()
        {
            var fakeFoo = new Mock<IFoo>();
            // It: 參數匹配
            // It 這個類其實我們已經在前面有所涉及。我們使用It.IsAny<string>()匹配任意字串。
            fakeFoo.Setup(fake => fake.DoSomething(It.IsAny<string>())).Returns((string value) => value + "makes  me slim");
            // 其實It 的功能非常強大，而且簡單易用。直接看API名稱就能瞭解。
            // 這裡就簡單列出，不載贅述。
            //•	It.IsAny < T >，匹配指定類型參數
            //•	It.IsNotNull < T >，匹配指定類型參數，Null除外
            //•	It.Is<T>(Predicate)，匹配指定類型參數，滿足Predicate的條件
            //•	It.IsInRange<T>(T from, T to, Range rangeKind)，匹配指定類型參數，滿足一定的from到to的範圍。
            // 其中Range.Inclusive代表參數在[from, to]之內滿足
            // 其中Range.Exclusive代表參數在(from, to)之內滿足
            //•	It.IsIn<T>(IEnumerable < T > items)，匹配指定類型參數，在序列內
            //•	It.IsIn<T> (params T[] items)，匹配指定類型參數，在序列內
            //•	It.IsNotIn<T>(IEnumerable < T > items)，匹配指定類型參數，在序列外
            //•	It.IsNotIn<T> (params T[] items)，匹配指定類型參數，在序列外
            //•	It.IsRegex(string regex)，字串正則匹配
            //•	It.IsRegex(string regex, RegexOptions options)，字串正則匹配


            // Moq 官方範例
            var mock = new Mock<IFoo>();

            // Matching Arguments
            // any value
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>())).Returns(true);


            // any value passed in a `ref` parameter (requires Moq 4.8 or later):
            mock.Setup(foo => foo.Submit(ref It.Ref<Bar>.IsAny)).Returns(true);


            // matching Func<int>, lazy evaluated
            mock.Setup(foo => foo.Add(It.Is<int>(i => i % 2 == 0))).Returns(true);


            // matching ranges
            mock.Setup(foo => foo.Add(It.IsInRange<int>(0, 10, Moq.Range.Inclusive))).Returns(true);


            // matching regex
            mock.Setup(x => x.DoSomethingStringy(It.IsRegex("[a-d]+", RegexOptions.IgnoreCase))).Returns("foo");
        }
        [TestMethod()]
        public void CallbackTest()
        {
            var fakeFoo = new Mock<IFoo>();
            var mock = new Mock<IFoo>();
            var call = 0;
            var calls = 0;
            var callArgs = new List<string>();

            // CallBack: 回檔
            // CallBack是指在執行一個Setup的偽造方法時，執行一個回呼函數
            // 他的無參數結構是
            // 無參數
            fakeFoo.Setup(fake => fake.DoSomething(It.IsAny<string>()))
                .Returns(true)
                .Callback(() => call++);
                        //他的帶參數結構有兩種
            //普通參數格式
            fakeFoo.Setup(fake => fake.DoSomething(It.IsAny<string>()))
                .Returns(true)
                .Callback((string s) => callArgs.Add(s));
                        //泛型參數格式
                        fakeFoo.Setup(fake => fake.DoSomething(It.IsAny<string>()))
                            .Returns(true)
                            .Callback<string>(s => callArgs.Add(s));
                        //當然你還可以選擇回檔時機
            //回檔時機
            fakeFoo.Setup(fake => fake.DoSomething(It.IsAny<string>()))
                .Callback(() => Console.WriteLine("Before returns"))
                .Returns(true)
                .Callback(() => Console.WriteLine("After returns"));


            // Moq 官方範例
            mock.Setup(foo => foo.DoSomething("ping"))
                .Callback(() => calls++)
                .Returns(true);

            // access invocation arguments
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
                .Callback((string s) => callArgs.Add(s))
                .Returns(true);

            // alternate equivalent generic method syntax
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
                .Callback<string>(s => callArgs.Add(s))
                .Returns(true);

            // access arguments for methods with multiple parameters
            mock.Setup(foo => foo.DoSomething(It.IsAny<int>(), It.IsAny<string>()))
                .Callback<int, string>((i, s) => callArgs.Add(s))
                .Returns(true);

            // callbacks can be specified before and after invocation
            mock.Setup(foo => foo.DoSomething("ping"))
                .Callback(() => Console.WriteLine("Before returns"))
                .Returns(true)
                .Callback(() => Console.WriteLine("After returns"));

     

            mock.Setup(foo => foo.Submit(ref It.Ref<Bar>.IsAny))
                .Callback(new SubmitCallback((ref Bar bar) => Console.WriteLine("Submitting a Bar!")));

            // returning different values on each invocation
            //var mock = new Mock<IFoo>();
            //var calls = 0;
            mock.Setup(foo => foo.GetCount())
                .Callback(() => calls++)
                .Returns(() => calls);
            // returns 0 on first invocation, 1 on the next, and so on
            Console.WriteLine(mock.Object.GetCount());

            //// access invocation arguments and set to mock setup property
            //mock.SetupProperty(foo => foo.Bar);
            //mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
            //    .Callback((string s) => mock.Object.Bar = s)
            //    .Returns(true);
        }

        // callbacks for methods with `ref` / `out` parameters are possible but require some work (and Moq 4.8 or later):
        delegate void SubmitCallback(ref Bar bar);

        [TestMethod()]
        public void VerificationTest()
        {
            var mock = new Mock<IFoo>();
            // Verification：驗證
            // 驗證是Assert環節行為，此時的偽對象作用是Mock
            // 這裡也只列出基本功能
            //•	Verify(expression)，驗證運算式是否被執行
            //•	Verify(expression, times)，驗證運算式的執行次數
            // 其中Times結構體構造可以參見官方文檔
            //•	VerifyGet<T>(expression)，驗證屬性Get是否被執行
            //•	VerifyGet<T>(expression, times)，驗證屬性Get的執行次數
            //•	VerifySet<T>(expression)，驗證屬性Set是否被執行
            //•	VerifySet<T>(expression, times)，驗證屬性Set的執行次數
            //•	VerifyNoOtherCalls()，處理已經驗證的調用外，fake物件沒有其他調用

            // Moq 官方範例
            mock.Verify(foo => foo.DoSomething("ping"));

            // Verify with custom error message for failure
            mock.Verify(foo => foo.DoSomething("ping"), "When doing operation X, the service should be pinged always");

            // Method should never be called
            mock.Verify(foo => foo.DoSomething("ping"), Times.Never());

            // Called at least once
            mock.Verify(foo => foo.DoSomething("ping"), Times.AtLeastOnce());

            // Verify getter invocation, regardless of value.
            mock.VerifyGet(foo => foo.Name);

            // Verify setter invocation, regardless of value.
            mock.VerifySet(foo => foo.Name);

            // Verify setter called with specific value
            mock.VerifySet(foo => foo.Name = "foo");

            // Verify setter with an argument matcher
            mock.VerifySet(foo => foo.Value = It.IsInRange(1, 5, Moq.Range.Inclusive));

            // Verify event accessors (requires Moq 4.13 or later):
            mock.VerifyAdd(foo => foo.FooEvent += It.IsAny<EventHandler>());
            mock.VerifyRemove(foo => foo.FooEvent -= It.IsAny<EventHandler>());

            // Verify that no other invocations were made other than those already verified (requires Moq 4.8 or later)
            mock.VerifyNoOtherCalls();
        }
    }
}