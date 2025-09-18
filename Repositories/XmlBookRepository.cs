using BooksList.Interfaces;
using BooksList.Models;
using System.Xml.Linq;

namespace BooksList.Repositories
{
    public class XmlBookRepository : IBookRepository
    {
        private readonly string _xmlFilePath;
        private static readonly object _lock = new();
        public XmlBookRepository(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
        }
        public async Task<List<Book>> GetBooksAsync()
        {
            var books = new List<Book>();
            var xDoc = await Task.Run(() => XDocument.Load(_xmlFilePath));
            lock (_lock)
            {
                try
                {
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
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading XML: {ex.Message}");
                    throw;
                }
            }
        }



        public async Task SaveBooksAsync(List<Book> books)
        {
            var xDoc = new XDocument(new XElement("bookstore"));
            lock (_lock)
            {
                try
                {
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

                }


                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving XML: {ex.Message}");
                    throw;
                }
            }
            await Task.Run(() => xDoc.Save(_xmlFilePath));
        }
    }


}
