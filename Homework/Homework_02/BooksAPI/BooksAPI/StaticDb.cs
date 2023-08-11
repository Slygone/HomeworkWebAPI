using BooksAPI.Models;

namespace BooksAPI
{
    public static class StaticDb
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book()
            {
                Id = 1,
                Author = "John",
                Title = "First Book",
            },
            new Book()
            {
                Id = 2,
                Author = "Jim",
                Title = "Second Book",
            },
            new Book()
            {
                Id = 3,
                Author = "Jane",
                Title = "Third book"
            }
        };
    }
}
