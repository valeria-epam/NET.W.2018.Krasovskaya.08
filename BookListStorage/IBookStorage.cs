using System.Collections.Generic;

namespace BookListStorage
{
    public interface IBookStorage
    {
        /// <summary>
        /// Appends book. 
        /// </summary>
        void AppendBook(Book book);

        /// <summary>
        /// Overwrites books. 
        /// </summary>
        void WriteBooks(IEnumerable<Book> books);

        /// <summary>
        /// Loads books. 
        /// </summary>
        IList<Book> LoadBooks();
    }
}