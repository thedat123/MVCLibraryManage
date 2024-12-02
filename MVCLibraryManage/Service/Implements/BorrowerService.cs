using MVCLibraryManage.Models.Entity;
using MVCLibraryManage.Repository;

namespace MVCLibraryManage.Service.Implements
{
	public class BorrowerService : IBorrowerService
	{
		private readonly IBorrowerRepository borrowerRepository;
		public BorrowerService(IBorrowerRepository borrowerRepository)
		{
			this.borrowerRepository = borrowerRepository;
		}

		public List<Borrower> GetListBorrower(string arrange, bool isDescending, string searchText)
		{
			return borrowerRepository.GetListBorrower(arrange, isDescending, searchText);
		}
		public void UpdateData(int id)
		{
			borrowerRepository.UpdateData(id);
		}
		public void DeleteData(int id)
		{
			borrowerRepository.DeleteData(id);
		}
		public List<Borrower> GetAllBorrower()
		{
			return borrowerRepository.GetAllBorrower();
		}
		public int GetBorrowerID(int accountID)
		{
			return borrowerRepository.GetBorrowerID(accountID);
		}
	}
}
