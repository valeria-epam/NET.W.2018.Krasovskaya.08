using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class AccountManager : IAccountManager
    {
        private ICalculateBonus calculateBonus;
        private IAccountStorage storage;

        public AccountManager(ICalculateBonus calculateBonus, IAccountStorage storage)
        {
            this.calculateBonus = calculateBonus;
            this.storage = storage;
        }

        public void AddBankAccount(BankAccount account)
        {
            CheckAccount(account);
            bool hasAccount = storage.ExistAccount(account);
            if (!hasAccount)
            {
                storage.AddAccount(account);
            }
            else
            {
                throw new Exception("Our storage already has this account.");
            }

        }

        public void CloseBankAccount(BankAccount account)
        {
            CheckAccount(account);
            var bankAccount = storage.GetAccount(account.Number);
            if (bankAccount != null)
            {
                bankAccount.State = AccountState.Close;
            }
            else
                throw new Exception("Our storage doesn't have this account.");
        }

        public void RefillAccount(BankAccount account, decimal amountOfMoney)
        {
            CheckAccount(account);
            var bankAccount = storage.GetAccount(account.Number);

            if (bankAccount != null)
            {
                bankAccount.Sum += amountOfMoney;
                calculateBonus.RefillBonus(bankAccount, amountOfMoney);
            }
            else
            {
                throw new Exception("Our storage doesn't have this account.");

            }

        }

        public void WithdrawalAccount(BankAccount account, decimal amountOfMoney)
        {
            CheckAccount(account);

            var bankAccount = storage.GetAccount(account.Number);
            if (bankAccount != null)
            {
                bankAccount.Sum -= amountOfMoney;
                calculateBonus.WithdrawalBonus(bankAccount, amountOfMoney);
            }
            else
            {
                throw new Exception("Our storage doesn't have this account.");

            }
        }

        public BankAccount GetAccount(string number)
        {
            return storage.GetAccount(number);
        }

        public IEnumerable<BankAccount> GetAccounts()
        {
            return storage.GetAccounts();
        }

        public void Save() => storage.Save();

        private static void CheckAccount(BankAccount account)
        {
            Check(account.Number, nameof(account.Number));
            CheckNotNull(account.Owner, nameof(account.Owner));
            Check(account.Owner.Name, nameof(account.Owner.Name));
            Check(account.Owner.Surname, nameof(account.Owner.Surname));
            CheckNotNull(account.AccountType, nameof(account.AccountType));
            Check(account.AccountType.TypeName, nameof(account.AccountType.TypeName));
        }

        private static void CheckNotNull(object value, string property)
        {
            if (ReferenceEquals(value, null))
                throw new Exception($"The property {property} cannot be null.");
        }

        private static void Check(string value, string property)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception($"The property {property} cannot be null or empty.");

        }
    }
}
