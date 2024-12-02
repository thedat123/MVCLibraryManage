using MVCLibraryManage.Models.Entity;
using MVCLibraryManage.Repository;

namespace MVCLibraryManage.Service.Implements
{
	public class StaffService : IStaffService
	{
		private readonly IStaffRepository staffRepository;
		public StaffService(IStaffRepository staffRepository)
		{
			this.staffRepository = staffRepository;
		}

		public int GetStaffID(int accountID)
		{
			return staffRepository.GetStaffID(accountID);
		}
		public List<Staff> GetListStaffs(string arrange, bool isDescending, string searchText)
		{
			return staffRepository.GetListStaffs(arrange, isDescending, searchText);
		}
		public void UpdateData(int id)
		{
			staffRepository.UpdateData(id);
		}
		public void DeleteData(int id)
		{
			staffRepository.DeleteData(id);
		}
		public List<Staff> GetAllStaffs()
		{
			return staffRepository.GetAllStaffs();
		}
	}
}
