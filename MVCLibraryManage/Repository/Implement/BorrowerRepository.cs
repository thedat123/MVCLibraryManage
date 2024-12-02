using MVCLibraryManage.Models;
using MVCLibraryManage.Models.Entity;

namespace MVCLibraryManage.Repository.Implement
{
	public class BorrowerRepository : IBorrowerRepository
	{
		private readonly LibraryDbContext context;
		public BorrowerRepository(LibraryDbContext context)
		{
			this.context = context;
		}
		public List<Borrower> GetListBorrower(string arrange, bool isDescending, string searchText)
		{
			List<Borrower> listBorrower = context.Borrowers.ToList();
			if (!string.IsNullOrEmpty(searchText))
			{
				listBorrower = listBorrower.Where(x => x.Name.Contains(searchText)).ToList();
			}
			if (arrange == "Name")
			{
				if (isDescending)
				{
					listBorrower = listBorrower.OrderByDescending(x => x.Name).ToList();
				}
				else
				{
					listBorrower = listBorrower.OrderBy(x => x.Name).ToList();
				}
			}
			else if (arrange == "LibraryCardId")
			{
				if (isDescending)
				{
					listBorrower = listBorrower.OrderByDescending(x => x.LibraryCardId).ToList();
				}
				else
				{
					listBorrower = listBorrower.OrderBy(x => x.LibraryCardId).ToList();
				}
			}
			else if (arrange == "Address")
			{
				if (isDescending)
				{
					listBorrower = listBorrower.OrderByDescending(x => x.Address).ToList();
				}
				else
				{
					listBorrower = listBorrower.OrderBy(x => x.Address).ToList();
				}
			}
			return listBorrower;
		}

		public void UpdateData(int id)
		{
			var borrower = context.Borrowers.Find(id);
			context.Borrowers.Update(borrower);
		}

		public void DeleteData(int id)
		{
			var borrower = context.Borrowers.Find(id);
			context.Borrowers.Remove(borrower);
		}

		public List<Borrower> GetAllBorrower()
		{
			return context.Borrowers.ToList();
		}
		public int GetBorrowerID(int accountID)
		{
			return context.Borrowers.Where(x => x.AccountID == accountID).FirstOrDefault().LibraryCardId;
		}
	}
}
