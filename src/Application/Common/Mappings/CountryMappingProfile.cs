using Application.Common.Models.Country;

namespace Application.Common.Mappings;

public class CountryMappingProfile : Profile
{
    public CountryMappingProfile()
    {
        CreateMap<Domain.Enteties.Country, CountryDto>()
            .ForMember( countryDto => countryDto.ContinentDto, opt => opt.MapFrom(src => src.Continent));
    }
}
