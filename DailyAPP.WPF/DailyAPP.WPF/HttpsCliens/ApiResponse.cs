using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliyAPP.WPF.HttpsCliens
{
   public class ApiResponse
    {
        /// <summary>
        /// 结果代码
        /// </summary>
        public int ResultCode { get; set; }
        /// <summary>
        /// 返回响应消息，如错误等
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
        public object ResultData { get; set; }
    }
}
