using BooksApi.Models;

namespace BooksApi
{
    public static class StaticDb
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book()
            {
                Title = "Harry Potter",
                Author = "J.K. Rowling",
            },

            new Book()
            {
                Title = "The Alchemist",
                Author = "Paulo Coelho"
            },

            new Book()
            {
                Title = "The Da Vinci Code",
                Author = "Dan Brown"
            }
        };
    }
}
