using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30DaysQuickStartTDDBy91.Day12
{
    internal class SegregationOfDuties_Step4
    {
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            //若頁面通過驗證
            if (this.IsValid)
            {
                //選黑貓，計算出運費，呈現物流商名稱與運費
                if (this.drpCompany.SelectedValue == "1")
                {
                    //CalculatedByBlackCat();
                    //取得畫面資料
                    //計算
                    BlackCat blackCat = new BlackCat();
                    blackCat.Calculate();

                    //呈現
                }
                //選新竹貨運，計算出運費，呈現物流商名稱與運費
                else if (this.drpCompany.SelectedValue == "2")
                {
                    //CalculatedByHsinchu();
                    //取得畫面資料
                    //計算
                    Hsinchu hsinchu = new Hsinchu();
                    hsinchu.Calculate();

                    //呈現
                }
                //選郵局，計算出運費，呈現物流商名稱與運費
                else if (this.drpCompany.SelectedValue == "3")
                {
                    //CalculatedByPostOffice();
                    //取得畫面資料
                    //計算
                    PostOffice postOffice = new PostOffice();
                    postOffice.Calculate();

                    //呈現
                }
                //發生預期以外的狀況，呈現警告訊息，回首頁
                else
                {
                    var js = "alert('發生不預期錯誤，請洽系統管理者');location.href='http://tw.yahoo.com/';";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "back", js, true);
                }
            }
        }



    }

    internal class PostOffice
    {
        public PostOffice()
        {
        }

        internal void Calculate()
        {
            throw new NotImplementedException();
        }
    }

    internal class Hsinchu
    {
        public Hsinchu()
        {
        }

        internal void Calculate()
        {
            throw new NotImplementedException();
        }
    }

    internal class BlackCat
    {
        public BlackCat()
        {
        }
        public Product ShipProduct { get; set; }

        internal void Calculate()
        {
            throw new NotImplementedException();
        }


        public string GetsCompanyName()
        {
            throw new NotImplementedException();
        }

        public double GetsFee()
        {
            throw new NotImplementedException();
        }
    }
}
