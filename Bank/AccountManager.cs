using System;
using System.Collections.Generic;

namespace Bank
{
    /// <summary>
    /// Class for managing account information.
    /// </summary>
    public class AccountManager : IAccountManager
    {
        private readonly ICalculateBonus _calculateBonus;
        private readonly IAccountStorage _storage;

        /// <summary>
        /// Initializes a new instanse of <see cref="AccountManager"/>. 
        /// </summary>
        public AccountManager(ICalculateBonus calculateBonus, IAccountStorage storage)
        {
            _calculateBonus = calculateBonus;
            _storage = storage;
        }

        /// <summary>
        /// Adds a new account if it doesn't exist.
        /// </summary>
        public void AddBankAccount(BankAccount account)
        {
            CheckAccount(account);
            bool hasAccount = _storage.AccountExists(account);
            if (!hasAccount)
            {
                _storage.AddAccount(account);
            }
            else
            {
                throw new Exception("Our storage already has this account.");
            }

        }

        /// <summary>
        /// Close account if it exists.
        /// </summary>
        public void CloseBankAccount(BankAccount account)
        {
            CheckAccount(account);
            var bankAccount = _storage.GetAccount(account.Number);
            if (bankAccount != null)
            {
                bankAccount.State = AccountState.Closed;
            }
            else
            {
                throw new Exception("Our storage doesn't have this account.");
            }
        }

        /// <summary>
        /// Adds amount of money to the account.
        /// </summary>
        public void RefillAccount(BankAccount account, decimal amountOfMoney)
        {
            if (amountOfMoney <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amountOfMoney));
            }
            CheckAccount(account);

            var bankAccount = _storage.GetAccount(account.Number);
            if (bankAccount == null)
            {
                throw new Exception("Our storage doesn't have this account.");
            }
            if (bankAccount.State == AccountState.Closed)
            {
                throw new Exception("Your account is closed.");
            }
            bankAccount.Sum += amountOfMoney;
            _calculateBonus.RefillBonus(bankAccount, amountOfMoney);
        }

        /// <summary>
        /// Withdraws amount of money from account.
        /// </summary>
        public void WithdrawalAccount(BankAccount account, decimal amountOfMoney)
        {
            if (amountOfMoney <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amountOfMoney));
            }
            CheckAccount(account);

            var bankAccount = _storage.GetAccount(account.Number);
            if (bankAccount == null)
            {
                throw new Exception("Our storage doesn't have this account.");
            }
            if (bankAccount.State == AccountState.Closed)
            {
                throw new Exception("Your account is closed.");
            }
            if (bankAccount.Sum < amountOfMoney)
            {
                throw new Exception("You don't have enough money on your account.");
            }
            bankAccount.Sum -= amountOfMoney;
            _calculateBonus.WithdrawalBonus(bankAccount, amountOfMoney);
        }

        /// <summary>
        /// Gets account by account <paramref name="number"/>.
        /// </summary>
        public BankAccount GetAccount(string number)
        {
            return _storage.GetAccount(number);
        }

        /// <summary>
        /// Gets all accounts.
        /// </summary>
        public IEnumerable<BankAccount> GetAccounts()
        {
            return _storage.GetAccounts();
        }

        /// <summary>
        /// Saves the accounts to the storage.
        /// </summary>
        public void Save() => _storage.Save();

        /// <summary>
        /// Reloads the accounts to the storage.
        /// </summary>
        public void Reload() => _storage.LoadAccounts();

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
