using MVCLibraryManage.Models.Entity;
using MVCLibraryManage.Repository;

namespace MVCLibraryManage.Service.Implements
{
    public class BorrowingHistoryService : IBorrowingHistoryService
    {
        private readonly IBorrowingHistoryRepository borrowingHistoryRepository;
        public BorrowingHistoryService(IBorrowingHistoryRepository borrowingHistoryRepository)
        {
            this.borrowingHistoryRepository = borrowingHistoryRepository;
        }
        public void AddBorrowingRecord(BorrowingHistory borrowingHistory)
        {
            borrowingHistoryRepository.AddBorrowingRecord(borrowingHistory);
        }

        public List<BorrowingHistory> GetAllBorrowingHistory()
        {
            return borrowingHistoryRepository.GetAllBorrowingHistory();
        }

        public BorrowingHistory GetBorrowedItemById(int id)
        {
            return borrowingHistoryRepository.GetBorrowedItemById(id);
        }

        public void UpdateReturnDate(int borrowingHistoryId, DateTime? returnDate)
        {
            borrowingHistoryRepository.UpdateReturnDate(borrowingHistoryId, returnDate);
        }

        public BorrowingHistory GetBorrowingHistoryByLibraryCardId(int libraryCardId)
        {
            return borrowingHistoryRepository.GetBorrowingHistoryByLibraryCardId(libraryCardId);
        }
        public IEnumerable<BorrowingHistory> GetBorrowingHistoryForUser(int userId)
        {
            return borrowingHistoryRepository.GetBorrowingHistoryForUser(userId);
        }
        public BorrowingHistory GetBorrowingHistoryById(int id)
        {
            return borrowingHistoryRepository.GetBorrowingHistoryById(id);
        }
    }
}
