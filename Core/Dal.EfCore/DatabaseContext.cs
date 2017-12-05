using System.Configuration;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Dal.EfCore.Mapping;

namespace ProjectManagement.Dal.EfCore
{
    public sealed class DatabaseContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // apply the various mappings (should use reflection and iterate through the assemvly)
            modelBuilder.ApplyConfiguration(new DonorMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        }
    }
}