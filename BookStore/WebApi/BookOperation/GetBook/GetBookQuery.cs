using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperation.GetBook{
    public class GetBookQuery{
        private readonly BookStoreDbContext _context;
        public GetBookQuery(BookStoreDbContext context)
        {
            _context = context;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _context.Books.OrderBy(b => b.Id).ToList<Book>();
            List<BooksViewModel> viewModel = new List<BooksViewModel>();
            foreach(var book in bookList){
                viewModel.Add(new BooksViewModel(){
                    Title = book.Title,
                    PageCount = book.PageCount,
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
                    Genre = ((GenreEnum)book.GenreId).ToString()
                });
            }
            return viewModel;
        }
    }

    public class BooksViewModel{
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}