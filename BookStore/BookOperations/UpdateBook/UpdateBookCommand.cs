﻿using BookStore.DBOperations;
using System;
using System.Linq;

namespace BookStore.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _context;

        public int BookId { get; set; }
        public UpdateBookViewModel Model { get; set; }
        public UpdateBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Güncellenecek klitap bulunamadı");
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;

            book.Title = Model.Title != default ? Model.Title : book.Title;


            _context.SaveChanges();

        }
        public class UpdateBookViewModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
        }
    }
}
