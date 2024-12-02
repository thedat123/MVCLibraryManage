using MVCLibraryManage.Models.Entity;

namespace MVCLibraryManage.Service
{
	public interface IBorrowerService
	{
		List<Borrower> GetListBorrower(string arrange, bool isDescending, string searchText);
		void UpdateData(int id);
		void DeleteData(int id);
		List<Borrower> GetAllBorrower();
		int GetBorrowerID(int accountID);
	}
}
