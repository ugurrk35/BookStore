using BookStore.DBOperations;
using FluentValidation;
using System;
using System.Linq;

namespace BookStore.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator:AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(commond=>commond.BookId).GreaterThan(0);
          RuleFor(commond=>commond.Model.GenreId).GreaterThan(0);
          RuleFor(commond=>commond.Model.Title).NotEmpty().MinimumLength(3);

        }
    }
}