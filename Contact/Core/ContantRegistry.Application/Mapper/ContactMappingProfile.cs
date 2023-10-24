using AutoMapper;
using ContactRegistry.Domain.Entities;
using ContantRegistry.Application.Features.Commands.ContactCreate;
using ContantRegistry.Application.Features.Commands.ContactFeatureCreate;

namespace ContantRegistry.Application.Mapper;

public class ContactMappingProfile : Profile
{
    public ContactMappingProfile()
    {
        CreateMap<Contact, ContactCreateCommandRequest>();
        CreateMap<ContactFeature, ContactFeatureCreateCommandRequest>();
    }
}