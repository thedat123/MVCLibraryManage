using MVCLibraryManage.Models.Entity;

namespace MVCLibraryManage.Repository
{
	public interface IStaffRepository
	{
		int GetStaffID(int accountID);
		List<Staff> GetListStaffs(string arrange, bool isDescending, string searchText);
		void UpdateData(int id);
		void DeleteData(int id);
		List<Staff> GetAllStaffs();
	}
}
