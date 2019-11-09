using System;
using System.IO;
using Hdd.EfData;

namespace Creator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Creating database");

            const string databasePath = "../../../../database.db";

            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }

            var databaseManager = new DatabaseManager();
            databaseManager.CreateDatabase(databasePath);

            using var context = new DatabaseContext(databasePath);

            for (long i = 1; i <= 100000; i++)
            {
                var entity = new Part
                {
                    Id = i,
                    Name = i.ToString()
                };

                context.Parts.Add(entity);
            }

            context.SaveChanges();
        }
    }
}
