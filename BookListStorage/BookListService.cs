using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListStorage
{
    /// <summary>
    /// Class for managing book information.
    /// </summary>
    public class BookListService
    {
        private IList<Book> _booksList;

        private readonly string _path;

        /// <summary>
        /// Initializes a new instanse of <see cref="BookListService"/>. 
        /// </summary>
        public BookListService(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Adds a new book if it doesn't exist.
        /// </summary>
        public void AddBook(Book book)
        {
            if (_booksList == null)
            {
                _booksList = LoadBooks();
            }

            bool hasBook = _booksList.Any(item => book == item);

            if (hasBook)
                throw new Exception("Our book storage already had this book.");

            WriteBook(book);
            _booksList.Add(book);
        }

        /// <summary>
        /// Remove book if it exists.
        /// </summary>
        public void RemoveBook(Book book)
        {
            if (_booksList == null)
            {
                _booksList = LoadBooks();
            }

            if (_booksList.Count > 0)
            {
                bool hasBook = false;
                int position;
                for (position = 0; position < _booksList.Count; position++)
                {
                    if (_booksList[position] == book)
                    {
                        hasBook = true;
                        break;
                    }
                }

                if (hasBook)
                {
                    _booksList.RemoveAt(position);
                    File.Delete(_path);
                    WriteBooks(_booksList);
                    return;
                }
            }

            throw new Exception("We don't have this book, so you cannot remove it.");
        }

        /// <summary>
        /// Find the book by the specified <paramref name="criterion"/>.
        /// </summary>
        public Book FindBookByTag(ICriterion criterion)
        {
            if (_booksList == null)
            {
                _booksList = LoadBooks();
            }

            foreach (var book in _booksList)
            {
                if (criterion.IsMatch(book))
                {
                    return book;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the list of books, sorted with the specified <paramref name="comparer"/>.
        /// </summary>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public IEnumerable<Book> SortBooksByTag(IComparer<Book> comparer)
        {
            if (_booksList == null)
            {
                _booksList = LoadBooks();
            }

            return _booksList.OrderBy(x => x, comparer);
        }

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> GetBooks()
        {
            if (_booksList == null)
            {
                _booksList = LoadBooks();
            }

            return _booksList;
        }

        private void WriteBooks(IEnumerable<Book> books)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(_path, FileMode.OpenOrCreate)))
            {
                foreach (var book in books)
                {
                    WriteBook(book, writer);
                }
            }
        }

        private void WriteBook(Book book)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(_path, FileMode.OpenOrCreate)))
            {
                writer.Seek(0, SeekOrigin.End);
                WriteBook(book, writer);
            }
        }

        private static void WriteBook(Book book, BinaryWriter writer)
        {
            writer.Write(book.ISBN);
            writer.Write(book.Author);
            writer.Write(book.Name);
            writer.Write(book.PublishingOffice);
            writer.Write(book.Year);
            writer.Write(book.NumberOfPages);
            writer.Write(book.Price);
        }

        private IList<Book> LoadBooks()
        {
            IList<Book> books = new List<Book>();
            using (BinaryReader reader = new BinaryReader(File.Open(_path, FileMode.OpenOrCreate)))
            {
                while (reader.PeekChar() > -1)
                {
                    string isbn = reader.ReadString();
                    string author = reader.ReadString();
                    string name = reader.ReadString();
                    string publishingOffice = reader.ReadString();
                    int year = reader.ReadInt32();
                    int numberOfPages = reader.ReadInt32();
                    decimal price = reader.ReadDecimal();

                    var book = new Book
                    {
                        Author = author,
                        Price = price,
                        ISBN = isbn,
                        NumberOfPages = numberOfPages,
                        Year = year,
                        Name = name,
                        PublishingOffice = publishingOffice
                    };

                    books.Add(book);
                }
            }

            return books;
        }
    }
}
