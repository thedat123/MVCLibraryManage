using Microsoft.CodeAnalysis.Scripting;
using MVCLibraryManage.Models;
using MVCLibraryManage.Models.Entity;
using System.Net;

namespace MVCLibraryManage.Repository.Implement
{
	public class AccountRepository : IAccountRepository
	{
		public readonly LibraryDbContext context;
		public AccountRepository(LibraryDbContext libraryDbContext)
		{
			context = libraryDbContext;
		}

		public Account GetAccount(string email, string password)
		{
			var user = context.Accounts.FirstOrDefault(p => p.Email == email);
			if (user != null)
			{
				return user;

            }
			return null;
		}

		public string GetNamebyID(int idAccount, bool isStaff)
		{
			if (isStaff)
				return context.Staffs.Where(p => p.AccountID == idAccount).FirstOrDefault().StaffName;
			else
				return context.Borrowers.Where(p => p.AccountID == idAccount).FirstOrDefault().Name;
		}
		public string GetPasswordbyID(int idAccount)
		{
			return context.Accounts.Where(p => p.AccountID == idAccount).Select(p => p.Password).FirstOrDefault();

		}
		public void ChangePassword(int idAccount, string newPassword)
		{
			var _acc = context.Accounts.FirstOrDefault(p => p.AccountID == idAccount);
			if (_acc != null)
			{
				_acc.Password = newPassword;
				context.SaveChanges();
			}
		}
		public void AddNewAccount(string email, string password, string name, string address)
		{
            var newAccount = new Account
            {
                Email = email,
                Password = password,
                isDeleted = false,
                isStaff = false
            };

            context.Accounts.Add(newAccount);
            context.SaveChanges();

            var newBorrower = new Borrower
            {
                Name = name,
                Address = address,
                AccountID = newAccount.AccountID // Set AccountID for the Borrower
            };

            context.Borrowers.Add(newBorrower);
            context.SaveChanges();
        }
		public List<Account> GetListAccount()
		{
			return context.Accounts.Where(p => p.isDeleted == false).Select(p => p).ToList();
		}
		public int? GetIDAccountByMail(string email)
		{
            var acc = context.Accounts.Where(p => p.Email.Equals(email)).FirstOrDefault();
			return acc?.AccountID;
        }
	}
}
