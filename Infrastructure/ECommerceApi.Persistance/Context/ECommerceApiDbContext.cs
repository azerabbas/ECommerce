using ECommerceApi.Domain.Entities;
using ECommerceApi.Domain.Entities.Common;
using ECommerceApi.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ECommerceApi.Persistance.Context
{
    public class ECommerceApiDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ECommerceApiDbContext(DbContextOptions options) : base(options)
        {

        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //SaveChanges Entityler uzerinde deyisiklikleri ve ya yeni add olunan datalari bildiren propertydir. Update prosesinde yeni elde edilen datalari goturub elde etmeyimizi heyata kecirir.
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow

                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Product> Products => this.Set<Product>();
        public DbSet<Order> Orders => this.Set<Order>();
        public DbSet<Customer> Customers => this.Set<Customer>();
        public DbSet<Domain.Entities.File> Files => this.Set<Domain.Entities.File>();
        public DbSet<ProductImageFile> ProductImageFiles => this.Set<ProductImageFile>();
        public DbSet<InvoiceFile> InvoiceFiles => this.Set<InvoiceFile>();
    }
}
