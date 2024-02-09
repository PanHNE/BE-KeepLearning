using System.Text.Json;
using Application.Common.Models.Continent;
using Application.Exam.Commands.SaveExamCountry;

namespace Application.Common.Mappings;

public class ExamMappingProfile : Profile
{
    public ExamMappingProfile()
    {
        CreateMap<SaveExamCountryCommand, Domain.Entities.Exam>()
            .ForMember(dest =>
                dest.ExtraInformations,
                opt => opt.MapFrom(src => CreateJsonForExamCountryExtraInformations(src.Category, src.Continents)));
    }

    class ExamCountryExtraInformation
    {
        public string? Category { get; set; }
        public List<string>? Continents { get; set; }
    }

    private string CreateJsonForExamCountryExtraInformations(string category, List<ContinentDto> continents)
    {
        var continentsString = continents.Select(c => c.Name).ToList();
        var extraInformations = new ExamCountryExtraInformation() { Category = category, Continents = continentsString };

        var categoryJson = JsonSerializer.Serialize<ExamCountryExtraInformation>(extraInformations);

        return categoryJson;
    }
}
