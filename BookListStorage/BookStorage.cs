using System.Collections.Generic;
using System.IO;
using Common.Logging;

namespace BookListStorage
{
    public class BookStorage : IBookStorage
    {
        private static readonly ILog Log = LogManager.GetLogger<BookStorage>();
        private readonly string _path;

        /// <summary>
        /// Initializes a new instance of <see cref="BookStorage"/>. 
        /// </summary>
        public BookStorage(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Appends book to the file. 
        /// </summary>
        public void AppendBook(Book book)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(_path, FileMode.OpenOrCreate)))
            {
                writer.Seek(0, SeekOrigin.End);
                WriteBook(book, writer);
            }
        }

        /// <summary>
        /// Overwrites books in the file. 
        /// </summary>
        public void WriteBooks(IEnumerable<Book> books)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(_path, FileMode.Create)))
            {
                foreach (var book in books)
                {
                    WriteBook(book, writer);
                }
            }
        }

        /// <summary>
        /// Loads books from the file. 
        /// </summary>
        public IList<Book> LoadBooks()
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

            Log.Debug("Books were loaded from file.");
            return books;
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

            Log.Debug("Books were written to file.");
        }
    }
}