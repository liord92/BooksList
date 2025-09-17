using BooksList.Interfaces;
using BooksList.Models;
using System.Xml.Linq;

namespace BooksList.Services
{
    public class BookService : IBookService
    {
        private readonly string _xmlFilePath;

        public BookService(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
        }
        public async Task<List<Book>> GetBooksAsync()
        {
            var books = new List<Book>();
            var xDoc = await Task.Run(() => XDocument.Load(_xmlFilePath));

            var bookElements = xDoc.Descendants("book");
            foreach (var bookElement in bookElements)
            {
                var book = new Book
                {
                    Isbn = bookElement.Element("isbn")?.Value,
                    Title = bookElement.Element("title")?.Value,
                    Year = int.Parse(bookElement.Element("year")?.Value ?? "0"),
                    Price = decimal.Parse(bookElement.Element("price")?.Value ?? "0"),
                    Category = bookElement.Attribute("category")?.Value,
                    Cover = bookElement.Attribute("cover")?.Value
                };

                var authors = bookElement.Elements("author").Select(a => a.Value).ToList();
                book.Authors.AddRange(authors);

                books.Add(book);
            }
            return books;
        }

        public async Task AddBookAsync(Book book)
        {
            var books = await GetBooksAsync();
            books.Add(book);

            await SaveBooksAsync(books);
        }

        public async Task UpdateBookAsync(string isbn, Book updatedBook)
        {
            var books = await GetBooksAsync();
            var existingBook = books.FirstOrDefault(b => b.Isbn == isbn);

            if (existingBook != null)
            {
                existingBook.Title = updatedBook.Title;
                existingBook.Authors = updatedBook.Authors;
                existingBook.Year = updatedBook.Year;
                existingBook.Price = updatedBook.Price;
                existingBook.Category = updatedBook.Category;
                existingBook.Cover = updatedBook.Cover;

                await SaveBooksAsync(books);
            }
        }
        public async Task DeleteBookAsync(string isbn)
        {
            var books = await GetBooksAsync();
            var bookToDelete = books.FirstOrDefault(b => b.Isbn == isbn);

            if (bookToDelete != null)
            {
                books.Remove(bookToDelete);
                await SaveBooksAsync(books);
            }
        }

        private async Task SaveBooksAsync(List<Book> books)
        {
            var xDoc = new XDocument(new XElement("bookstore"));

            foreach (var book in books)
            {
                var bookElement = new XElement("book", new XAttribute("category", book.Category));

                bookElement.Add(new XElement("isbn", book.Isbn));
                bookElement.Add(new XElement("title", new XAttribute("lang", "en"), book.Title));

                foreach (var author in book.Authors)
                {
                    bookElement.Add(new XElement("author", author));
                }

                bookElement.Add(new XElement("year", book.Year));
                bookElement.Add(new XElement("price", book.Price));

                if (!string.IsNullOrEmpty(book.Cover))
                {
                    bookElement.Add(new XAttribute("cover", book.Cover));
                }

                xDoc.Element("bookstore").Add(bookElement);
            }

            await Task.Run(() => xDoc.Save(_xmlFilePath));
        }

    }
}
