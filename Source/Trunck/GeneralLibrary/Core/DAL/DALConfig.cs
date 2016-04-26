using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LJH.GeneralLibrary.Core.DAL
{
    public class DALConfig
    {
        #region 静态属性
        private static  DALConfig _Instance = null;
        private static object _Locker = new object();

        /// <summary>
        /// 获取默认设置
        /// </summary>
        public static DALConfig Default
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_Locker)
                    {
                        if (_Instance == null) _Instance = new DALConfig();
                    }
                }
                return _Instance;
            }
        }
        #endregion

        #region 构造函数
        private DALConfig()
        {
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置是否记录日志
        /// </summary>
        public bool Log { get; set; }
        #endregion
    }
}
