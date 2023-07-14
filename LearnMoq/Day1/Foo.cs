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
        // User Story: 作為一個Foo，我希望在DoA方法中，能夠寫入"Finish A"到Log中。
        // 但是 Log 類別是實體物件，無法直接驗證
        // 所以我們需要用 Mock 來模擬 Log 類別
        // 現在我們期望能夠寫一個單元測試，驗證運行DoA方法時，是否向日誌寫入了Finish A
        // 那麼問題來了，我們需要在每次運行單元測試時，要真正的讀寫檔。
        // 那麼這個單元測試能夠做到運行快，結果穩定，隔離等等要求嗎？
        // 如果我們的例子中的日誌系統換成資料庫，網路請求會怎樣呢？
        // 如果這個時候我們能夠偽造一個日誌系統，是否問題就能夠解決了呢？
        // 那麼首先我們需要引入一個介面ILog
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
            //new Log("log.txt").Write("Finish A");
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
