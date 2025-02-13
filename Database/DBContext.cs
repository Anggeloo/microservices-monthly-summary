using Microsoft.EntityFrameworkCore;

namespace microservices_monthly_summary.Database
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Models.MonthlySummary> MonthlySummary { get; set; }
    }
}
