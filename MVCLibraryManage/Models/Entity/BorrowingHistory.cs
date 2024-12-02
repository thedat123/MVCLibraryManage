using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MVCLibraryManage.Models.Entity
{
    public class BorrowingHistory
    {
        [Key]
        public int BorrowingHistoryId { get; set; }

        [Required]
        [ForeignKey("LibraryCardId")]
        public int LibraryCardId { get; set; }

        [Required]
        [ForeignKey("LibraryItemId")]
        public int LibraryItemId { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }  

        public LibraryItem LibraryItem { get; set; }

        public override string ToString()
        {
            return $"BorrowingHistoryId: {BorrowingHistoryId}, LibraryCardId: {LibraryCardId}, LibraryItemId: {LibraryItemId}, BorrowDate: {BorrowDate}, ReturnDate: {ReturnDate}";
        }
    }

}
