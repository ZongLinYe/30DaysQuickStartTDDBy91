using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30DaysQuickStartTDDBy91.Day17
{
    internal class StrategyPattern_Step9
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

                ILogistics logistics = GetLogistics(this.drpCompany.SelectedValue, product);

                ////選黑貓，計算出運費
                //if (this.drpCompany.SelectedValue == "1")
                //{
                //    // 初始化物流商物件
                //    //計算
                //    //BlackCat blackCat = new BlackCat() { ShipProduct = product };
                //    //blackCat.Calculate();
                //    //companyName = blackCat.GetsComapanyName();
                //    //fee = blackCat.GetsFee();
                //    ILogistics logistics = new BlackCat() { ShipProduct = product };
                //    logistics.Calculate();
                //    // 取得物流商名稱
                //    companyName = logistics.GetsCompanyName();
                //    // 取得運費結果
                //    fee = logistics.GetsFee();
                //}
                ////選新竹貨運，計算出運費
                //else if (this.drpCompany.SelectedValue == "2")
                //{
                //    //計算
                //    //Hsinchu hsinchu = new Hsinchu() { ShipProduct = product };
                //    //hsinchu.Calculate();
                //    //companyName = hsinchu.GetsComapanyName();
                //    //fee = hsinchu.GetsFee();
                //    ILogistics logistics = new Hsinchu() { ShipProduct = product };
                //    logistics.Calculate();
                //    companyName = logistics.GetsCompanyName();
                //    fee = logistics.GetsFee();
                //}
                ////選郵局，計算出運費
                //else if (this.drpCompany.SelectedValue == "3")
                //{
                //    //計算
                //    //PostOffice postOffice = new PostOffice() { ShipProduct = product };
                //    //postOffice.Calculate();
                //    //companyName = postOffice.GetsComapanyName();
                //    //fee = postOffice.GetsFee();
                //    ILogistics logistics = new PostOffice() { ShipProduct = product };
                //    logistics.Calculate();
                //    companyName = logistics.GetsCompanyName();
                //    fee = logistics.GetsFee();
                //}
                ////發生預期以外的狀況，呈現警告訊息，回首頁
                //else
                //{
                //    var js = "alert('發生不預期錯誤，請洽系統管理者');location.href='http://tw.yahoo.com/';";
                //    this.ClientScript.RegisterStartupScript(this.GetType(), "back", js, true);
                //}

                if(logistics != null)
                {
                    logistics.Calculate();
                    companyName = logistics.GetsCompanyName();
                    fee = logistics.GetsFee();
                    // 呈現結果
                    // 呈現運費結果與物流商名稱
                    this.SetResult(companyName, fee);
                }
                else
                {
                    var js = "alert('發生不預期錯誤，請洽系統管理者');location.href='http://tw.yahoo.com/';";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "back", js, true);
                }

            }

        }

        private ILogistics GetLogistics(string company, Product product)
        {
            switch(company)
            {
                case "1":
                    return new BlackCat() { ShipProduct = product };
                case "2":
                    return new Hsinchu() { ShipProduct = product };
                case "3":
                    return new PostOffice() { ShipProduct = product };
                default:
                    return null;
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

        /// <summary>
        /// 取得畫面資料
        /// </summary>
        /// <returns></returns>
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

    public interface ILogistics
    {
        //Product ShipProduct { get; set; }

        void Calculate();
        string GetsCompanyName();
        double GetsFee();
    }

    public class BlackCat : ILogistics
    {
        private double _fee;
        private readonly string _companyName;
        public Product ShipProduct { get; set; }
        public BlackCat()
        {
            _companyName = "黑貓";
        }
        public void Calculate()
        {
            var weight = ShipProduct.Weight;

            //計算運費
            if (weight < 20)
            {

                _fee = 100 + weight * 10;
            }
            else
            {
                _fee = 500;
            }
        }



        public string GetsCompanyName()
        {
            return _companyName;
        }

        public double GetsFee()
        {
            return _fee;
        }
    }

    public class Hsinchu : ILogistics
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

    public class PostOffice : ILogistics
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

    public class Size
    {
        public int Height { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
    }

    public class Product
    {
        public Product()
        {
        }

        public bool IsNeedCool { get; set; }
        public string Name { get; set; }
        public Size Size { get; set; }
        public int Weight { get; set; }
    }
}
