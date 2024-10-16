using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // Doğrudan BookStoreDBContext'i alıyoruz
            using (var context = new BookStoreDBContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDBContext>>()))
            {
                // Eğer veri varsa veritabanını doldurmayız
                if (context.Books.Any())
                {
                    return; // Veritabanı zaten başlatılmış
                }

                // Genre verilerini ekliyoruz
                context.Genres.AddRange(new Genre
                {
                    Name = "Personal Growth",
                }, new Genre
                {
                    Name = "Science Fiction",
                }, new Genre
                {
                    Name = "Romance",
                });

                // Kitap verilerini ekliyoruz
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
}
