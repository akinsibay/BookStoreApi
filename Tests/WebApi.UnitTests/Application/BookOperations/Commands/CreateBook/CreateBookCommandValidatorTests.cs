using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory] // tek fonksiyonla çoklu test yazmak için Theory attribute kullanılır
        [InlineData("Lord of The Rings", 0, 0)] // case 1
        [InlineData("Lord of The Rings", 0, 1)] // case 2
        [InlineData("Lord of The Rings", 100, 0)] // case 3
        [InlineData("", 0, 0)] // case 4
        [InlineData("", 100, 1)] // case 5
        [InlineData("", 0, 1)] // case 6
        [InlineData("Lor", 100, 1)] // case 7
        [InlineData("Lord", 100, 0)] // case 8
        [InlineData("Lord", 0, 1)] // case 9
        [InlineData(" ", 100, 1)] // case 10
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            // ARRANGE
            // çalıştırmayacağız yalnızca inputlarıyla ilgileniyoruz o nedenle null gönderdik
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };

            // ACT
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // ASSERT
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord of The Rings",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date,
                GenreId = 2
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord of The Rings",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 2
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();

            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}