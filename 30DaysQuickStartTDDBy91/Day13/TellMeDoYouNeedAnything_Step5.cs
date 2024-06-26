﻿using _30DaysQuickStartTDDBy91.Day12;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30DaysQuickStartTDDBy91.Day13
{
    internal class TellMeDoYouNeedAnything_Step5
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            //若頁面通過驗證
            if (this.IsValid)
            {
                // 取得畫面資料
                // 物流商需要產品資訊
                var product = this.GetProduct();

                // 頁面需要物流商的名稱與運費
                var companyName = "";
                double fee = 0;

                //選黑貓，計算出運費
                if (this.drpCompany.SelectedValue == "1")
                {
                    // 初始化物流商物件
                    //計算
                    BlackCat blackCat = new BlackCat() { ShipProduct = product };
                    blackCat.Calculate();
                    // 取得物流商名稱
                    companyName = blackCat.GetsCompanyName();
                    // 取得運費結果
                    fee = blackCat.GetsFee();
                }
                //選新竹貨運，計算出運費
                else if (this.drpCompany.SelectedValue == "2")
                {
                    //計算
                    Hsinchu hsinchu = new Hsinchu() { ShipProduct = product };
                    hsinchu.Calculate();
                    companyName = hsinchu.GetsComapanyName();
                    fee = hsinchu.GetsFee();
                }
                //選郵局，計算出運費
                else if (this.drpCompany.SelectedValue == "3")
                {
                    //計算
                    PostOffice postOffice = new PostOffice() { ShipProduct = product };
                    postOffice.Calculate();
                    companyName = postOffice.GetsComapanyName();
                    fee = postOffice.GetsFee();
                }
                //發生預期以外的狀況，呈現警告訊息，回首頁
                else
                {
                    var js = "alert('發生不預期錯誤，請洽系統管理者');location.href='http://tw.yahoo.com/';";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "back", js, true);
                }

                // 呈現結果
                // 呈現運費結果與物流商名稱
                this.SetResult(companyName, fee);
            }

        }

        /// <summary>
        /// 呈現結果
        /// </summary>
        /// <param name="companyName"></param>
        /// <param name="fee"></param>        
        private void SetResult(string companyName, double fee)
        {
            this.lblCompany.Text = companyName;
            this.lblCharge.Text = fee.ToString();
        }

        /// <summary>/// 取得畫面資料/// </summary>/// <returns></returns>
        private Product GetProduct()
        {
            var result = new Product
            {
                Name = this.txtProductName.Text.Trim(),
                Weight = Convert.ToDouble(this.txtProductWeight.Text),
                Size = new Size()
                {
                    Length = Convert.ToDouble(this.txtProductLength.Text),
                    Width = Convert.ToDouble(this.txtProductWidth.Text),
                    Height = Convert.ToDouble(this.txtProductHeight.Text)
                },
                IsNeedCool = this.rdoNeedCool.SelectedValue == "1"
            };

            return result;
        }

    }

    public class BlackCat
    {
        public void Calculate()
        {
            throw new NotImplementedException();
        }


        public Product ShipProduct { get; set; }

        public string GetsCompanyName()
        {
            throw new NotImplementedException();
        }

        public double GetsFee()
        {
            throw new NotImplementedException();
        }
    }

    public class Hsinchu
    {
        public void Calculate()
        {
            throw new NotImplementedException();
        }

        public Product ShipProduct { get; set; }

        public string GetsComapanyName()
        {
            throw new NotImplementedException();
        }

        public double GetsFee()
        {
            throw new NotImplementedException();
        }
    }

    public class PostOffice
    {
        public void Calculate()
        {
            throw new NotImplementedException();
        }

        public Product ShipProduct { get; set; }

        public string GetsComapanyName()
        {
            throw new NotImplementedException();
        }

        public double GetsFee()
        {
            throw new NotImplementedException();
        }
    }
}
