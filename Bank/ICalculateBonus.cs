namespace Bank
{
    /// <summary>
    /// Interface logic for calculation of bonuses.
    /// </summary>
    public interface ICalculateBonus
    {
        /// <summary>
        /// Calculates bonus for the refill operation and updates the account bonus.
        /// </summary>
        void RefillBonus(BankAccount account, decimal amountOfMoney);

        /// <summary>
        /// Calculates bonus for the withdraw operation and updates the account bonus.
        /// </summary>
        void WithdrawalBonus(BankAccount account, decimal amountOfMoney);
    }
}