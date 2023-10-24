using ContantRegistry.Application.DTOs;

namespace ContantRegistry.Application.Features.Queries.GetContacts;

public class GetContactsQueryResponse
{
    public object Contacts { get; set; }
    public int TotalCount { get; set; }
  
}