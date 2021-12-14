using BookStore.DBOperations;
using FluentValidation;
using System;
using System.Linq;

namespace BookStore.BookOperations.DeleteBookCommand
{
    public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
    {
            public DeleteBookCommandValidator()
            {
                RuleFor(command=>command.BookId).GreaterThan(0);
                
            }
    }
}