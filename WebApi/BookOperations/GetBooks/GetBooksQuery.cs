using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{

    public class GetBooksQuery
    {
        private readonly BookStoreDBContext _dbContext;
        public GetBooksQuery(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(book => book.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();

            foreach (var item in bookList)
            {
                vm.Add(new BooksViewModel()
                {
                    Title = item.Title,
                    Genre = ((GenreEnum)item.GenreId).ToString(),
                    PublishDate = item.PublishDate.Date.ToString("dd/MM/yyy"),
                    PageCount = item.PageCount
                });
            }
            return vm;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}