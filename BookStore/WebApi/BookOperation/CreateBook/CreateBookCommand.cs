using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperation.CreateBook{

    public class CreateBookCommand{

        public CreateBookModel model {get; set;}
        private readonly BookStoreDbContext _context;
        public CreateBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle(){
            var book = _context.Books.SingleOrDefault(b => b.Title == model.Title);

            if(book is not null){
                throw new InvalidOperationException("Kitap zaten mevcut");
            }
            
            book = new Book();
            book.Title = model.Title;
            book.GenreId = model.GenreId;
            book.PageCount = model.PageCount;
            book.PublishDate = model.PublishDate;

            _context.Books.Add(book);
            _context.SaveChanges();
        }
    }

    public class CreateBookModel{
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        
    }
}