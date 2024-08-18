using Microsoft.EntityFrameworkCore;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Data
{
    public class ManageDBContext: DbContext
    {
        public ManageDBContext(DbContextOptions options) : base(options)
        {
            
        }

        #region Dbset
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        #endregion
    }
}
