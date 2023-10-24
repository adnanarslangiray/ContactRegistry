namespace ContantRegistry.Application.Features.Queries.GetContactById;

public class GetContactByIdQueryResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Company { get; set; }
    public object ContactFeatures { get; set; }
}