using System;
using System.Collections.Generic;
using System.Linq;

namespace BookListStorage
{
    /// <summary>
    /// Class for managing book information.
    /// </summary>
    public class BookListService
    {
        private readonly IBookStorage _storage;
        private IList<Book> _booksList;

        /// <summary>
        /// Initializes a new instance of <see cref="BookListService"/>. 
        /// </summary>
        public BookListService(IBookStorage storage)
        {
            this._storage = storage;
        }

        /// <summary>
        /// Adds a new book if it doesn't exist.
        /// </summary>
        public void AddBook(Book book)
        {
            if (_booksList == null)
            {
                _booksList = _storage.LoadBooks();
            }

            bool hasBook = _booksList.Any(item => book == item);

            if (hasBook)
            {
                throw new Exception("Our book storage already had this book.");
            }

            _storage.AppendBook(book);
            _booksList.Add(book);
        }

        /// <summary>
        /// Remove book if it exists.
        /// </summary>
        public void RemoveBook(Book book)
        {
            if (_booksList == null)
            {
                _booksList = _storage.LoadBooks();
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
                    _storage.WriteBooks(_booksList);
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
                _booksList = _storage.LoadBooks();
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
                _booksList = _storage.LoadBooks();
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
                _booksList = _storage.LoadBooks();
            }

            return _booksList;
        }
    }
}
