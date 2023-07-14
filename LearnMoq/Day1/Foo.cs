using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://xinyuehtx.github.io/post/Moq%E5%9F%BA%E7%A1%80-%E4%B8%80.html
namespace LearnMoq.Day1
{
    // Foo 類別依賴 Log 類別
    public class Foo
    {
        // Step1. 建立 ILog 介面
        //private Log _log;
        private ILog _log;

        // Step1. 建立 ILog 介面
        //public Foo(Log log)
        public Foo(ILog log)

        {
            _log = log;
        }

        // 現在要用 Unit Test 驗證 DoA()，是否向 Log 寫入 "Finish A"
        // 但是 Log 類別是實體物件，無法直接驗證
        // 所以我們需要用 Mock 來模擬 Log 類別
        // Step1. 建立 ILog 介面
        // 透過 Interface 來隔離測試環境的 Log 類別 & 實際執行環境的 Log 類別
        public void DoA()
        {
            //do something
            _log.Write("Finish A");
        }
    }
    // Step1. 建立 ILog 介面
    public interface ILog
    {
        void Write(string text);
        string Read();
    }
    // Step1. 建立 ILog 介面，實作 ILog
    public class Log: ILog
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
