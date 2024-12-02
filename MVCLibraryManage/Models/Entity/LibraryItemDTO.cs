namespace MVCLibraryManage.Models.Entity
{
    public class LibraryItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public IFormFile FileUpload { get; set; }
        public string ImagePath { get; set; }
        public int NumberOfPages { get; set; }
        public double Runtime { get; set; }
        public string Type { get; set; }
    }
}
