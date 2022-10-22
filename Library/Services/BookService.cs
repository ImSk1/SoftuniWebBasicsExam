using Library.Common;
using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;     
        }
        public async Task<List<BookViewModel>> GetAllAsync()
        {
            var entities = await _context.Books
                .Include(m => m.Category)
                .ToListAsync();

            return entities
                .Select(m => new BookViewModel()
                {
                    Description = m.Description,
                    Author = m.Author,
                    Category = m?.Category?.Name,
                    Id = m.Id,
                    ImageUrl = m.ImageUrl,
                    Rating = m.Rating,
                    Title = m.Title
                }).ToList();
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task AddBookAsync(BookAddViewModel model)
        {
            var entity = new Book()
            {
                Description = model.Description,
                Author = model.Author,
                CategoryId = model.CategoryId,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                Title = model.Title
            };

            await _context.Books.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddBookToCollectionAsync(int bookId, string userId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.ApplicationUsersBooks)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException(ErrorConstants.INVALID_USERID);
            }

            var book = await _context.Books.FirstOrDefaultAsync(u => u.Id == bookId);

            if (book == null)
            {
                throw new ArgumentException(ErrorConstants.INVALID_BOOKID);
            }

            if (!user.ApplicationUsersBooks.Any(m => m.BookId == bookId))
            {
                user.ApplicationUsersBooks.Add(new ApplicationUserBook()
                {
                    BookId = book.Id,
                    ApplicationUserId = user.Id,
                    Book = book,
                    ApplicationUser = user
                });

                await _context.SaveChangesAsync();
            }
            
        }
        public async Task<List<BookViewModel>> GetMineAsync(string userId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.ApplicationUsersBooks)
                .ThenInclude(aub => aub.Book)
                .ThenInclude(b => b.Category)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException(ErrorConstants.INVALID_USERID);
            }

            return user.ApplicationUsersBooks
                .Select(b => new BookViewModel()
                {
                    Description = b.Book.Description,
                    Author = b.Book.Author,
                    Category = b?.Book.Category?.Name,
                    Id = b.Book.Id,
                    ImageUrl = b.Book.ImageUrl,
                    Rating = b.Book.Rating,
                    Title = b.Book.Title
                }).ToList();
        }
        public async Task RemoveBookFromCollectionAsync(int bookId, string userId)
        {
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.ApplicationUsersBooks)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException(ErrorConstants.INVALID_USERID);
            }

            var book = user.ApplicationUsersBooks.FirstOrDefault(b => b.BookId == bookId);

            if (book != null)
            {
                user.ApplicationUsersBooks.Remove(book);

                await _context.SaveChangesAsync();
            }
        }
    }
}
