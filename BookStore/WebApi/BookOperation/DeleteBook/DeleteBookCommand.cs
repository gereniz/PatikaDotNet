using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.BookOperation.DeleteBook{

    public class DeleteBookCommand
    {
        public int bookId { get; set; }
        private readonly BookStoreDbContext _context;
        public DeleteBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle(){
            var book = _context.Books.SingleOrDefault(b => b.Id == bookId);

            if(book is null){
                throw new InvalidOperationException("Silinecek kitap bulunamadı");
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}