using MVCLibraryManage.Models.Entity;

namespace MVCLibraryManage.Models
{
    public class LibraryManageViewModel
    {
        public IEnumerable<LibraryItem> Items { get; set; }
        public PagingModel PagingModel { get; set; }
    }
}
