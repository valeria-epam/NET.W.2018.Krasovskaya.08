using NUnit.Framework;

namespace Book.Tests
{
    [TestFixture]
    public class BookTest
    {
        [TestCase("V", ExpectedResult = "Jeffrey Richter, CLR via C#, 2012, Microsoft Press")]
        [TestCase("L", ExpectedResult = "CLR via C#, 2012, Microsoft Press")]
        [TestCase("S", ExpectedResult = "Jeffrey Richter, CLR via C#")]
        public string RepresentationTest(string format)
        {
            var book = new BookListStorage.Book
            {
                Name = "CLR via C#",
                Year = 2012,
                PublishingOffice = "Microsoft Press",
                Author = "Jeffrey Richter",
                ISBN = "978-0-7356-6745-7",
                NumberOfPages = 826,
                Price = 59.99m
            };
            return book.ToString(format);
        }
    }
}
