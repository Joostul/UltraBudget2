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

        IEnumerable<MasterCategory> GetMasterCategories();
        MasterCategory GetMasterCategory(Guid id);
        void UpsertMasterCategory(MasterCategory masterCategory);
        void DeleteCategory(Guid id);

        IEnumerable<SubCategory> GetSubCategories();
        IEnumerable<SubCategory> GetSubCategoriesForMaster(Guid id);
        SubCategory GetSubCategory(Guid id);
        void UpsertSubcategory(Guid mastercategoryId, SubCategory subCategory);

        IEnumerable<Account> GetAccounts();
        Account GetAccount(Guid id);
        void UpsertAccount(Account account);
        void DeleteAccount(Guid id);

        void UpsertBudget(Guid mastercategoryId, Guid subcategoryId, Budget budget);
    }
}
