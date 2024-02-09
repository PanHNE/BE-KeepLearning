using Application.Common.Models.Question;

namespace Application.Common.Mappings;

public class QuestionMappingProfile : Profile
{
    public QuestionMappingProfile()
    {
        CreateMap<QuestionDto, Domain.Entities.Question>();
    }
}
