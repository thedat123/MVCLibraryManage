using MVCLibraryManage.Models;
using MVCLibraryManage.Models.Entity;

namespace MVCLibraryManage.Repository.Implement
{
	public class LibraryItemRepository : ILibraryItemRepository
	{
		private readonly LibraryDbContext context;
		public LibraryItemRepository(LibraryDbContext context)
		{
			this.context = context;
		}
        public List<LibraryItem> GetListLibraryItem(string arrange, bool isDescending, string searchText)
        {
            var listLibraryItem = context.LibraryItems.AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                listLibraryItem = listLibraryItem.Where(x => x.Title.Contains(searchText));
            }

            switch (arrange)
            {
                case "Title":
                    listLibraryItem = isDescending
                        ? listLibraryItem.OrderByDescending(x => x.Title)
                        : listLibraryItem.OrderBy(x => x.Title);
                    break;

                case "Author":
                    listLibraryItem = isDescending
                        ? listLibraryItem.OrderByDescending(x => x.Author)
                        : listLibraryItem.OrderBy(x => x.Author);
                    break;

                default:
                    listLibraryItem = isDescending
                        ? listLibraryItem.OrderByDescending(x => x.Id)
                        : listLibraryItem.OrderBy(x => x.Id);
                    break;
            }

            return listLibraryItem.ToList();
        }
        public void UpdateData(int id)
		{
			var libraryItem = context.LibraryItems.Find(id);
			context.LibraryItems.Update(libraryItem);
            context.SaveChanges();
		}
		public void DeleteData(int id)
		{
			var libraryItem = context.LibraryItems.Find(id);
			context.LibraryItems.Remove(libraryItem);
            context.SaveChanges();
        }
        public List<LibraryItem> GetAllLibraryItem()
        {
            return context.LibraryItems.ToList();
        }

        public void AddData(LibraryItem libraryItem)
        {
            context.LibraryItems.Add(libraryItem);
            context.SaveChanges();
        }

		public LibraryItem GetLibraryItemById(int id)
		{
            return context.LibraryItems.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
