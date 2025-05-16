namespace DaliyAPP.API.ApiResponses
{
    public class ApiResponse
    {
        /// <summary>
        /// 结果代码
        /// </summary>
        public int ResultCode {get;set;}
        /// <summary>
        /// 返回响应消息，如错误等
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
        public object   ResultData { get; set; }

    }
}
