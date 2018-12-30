using System;
using System.Collections.Generic;

namespace UltraBudget2.Models
{
    public class MasterCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }
}
