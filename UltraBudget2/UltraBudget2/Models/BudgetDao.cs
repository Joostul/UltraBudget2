using System.Collections.Generic;

namespace UltraBudget2.Models
{
    public class BudgetDao
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Account> Accounts { get; set; }
    }
}
