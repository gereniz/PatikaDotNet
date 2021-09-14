using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperation.UpdateBook{

    public class UpdateBookCommand{

        public UpdateBookModel model {get; set;}

        public int bookId { get; set; }
        private readonly BookStoreDbContext _context;
        public UpdateBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle(){
            var book = _context.Books.SingleOrDefault(b => b.Id == bookId);

            if(book is null){
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı");
            }
            
            book.Title = model.Title != default ? model.Title : book.Title;
            book.GenreId = model.GenreId != default ? model.GenreId : book.GenreId;
            book.PageCount = model.PageCount != default ? model.PageCount : book.PageCount; 
            book.PublishDate = model.PublishDate != default ? model.PublishDate : book.PublishDate;
            
            _context.Books.Update(book);
            _context.SaveChanges();
        }
    }

    public class UpdateBookModel{
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        
    }
}