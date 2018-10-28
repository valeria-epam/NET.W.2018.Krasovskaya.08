using System.Collections.Generic;

namespace Bank
{
    public interface IAccountStorage
    {
        bool ExistAccount(BankAccount account);
        void AddAccount(BankAccount account);
        void DeleteAccount(string number);
        BankAccount GetAccount(string number);
        IEnumerable<BankAccount> GetAccounts();
        void Save();
    }
}