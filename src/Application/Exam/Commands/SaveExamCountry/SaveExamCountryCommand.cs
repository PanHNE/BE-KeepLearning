using Application.Common.Models.Continent;
using Application.Common.Models.Question;

namespace Application.Exam.Commands.SaveExamCountry;

public class SaveExamCountryCommand : IRequest<Guid>
{
    public string? Name { get; set; }
    public required string Category { get; set; }
    public required List<ContinentDto> Continents { get; set; }
    public required IEnumerable<QuestionDto> Questions { get; set; }
}
