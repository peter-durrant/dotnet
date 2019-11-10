using System.Collections.Generic;

namespace Hdd.EfData
{
    public class Part
    {
        public long Id { get; set; }
        public PartType PartType { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public ICollection<Feature> Features { get; set; }
    }
}
