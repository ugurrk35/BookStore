using BookStore.DBOperations;
using System;
using System.Linq;

namespace BookStore.BookOperations.DeleteBookCommand
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _dbcontext;
        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
             throw new InvalidOperationException("Silinecek kitap bulunamadı");

            _dbcontext.Books.Remove(book);
            _dbcontext.SaveChanges();
        }
    }


}
