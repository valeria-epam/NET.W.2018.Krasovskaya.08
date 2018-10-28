using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank;

namespace BankTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var owner = new AccountOwner()
            {
                Name = "Ivan",
                Surname = "Ivanov"
            };

            var type = AccountType.BaseAccount;
        }
    }
}
