using Microsoft.EntityFrameworkCore;
using MVCLibraryManage.Models;
using MVCLibraryManage.Models.Entity;

namespace MVCLibraryManage.Repository.Implement
{
    public class BorrowingHistoryRepository : IBorrowingHistoryRepository
    {
        public LibraryDbContext _libraryDbContext;
        public BorrowingHistoryRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }
        public List<BorrowingHistory> GetAllBorrowingHistory()
        {
            return _libraryDbContext.BorrowingHistories.ToList();
        }

        public BorrowingHistory GetBorrowedItemById(int id)
        {
            return _libraryDbContext.BorrowingHistories.ToList().Find(x => x.LibraryCardId == id);
        }

        public void AddBorrowingRecord(BorrowingHistory borrowingHistory)
        {
            try
            {
                _libraryDbContext.BorrowingHistories.Add(borrowingHistory);

                Console.WriteLine("Before SaveChanges: " + _libraryDbContext.ChangeTracker.Entries().Count());

                _libraryDbContext.SaveChanges();

                Console.WriteLine("After SaveChanges: " + _libraryDbContext.ChangeTracker.Entries().Count());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while adding borrowing history: {ex.Message}");
                throw; // Optionally rethrow the error
            }
        }


        // Update the return date for a borrowing record
        public void UpdateReturnDate(int borrowingHistoryId, DateTime? returnDate)
        {
            var borrowingRecord = _libraryDbContext.BorrowingHistories.Find(borrowingHistoryId);
            if (borrowingRecord != null)
            {
                borrowingRecord.ReturnDate = returnDate;
                _libraryDbContext.SaveChanges();
            }
        }
        public BorrowingHistory GetBorrowingHistoryByLibraryCardId(int libraryCardId)
        {
            return _libraryDbContext.BorrowingHistories.Where(x => x.LibraryCardId == libraryCardId).FirstOrDefault();
        }

        public IEnumerable<BorrowingHistory> GetBorrowingHistoryForUser(int userId)
        {
            var borrowingHistory = _libraryDbContext.BorrowingHistories
                .Where(b => b.LibraryCardId == userId)  // Assuming LibraryCardId is related to the user
                .Include(b => b.LibraryItem)  // Eagerly load the LibraryItem related to BorrowingHistory
                .ToList();

            return borrowingHistory;
        }

        public BorrowingHistory GetBorrowingHistoryById(int id)
        {
            return _libraryDbContext.BorrowingHistories.Find(id);
        }
    }
}
