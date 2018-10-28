using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class BankAccount : IEquatable<BankAccount>
    {

        public string Number { get; set; }
        public AccountOwner Owner { get; set; }
        public decimal Sum { get; set; }
        public decimal Bonus { get; set; }
        public AccountType AccountType { get; set; }
        public AccountState State { get; set; }

        public bool Equals(BankAccount other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Number, other.Number) &&
                   Equals(Owner, other.Owner) && Sum == other.Sum &&
                   Bonus == other.Bonus && Equals(AccountType, other.AccountType) &&
                   State == other.State;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BankAccount)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Number != null ? Number.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Owner != null ? Owner.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Sum.GetHashCode();
                hashCode = (hashCode * 397) ^ Bonus.GetHashCode();
                hashCode = (hashCode * 397) ^ (AccountType != null ? AccountType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)State;
                return hashCode;
            }
        }

        public static bool operator ==(BankAccount left, BankAccount right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BankAccount left, BankAccount right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"{nameof(Number)}: {Number}, {nameof(Owner)}: {Owner}, {nameof(Sum)}: {Sum}," +
                   $" {nameof(Bonus)}: {Bonus}, {nameof(AccountType)}: {AccountType}, {nameof(State)}: {State}";
        }
    }


}
