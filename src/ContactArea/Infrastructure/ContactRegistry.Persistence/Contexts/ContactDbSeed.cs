﻿using ContactRegistry.Domain.Entities;

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
        List<ContactFeature> features = new();
        features.AddRange(new List<ContactFeature>
        {
            new ContactFeature()
            {
                ContactFeatureType = ContactFeatureType.Phone,
                ContactFeatureValue = "5555555555",
                ContactId = contact.Id
            },
            new ContactFeature()
            {
                ContactFeatureType = ContactFeatureType.Location,
                ContactFeatureValue = "İstanbul",
                ContactId = contact.Id
            },
            new ContactFeature()
            {
                ContactFeatureType = ContactFeatureType.Email,
                ContactFeatureValue = "adnan.arslangiray@gmail.com",
                ContactId = contact.Id
            }
        });
        contact.ContactFeatures = features;
        yield return contact;
    }
}