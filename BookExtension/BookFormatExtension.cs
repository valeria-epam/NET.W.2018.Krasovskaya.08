using BookListStorage;

namespace BookExtension
{
    public static class BookFormatExtension
    {
        /// <summary>
        /// Returns representation of book, including Name, Year, Price
        /// </summary>
        public static string ToNameYearPriceString(this Book book)
        {
            return "Book record: " + book.ToString("NY") + ", " + book.Price.ToString("C");
        }

        /// <summary>
        /// Returns representation of book, including Name, Author, Price
        /// </summary>
        public static string ToNameAuthorPriceString(this Book book)
        {
            return "Book record: " + book.ToString("NA") + ", " + book.Price.ToString("C");
        }

        /// <summary>
        /// Returns representation of book, including ISBN, Author, Name, Publisher, Year, Pages, Price
        /// </summary>
        public static string ToIsbnAuthorNamePublisherYearPagesPriceString(this Book book)
        {
            return "Book record: " + book.ToString("IANHYP") + ", " + book.Price.ToString("C");
        }
    }
}
