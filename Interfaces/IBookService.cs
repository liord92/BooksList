using BooksList.Models;

namespace BooksList.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetBooksAsync();
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(string isbn, Book updatedBook);
        Task DeleteBookAsync(string isbn);
    }
}
