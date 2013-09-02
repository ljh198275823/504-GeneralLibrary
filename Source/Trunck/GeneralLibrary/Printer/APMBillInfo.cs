using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Printer
{
    /// <summary>
    /// 
    /// </summary>
    public class APMCheckOutBillInfo
    {
        /// <summary>
        /// 获取或设置公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 缴费机编号
        /// </summary>
        public string MID { get;set; }

        /// <summary>
        /// 结账时间
        /// </summary>
        public DateTime CheckOutDateTime { get; set; }

        /// <summary>
        /// 上次结账时间
        /// </summary>
        public DateTime LastDateTime { get; set; }

        /// <summary>
        /// 上次结账余额
        /// </summary>
        public decimal LastBalance { get; set; }

        /// <summary>
        /// 结账金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 本次结余
        /// </summary>
        public decimal TheBalance { get; set; }

        /// <summary>
        /// 支出金额
        /// </summary>
        public decimal PayMoney { get; set; }

        /// <summary>
        /// 收入金额
        /// </summary>
        public decimal IncomeMoneny { get; set; }

        /// <summary>
        /// 纸币数量
        /// </summary>
        public int Cash { get; set; }

        /// <summary>
        /// 硬币数量
        /// </summary>
        public int Coin { get; set; }

        /// <summary>
        /// 钱箱总金额
        /// </summary>
        public decimal TotalAmount
        {
            get
            {
                return Amount + TheBalance;
            }
        }

        /// <summary>
        /// 管理员
        /// </summary>
        public string Operator { get; set; }
    }

    /// <summary>
    /// 自助缴费机临时卡缴费小票
    /// </summary>
    public class APMPaymentBillInfo
    {
        /// <summary>
        /// 获取或设置公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置车牌号
        /// </summary>
        public string CarPlate { get; set; }
        /// <summary>
        /// 获取或设置持卡人姓名
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// 获取或设置入场时间
        /// </summary>
        public DateTime EnterDateTime { get; set; }
        /// <summary>
        /// 获取或设置出场时间
        /// </summary>
        public DateTime ChargeDateTime { get; set; }
        /// <summary>
        /// 获取或设置收费车型
        /// </summary>
        public string CarType { get; set; }
        /// <summary>
        /// 获取或设置收费类型
        /// </summary>
        public string TariffType { get; set; }
        /// <summary>
        /// 获取或设置卡片上次收费时的应收费用
        /// </summary>
        public decimal LastAccounts { get; set; }
        /// <summary>
        /// 获取或设置卡片入场以来已经收取的停车费用(卡片入场后可能会有多条收费记录)
        /// </summary>
        public decimal HavePaid { get; set; }
        /// <summary>
        /// 获取或设置应收停车费用
        /// </summary>
        public decimal Accounts { get; set; }
        /// <summary>
        /// 获取或设置本次收取的费用
        /// </summary>
        public decimal Paid { get; set; }
        /// <summary>
        /// 获取或设置实缴费用
        /// </summary>
        public decimal PaidIn { get; set; }
        /// <summary>
        /// 获取或设置收费操作员
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 获取或设置收费工作站
        /// </summary>
        public string StationID { get; set; }

        /// <summary>
        /// 获取停车时长
        /// </summary>
        public string ParkInterval
        {
            get
            {
                string ret = string.Empty;
                TimeSpan span = new TimeSpan(ChargeDateTime.Ticks - EnterDateTime.Ticks);
                return string.Format(Resources.Resource1.EpsonMT532_Bill_ParkInterval, span.Days * 24 + span.Hours, span.Minutes);
            }
        }

        
        /// 缴费流水号
        /// </summary>
        public string SerialNumber { get; set; }
        

        /// <summary>
        /// 异常描述
        /// </summary>
        public List<string> Description { get; set; }
               

        /// <summary>
        /// 应找零金额
        /// </summary>
        public decimal Change { get; set; }

        /// <summary>
        /// 已找零金额
        /// </summary>
        public decimal HadChange { get; set; }

    }

    /// <summary>
    /// 自助缴费机充值小票
    /// </summary>
    public class APMChargeBillInfo
    {

        /// <summary>
        /// 获取或设置公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 充值流水号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 充值卡号
        /// </summary>
        public string CardID { get; set; }

        /// <summary>
        /// 充值时间
        /// </summary>
        public DateTime ChargeDateTime { get; set; }
        
        /// <summary>
        /// 异常描述
        /// </summary>
        public List<string> Description { get; set; }

        /// <summary>
        /// 卡片余额
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal Payment { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 充值后余额
        /// </summary>
        public decimal ChargeBalance
        {
            get { return Balance + Amount; }
        }
       
        /// <summary>
        /// 缴费机编号
        /// </summary>
        public string MID { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }


    } 
}
