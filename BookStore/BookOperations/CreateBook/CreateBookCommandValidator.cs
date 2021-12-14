using System;
using FluentValidation;

namespace BookStore.BookOperations.CreateBook 
{
    public class CreateBookCommandValidator:AbstractValidator<CreateBookCommand>
    {

        public CreateBookCommandValidator()
        {
            RuleFor(command=>command.Model.GenreId).GreaterThan(0);
            RuleFor(command=>command.Model.PageCount).GreaterThan(0);
            RuleFor(Commond=>Commond.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command=>command.Model.Title).MinimumLength(4);
            

        }
    }
}