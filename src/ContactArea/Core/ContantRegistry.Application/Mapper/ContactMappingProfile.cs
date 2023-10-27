using AutoMapper;
using ContactRegistry.Domain.Entities;
using ContactRegistry.Domain.Entities.Common;
using ContantRegistry.Application.DTOs;
using ContantRegistry.Application.Features.Commands.ContactCreate;
using ContantRegistry.Application.Features.Commands.ContactFeatureCreate;
using ContantRegistry.Application.Features.Queries.GetContactById;

namespace ContantRegistry.Application.Mapper;

public class ContactMappingProfile : Profile
{
    public ContactMappingProfile()
    {
        //CreateMap<BaseEntity, BaseDto>().IncludeAllDerived(); // base entity mapping

        CreateMap<Contact, ContactCreateCommandRequest>().ReverseMap();
        CreateMap<ContactFeature, ContactFeatureCreateCommandRequest>().ReverseMap();
        CreateMap<Contact, GetContactByIdQueryResponse>().ReverseMap();
        CreateMap<Contact, ContactCreateCommandResponse>();

        // Include base entity mapping
        CreateMap<BaseEntity, BaseDto>()
        .Include<Contact, ContactCreateCommandResponse>();
        CreateMap<Contact, ContactCreateCommandResponse>().ReverseMap();

    }
}