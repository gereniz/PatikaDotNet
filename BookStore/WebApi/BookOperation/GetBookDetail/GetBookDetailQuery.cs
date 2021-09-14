using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperation.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _context;
        public int bookId { get; set; }

        public GetBookDetailQuery(BookStoreDbContext context)
        {
            _context = context;
        }

        public BookDetailViewModel Handle()
        {
            var book = _context.Books.Where(b => b.Id == bookId).SingleOrDefault();

            if (book is null){
                throw new InvalidOperationException("Kitap bulunamadı");
            }
            BookDetailViewModel viewModel = new BookDetailViewModel();

            viewModel.Title = book.Title;
            viewModel.PageCount = book.PageCount;
            viewModel.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy");
            viewModel.Genre = ((GenreEnum)book.GenreId).ToString();


            return viewModel;

        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}