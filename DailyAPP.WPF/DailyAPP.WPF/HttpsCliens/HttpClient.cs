using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DaliyAPP.WPF.HttpsCliens
{
   public class HttpClient
    {

        private readonly RestClient _client;//客户端

        private readonly string baseurl = "http://localhost:59308/api/";
       

        public HttpClient()
        {
            _client = new RestClient();
        }

        public ApiResponse Execute(ApiRequest apirequest)
        {
            string fullUrl = baseurl + apirequest.Route;
            RestRequest rest = new RestRequest(fullUrl,apirequest.Method);
            //请求方式
                                                                         
            // rest.AddHeader("Content-Type", apirequest.ContextTyple);//数据类型
            rest.AddHeader("Content-Type", "application/json");
            //参数序列号
            if (apirequest.Partement != null)
            {
                //SerializeObject 序列号 对象->JSON
                rest.AddJsonBody(apirequest.Partement); // 自动序列化成 JSON
            }


            // RestRequest rest = new RestRequest(fullUrl, apirequest.Method);
            //BaseUrl 基础地址+路由
            //_client.BaseUrl = new Uri(fullUrl);resharp和.net8不兼容
            var res = _client.Execute(rest); //执行请求
          //  Console.WriteLine("返回内容：" + res.Content); // 👉 添加调试输出

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //DeserializeObject 反序列化 JSON->对象
                return JsonConvert.DeserializeObject<ApiResponse>(res.Content);
            }
            else
            {
                //返回错误信息
                return new ApiResponse { ResultCode = 99, Msg = "服务器忙", ResultData = null };
            }
        }

    }
    
}
