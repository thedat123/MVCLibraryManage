using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using MVCLibraryManage.Models;
using MVCLibraryManage.Models.Entity;
using MVCLibraryManage.Service;
using MVCLibraryManage.Service.Implements;
using System.Globalization;

namespace MVCLibraryManage.Controllers
{
    public class StaffController : Controller
    {
        private readonly ILibraryItemService _libraryItemService;
        private readonly IWebHostEnvironment _webHostEnvironment;
		private IBorrowingHistoryService _borrowingHistoryService;
        private IBorrowerService _borrowerService;

		public StaffController(ILibraryItemService libraryItemService, IWebHostEnvironment webHostEnvironment, IBorrowingHistoryService borrowingHistoryService, IBorrowerService borrowerService)
        {
            _libraryItemService = libraryItemService;
            _webHostEnvironment = webHostEnvironment;
			_borrowingHistoryService = borrowingHistoryService;
            _borrowerService = borrowerService;
		}

        public int currentPage { get; set; }

        public IActionResult ManageLibraryItem(int page = 1, int pageSize = 3)
        {
            var itemsQuery = _libraryItemService.GetAllLibraryItem();

            // Calculate total items and total pages
            var totalItems = itemsQuery.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Validate the current page
            page = Math.Max(1, Math.Min(page, totalPages));

            // Get the items for the current page
            var items = itemsQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Prepare the PagingModel
            var pagingModel = new PagingModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                GenerateUrl = p => Url.Action("ManageLibraryItem", new { page = p, pageSize })
            };

            // Pass the data to the view
            var viewModel = new LibraryManageViewModel
            {
                Items = items,
                PagingModel = pagingModel
            };

            return View(viewModel);
        }

