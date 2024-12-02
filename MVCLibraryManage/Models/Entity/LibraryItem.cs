using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace MVCLibraryManage.Models.Entity
{
	public class LibraryItem
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Title { get; set; }

		[Required]
		[StringLength(100)]
		public string Author { get; set; }

		[Required]
		public DateTime PublicationDate { get; set; }

		[StringLength(1000)]
		public string ImagePath { get; set; } 
	}
}
