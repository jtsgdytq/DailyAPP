using DaliyAPP.API.ApiResponses;
using DaliyAPP.API.DataModel;
using DaliyAPP.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DaliyAPP.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //这是通过 构造函数注入 DbContext（也就是你的数据库连接）

       // _dbContext 就是用来操作数据库的工具，比如查询、插入数据等。
       public AccountController(DaliyDBContext _dbContext)
        {
            dbContext = _dbContext;
        }

        private  DaliyDBContext dbContext;
        #region 注册
        //业务处理，账号已存在，返回-1，账号注册成功，返回1，其他返回-99
        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="accountDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Reg(AccountDTO accountDTO )
            
        {
            ApiResponse response = new ApiResponse();

            try
            {
               var dbAcount= dbContext.AccountInfo.Where(t=>(t.Account==accountDTO.Account)).FirstOrDefault();
                if (dbAcount != null)
                {
                    response.ResultCode = -1;
                    response.Msg = "账号已存在";

                    return Ok(response);

                }
                AccountInfo accountInfo = new AccountInfo() { Account = accountDTO.Account, AccountName = accountDTO.AccountName, Password = accountDTO.Password };

                dbContext.AccountInfo.Add(accountInfo);

                int result=dbContext.SaveChanges();

                if (result == 1)
                {
                    response.ResultCode = 1;
                    response.Msg = "账号注册成功";
                    return Ok(response);

                }
                else
                {
                    response.ResultCode = -99;
                    response.Msg = "服务器繁忙";
                    return Ok(response);
                }
               

            }
            catch (Exception)
            {
                response.ResultData = -99;

                response.Msg = "服务器繁忙";
                return Ok(response);
               
            }

            //return Ok(response);
         }

        #endregion


        #region 登录
        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            string Account = loginDTO.Account;
            string Password = loginDTO.Password;

            ApiResponse res = new ApiResponse();

            var dbContextInfo = dbContext.AccountInfo.FirstOrDefault(t => t.Account == Account);

            if (dbContextInfo == null)
            {
                res.ResultCode = -2;
                res.Msg = "账号不存在或错误";
                return Ok(res);
            }

            if (dbContextInfo.Password != Password)
            {
                res.ResultCode = -3;
                res.Msg = "密码错误";
                return Ok(res);
            }

            res.ResultCode = 666;
            res.Msg = "登陆成功";
            res.ResultData = dbContextInfo;
            return Ok(res);
        }

        #endregion
    }
}
