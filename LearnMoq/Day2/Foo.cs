using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnMoq.Day2
{
    public class Foo
    {
        private ILog _log1;
        private ILog _log2;

        // Stub(存根)和Mock(模擬)
        // Stub和Mock都是測試方法依賴隔離的偽造物件，
        // 不同之處是Stub是測試方法運行所需要的依賴，
        // Mock是測試方法驗證說需要的依賴。
        // UserStory: 作為一個Foo，我希望在DoB方法中，能夠讀取Log1的內容，並寫入到Log2中。
        public Foo(ILog log1, ILog log2)
        {
            _log1 = log1;
            _log2 = log2;
        }

        public void DoB()
        {
            //do something
            var text = _log1.Read();
            _log2.Write(text);
        }
    }

    public interface ILog
    {
        void Write(string text);
        string Read();
    }

    public class Log : ILog
    {
        private readonly string _uri;

        public Log(string uri)
        {
            _uri = uri;
        }

        public void Write(string text)
        {
            using (var stream = File.AppendText(_uri))
            {
                stream.Write(text);
            }
        }

        public string Read()
        {
            if (!File.Exists(_uri))
            {
                return string.Empty;
            }

            using (var stream = File.OpenRead(_uri))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
