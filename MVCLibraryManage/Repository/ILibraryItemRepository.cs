using MVCLibraryManage.Models.Entity;

namespace MVCLibraryManage.Repository
{
	public interface ILibraryItemRepository
	{
		List<LibraryItem> GetListLibraryItem(string arrange, bool isDescending, string searchText);
		void UpdateData(int id);
		void DeleteData(int id);
		List<LibraryItem> GetAllLibraryItem();

        void AddData(LibraryItem libraryItem);
		LibraryItem GetLibraryItemById(int id);
    }
}
