using BooksList.Interfaces;
using BooksList.Models;
using System.Xml.Linq;

namespace BooksList.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        

        public async Task<List<Book>> GetBooksAsync()
        {
            try
            {
                return await _bookRepository.GetBooksAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting books: {ex.Message}");
                throw;
            }
        }
        public async Task AddBookAsync(Book book)
        {
            try
            {
                var books = await _bookRepository.GetBooksAsync();
                books.Add(book);

                await _bookRepository.SaveBooksAsync(books);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding book: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateBookAsync(string isbn, Book updatedBook)
        {
            try
            {
                var books = await _bookRepository.GetBooksAsync();
                var existingBook = books.FirstOrDefault(b => b.Isbn == isbn);

                if (existingBook != null)
                {
                    existingBook.Isbn = updatedBook.Isbn;
                    existingBook.Title = updatedBook.Title;
                    existingBook.Authors = updatedBook.Authors;
                    existingBook.Year = updatedBook.Year;
                    existingBook.Price = updatedBook.Price;
                    existingBook.Category = updatedBook.Category;
                    existingBook.Cover = updatedBook.Cover;

                    await _bookRepository.SaveBooksAsync(books);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating book: {ex.Message}");
                throw;
            }
        }
        public async Task DeleteBookAsync(string isbn)
        {
            try
            {
                var books = await _bookRepository.GetBooksAsync();
                var bookToDelete = books.FirstOrDefault(b => b.Isbn == isbn);

                if (bookToDelete != null)
                {
                    books.Remove(bookToDelete);
                    await _bookRepository.SaveBooksAsync(books);
                }
                else
                {
                    throw new Exception("Book not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting book: {ex.Message}");
                throw;
            }
        }
    }


}
