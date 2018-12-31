using System;
using System.Collections.Generic;

namespace UltraBudget2.Models
{
    public class SubCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MasterCategory { get; set; }
        public List<Budget> Budgets { get; set; }
    }
}
