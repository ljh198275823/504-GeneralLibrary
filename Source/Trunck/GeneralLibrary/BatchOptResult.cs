using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJH.GeneralLibrary
{
    /// <summary>
    /// 表示批量增加时的返回值
    /// </summary>
    /// <typeparam name="TID"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class BatchOptResult<TID, TEntity>
    {
        public BatchOptResult()
        {
        }

        public BatchOptResult (Dictionary <TID,TEntity > successes,Dictionary <TID, string> errors)
        {
            this.Successes = successes;
            this.Errors = errors;
        }

        public Dictionary<TID, TEntity> Successes { get; set; }

        public Dictionary<TID, string> Errors { get; set; }
    }
}
