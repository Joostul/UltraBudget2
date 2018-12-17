using System;
using System.Collections.Generic;
using UltraBudget2.Models;

namespace UltraBudget2.Repositories
{
    public interface IBudgetRepository
    {
        IEnumerable<Transaction> GetTransactions();
        void UpsertTransaction(Transaction transaction);
        Transaction GetTransaction(Guid id);
        void DeleteTransaction(Guid id);
    }
}
