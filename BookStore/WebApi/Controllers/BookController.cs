using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperation.CreateBook;
using WebApi.BookOperation.DeleteBook;
using WebApi.BookOperation.GetBook;
using WebApi.BookOperation.GetBookDetail;
using WebApi.BookOperation.UpdateBook;
using WebApi.DbOperations;
using static WebApi.BookOperation.UpdateBook.UpdateBookCommand;

namespace WebApi.Controllers{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {

        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(){
            GetBookQuery query = new GetBookQuery(_context);
            var result =  query.Handle();
            return Ok(result);
        }
        

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            
            BookDetailViewModel result;

            try{
                GetBookDetailQuery  query = new GetBookDetailQuery(_context);
                query.bookId = id;
                result = query.Handle();
            }catch(Exception e){
                return BadRequest(e.Message);
            }
            return Ok(result);
        }

        /*
        
        [HttpGet]
        public Book Get([FromQuery] int id){
            var book = BookList.Where(b => b.Id == id).SingleOrDefault();
            return book;
        }
        */


        [HttpPost]
        public IActionResult Add([FromBody] CreateBookModel book){

            CreateBookCommand command = new CreateBookCommand(_context);
            try{
                command.model = book;
                command.Handle();
            }catch(Exception e){
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] UpdateBookModel book){
            
            try{
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.bookId = id;
                command.model = book;
                command.Handle();
            }catch(Exception e){
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id){
            
            try{
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.bookId = id;
                command.Handle();

            }catch(Exception e){
                return BadRequest(e.Message);
            }
         
            return Ok();

        }
    }
}