using System;
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
            var expected = $"Author: {book.Author}, Price: {book.Price:C0}";
            Assert.AreEqual(expected, string.Format(new BookFormatExtension(), "{0:R}", book));
        }

        [Test]
        public void FormatExceptionTest()
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

            Assert.Throws<FormatException>(() => string.Format(new BookFormatExtension(), "{0:W}", book));
        }
    }
}