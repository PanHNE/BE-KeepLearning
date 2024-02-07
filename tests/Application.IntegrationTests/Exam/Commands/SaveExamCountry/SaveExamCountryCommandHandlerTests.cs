using System.ComponentModel;
using Application.Common.Exceptions;
using Application.Common.Models.Continent;
using Application.Common.Models.Question;
using Application.Exam.Commands.SaveExamCountry;
using Application.Exam.Queries.GenerateExamCountry;

namespace Application.Country.Commands.SaveExamCountry.UnitTest;

public class SaveExamCountryCommandHandlerTests : TestBasics
{
    [Fact()]
    public async void SaveExamCommandHandle_CorrectAllData_SaveExamAndReturnExamId()
    {
        // arrange
        var command = new SaveExamCountryCommand()
        {
            Name = "Test",
            Category = "Capital City",
            Continents = new List<ContinentDto>(){
                new ContinentDto("Europe"),
                new ContinentDto("Asia"),
            },
            Questions = new List<QuestionDto>(){
                new QuestionDto( 1, "Pakistan", "Islamabad"),
                new QuestionDto( 2, "Tajikistan", "Dushanbe"),
                new QuestionDto( 3, "Philippines", "Manila"),
                new QuestionDto( 4, "Laos", "Vientiane"),
                new QuestionDto( 5, "Malta", "Valletta"),
                new QuestionDto( 6, "Azerbaijan", "Baku"),
                new QuestionDto( 7, "Poland", "Warsaw"),
                new QuestionDto( 8, "Indonesia", "Jakarta"),
                new QuestionDto( 9, "Kuwait", "Kuwait City"),
                new QuestionDto( 10, "South Korea", "Seoul"),
            }
        };

        var handler = new SaveExamCountryCommandHandler(_continentRepository, _examRepository, _countryService, _mapper);

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        result.Should().NotBeEmpty();
    }

    [Fact()]
    public async void SaveExamCommandHandle_InvalidContinentName_ThrowNotFoundExceptionForContinent()
    {
        // arrange
        var command = new SaveExamCountryCommand()
        {
            Name = "Test",
            Category = "Capital City",
            Continents = new List<ContinentDto>(){
                new ContinentDto("Europeaa, Asia")
            },
            Questions = new List<QuestionDto>(){
                new QuestionDto( 1, "Pakistan", "Islamabad"),
                new QuestionDto( 2, "Tajikistan", "Dushanbe"),
                new QuestionDto( 3, "Philippines", "Manila"),
                new QuestionDto( 4, "Laos", "Vientiane"),
                new QuestionDto( 5, "Malta", "Valletta"),
                new QuestionDto( 6, "Azerbaijan", "Baku"),
                new QuestionDto( 7, "Poland", "Warsaw"),
                new QuestionDto( 8, "Indonesia", "Jakarta"),
                new QuestionDto( 9, "Kuwait", "Kuwait City"),
                new QuestionDto( 10, "South Korea", "Seoul"),
            }
        };

        var handler = new SaveExamCountryCommandHandler(_continentRepository, _examRepository, _countryService, _mapper);

        // act
        var action = () => handler.Handle(command, CancellationToken.None);

        // assert
        await action.Invoking(a => a.Invoke())
            .Should().ThrowAsync<NotFoundException>("Not found continent");
    }

    [Fact()]
    public async void SaveExamCommandHandlee_NoAllContinentsFromQuestionAreInContinents_ThrowInvalidDataException()
    {
        // arrange
        var command = new SaveExamCountryCommand()
        {
            Name = "Test",
            Category = "Capital City",
            Continents = new List<ContinentDto>(){
                new ContinentDto("Europe")
            },
            Questions = new List<QuestionDto>(){
                new QuestionDto( 1, "Pakistan", "Islamabad"),
                new QuestionDto( 2, "Tajikistan", "Dushanbe"),
                new QuestionDto( 3, "Philippines", "Manila"),
                new QuestionDto( 4, "Laos", "Vientiane"),
                new QuestionDto( 5, "Malta", "Valletta"),
                new QuestionDto( 6, "Azerbaijan", "Baku"),
                new QuestionDto( 7, "Poland", "Warsaw"),
                new QuestionDto( 8, "Indonesia", "Jakarta"),
                new QuestionDto( 9, "Kuwait", "Kuwait City"),
                new QuestionDto( 10, "South Korea", "Seoul"),
            }
        };

        var handler = new SaveExamCountryCommandHandler(_continentRepository, _examRepository, _countryService, _mapper);

        // act
        var action = () => handler.Handle(command, CancellationToken.None);

        // assert
        await action.Invoking(a => a.Invoke())
            .Should().ThrowAsync<InvalidDataException>("Countries from wrong continents"); ;
    }

    [Fact()]
    public async void SaveExamCommandHandle_InvalidQuestionText_ThrowNotFoundExceptionForCountry()
    {
        // arrange
        var command = new SaveExamCountryCommand()
        {
            Name = "Test",
            Category = "Capital City",
            Continents = new List<ContinentDto>(){
                new ContinentDto("Europe"),
                new ContinentDto("Asia"),
            },
            Questions = new List<QuestionDto>(){
                new QuestionDto( 1, "InvalidCountry", "Islamabad"),
                new QuestionDto( 2, "Tajikistana", "Dushanbe"),
                new QuestionDto( 3, "InvalidCountry", "Manila"),
                new QuestionDto( 4, "Laos", "Vientiane"),
                new QuestionDto( 5, "Malta", "Valletta"),
                new QuestionDto( 6, "InvalidCountry", "Baku"),
                new QuestionDto( 7, "Poland", "Warsaw"),
                new QuestionDto( 8, "Indonesia", "Jakarta"),
                new QuestionDto( 9, "Kuwait", "Kuwait City"),
                new QuestionDto( 10, "South Korea", "Seoul"),
            }
        };

        var handler = new SaveExamCountryCommandHandler(_continentRepository, _examRepository, _countryService, _mapper);

        // act
        var action = () => handler.Handle(command, CancellationToken.None);

        // assert
        await action.Invoking(a => a.Invoke())
            .Should().ThrowAsync<NotFoundException>("Not found country");
    }

    [Fact()]
    public async void SaveExamCommandHandle_InvalidCategory_ThrowInvalidEnumArgumentException()
    {
        // arrange
        var command = new SaveExamCountryCommand()
        {
            Name = "Test",
            Category = "Capital Cityss",
            Continents = new List<ContinentDto>(){
                new ContinentDto("Europe"),
                new ContinentDto("Asia")
            },
            Questions = new List<QuestionDto>(){
                new QuestionDto( 1, "Pakistan", "Islamabad"),
                new QuestionDto( 2, "Tajikistan", "Dushanbe"),
                new QuestionDto( 3, "Philippines", "Manila"),
                new QuestionDto( 4, "Laos", "Vientiane"),
                new QuestionDto( 5, "Malta", "Valletta"),
                new QuestionDto( 6, "Azerbaijan", "Baku"),
                new QuestionDto( 7, "Poland", "Warsaw"),
                new QuestionDto( 8, "Indonesia", "Jakarta"),
                new QuestionDto( 9, "Kuwait", "Kuwait City"),
                new QuestionDto( 10, "South Korea", "Seoul"),
            }
        };

        var handler = new SaveExamCountryCommandHandler(_continentRepository, _examRepository, _countryService, _mapper);

        // act
        var action = () => handler.Handle(command, CancellationToken.None);

        // assert
        await action.Invoking(a => a.Invoke())
            .Should().ThrowAsync<InvalidEnumArgumentException>("Invalid category");
    }
}