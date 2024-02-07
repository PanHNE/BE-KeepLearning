using Application.Common.Models.Continent;

namespace Application.Common.Models.Exam.Country;

public class ExamCountryDto : ExamDto
{
    public required string Category { get; set; }
    public IEnumerable<ContinentDto> Continents { get; set; } = new List<ContinentDto>();
}
