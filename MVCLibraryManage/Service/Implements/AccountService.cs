using MVCLibraryManage.Models.Entity;
using MVCLibraryManage.Repository;

namespace MVCLibraryManage.Service.Implements
{
	public class AccountService : IAccountService
	{
		private readonly IAccountRepository accountRespository;

		public AccountService(IAccountRepository accountRespository)
		{
			this.accountRespository = accountRespository;
		}
		public Account GetAccount(string email, string password)
		{
			return accountRespository.GetAccount(email, password);
		}

		public string GetNamebyID(int idAccount, bool isStaff)
		{
			return accountRespository.GetNamebyID(idAccount, isStaff);
		}
		public string GetPasswordbyID(int idAccount)
		{
			return accountRespository.GetPasswordbyID(idAccount);
		}
		public void ChangePassword(int idAccount, string newPassword)
		{
			accountRespository.ChangePassword(idAccount, newPassword);
		}
		public void AddNewAccount(string email, string password, string name, string address)
		{
			accountRespository.AddNewAccount(email, password, name, address);
		}
		public List<Account> GetListAccount()
		{
			return accountRespository.GetListAccount();
		}
		public int? GetIDAccountByMail(string email)
		{
			return accountRespository.GetIDAccountByMail(email);
		}


	}
}
