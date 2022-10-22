using Library.Data.Models;
using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task<List<BookViewModel>> GetAllAsync();
        Task<List<Category>> GetCategoriesAsync();
        Task AddBookAsync(BookAddViewModel model);
        Task AddBookToCollectionAsync(int bookId, string userId);
        Task<List<BookViewModel>> GetMineAsync(string userId);
        Task RemoveBookFromCollectionAsync(int bookId, string userId);
    }
}
