using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MVCLibraryManage.Models.Entity
{
	public class Staff
	{
		[Key]
		public int StaffID { get; set; }
		[StringLength(100)]
		public string StaffName { get; set; }
		[StringLength(10)]
		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }
		public bool Gender { get; set; }
		public int AccountID { get; set; }
		[ForeignKey("AccountID")]
		public Account Account { get; set; }
	}
}
