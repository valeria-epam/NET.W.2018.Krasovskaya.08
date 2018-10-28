using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BookListStorage;

namespace BookListStorageTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Initialize
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
                Name = "Алгоритмы для начинающих",
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
            var service = new BookListService(path);
            #endregion

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
        class NameCriterion : ICriterion
        {
            public NameCriterion(string name)
            {
                _name = name;
            }

            private readonly string _name;
            public bool IsMatch(Book book)
            {
                return book?.Name == _name;
            }
        }
        #endregion

    }
}
