using AutoMapper;
using ContactRegistry.Domain.Entities;
using ContantRegistry.Application.Features.Commands.ContactCreate;
using ContantRegistry.Application.Features.Commands.ContactFeatureCreate;
using ContantRegistry.Application.Features.Queries.GetContactById;

namespace ContantRegistry.Application.Mapper;

public class ContactMappingProfile : Profile
{
    public ContactMappingProfile()
    {
        CreateMap<Contact, ContactCreateCommandRequest>().ReverseMap();
        CreateMap<ContactFeature, ContactFeatureCreateCommandRequest>().ReverseMap();
        CreateMap<Contact, GetContactByIdQueryResponse>().ReverseMap();
    }
}