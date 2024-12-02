using Microsoft.EntityFrameworkCore;
using MVCLibraryManage.Models.Entity;

namespace MVCLibraryManage.Models
{
	public class LibraryDbContext : DbContext
	{
		public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

		public DbSet<Borrower> Borrowers { get; set; }
		public DbSet<LibraryItem> LibraryItems { get; set; }
		public DbSet<BorrowingHistory> BorrowingHistories { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<DVD> DVDs { get; set; }
		public DbSet<Magazine> Magazines { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Staff> Staffs { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<LibraryItem>()
				.HasDiscriminator<string>("ItemType")
				.HasValue<Book>("Book")
				.HasValue<DVD>("DVD")
				.HasValue<Magazine>("Magazine");

			base.OnModelCreating(modelBuilder);
		}
	}
}
