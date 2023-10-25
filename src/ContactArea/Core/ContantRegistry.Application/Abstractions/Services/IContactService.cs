using ContactRegistry.Domain.Entities;
using ContantRegistry.Application.DTOs;

namespace ContantRegistry.Application.Abstractions.Services;

public interface IContactService
{
    Task<Contact> GetbyIdAsync(string id);
    Task<ContactList> GetAllAsync(int page, int size);
    Task<ContactCreateList> PrepareContactReport();
}