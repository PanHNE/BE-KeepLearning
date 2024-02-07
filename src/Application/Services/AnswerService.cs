using Domain.Models.Enums;
using static Domain.Models.Enums.GuessType;

namespace Infrastructure.Services;

public class AnswerService : IAnswerService
{
    private readonly ICountryService _countryService;

    public AnswerService(ICountryService countryService)
    {
        _countryService = countryService;
    }

    public async Task<string> GetCorrectAnswer(string questionText, GuessType.Category category)
    {
        var country = await _countryService.GetCountry(questionText, category);

        if (country == null)
            throw new NotFoundException(questionText, "Country");

        switch (category)
        {
            case Category.CapitalCity:
                return country.CapitalCity;

            case Category.Country:
                return country.Name;

            default:
                throw new NotImplementedException();
        }
    }

    public async Task<bool> IsCorrectAnswer(string questionText, string answerText, GuessType.Category category)
    {
        var country = await _countryService.GetCountry(questionText, category);
        if (country == null)
            throw new NotFoundException(questionText, "Country");

        if (answerText is null)
            return false;

        switch (category)
        {
            case Category.Country:
                return country.Name.ToLower().Equals(answerText.ToLower());

            case Category.CapitalCity:
                return country.CapitalCity.ToLower().Equals(answerText.ToLower());

            default: return false;
        }
    }
}
