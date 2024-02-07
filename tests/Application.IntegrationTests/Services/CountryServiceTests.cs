using Application.Common.Mappings;
using Application.Helper.Seeders.IntegrationTests;
using Application.UnitTests.Helper;
using AutoMapper;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using static Domain.Models.Enums.GuessType;
using EntityCountry = Domain.Enteties.Country;

namespace Application.Services.UnitTests;

public class CountryServiceTests : TestBasics
{
    public record CountryAndCategory(EntityCountry country, Category category) { }

    public static IEnumerable<object[]> GetQuestionCategoryAndAnswer()
    {
        var list = new List<CountryAndCategory>()
        {
            new CountryAndCategory(new EntityCountry() {
                Name = "Bolivia",
                Abbreviation = "BOL",
                CapitalCity = "La Paz",
                ContinentId = Guid.NewGuid()
            }, Category.CapitalCity),

            new CountryAndCategory(new EntityCountry() {
                Name = "Poland",
                Abbreviation = "POL",
                CapitalCity = "Warsaw",
                ContinentId = Guid.NewGuid()
            }, Category.Country),

            new CountryAndCategory(new EntityCountry() {
                Name = "Germany",
                Abbreviation = "GER",
                CapitalCity = "Berlin",
                ContinentId = Guid.NewGuid()
            }, Category.CapitalCity),

            new CountryAndCategory(new EntityCountry() {
                Name = "Australia",
                Abbreviation = "AUS",
                CapitalCity = "Canberra",
                ContinentId = Guid.NewGuid()
            }, Category.Country)
        };

        return list.Select(el => new[] { el });
    }

    [Theory]
    [MemberData(nameof(GetQuestionCategoryAndAnswer))]
    public async Task GetCorrectAnswer_WithValidCountry_ReturnCorrectAnswer(CountryAndCategory countryAndCategory)
    {
        // arrange
        var guessTypeCategory = countryAndCategory.category;
        var questionText = GetQuestionText(countryAndCategory.country, guessTypeCategory);
        var answerText = GetAnswerText(countryAndCategory.country, guessTypeCategory);

        // act
        var result = await _answerService.GetCorrectAnswer(questionText, guessTypeCategory);

        // assert
        result.Should().Be(answerText);
    }

    [Theory]
    [MemberData(nameof(GetQuestionCategoryAndAnswer))]
    public async Task IsCorrectAnswer_WithValidCountry_ReturnTrue(CountryAndCategory countryAndCategory)
    {
        // arrange
        var guessTypeCategory = countryAndCategory.category;
        var questionText = GetQuestionText(countryAndCategory.country, guessTypeCategory);
        var answerText = GetAnswerText(countryAndCategory.country, guessTypeCategory);

        // act
        var result = await _answerService.IsCorrectAnswer(questionText, answerText, guessTypeCategory);

        // assert
        result.Should().Be(true);
    }

    [Fact]
    public async Task IsCorrectAnswer_WithWrongAnswer_ReturnFalse()
    {
        // arrange
        var country = new EntityCountry()
        {
            Name = "Australia",
            Abbreviation = "AUS",
            CapitalCity = "Canberra",
            ContinentId = Guid.NewGuid()
        };

        var guessTypeCategory = Category.Country;

        var questionText = GetQuestionText(country, guessTypeCategory);
        var wrongAnswerText = "Wrong answer";

        // act
        var result = await _answerService.IsCorrectAnswer(questionText, wrongAnswerText, guessTypeCategory);

        // assert
        result.Should().Be(false);
    }

    [Fact()]
    public async void GetRandomCountry_ForValidContinent_ReturnRadnomCountryWithGivenContinent()
    {
        // arrange
        var continentId = await _dbContext.Continents.Where(c => c.Name == "Asia").Select(c => c.Id).FirstAsync();

        // act
        var randomCountry = await _countryService.GetRandom(continentId);

        // assert
        randomCountry.Should().NotBeNull();
        randomCountry!.ContinentId.Should().Be(continentId);
    }

    [Theory()]
    [InlineData(1)]
    [InlineData(2)]
    public async void GetRandomCountries_ForValidContinent_ReturnRadnomCountriesWithoutDuplicate(int numberOfElemetns)
    {
        // arrange
        var continentIds = await _dbContext.Continents.Where(c => c.Name == "Asia").Select(c => c.Id).ToListAsync();

        // act
        var randomCountries = await _countryService.RandomCountries(continentIds, numberOfElemetns);

        // assert
        randomCountries.Should().NotBeNull();
        randomCountries.Count().Should().Be(numberOfElemetns);
        randomCountries.Distinct().Count().Should().Be(numberOfElemetns);
    }

    private string GetQuestionText(EntityCountry country, Category categry)
    {
        switch (categry)
        {
            case Category.Country: return country.CapitalCity;
            case Category.CapitalCity: return country.Name;
            default: throw new NotImplementedException();
        }
    }

    private string GetAnswerText(EntityCountry country, Category categry)
    {
        switch (categry)
        {
            case Category.Country: return country.Name;
            case Category.CapitalCity: return country.CapitalCity;
            default: throw new NotImplementedException();
        }
    }
}
