using BooksList.Models;

namespace BooksList.Interfaces
{
    public interface IBookRepository
    {
        Task SaveBooksAsync(List<Book> books);
        Task<List<Book>> GetBooksAsync();

    }
}
