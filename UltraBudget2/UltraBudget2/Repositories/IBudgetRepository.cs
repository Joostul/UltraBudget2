using System;
using System.Collections.Generic;
using UltraBudget2.Models;

namespace UltraBudget2.Repositories
{
    public interface IBudgetRepository
    {
        BudgetDao Export();
        void Import(BudgetDao budget);

        IEnumerable<Transaction> GetTransactions();
        Transaction GetTransaction(Guid id);
        void UpsertTransaction(Transaction transaction);
        void DeleteTransaction(Guid id);

        IEnumerable<MasterCategory> GetCategories();
        MasterCategory GetCategory(Guid id);
        void UpsertCategory(MasterCategory category);
        void DeleteCategory(Guid id);

        IEnumerable<Account> GetAccounts();
        Account GetAccount(Guid id);
        void UpsertAccount(Account account);
        void DeleteAccount(Guid id);
    }
}
