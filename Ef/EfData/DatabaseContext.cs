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

        public DbSet<PartType> PartTypes { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Feature> Features { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Part>().HasKey(part => part.Id);
            modelBuilder.Entity<Part>().Property(part => part.Id).IsRequired();
            modelBuilder.Entity<Part>().HasOne(part => part.PartType).WithMany().IsRequired();
            modelBuilder.Entity<Part>().Property(part => part.Name).IsRequired();
            modelBuilder.Entity<Part>().Property(part => part.Status).IsRequired();

            modelBuilder.Entity<PartType>().HasKey(partType => partType.Id);
            modelBuilder.Entity<PartType>().Property(partType => partType.Id).IsRequired();
            modelBuilder.Entity<PartType>().Property(partType => partType.Name).IsRequired();

            modelBuilder.Entity<Feature>().HasKey(feature => feature.Id);
            modelBuilder.Entity<Feature>().Property(feature => feature.Id).IsRequired();
            modelBuilder.Entity<Feature>().Property(feature => feature.Name).IsRequired();
            modelBuilder.Entity<Feature>().Property(feature => feature.Status).IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={_path}");
        }
    }
}
