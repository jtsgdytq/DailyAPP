using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaliyAPP.API.DataModel
{
    [Table("AccountInfo")]//设置表名
    public class AccountInfo
    {
        [Key]//将AccountID设置为主键，int类型会自动生成
        public int AccountID { get; set; }
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
       
    }
}
