using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30DaysQuickStartTDDBy91.Day9
{
    //- 什麼樣的程式碼算的上是Bad smell？
    //- 繼承深度 > 3
    //- 區塊深度 > 4
    //- 相似程式碼 > 15行
    //- 綜合可維護性指數< 75
    public class FindBadSmellCodeByXXX
    {
        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            //if (this.IsValid)
            //{
            //    if (this.drpCompany.SelectedValue == "1")
            //    {
            //        this.lblCompany.Text = "黑貓";
            //        var weight = Convert.ToDouble(this.txtProductWeight.Text);
            //        if (weight > 20)
            //        {
            //            this.lblCharge.Text = "500";
            //        }
            //        else
            //        {
            //            var fee = 100 + weight * 10;
            //            this.lblCharge.Text = fee.ToString();
            //        }
            //    }
            //    else if (this.drpCompany.SelectedValue == "2")
            //    {
            //        this.lblCompany.Text = "新竹貨運";
            //        var length = Convert.ToDouble(this.txtProductLength.Text);
            //        var width = Convert.ToDouble(this.txtProductWidth.Text);
            //        var height = Convert.ToDouble(this.txtProductHeight.Text);

            //        var size = length * width * height;

            //        //長 x 寬 x 高（公分）x 0.0000353
            //        if (length > 100 || width > 100 || height > 100)
            //        {
            //            this.lblCharge.Text = (size * 0.0000353 * 1100 + 500).ToString();
            //        }
            //        else
            //        {
            //            this.lblCharge.Text = (size * 0.0000353 * 1200).ToString();
            //        }
            //    }
            //    else if (this.drpCompany.SelectedValue == "3")
            //    {
            //        this.lblCompany.Text = "郵局";

            //        var weight = Convert.ToDouble(this.txtProductWeight.Text);
            //        var feeByWeight = 80 + weight * 10;

            //        var length = Convert.ToDouble(this.txtProductLength.Text);
            //        var width = Convert.ToDouble(this.txtProductWidth.Text);
            //        var height = Convert.ToDouble(this.txtProductHeight.Text);
            //        var size = length * width * height;
            //        var feeBySize = size * 0.0000353 * 1100;

            //        if (feeByWeight < feeBySize)
            //        {
            //            this.lblCharge.Text = feeByWeight.ToString();
            //        }
            //        else
            //        {
            //            this.lblCharge.Text = feeBySize.ToString();
            //        }
            //    }
            //    else
            //    {
            //        var js = "alert('發生不預期錯誤，請洽系統管理者');location.href='http://tw.yahoo.com/';";
            //        this.ClientScript.RegisterStartupScript(this.GetType(), "back", js, true);
            //    }
            //}
        }
    }
}
