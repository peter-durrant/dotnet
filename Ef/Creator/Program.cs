using System;
using System.Collections.Generic;
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

            var partTypeEntities = new List<PartType>
            {
                new PartType {Id = 1, Name = "widget"},
                new PartType {Id = 2, Name = "thing"},
                new PartType {Id = 3, Name = "connector"},
                new PartType {Id = 4, Name = "socket"}
            };

            var databaseManager = new DatabaseManager();
            databaseManager.CreateDatabase(databasePath);

            using var context = new DatabaseContext(databasePath);

            foreach (var entity in partTypeEntities)
            {
                context.PartTypes.Add(entity);
            }

            var random = new Random();
            for (long i = 1; i <= 100000; i++)
            {
                var entity = new Part
                {
                    Id = i,
                    PartType = partTypeEntities[random.Next(0, partTypeEntities.Count)],
                    Name = i.ToString(),
                    Status = (Status)random.Next((int)Status.None, (int)Status.Error + 1)
                };

                context.Parts.Add(entity);
            }

            context.SaveChanges();
        }
    }
}
