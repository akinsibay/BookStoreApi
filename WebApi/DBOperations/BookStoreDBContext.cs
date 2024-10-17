using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class BookStoreDBContext : DbContext, IBookStoreDbContext
    {
        private Func<DbContextOptions<BookStoreDBContext>> getRequiredService;

        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options) : base(options)
        {

        }

        public BookStoreDBContext(Func<DbContextOptions<BookStoreDBContext>> getRequiredService)
        {
            this.getRequiredService = getRequiredService;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }

        // SaveChanges zaten DBContext sınıfında var, biz ekstra eklemek istediğimiz için override etmek zorundayız
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}