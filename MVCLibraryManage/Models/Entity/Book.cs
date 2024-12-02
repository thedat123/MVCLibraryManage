using System.ComponentModel.DataAnnotations;

namespace MVCLibraryManage.Models.Entity
{
	public class Book : LibraryItem
	{
		[Required]
		public int NumberOfPage { get; set; }
	}
}
