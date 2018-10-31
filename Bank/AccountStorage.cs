using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bank
{
    /// <summary>
    /// Class for storing account information.
    /// </summary>
    public class AccountStorage : IAccountStorage
    {
        private readonly string _path;
        private IList<BankAccount> _accounts;

        /// <summary>
        /// Initializes a new instance of <see cref="AccountStorage"/>. 
        /// </summary>
        public AccountStorage(string path)
        {
            _path = path;
        }

        private IList<BankAccount> Accounts
        {
            get
            {
                if (_accounts == null)
                {
                    LoadAccounts();
                }

                return _accounts;
            }
        }

        /// <summary>
        /// Checks if the account exists.
        /// </summary>
        public bool AccountExists(BankAccount account)
        {
            return Accounts.Contains(account);
        }

        /// <summary>
        /// Adds the account to the storage.
        /// </summary>
        public void AddAccount(BankAccount account)
        {
            Accounts.Add(account);
        }

        /// <summary>
        /// Delete the account with the specified <paramref name="number"/> from the storage.
        /// </summary>
        public void DeleteAccount(string number)
        {
            Accounts.Remove(_accounts.First(t => t.Number == number));
        }

        /// <summary>
        /// Gets the account with the specified <paramref name="number"/>.
        /// </summary>
        public BankAccount GetAccount(string number)
        {
            return Accounts.FirstOrDefault(t => t.Number == number);
        }

        /// <summary>
        /// Gets all accounts.
        /// </summary>
        public IEnumerable<BankAccount> GetAccounts()
        {
            return Accounts;
        }

        /// <summary>
        /// Saves the accounts to the storage.
        /// </summary>
        public void Save()
        {
            WriteAccounts(Accounts);
        }

        /// <summary>
        /// Loads the accounts from the file.
        /// </summary>
        public void LoadAccounts()
        {
            using (BinaryReader reader = new BinaryReader(File.Open(_path, FileMode.OpenOrCreate)))
            {
                _accounts = new List<BankAccount>();

                while (reader.PeekChar() > -1)
                {
                    string number = reader.ReadString();
                    string name = reader.ReadString();
                    string surname = reader.ReadString();
                    decimal sum = reader.ReadDecimal();
                    decimal bonus = reader.ReadDecimal();
                    string typeName = reader.ReadString();
                    int balanceCost = reader.ReadInt32();
                    int refillCost = reader.ReadInt32();
                    AccountState state = (AccountState)reader.ReadInt32();

                    var account = new BankAccount()
                    {
                        Number = number,
                        AccountType = new AccountType()
                        {
                            TypeName = typeName,
                            RefillCost = refillCost,
                            BalanceCost = balanceCost
                        },
                        Owner = new AccountOwner()
                        {
                            Name = name,
                            Surname = surname
                        },
                        Sum = sum,
                        Bonus = bonus,
                        State = state
                    };

                    _accounts.Add(account);
                }
            }
        }

        private static void WriteAccount(BankAccount account, BinaryWriter writer)
        {
            writer.Write(account.Number);
            writer.Write(account.Owner.Name);
            writer.Write(account.Owner.Surname);
            writer.Write(account.Sum);
            writer.Write(account.Bonus);
            writer.Write(account.AccountType.TypeName);
            writer.Write(account.AccountType.BalanceCost);
            writer.Write(account.AccountType.RefillCost);
            writer.Write((int)account.State);
        }

        private void WriteAccounts(IEnumerable<BankAccount> bankAccounts)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(_path, FileMode.Create)))
            {
                foreach (var account in bankAccounts)
                {
                    WriteAccount(account, writer);
                }
            }
        }
    }
}
