using System;
using Microsoft.EntityFrameworkCore;

namespace Hdd.EfData
{
    public class DatabaseContext : DbContext
    {
        private readonly string _path;

        public DatabaseContext(string path)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public DbSet<Part> Parts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Part>().HasKey(part => part.Id);

            modelBuilder.Entity<Part>().Property(part => part.Id).IsRequired();
            modelBuilder.Entity<Part>().Property(part => part.Name).IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={_path}");
        }
    }
}
