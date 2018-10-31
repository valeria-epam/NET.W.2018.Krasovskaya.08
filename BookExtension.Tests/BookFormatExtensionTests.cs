using BookListStorage;
using NUnit.Framework;

namespace BookExtension.Tests
{
    [TestFixture]
    public class BookFormatExtensionTests
    {
        [Test]
        public void RepresentationTest()
        {
            var book = new Book()
            {
                Name = "CLR via C#",
                Year = 2012,
                PublishingOffice = "Microsoft Press",
                Author = "Jeffrey Richter",
                ISBN = "978-0-7356-6745-7",
                NumberOfPages = 826,
                Price = 59.99m
            };
            var expected = "Book record: CLR via C#, Jeffrey Richter, $59.99";
            Assert.AreEqual(expected, book.ToNameAuthorPriceString());
        }

        [Test]
        public void RepresentationTest1()
        {
            var book = new Book()
            {
                Name = "CLR via C#",
                Year = 2012,
                PublishingOffice = "Microsoft Press",
                Author = "Jeffrey Richter",
                ISBN = "978-0-7356-6745-7",
                NumberOfPages = 826,
                Price = 59.99m
            };
            var expected = "Book record: CLR via C#, 2012, $59.99";
            Assert.AreEqual(expected, book.ToNameYearPriceString());
        }

        [Test]
        public void RepresentationTest2()
        {
            var book = new Book()
            {
                Name = "CLR via C#",
                Year = 2012,
                PublishingOffice = "Microsoft Press",
                Author = "Jeffrey Richter",
                ISBN = "978-0-7356-6745-7",
                NumberOfPages = 826,
                Price = 59.99m
            };
            var expected = "Book record: ISBN 13: 978-0-7356-6745-7, Jeffrey Richter, CLR via C#, \"Microsoft Press\", 2012, P. 826, $59.99";
            Assert.AreEqual(expected, book.ToIsbnAuthorNamePublisherYearPagesPriceString());
        }
    }
}
