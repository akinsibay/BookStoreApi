using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Queries.
GetBooks
{

    public class GetBooksQuery
    {
        private readonly BookStoreDBContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.Include(book => book.Genre).OrderBy(book => book.Id).ToList<Book>();

            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);
            // List<BooksViewModel> vm = new List<BooksViewModel>();

            // foreach (var item in bookList)
            // {
            //     vm.Add(new BooksViewModel()
            //     {
            //         Title = item.Title,
            //         Genre = ((GenreEnum)item.GenreId).ToString(),
            //         PublishDate = item.PublishDate.Date.ToString("dd/MM/yyy"),
            //         PageCount = item.PageCount
            //     });
            // }
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