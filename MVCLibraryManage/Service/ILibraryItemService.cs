using MVCLibraryManage.Models.Entity;

namespace MVCLibraryManage.Service
{
	public interface ILibraryItemService
	{
		List<LibraryItem> GetListLibraryItem(string arrange, bool isDescending, string searchText);
		void UpdateData(int id, LibraryItem item);
		void DeleteData(int id);
		List<LibraryItem> GetAllLibraryItem();
        void AddData(LibraryItem libraryItem);
		LibraryItem GetLibraryItemById(int id);
    }
}
