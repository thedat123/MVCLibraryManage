using MVCLibraryManage.Models.Entity;

namespace MVCLibraryManage.Models
{
    public class BorrowStatusViewModel
    {
        public IEnumerable<BorrowingHistory> BorrowingHistory { get; set; }
        public PagingModel PagingModel { get; set; }
    }
}
