using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliyAPP.WPF.DTOs
{
    /// <summary>
    /// 统计待办数据dto
    /// </summary>
    class StaWaitDTO
    {
        /// <summary>
        /// 待办事项数量
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 已完成事项数量
        /// </summary>
        public int FinishCount { get; set; }
        /// <summary>
        /// 百分比
        /// </summary>

        public string FinnishingRate { get; set; }
    }
}
