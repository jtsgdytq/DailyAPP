using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliyAPP.WPF.HttpsCliens
{
   public class ApiRequest
    {
        /// <summary>
        /// api路由地址
        /// </summary>
        public string Route { get; set; }
        /// <summary>
        /// 请求方式（get/post/delect/put）
        /// </summary>

        public Method Method { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>

        public object Partement { get; set; }
        /// <summary>
        /// 发送的数据类型
        /// </summary>

        public string ContextTyple { get; set; } = "application/jsion";
    }
}
