using MVCLibraryManage.Models;
using MVCLibraryManage.Models.Entity;

namespace MVCLibraryManage.Repository.Implement
{
	public class StaffRepository : IStaffRepository
	{
		private readonly LibraryDbContext context;
		public StaffRepository(LibraryDbContext context)
		{
			this.context = context;
		}

		public List<Staff> GetListStaffs(string arrange, bool isDescending, string searchText)
		{
			List<Staff> listStaff = context.Staffs.ToList();
			if (!string.IsNullOrEmpty(searchText))
			{
				listStaff = listStaff.Where(x => x.StaffName.Contains(searchText)).ToList();
			}
			if (arrange == "Name")
			{
				if (isDescending)
				{
					listStaff = listStaff.OrderByDescending(x => x.StaffName).ToList();
				}
				else
				{
					listStaff = listStaff.OrderBy(x => x.StaffName).ToList();
				}
			}
			return listStaff;
		}

		public void UpdateData(int id)
		{
			var staff = context.Staffs.Find(id);
			context.Staffs.Update(staff);
		}

		public void DeleteData(int id) {
			var staff = context.Staffs.Find(id);
			context.Staffs.Remove(staff);
		}

		public List<Staff> GetAllStaffs()
		{
			return context.Staffs.ToList();
		}

		public int GetStaffID(int accountID)
		{
			return context.Staffs.Where(x => x.AccountID == accountID).Select(x => x.StaffID).FirstOrDefault();
		}
	}
}
