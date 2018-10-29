using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListStorage
{
    /// <summary>
    /// Represents a book.
    /// </summary>
    public class Book : IEquatable<Book>, IComparable<Book>
    {
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public string PublishingOffice { get; set; }
        public int Year { get; set; }
        public int NumberOfPages { get; set; }
        public decimal Price { get; set; }


        /// <inheritdoc />
        public bool Equals(Book other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(ISBN, other.ISBN) && string.Equals(Author, other.Author)
                   && string.Equals(Name, other.Name) && string.Equals(PublishingOffice, other.PublishingOffice)
                   && Year == other.Year && NumberOfPages == other.NumberOfPages && Price == other.Price;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Book)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ISBN != null ? ISBN.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Author != null ? Author.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PublishingOffice != null ? PublishingOffice.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Year;
                hashCode = (hashCode * 397) ^ NumberOfPages;
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Book left, Book right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Book left, Book right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public int CompareTo(Book other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(ISBN)}: {ISBN}, {nameof(Author)}: {Author}, " +
                   $"{nameof(Name)}: {Name}, {nameof(PublishingOffice)}: {PublishingOffice}," +
                   $" {nameof(Year)}: {Year}, {nameof(NumberOfPages)}: {NumberOfPages}," +
                   $" {nameof(Price)}: {Price:C}";
        }
    }
}
