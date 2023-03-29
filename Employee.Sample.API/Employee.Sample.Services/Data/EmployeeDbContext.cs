using Employee.Sample.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Employee.Sample.Services.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        public DbSet<EmployeeData> employeeDatas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeData>().ToTable(name: "TblEmployee");
            modelBuilder.Entity<EmployeeData>().HasIndex(c => new { c._name });
        }
    }
}
