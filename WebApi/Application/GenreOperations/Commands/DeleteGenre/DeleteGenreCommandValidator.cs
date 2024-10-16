using FluentValidation;
using WebApi.Application.GenreOperations.Commmands.DeleteGenre;

namespace WebApi.Application.GenreOperations.Commmands.DeleteGenre
{
    public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0);
        }
    }
}
