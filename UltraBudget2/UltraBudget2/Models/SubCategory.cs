using System;

namespace UltraBudget2.Models
{
    public class SubCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MasterCategory { get; set; }
    }
}
