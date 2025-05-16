using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliyAPP.WPF.DTOs
{
   public class AccountInfoDTO
    {
        /// <summary>
        /// 账户名称
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 账户账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 账户密码
        /// </summary>
        public string Password { get; set; }


        public string ConfirmPassword { get; set; }

    }
}
