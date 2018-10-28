using System.Collections.Generic;

namespace Bank
{
    public interface IAccountManager
    {
        void AddBankAccount(BankAccount account);
        void CloseBankAccount(BankAccount account);
        void RefillAccount(BankAccount account, decimal amountOfMoney);
        void WithdrawalAccount(BankAccount account, decimal amountOfMoney);
        BankAccount GetAccount(string number);
        IEnumerable<BankAccount> GetAccounts();
        void Save();
    }
}