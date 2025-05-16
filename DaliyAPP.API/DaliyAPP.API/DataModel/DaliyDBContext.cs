using Microsoft.EntityFrameworkCore;

namespace DaliyAPP.API.DataModel
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class DaliyDBContext:DbContext
    {
        public DaliyDBContext(DbContextOptions<DaliyDBContext> options):base(options) 
        {

        }

        public DbSet<AccountInfo> AccountInfo { get; set; }
        public DbSet<WaitInfo> WaitInfo { get; set; }
        public DbSet<MemoInfo> MemoInfo { get; set; }
    }
}
