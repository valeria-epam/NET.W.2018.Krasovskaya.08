using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BookListStorage;

namespace BookListStorageTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var book1 = new Book()
            {
                Author = "Джеффри Рихтер",
                Name = "CLR via C#",
                NumberOfPages = 896,
                PublishingOffice = "Питер",
                Year = 2017,
                Price = 67.50m,
                ISBN = "978-5-496-00433-6"
            };

            var book2 = new Book()
            {
                Author = "Панос Луридас",
                Name = "Aлгоритмы для начинающих",
                NumberOfPages = 608,
                PublishingOffice = "Эксмо",
                Year = 2018,
                Price = 45.50m,
                ISBN = "978-5-04-089834-3"
            };

            var appFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var storageFolder = Path.Combine(appFolder, "Storage");
            Directory.CreateDirectory(storageFolder);
            var path = Path.Combine(storageFolder, "BookListStorage");

            var storage = new BookStorage(path);
            var service = new BookListService(storage);

            service.AddBook(book1);
            service.AddBook(book2);

            foreach (var book in service.GetBooks())
            {
                Console.WriteLine(book.ToString());
            }

            Console.ReadKey();
            Console.WriteLine();

            service.RemoveBook(book2);

            var criterion = new NameCriterion("CLR via C#");
            var book3 = service.FindBookByTag(criterion);
            Console.WriteLine(book3.ToString());
            Console.ReadKey();
            Console.WriteLine();

            service.AddBook(book2);
            var books = service.SortBooksByTag(Comparer<Book>.Default);
            foreach (var book in books)
            {
                Console.WriteLine(book.ToString());
            }

            Console.ReadKey();
            File.Delete(path);
        }

        #region Nested class
        private class NameCriterion : ICriterion
        {
            private readonly string _name;

            public NameCriterion(string name)
            {
                _name = name;
            }

            public bool IsMatch(Book book)
            {
                return book?.Name == _name;
            }
        }
        #endregion
    }
}
