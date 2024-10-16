using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetBookDetailQuery
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}