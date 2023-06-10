using Microsoft.EntityFrameworkCore;
using ProvidersBase.Model.Models;

namespace ProvidersBase.Model.DataAccessLayer
{
    /// <summary>
    /// Represents a database context for the Providers Base application, allowing access to the <see cref="ProviderCompany"/>, <see cref="ProviderUser"/>, and <see cref="ProviderProduct"/> tables.
    /// </summary>
    public class ProvidersContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProvidersContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public ProvidersContext(DbContextOptions<ProvidersContext> options) : base(options) { }

        /// <summary>
        /// The DbSet for the <see cref="ProviderCompany"/> table.
        /// </summary>
        public DbSet<ProviderCompany> Providers { get; set; }

        /// <summary>
        /// The DbSet for the <see cref="ProviderUser"/> table.
        /// </summary>
        public DbSet<ProviderUser> Users { get; set; }

        /// <summary>
        /// The DbSet for the <see cref="ProviderProduct"/> table.
        /// </summary>
        public DbSet<ProviderProduct> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //This code sets up the One-to-Many relationships between the entities in the database using a Foreign Key constraint.
            //Specifically, it uses the Fluent API to configure the relationships between the Provider company entity and the Users and Products entities.
            modelBuilder.Entity<ProviderUser>()
                .HasOne(u => u.Provider)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.ProviderId);
            modelBuilder.Entity<ProviderProduct>()
                .HasOne(prod => prod.Provider)
                .WithMany(p => p.Products)
                .HasForeignKey(prod => prod.ProviderId);
        }

    }
}
