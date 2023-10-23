using ContactRegistry.Domain.Entities;
using ContactRegistry.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ContactRegistry.Persistence.Contexts; 

public class ContactDbContext : DbContext {

    public ContactDbContext( DbContextOptions<ContactDbContext> options ) : base( options ) {
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder ) {
        modelBuilder.ApplyConfigurationsFromAssembly( typeof( ContactDbContext ).Assembly );
    }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<ContactFeature> ContactFeatures { get; set; }

    public override async Task<int> SaveChangesAsync( CancellationToken cancellationToken = default ) {
        //ChangeTracker : Entityler üzerinden yapılan değişiklerin ya da yeni eklenen verinin yakalanmasını sağlayan propertydir. Update operasyonlarında Track edilen verileri yakalayıp elde etmemizi sağlar.

        var changedData = ChangeTracker
             .Entries<BaseEntity>();

        foreach (var data in changedData) {
            _ = data.State switch {
                EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                _ => DateTime.UtcNow
            };
        }

        return await base.SaveChangesAsync( cancellationToken );
    }
}