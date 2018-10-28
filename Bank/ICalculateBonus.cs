namespace Bank
{
    public interface ICalculateBonus
    {
        void RefillBonus(BankAccount account, decimal amountOfMoney);
        void WithdrawalBonus(BankAccount account, decimal amountOfMoney);
    }
}