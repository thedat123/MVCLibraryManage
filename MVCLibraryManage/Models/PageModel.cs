namespace MVCLibraryManage.Models
{
    public class PagingModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public Func<int?, string> GenerateUrl { get; set; }
    }
}
