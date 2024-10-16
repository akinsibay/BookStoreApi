using FluentValidation;
using WebApi.Application.GenreOperations.Commmands.DeleteGenre;

namespace WebApi.Application.GenreOperations.Commmands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name.Trim() != string.Empty);
        }
    }
}
