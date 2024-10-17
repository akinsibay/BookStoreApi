using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDBContext context)
        {
            context.Books.AddRange(
                    new Book
                    {
                        Title = "Lean Startup",
                        GenreId = 1, // Personal Growth
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 06, 12)
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2, // Science Fiction
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 05, 23)
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 2, // Science Fiction
                        PageCount = 540,
                        PublishDate = new DateTime(2007, 12, 21)
                    });

            context.SaveChanges(); // Veritabanına değişiklikleri kaydediyoruz
        }
    }
}