using MVCLibraryManage.Models.Entity;
using MVCLibraryManage.Repository;
using MVCLibraryManage.Service;

namespace MVCLibraryManage.Service.Implements
{
    public class LibraryItemService : ILibraryItemService
    {
		private readonly ILibraryItemRepository libraryItemRepository;
		public LibraryItemService(ILibraryItemRepository libraryItemRepository)
		{
			this.libraryItemRepository = libraryItemRepository;
		}
		public List<LibraryItem> GetListLibraryItem(string arrange, bool isDescending, string searchText)
		{
			return libraryItemRepository.GetListLibraryItem(arrange, isDescending, searchText);
		}
		public void UpdateData(int id, LibraryItem item)
		{
			libraryItemRepository.UpdateData(id);
		}
		public void DeleteData(int id)
		{
			libraryItemRepository.DeleteData(id);
		}

        public void AddData(LibraryItem libraryItem)
        {
            libraryItemRepository.AddData(libraryItem);
        }
		public LibraryItem GetLibraryItemById(int id)
		{
			return libraryItemRepository.GetLibraryItemById(id);
        }

        public List<LibraryItem> GetAllLibraryItem()
        {
			return libraryItemRepository.GetAllLibraryItem();
        }
    }
}
