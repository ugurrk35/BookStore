﻿using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.GetBooks;
using BookStore.DBOperations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }


     
       [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result); // HTTP 200 VE OBJE GÖNDERİYOPRUZ
        } 
       [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
            return book;
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
          CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.Title = updatedBook.Title !=default ? updatedBook.Title : book.Title;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

            _context.SaveChanges();
            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook (int id)
        {
            var book = _context.Books.SingleOrDefault (x => x.Id == id);
            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}
