namespace Bank
{
    /// <summary>
    /// Represents logic for calculation of bonuses.
    /// </summary>
    public class CalculateBonus : ICalculateBonus
    {
        /// <summary>
        /// Calculates bonus for the refill operation and updates the account bonus.
        /// </summary>
        public void RefillBonus(BankAccount account, decimal amountOfMoney)
        {
            account.Bonus += amountOfMoney * account.AccountType.RefillCost / 100;
        }

        /// <summary>
        /// Calculates bonus for the withdraw operation and updates the account bonus.
        /// </summary>
        public void WithdrawalBonus(BankAccount account, decimal amountOfMoney)
        {
            account.Bonus -= amountOfMoney * account.AccountType.BalanceCost / 100;

            if (account.Bonus < 0)
            {
                account.Bonus = 0m;
            }
        }
    }
}
