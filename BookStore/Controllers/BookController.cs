using AutoMapper;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBookCommand;
using BookStore.BookOperations.GetBookDetail;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UpdateBook;
using BookStore.DBOperations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.BookOperations.UpdateBook.UpdateBookCommand;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }


     
       [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result); // HTTP 200 VE OBJE GÖNDERİYOPRUZ
        } 
       [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
                query.BookId = id;
                GetBookDetailQueryValidator validator =new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);

                result=query.handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
      
            return Ok(result);
        } 
       /* [HttpGet]
        public Book Get([FromQuery] string id)
        {
            var book = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        } */

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
          CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator= new CreateBookCommandValidator();      
                validator.ValidateAndThrow(command);
                command.Handle();
                /*if  (!result.IsValid)
                foreach (var item in result.Errors)
           
                    Console.WriteLine("Özeellik " + item.PropertyName + "Errror message "+ item.ErrorMessage); 
             else 
                command.Handle();*/
             
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookViewModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;

                UpdateBookCommandValidator validator =new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);

                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook (int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;

                DeleteBookCommandValidator validator= new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {

              return BadRequest(ex.Message);
            }
          
            return Ok();
        }
    }
}
