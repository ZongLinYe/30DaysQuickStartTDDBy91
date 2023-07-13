using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30DaysQuickStartTDDBy91.Day5
{
    // 假設現在有一個Validation的服務，要針對使用者輸入的id與密碼進行驗證。Validation的CheckAuthentication方法商業邏輯如下：
    // 1. 根據id，取得存在資料來源中的密碼（僅存放經過hash運算後的結果）
    // 2. 根據傳入的密碼，進行hash運算
    // 3. 比對資料來源回傳的密碼，與輸入密碼經過雜湊運算的結果，是否吻合
    public class Validation
    {
        // 先將職責分離，所以取得資料是透過AccountDao物件，Hash運算則透過Hash物件。
        // 在物件導向的設計，要滿足單一職責原則，所以將不同的職責，交由不同的物件負責，再透過物件之間的互動來滿足使用者需求。
        // 但是，對Validation的CheckAuthentication方法來說，其實根本就不管、不在乎AccountDao以及Hash物件，因為那不在它的商業邏輯中。
        // 但卻為了取得密碼，而直接初始化AccountDao物件，為了取得hash結果，而直接初始化Hash物件。
        // 所以，Validation物件便與AccountDao物件以及Hash物件「直接相依」。
        // 這樣的直接相依，會造成Validation物件的測試，變得困難。
        public bool CheckAuthentication(string id, string password)
        {
            // 取得資料中，id對應的密碼           
            AccountDao dao = new AccountDao();
            var passwordByDao = dao.GetPassword(id);

            // 針對傳入的password，進行hash運算
            Hash hash = new Hash();
            var hashResult = hash.GetHashResult(password);

            // 比對hash後的密碼，與資料中的密碼是否吻合
            return passwordByDao == hashResult;
        }

    }

    public class AccountDao
    {
        internal string GetPassword(string id)
        {
            //連接DB
            throw new NotImplementedException();
        }
    }

    public class Hash
    {
        internal string GetHashResult(string passwordByDao)
        {
            //使用SHA512
            throw new NotImplementedException();
        }
    }
}
