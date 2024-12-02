using MVCLibraryManage.Models.Entity;

namespace MVCLibraryManage.Models
{
    public class BorrowViewModel
    {
        public List<BorrowingHistoryWithUser> BorrowingHistory { get; set; }
        public PagingModel PagingModel { get; set; }
    }
}
