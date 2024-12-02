using MVCLibraryManage.Models.Entity;
using System.Collections.Generic;

namespace MVCLibraryManage.Service
{
    public interface IBorrowingHistoryService
    {
        List<BorrowingHistory> GetAllBorrowingHistory();
        BorrowingHistory GetBorrowingHistoryByLibraryCardId(int libraryCardId);
        BorrowingHistory GetBorrowedItemById(int id);
        void AddBorrowingRecord(BorrowingHistory borrowingHistory);
        public IEnumerable<BorrowingHistory> GetBorrowingHistoryForUser(int userId);
        void UpdateReturnDate(int borrowingHistoryId, DateTime? returnDate);
        BorrowingHistory GetBorrowingHistoryById(int id);
    }
}
