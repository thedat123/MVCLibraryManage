using Microsoft.AspNetCore.Mvc;
using MVCLibraryManage.Service;

namespace MVCLibraryManage.Controllers
{
    public class BorrowingHistoryController
    {
        public IBorrowingHistoryService _borrowingHistoryService;
        public BorrowingHistoryController(IBorrowingHistoryService borrowingHistoryService)
        {
            _borrowingHistoryService = borrowingHistoryService;
        }
    }
}
