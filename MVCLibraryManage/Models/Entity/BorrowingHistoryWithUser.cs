namespace MVCLibraryManage.Models.Entity
{
    public class BorrowingHistoryWithUser
    {
        public int BorrowingHistoryId { get; set; }
        public string BorrowerName { get; set; }
        public int BorrowerId { get; set; }
        public string ItemName { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
