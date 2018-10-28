using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class CalculateBonus : ICalculateBonus
    {
        public void RefillBonus(BankAccount account, decimal amountOfMoney)
        {
            account.Bonus += amountOfMoney * account.AccountType.RefillCost / 100;
        }

        public void WithdrawalBonus(BankAccount account, decimal amountOfMoney)
        {
            account.Bonus -= amountOfMoney * account.AccountType.BalanceCost / 100;
        }
    }
}
