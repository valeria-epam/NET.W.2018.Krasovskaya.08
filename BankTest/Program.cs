using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bank;

namespace BankTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Initialization
            var owner = new AccountOwner()
            {
                Name = "Ivan",
                Surname = "Ivanov"
            };

            var type1 = AccountType.BaseAccount;
            var type2 = AccountType.PlatinumAccount;

            var account1 = new BankAccount()
            {
                Number = "123456",
                AccountType = type1,
                Owner = owner,
                Sum = 10m,
                Bonus = 0m,
                State = AccountState.Active
            };

            var account2 = new BankAccount()
            {
                Number = "1248956",
                AccountType = type2,
                Owner = owner,
                Sum = 0m,
                Bonus = 0m,
                State = AccountState.Active
            };

            var calculateBonus = new CalculateBonus();

            var appFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var storageFolder = Path.Combine(appFolder, "Storage");
            Directory.CreateDirectory(storageFolder);
            var path = Path.Combine(storageFolder, "BankAccountStorage");

            var storage = new AccountStorage(path);
            var manager = new AccountManager(calculateBonus, storage);
            #endregion

            manager.AddBankAccount(account1);
            manager.AddBankAccount(account2);
            var accounts1 = manager.GetAccounts();

            foreach (var bankAccount in accounts1)
            {
                Console.WriteLine(bankAccount.ToString());
            }

            Console.ReadKey();
            Console.WriteLine();

            manager.RefillAccount(account1, 500m);
            manager.RefillAccount(account2, 1000m);

            var accounts2 = manager.GetAccounts();

            foreach (var bankAccount in accounts2)
            {
                Console.WriteLine(bankAccount.ToString());
            }

            Console.ReadKey();
            Console.WriteLine();

            manager.WithdrawalAccount(account2, 1000m);

            var accounts3 = manager.GetAccounts();

            foreach (var bankAccount in accounts3)
            {
                Console.WriteLine(bankAccount.ToString());
            }

            Console.ReadKey();
            Console.WriteLine();

            manager.CloseBankAccount(account2);

            var accounts4 = manager.GetAccounts();

            foreach (var bankAccount in accounts4)
            {
                Console.WriteLine(bankAccount.ToString());
            }

            Console.ReadKey();
            Console.WriteLine();

            manager.Save();
            manager.Reload();

            var accounts5 = manager.GetAccounts();

            foreach (var bankAccount in accounts5)
            {
                Console.WriteLine(bankAccount.ToString());
            }
            Console.ReadKey();
            File.Delete(path);
        }
    }
}
