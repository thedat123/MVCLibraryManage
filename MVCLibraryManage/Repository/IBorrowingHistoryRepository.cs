using MVCLibraryManage.Models.Entity;

namespace MVCLibraryManage.Repository
{
    public interface IBorrowingHistoryRepository
    {
        List<BorrowingHistory> GetAllBorrowingHistory();
        BorrowingHistory GetBorrowingHistoryByLibraryCardId(int libraryCardId);
        BorrowingHistory GetBorrowedItemById(int id);
        void AddBorrowingRecord(BorrowingHistory borrowingHistory);
        void UpdateReturnDate(int borrowingHistoryId, DateTime? returnDate);
        IEnumerable<BorrowingHistory> GetBorrowingHistoryForUser(int userId);
        BorrowingHistory GetBorrowingHistoryById(int id);
    }
}
