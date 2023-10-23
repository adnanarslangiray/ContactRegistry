using ContactRegistry.Domain.Entities;

namespace ContactRegistry.Persistence.Contexts;

public class ContactDbSeed
{
    public static async Task SeedAsync(ContactDbContext context)
    {
        if (context.Contacts.Count() == 0)
        {
            context.Contacts.AddRange(GetFirstContactValues());
            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<Contact> GetFirstContactValues()
    {
        var contact = new Contact
        {
            FirstName = "Adnan",
            LastName = "Arslangiray",
            Company = "FreeCodeCamp"
        };
        yield return contact;
    }
}