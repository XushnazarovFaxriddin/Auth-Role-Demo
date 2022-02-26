using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AuthRoleDemo.Models;

namespace AuthRoleDemo.Data
{
    public class DataContext : DbContext
    {
        public virtual DbSet<UserValid> Users { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserValid>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }

        /* protected override void OnConfiguring(DbContextOptionsBuilder options)
         {
             // soddaligi uchun foydalaniladigan xotira ma'lumotlar bazasida ishlab chiqarish ilovalari uchun haqiqiy DB ga o'zgartiring
             options.UseInMemoryDatabase("dbAuthRoleDemo");
         }*/
    }
}
