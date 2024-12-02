using System.ComponentModel.DataAnnotations;

namespace MVCLibraryManage.Models.Entity
{
	public class DVD : LibraryItem
	{
		[Required]
		public double Runtime { get; set; }
	}
}
