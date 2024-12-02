using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MVCLibraryManage.Models;
using MVCLibraryManage.Models.Entity;
using MVCLibraryManage.Service;
using MVCLibraryManage.Service.Implements;
using System.Drawing.Printing;
using System.Linq;

namespace MVCLibraryManage.Controllers
{
    public class BorrowerController : Controller
    {
        private readonly ILibraryItemService _libraryItemService;
        private readonly IBorrowerService _borrowerService;
        private IBorrowingHistoryService _borrowingHistoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BorrowerController(ILibraryItemService libraryItemService, IBorrowerService borrowerService, IHttpContextAccessor httpContextAccessor, IBorrowingHistoryService borrowingHistoryService)
        {
            _libraryItemService = libraryItemService;
            _borrowerService = borrowerService;
            _httpContextAccessor = httpContextAccessor;
            _borrowingHistoryService = borrowingHistoryService;
        }

        public IActionResult Menu(int page = 1, int pageSize = 4)
        {
            var itemsQuery = _libraryItemService.GetAllLibraryItem();
            var totalItems = itemsQuery.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            page = Math.Max(1, Math.Min(page, totalPages));
            var menu = itemsQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pagingModel = new PagingModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                GenerateUrl = p => Url.Action("Menu", new { page = p, pageSize })
            };

            var viewModel = new LibraryManageViewModel
            {
                Items = menu,
                PagingModel = pagingModel
            };

            return View(viewModel);
        }

        public IActionResult FilteredLibraryItems(string sortBy, string search, int page = 1, int pageSize = 4)
        {
            bool isDescending = sortBy?.EndsWith("desc") ?? false;
            string arrange = sortBy?.Replace("-desc", "").Replace("-asc", "") ?? "all";

            var items = _libraryItemService.GetListLibraryItem(arrange, isDescending, search);

            var totalItems = items.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            page = page < 1 ? 1 : page;
            page = page > totalPages ? totalPages : page;

            var pagedItems = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pagingModel = new PagingModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                GenerateUrl = p => Url.Action("Menu", new { page = p, pageSize })
            };

            var viewModel = new LibraryManageViewModel
            {
                Items = pagedItems,
                PagingModel = pagingModel
            };

            ViewBag.sortBy = sortBy;
            ViewBag.searchText = search;

            return View("Menu", viewModel);
        }

        public IActionResult BorrowStatus(int page = 1, int pageSize = 5)
        {
            string userIdString = _httpContextAccessor.HttpContext?.Session.GetString("Id");
            int userId = int.Parse(userIdString);
            var libraryCardId = _borrowerService.GetBorrowerID(userId);

            var borrowingHistoryQuery = _borrowingHistoryService.GetBorrowingHistoryForUser(libraryCardId).Where(bh => bh.ReturnDate == null)
                .ToList(); 

            var totalItems = borrowingHistoryQuery.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            page = Math.Max(1, Math.Min(page, totalPages));

            var borrowingHistory = borrowingHistoryQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pagingModel = new PagingModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                GenerateUrl = p => Url.Action("BorrowStatus", new { page = p, pageSize })
            };

            var viewModel = new BorrowStatusViewModel
            {
                BorrowingHistory = borrowingHistory,
                PagingModel = pagingModel
            };

            return View(viewModel);
        }

        public IActionResult AddToCart(int id)
        {
            try
            {
                // Retrieve session value for "Id"
                string? accountIdString = _httpContextAccessor.HttpContext?.Session.GetString("Id");

                // Check if user is logged in
                if (string.IsNullOrEmpty(accountIdString))
                {
                    return Json(new { success = false, redirectToPage = true, message = "User not logged in" });
                }

                // Try to parse the session ID
                if (!int.TryParse(accountIdString, out int accountID))
                {
                    return Json(new { success = false, redirectToPage = true, message = "Invalid session ID" });
                }

                // Retrieve the library card ID for the borrower
                var libraryCardId = _borrowerService.GetBorrowerID(accountID);
                if (libraryCardId == null)
                {
                    return Json(new { success = false, redirectToPage = true, message = "Borrower ID not found" });
                }

                // Retrieve the library item based on the provided ID
                var libraryItem = _libraryItemService.GetLibraryItemById(id);
                if (libraryItem == null)
                {
                    return Json(new { success = false, redirectToPage = false, message = "Item not found" });
                }

                // Create a borrowing history record
                var borrowingHistory = new BorrowingHistory
                {
                    LibraryCardId = libraryCardId,
                    LibraryItemId = libraryItem.Id,
                    BorrowDate = DateTime.Now,
                    ReturnDate = null
                };

                // Add the borrowing record
                _borrowingHistoryService.AddBorrowingRecord(borrowingHistory);

                // Return success response
                return Json(new { success = true, redirectToPage = false, message = "Item successfully added to borrowing cart" });
            }
            catch (Exception ex)
            {
                // Log the error for debugging purposes (optional)
                Console.WriteLine($"Error in AddToCart: {ex.Message}");

                // Return an error response
                return Json(new { success = false, redirectToPage = false, message = "An unexpected error occurred" });
            }
        }


    }
}
