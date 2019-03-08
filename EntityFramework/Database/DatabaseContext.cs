using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<MeasurementItem> MeasurementItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database2.db");
        }
    }

    public class MeasurementItem
    {
        public int Id { get; set; }
        public double Value { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public ICollection<MeasurementItem> MeasurementItems { get; set; }
    }
}