namespace DaliyAPP.API.DTOs
{
    /// <summary>
    /// 待办事项统计DTO
    /// </summary>
    public class StaWaitDTO
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
      
        public string FinnishingRate 
        { 
            get
            {
                if (TotalCount == 0)
                {
                    return "0%";
                }
                else
                {
                    return ((double)FinishCount / TotalCount * 100).ToString("0.00") + "%";
                }
            }
        }
    }
}
