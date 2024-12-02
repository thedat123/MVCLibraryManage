using MVCLibraryManage.Models.Entity;

namespace MVCLibraryManage.Repository
{
	public interface IAccountRepository
	{
		Account GetAccount(string email, string password);
		string GetNamebyID(int idAccount, bool isStaff);
		string GetPasswordbyID(int idAccount);
		void ChangePassword(int idAccount, string newPassword);
		void AddNewAccount(string email, string password, string name, string address);
		List<Account> GetListAccount();
		int? GetIDAccountByMail(string email);
	}
}
