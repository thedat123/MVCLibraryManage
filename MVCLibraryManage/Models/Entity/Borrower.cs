using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCLibraryManage.Models.Entity
{
	public class Borrower
	{
		[Key]
		public int LibraryCardId { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }
		[Required]
		[StringLength(100)]
		public string Address { get; set; }
		public int AccountID { get; set; }
		[ForeignKey("AccountID")]
		public Account Account { get; set; }
	}
}
