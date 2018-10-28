using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class AccountType
    {
        public string TypeName { get; set; }
        public int BalanceCost { get; set; }
        public int RefillCost { get; set; }

        public static AccountType BaseAccount { get; } = new AccountType() { TypeName = "Base", BalanceCost = 5, RefillCost = 2 };
        public static AccountType GoldAccount { get; } = new AccountType() { TypeName = "Gold", BalanceCost = 15, RefillCost = 10 };
        public static AccountType PlatinumAccount { get; } = new AccountType() { TypeName = "Platinum", BalanceCost = 10, RefillCost = 50 };

    }
}
