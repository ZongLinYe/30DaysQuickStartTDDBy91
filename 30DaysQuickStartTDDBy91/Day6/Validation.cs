using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30DaysQuickStartTDDBy91.Day6
{ 
    // 建構式注入 Constructor Injection
    // 可以使用 AutoFac 等 DI 套件，自動注入，不用手動 new 物件，
    // 甚至 ASP.NET Core 有內建的 DI 注入功能
    // 缺點?
    // 當物件越來越複雜時，建構式也會趨於複雜。倘若沒有DI framework的輔助，則使用物件上，面對許多overload的建構式，
    // 或是一個建構式參數有好幾個，會造成使用目標物件上的困難與疑惑。若沒有好好進行refactoring，也可能因此而埋藏許多 bad smell。
    public class ValidationConstructor
    {
        private IAccountDao _accountDao;
        private IHash _hash;

        public ValidationConstructor(IAccountDao dao, IHash hash)
        {
            this._accountDao = dao;
            this._hash = hash;
        }

        public bool CheckAuthentication(string id, string password)
        {
            var passwordByDao = this._accountDao.GetPassword(id);
            var hashResult = this._hash.GetHashResult(password);

            return passwordByDao == hashResult;
        }

    }
    public interface IAccountDao
    {
        string GetPassword(string id);
    }

    public interface IHash
    {
        string GetHashResult(string password);
    }

    public class AccountDaoByInterface : IAccountDao
    {
        public virtual string GetPassword(string id)
        {
            throw new NotImplementedException();
        }
    }

    public class HashByInterface : IHash
    {
        public virtual string GetHashResult(string password)
        {
            throw new NotImplementedException();
        }
    }

    // 公開屬性 （public setter property）
    public class ValidationPublicSetterProperty
    {
        public IAccountDao AccountDao { private get; set; }

        public IHash Hash { private get; set; }

        public bool CheckAuthentication(string id, string password)
        {
            if (this.AccountDao == null)
            {
                throw new ArgumentNullException();
            }

            if (this.Hash == null)
            {
                throw new ArgumentNullException();
            }

            var passwordByDao = this.AccountDao.GetPassword(id);
            var hashResult = this.Hash.GetHashResult(password);

            return passwordByDao == hashResult;
        }
    }

    // 呼叫方法時傳入參數
    public class ValidationPassParametersWhenCallingMethod
    {
        public bool CheckAuthentication(
            IAccountDao accountDao, 
            IHash hash, 
            string id, 
            string password)
        {
            var passwordByDao = accountDao.GetPassword(id);
            var hashResult = hash.GetHashResult(password);

            return passwordByDao == hashResult;
        }
    }

    // 可覆寫的保護方法  
    public class ValidationOverrideVirtualMethod
    { 
        public bool CheckAuthentication(string id, string password)
        {
            // 接下來，我們只用簡單的物件導向概念：繼承、覆寫，就可以對Validation物件的CheckAuthentication方法進行測試。
            // 首先，一定要記得，把new物件的動作抽離高層抽象的context中。（可以透過extract method的方式抽離）
            var accountDao = GetAccountDao();
            var passwordByDao = accountDao.GetPassword(id);

            var hash = GetHash();
            var hashResult = hash.GetHashResult(password);

            return passwordByDao == hashResult;
        }
        // 這裡的 virtual 關鍵字，是為了讓子類別可以覆寫這個方法
        // 這裡的 protected 關鍵字，是為了讓子類別可以存取這個方法
        protected virtual Hash GetHash()
        {
            var hash = new Hash();
            return hash;
        }
        protected virtual AccountDao GetAccountDao()
        {
            var accountDao = new AccountDao();
            return accountDao;
        }


    }

    // 另外，將要使用到Hash與AccountDao的方法，也要宣告為virtual。程式碼如下：
    public class AccountDao
    {
        public virtual string GetPassword(string id)
        {
            throw new NotImplementedException();
        }
    }

    public class Hash
    {
        public virtual string GetHashResult(string password)
        {
            throw new NotImplementedException();
        }
    }
    
}
