using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltraBudget2.Models;

namespace UltraBudget2.Repositories
{
    public interface IBudgetRepository
    {
        IEnumerable<Transaction> GetTransactions();
        void UpsertTransaction(Transaction transaction);
        void DeleteTransaction();


    }
}
