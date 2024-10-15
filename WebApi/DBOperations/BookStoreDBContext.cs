using Microsoft.EntityFrameworkCore;

namespace WebApi.DBOperations
{
    public class BookStoreDBContext : DbContext
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
    }
}