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
    public class BarTests
    {
        [TestMethod()]
        public void SubmitTest()
        {
            // 我期望IFoo的GetCount方法返回值為 3 那麼就可以寫
            var fakeFoo = new Mock<IFoo>();
            fakeFoo.Setup(fake=>fake.GetCount()).Returns(3);
            // 或者我期望 DoSomethingStringy 方法傳入"fitness"是返回"slim",那麼我可以寫
            fakeFoo.Setup(fake => fake.DoSomethingStringy("fitness")).Returns("slim");
            // 例如我們有時候會期望返回值和輸入參數有關，例如 DoSomethingStringy 方法傳入"fitness"是返回"fitness makes me slim",那麼可以用如下寫法
            fakeFoo.Setup(fake => fake.DoSomethingStringy(It.Is<string>(s => s.Contains("fitness")))).Returns("fitness makes me slim");
            fakeFoo.Setup(fake => fake.DoSomethingStringy("fitness")).Returns((string value) => value + "makes  me slim");
        }
    }
}