namespace BooksAPI.Models
{
    public class Book : BaseEntity
    {
        public string Author { get; set; }
        public string Title { get; set; }
    }
}
