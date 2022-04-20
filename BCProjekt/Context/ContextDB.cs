using Microsoft.EntityFrameworkCore;
using BC.Models;

namespace BC.Context
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options)
        {
          
        }
        public DbSet<Device> Device { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Sensors> Sensors { get; set; }
        public DbSet<Values> Values { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sensors>().HasOne(e=>e.Device).WithMany(e=>e.Sensors).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
