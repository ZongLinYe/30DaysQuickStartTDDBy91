﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _30DaysQuickStartTDDBy91.Day7
{
    // User Story ?
    // 效益：顧客入場時，幫助店員統計出門票收入，確認是否核帳正確
    // 角色：Pub店員
    // 目的：根據顧客與相關條件，算出對應的門票收入總值
    // CheckIn說明：
    // 當顧客進場時，如果是女生，則免費入場。
    // 若為男生，則根據ICheckInFee介面來取得門票的費用，並累計到inCome中。透過GetInCome()方法取得這一次的門票收入總金額。
    public class Pub
    {
        private ICheckInFee _checkInFee;
        private decimal _inCome = 0;
        //private readonly IDateTimeProvider _dateTimeProvider;
        private readonly Func<DateTime> _dateTimeNow;

        //public Pub(ICheckInFee checkInFee, IDateTimeProvider dateTimeProvider)
        public Pub(ICheckInFee checkInFee, Func<DateTime> dateTimeNow)
        {
            this._checkInFee = checkInFee;
            //_dateTimeProvider = dateTimeProvider;
            _dateTimeNow = dateTimeNow;
        }

        /// <summary>
        /// 入場
        /// </summary>
        /// <param name="customers"></param>
        /// <returns>收費的人數</returns>
        public int CheckIn(List<Customer> customers)
        {
            var result = 0;

            foreach (var customer in customers)
            {
                var isFemale = !customer.IsMale;

                //女生免費入場
                if (isFemale)
                {
                    continue;
                }
                else
                {
                    //for stub, validate status: income value
                    //for mock, validate only male
                    this._inCome += this._checkInFee.GetFee(customer);

                    result++;
                }
            }

            //for stub, validate return value
            return result;
        }

        /// <summary>
        /// 入場
        /// 以這例子來說，假設CheckIn的需求改變，從原本的「女生免費入場」變成「只有當天為星期五，女生才免費入場」，修改程式碼如下：
        /// </summary>
        /// <param name="customers"></param>
        /// <returns>收費的人數</returns>
        public int CheckInOnlyFridayWomanFree(List<Customer> customers)
        {
            var result = 0;

            foreach (var customer in customers)
            {
                var isFemale = !customer.IsMale;
                //var isLadyNight = DateTime.Today.DayOfWeek == DayOfWeek.Friday;              
                //var isLadyNight = _dateTimeProvider.Now.DayOfWeek == DayOfWeek.Friday;
                var isLadyNight = _dateTimeNow().DayOfWeek == DayOfWeek.Friday;


                // 禮拜五女生免費入場
                if (isLadyNight && isFemale)
                {
                    continue;
                }
                else
                {
                    //for stub, validate status: income value
                    //for mock, validate only male
                    this._inCome += this._checkInFee.GetFee(customer);

                    result++;
                }
            }

            //for stub, validate return value
            return result;
        }


        public decimal GetInCome()
        {
            return this._inCome;
        }
    }

    public class Customer
    {
        public bool IsMale { get; set; }
        public int Seq { get; set; }
    }

    public interface ICheckInFee
    {
        decimal GetFee(Customer customer);
    }
}
