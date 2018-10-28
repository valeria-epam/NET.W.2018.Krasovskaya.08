using System.Collections.Generic;

namespace Bank
{
    /// <summary>
    /// Interface for storing account information.
    /// </summary>
    public interface IAccountStorage
    {
        /// <summary>
        /// Checks if the account exists.
        /// </summary>
        bool AccountExists(BankAccount account);

        /// <summary>
        /// Adds the account to the storage.
        /// </summary>
        void AddAccount(BankAccount account);

        /// <summary>
        /// Delete the account with the specified <paramref name="number"/> from the storage.
        /// </summary>
        void DeleteAccount(string number);

        /// <summary>
        /// Gets the account with the specified <paramref name="number"/>.
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
        /// Loads the accounts.
        /// </summary>
        void LoadAccounts();
    }
}