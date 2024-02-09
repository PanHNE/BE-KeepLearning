﻿using Application.Common.Models.Continent;

namespace Application.Common.Mappings;

public class ContinentMappingProfile : Profile
{
    public ContinentMappingProfile()
    {
        CreateMap<Domain.Entities.Continent, ContinentDto>();
    }
}
