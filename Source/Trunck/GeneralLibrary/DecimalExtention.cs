using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary
{
    public static class DecimalExtention
    {
        /// <summary>
        /// 去掉实数小数点后不必要的0,比如12.500 会变成12.5,12.00会变成12等
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static decimal Trim(this decimal d)
        {
            string temp = d.ToString();
            //去掉数字两头的0，如果是整数，把小数点也去掉
            if (temp.IndexOf('.') > 0)
            {
                temp = temp.TrimEnd('0');
                temp = temp.Trim('.');
            }
            decimal ret = 0;
            decimal.TryParse(temp, out ret);
            return ret;
        }

        /// <summary>
        /// 将一个实数按指定的舍入方式转换成指定小数点位数的实数
        /// </summary>
        /// <param name="value">要转换的实数</param>
        /// <param name="mode">指定的舍入方式 0表示非零进一，1表示银行家算法四舍五入 2表示直接截取</param>
        /// <param name="pointCount"></param>
        /// <returns></returns>
        public static decimal Convert(this decimal value, int mode, int pointCount)
        {
            decimal ret = value;
            if (mode == 1) //四舍五入
            {
                ret = Math.Round(value, pointCount);
            }
            else if (mode == 2) //直接截取
            {
                var temp = Math.Floor((double)(value * System.Convert.ToDecimal(Math.Pow(10, pointCount)))) / Math.Pow(10, pointCount);
                ret = (decimal)temp;
            }
            else //非零进一
            {
                var temp = Math.Ceiling((double)(value * System.Convert.ToDecimal(Math.Pow(10, pointCount))));
                ret = (decimal)(temp / Math.Pow(10, pointCount));
            }
            return decimal.Parse(ret.ToString($"F{pointCount}"));
        }

        /// <summary>
        /// 将一个实数按指定的舍入方式转换成指定小数点位数的实数
        /// </summary>
        /// <param name="value">要转换的实数</param>
        /// <param name="mode">指定的舍入方式 0表示非零进一，1表示四舍五入 2表示直接截取</param>
        /// <param name="pointCount"></param>
        /// <returns></returns>
        public static decimal Convert_总分(this decimal value, int mode, int pointCount)
        {
            decimal ret = value;
            if (mode == 1) //四舍五入
            {
                ret = Math.Round(value, pointCount, MidpointRounding.AwayFromZero);
            }
            else if (mode == 2) //直接截取
            {
                var temp = Math.Floor((double)(value * System.Convert.ToDecimal(Math.Pow(10, pointCount)))) / Math.Pow(10, pointCount);
                ret = (decimal)temp;
            }
            else //非零进一
            {
                var temp = Math.Ceiling((double)(value * System.Convert.ToDecimal(Math.Pow(10, pointCount))));
                ret = (decimal)(temp / Math.Pow(10, pointCount));
            }
            return decimal.Parse(ret.ToString($"F{pointCount}"));
        }
    }
}
