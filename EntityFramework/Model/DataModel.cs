using System.Collections.Generic;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    public class DataModel
    {
        public DataModel()
        {
            using (var db = new DatabaseContext())
            {
                //db.Items.Add(
                //    new Item
                //    {
                //        Id = 0,
                //        MeasurementItems = new List<MeasurementItem>
                //        {
                //            new MeasurementItem {Id = 0, Value = 1},
                //            new MeasurementItem {Id = 0, Value = 2},
                //            new MeasurementItem {Id = 0, Value = 3}
                //        }
                //    });

                db.Database.EnsureCreated();
                db.Database.Migrate();
                db.SaveChanges();
            }
        }
    }
}