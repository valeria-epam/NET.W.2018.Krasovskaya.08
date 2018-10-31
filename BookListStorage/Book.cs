using System;
using System.Globalization;
using System.Linq;

namespace BookListStorage
{
    /// <summary>
    /// Represents a book.
    /// </summary>
    public class Book : IEquatable<Book>, IComparable<Book>
    {
        private static readonly char[] SimpleFormats = new[] { 'N', 'Y', 'H', 'I', 'P', 'A' };

        public string ISBN { get; set; }

        public string Author { get; set; }

        public string Name { get; set; }

        public string PublishingOffice { get; set; }

        public int Year { get; set; }

        public int NumberOfPages { get; set; }

        public decimal Price { get; set; }

        public static bool operator ==(Book left, Book right)
        {
            return object.Equals(left, right);
        }

        public static bool operator !=(Book left, Book right)
        {
            return !object.Equals(left, right);
        }

        /// <inheritdoc />
        public bool Equals(Book other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(ISBN, other.ISBN) && string.Equals(Author, other.Author)
                   && string.Equals(Name, other.Name) && string.Equals(PublishingOffice, other.PublishingOffice)
                   && Year == other.Year && NumberOfPages == other.NumberOfPages && Price == other.Price;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((Book)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ISBN != null ? ISBN.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Author != null ? Author.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PublishingOffice != null ? PublishingOffice.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Year;
                hashCode = (hashCode * 397) ^ NumberOfPages;
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                return hashCode;
            }
        }

        /// <inheritdoc />
        public int CompareTo(Book other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

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

        /// <summary>
        /// Returns representation of book.
        /// <list type="bullet">
        /// <item>
        /// <term>"V"</term>
        /// <description>Returns representation of book, including Author,Name,Year,PublishingOffice</description>
        /// </item>
        /// <item>
        /// <term>"B"</term>
        /// <description>Returns representation of book, including Author,Name,Year</description>
        /// </item>
        /// <item>
        /// <term>"S"</term>
        /// <description>Returns representation of book, including Author,Name</description>
        /// </item>
        /// <item>
        /// <term>"L"</term>
        /// <description>Returns representation of book, including Name,Year,PublishingOffice</description>
        /// </item>
        /// <item>
        /// <term>"D"</term>
        /// <description>Returns representation of book, including Name,Author,Price</description>
        /// </item>
        /// <item>
        /// <term>"A"</term>
        /// <description>Returns representation of book, including only Author</description>
        /// </item>
        /// <item>
        /// <term>"N"</term>
        /// <description>Returns representation of book, including only Name</description>
        /// </item>
        /// <item>
        /// <term>"Y"</term>
        /// <description>Returns representation of book, including only Year</description>
        /// </item>
        /// <item>
        /// <term>"H"</term>
        /// <description>Returns representation of book, including only PublishingOffice</description>
        /// </item>
        /// <item>
        /// <term>"I"</term>
        /// <description>Returns representation of book, including only ISBN</description>
        /// </item>
        /// <item>
        /// <term>"P"</term>
        /// <description>Returns representation of book, including only Pages</description>
        /// </item>
        /// </list>
        /// </summary>
        public string ToString(string format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns representation of book, including <see cref="formatProvider"/>
        /// <list type="bullet">
        /// <item>
        /// <term>"V"</term>
        /// <description>Returns representation of book, including Author,Name,Year,PublishingOffice</description>
        /// </item>
        /// <item>
        /// <term>"B"</term>
        /// <description>Returns representation of book, including Author,Name,Year</description>
        /// </item>
        /// <item>
        /// <term>"S"</term>
        /// <description>Returns representation of book, including Author,Name</description>
        /// </item>
        /// <item>
        /// <term>"L"</term>
        /// <description>Returns representation of book, including Name,Year,PublishingOffice</description>
        /// </item>
        /// <item>
        /// <term>"A"</term>
        /// <description>Returns representation of book, including only Author</description>
        /// </item>
        /// <item>
        /// <term>"N"</term>
        /// <description>Returns representation of book, including only Name</description>
        /// </item>
        /// <item>
        /// <term>"Y"</term>
        /// <description>Returns representation of book, including only Year</description>
        /// </item>
        /// <item>
        /// <term>"H"</term>
        /// <description>Returns representation of book, including only PublishingOffice</description>
        /// </item>
        /// <item>
        /// <term>"I"</term>
        /// <description>Returns representation of book, including only ISBN</description>
        /// </item>
        /// <item>
        /// <term>"P"</term>
        /// <description>Returns representation of book, including only Pages</description>
        /// </item>
        /// </list>
        /// </summary>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "V";
            }

            if (formatProvider == null)
            {
                formatProvider = CultureInfo.CurrentCulture;
            }

            switch (format.ToUpperInvariant())
            {
                case "A":
                    return Author?.ToString(formatProvider) ?? string.Empty;

                case "N":
                    return Name?.ToString(formatProvider) ?? string.Empty;

                case "Y":
                    return Year.ToString(formatProvider);

                case "H":
                    return PublishingOffice != null ? "\"" + PublishingOffice?.ToString(formatProvider) + "\"" : string.Empty;

                case "I":
                    return "ISBN 13: " + ISBN.ToString(formatProvider);

                case "P":
                    return "P. " + NumberOfPages.ToString(formatProvider);

                case "V":
                    return Author?.ToString(formatProvider) + ", " + Name?.ToString(formatProvider) + ", "
                           + Year.ToString(formatProvider) + ", " + PublishingOffice?.ToString(formatProvider);
                case "B":
                    return Author?.ToString(formatProvider) + ", " + Name?.ToString(formatProvider) + ", "
                           + Year.ToString(formatProvider);
                case "S":
                    return Author?.ToString(formatProvider) + ", " + Name?.ToString(formatProvider);

                case "L":
                    return Name?.ToString(formatProvider) + ", " + Year.ToString(formatProvider) + ", "
                           + PublishingOffice?.ToString(formatProvider);

                case string str when !str.Except(SimpleFormats).Any():
                    return string.Join(", ", format.Select(c => this.ToString(c.ToString(), formatProvider)));

                default:
                    throw new FormatException($"The {format} format string is not supported.");
            }
        }
    }
}
