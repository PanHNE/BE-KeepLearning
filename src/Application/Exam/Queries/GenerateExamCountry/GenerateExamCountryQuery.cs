using Application.Common.Models.Exam.Country;

namespace Application.Exam.Queries.GenerateExamCountry;

public class GenerateExamCountryQuery : IRequest<ExamCountryDto>
{
    public string? Name { get; set; }
    public int NumberOfQuestion { get; set; } = 10;
    public required string Category { get; set; }
    public List<string> Continents { get; set; } = new List<string>();
}