        public IActionResult FilteredLibraryItems(string sortBy, string search, int page = 1, int pageSize = 3)
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
                GenerateUrl = p => Url.Action("ManageLibraryItem", new { page = p, pageSize })
            };

            var viewModel = new LibraryManageViewModel
            {
                Items = pagedItems,
                PagingModel = pagingModel
            };

            ViewBag.sortBy = sortBy;
            ViewBag.searchText = search;

            return View("ManageLibraryItem", viewModel);
        }

        public IActionResult AddItem()
        {

            return View();
        }

        [HttpGet]
        public IActionResult GetItem(int id)
        {
            var item = _libraryItemService.GetLibraryItemById(id);
            if (item == null)
            {
                return NotFound();
            }

            var itemType = item.GetType().Name;
            if (itemType == "Book")
            {
                var bookItem = (Book)item;
                return Json(new { item = bookItem, type = itemType });
            }
            else if (itemType == "DVD")
            {
                var dvdItem = (DVD)item;
                return Json(new { item = dvdItem, type = itemType });
            }
            else if (itemType == "Magazine")
            {
                var magazineItems = (Magazine)item; 
                return Json(new { item = magazineItems, type = itemType });
            }
            return Json(new { item = item, type = itemType });
        }

        [HttpPost]
        public IActionResult Update(LibraryItemDTO libraryItem, IFormFile Image)
        {
            var existingItem = _libraryItemService.GetLibraryItemById(libraryItem.Id);
            if (existingItem == null)
            {
                return Json(new { success = false, message = "Item not found" });
            }

            // Update fields
            existingItem.Title = libraryItem.Title;
            existingItem.Author = libraryItem.Author;
            existingItem.PublicationDate = libraryItem.PublicationDate;

            // Handle specific fields for Book and DVD
            if (existingItem is Book book)
            {
                book.NumberOfPage = libraryItem.NumberOfPages;
            }
            else if (existingItem is DVD dvd)
            {
                dvd.Runtime = libraryItem.Runtime;
            }

            // Handle image upload
            if (Image != null && Image.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img", "upload");
                Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }

                existingItem.ImagePath = $"/img/upload/{uniqueFileName}";
            }

            // Save changes
            _libraryItemService.UpdateData(libraryItem.Id, existingItem);

            return RedirectToAction("ManageLibraryItem");
        }


        [HttpPost]
        public IActionResult Add(LibraryItemDTO libraryItem)
        {
            string imagePath = null;

            // Kiểm tra FileUpload
            if (libraryItem.FileUpload != null)
            {
                // Xử lý file upload
                try
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(libraryItem.FileUpload.FileName);
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "upload");

                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    string filePath = Path.Combine(uploadPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        libraryItem.FileUpload.CopyTo(stream);
                    }

                    imagePath = $"/img/upload/{fileName}";
                    Console.WriteLine(imagePath);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Failed to upload image: " + ex.Message });
                }
            }

            LibraryItem newLibraryItem = null;
            switch (libraryItem.Type)
            {
                case "Book":
                    newLibraryItem = new Book
                    {
                        Title = libraryItem.Title,
                        Author = libraryItem.Author,
                        PublicationDate = libraryItem.PublicationDate,
                        ImagePath = imagePath,
                        NumberOfPage = libraryItem.NumberOfPages,
                    };
                    break;

                case "DVD":
                    newLibraryItem = new DVD
                    {
                        Title = libraryItem.Title,
                        Author = libraryItem.Author,
                        PublicationDate = libraryItem.PublicationDate,
                        ImagePath = imagePath,
                        Runtime = libraryItem.Runtime
                    };
                    break;

                case "Magazine":
                    newLibraryItem = new Magazine
                    {
                        Title = libraryItem.Title,
                        Author = libraryItem.Author,
                        PublicationDate = libraryItem.PublicationDate,
                        ImagePath = imagePath
                    };
                    break;

                default:
                    return Json(new { success = false, message = "Invalid item type" });
            }

            // Gọi service thêm mới
            _libraryItemService.AddData(newLibraryItem);

            return RedirectToAction("ManageLibraryItem");
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            _libraryItemService.DeleteData(id);
            return RedirectToAction("ManageLibraryItem");
        }

        public IActionResult ReturnItemStatus(int page = 1, int pageSize = 3)
        {
            var borrowers = _borrowerService.GetAllBorrower();
            var borrowingHistoryQuery = _borrowingHistoryService
                .GetAllBorrowingHistory()
                .Where(bh => bh.ReturnDate == null)
                .ToList();

            var itemQuery = _libraryItemService.GetAllLibraryItem();
            var borrowingHistoryWithUsers = borrowingHistoryQuery
                .Select(bh => new BorrowingHistoryWithUser
                {
                    BorrowingHistoryId = bh.BorrowingHistoryId,
                    BorrowDate = bh.BorrowDate,
                    BorrowerName = borrowers.FirstOrDefault(b => b.LibraryCardId == bh.LibraryCardId)?.Name,
                    BorrowerId = bh.LibraryCardId,
                    ItemName = itemQuery.FirstOrDefault(i => i.Id == bh.LibraryItemId)?.Title,
                })
                .ToList();

            var totalItems = borrowingHistoryWithUsers.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            page = Math.Max(1, Math.Min(page, totalPages));

            var pagedBorrowingHistory = borrowingHistoryWithUsers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Prepare paging model
            var pagingModel = new PagingModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                GenerateUrl = p => Url.Action("ReturnItemStatus", new { page = p, pageSize })
            };

            var viewModel = new BorrowViewModel
            {
                BorrowingHistory = pagedBorrowingHistory,
                PagingModel = pagingModel
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ReturnItem(int BorrowingHistoryId)
        {
            var borrowingHistory = _borrowingHistoryService.GetBorrowingHistoryById(BorrowingHistoryId);

            if (borrowingHistory != null && borrowingHistory.ReturnDate == null)
            {
                borrowingHistory.ReturnDate = DateTime.Now;
                _borrowingHistoryService.UpdateReturnDate(borrowingHistory.BorrowingHistoryId, borrowingHistory.ReturnDate);

                return Json(new { success = true, message = "Item returned successfully!" });
            }
            return Json(new { success = false, message = "Item not found or already returned!" });
        }
    }
}
