using Library.Common;
using Library.Contracts;
using Library.Data.Models;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers
{
    [Authorize]

    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = new BooksAllViewModel() { Books = await _bookService.GetAllAsync() };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new BookAddViewModel()
            {
                Categories = await _bookService.GetCategoriesAsync()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(BookAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _bookService.AddBookAsync(model);

                return RedirectToAction(nameof(All));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", ErrorConstants.UNIVERSAL);

                return View(model);
            }
        }
        public async Task<IActionResult> AddToCollection(int bookId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                await _bookService.AddBookToCollectionAsync(bookId, userId);
            }
            catch (Exception)
            {

                throw;
            }             
            return RedirectToAction(nameof(All));
        }
        [HttpGet]

        public async Task<IActionResult> Mine()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var model = new BooksAllViewModel() { Books = await _bookService.GetMineAsync(userId) };

            return View("Mine", model);
        }
        public async Task<IActionResult> RemoveFromCollection(int bookId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await _bookService.RemoveBookFromCollectionAsync(bookId, userId);

            return RedirectToAction(nameof(Mine));
        }
    }
}
