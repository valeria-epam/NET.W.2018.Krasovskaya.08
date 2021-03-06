﻿using System.Collections.Generic;

namespace Bank
{
    /// <summary>
    /// Interface for managing account information.
    /// </summary>
    public interface IAccountManager
    {
        /// <summary>
        /// Adds a new account if it doesn't exist.
        /// </summary>
        void AddBankAccount(BankAccount account);

        /// <summary>
        /// Close account if it exists.
        /// </summary>
        void CloseBankAccount(BankAccount account);

        /// <summary>
        /// Adds amount of money to the account.
        /// </summary>
        void RefillAccount(BankAccount account, decimal amountOfMoney);

        /// <summary>
        /// Withdraws amount of money from account.
        /// </summary>
        void WithdrawalAccount(BankAccount account, decimal amountOfMoney);

        /// <summary>
        /// Gets account by account <paramref name="number"/>.
        /// </summary>
        BankAccount GetAccount(string number);

        /// <summary>
        /// Gets all accounts.
        /// </summary>
        IEnumerable<BankAccount> GetAccounts();

        /// <summary>
        /// Saves the accounts to the storage.
        /// </summary>
        void Save();

        /// <summary>
        /// Reloads the accounts to the storage.
        /// </summary>
        void Reload();
    }
}