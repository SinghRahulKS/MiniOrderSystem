using Microsoft.EntityFrameworkCore;
using ProductService.Repository.Entity;
using System;

namespace ProductService.Repository
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Apply snake_case to all table and column names
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // table name
                entity.SetTableName(entity.GetTableName()?.ToLowerInvariant());

                // column names
                foreach (var property in entity.GetProperties())
                    property.SetColumnName(property.Name.ToLowerInvariant());
            }

            // ✅ Table names
            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Category>().ToTable("categories");

            // ✅ Auto-generate GUID for Id
            modelBuilder.Entity<Product>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Category>()
                .Property(c => c.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            // ✅ Relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ Indexes
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique(false);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique(true);

            // ✅ Configure timestamps
            modelBuilder.Entity<Product>()
                .Property(p => p.CreatedOn)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Product>()
                .Property(p => p.UpdatedOn)
                .HasColumnType("timestamp with time zone")
                .IsRequired(false);

            modelBuilder.Entity<Category>()
               .Property(p => p.CreatedOn)
               .HasColumnType("timestamp with time zone")
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Category>()
                .Property(p => p.UpdatedOn)
                .HasColumnType("timestamp with time zone")
                .IsRequired(false);
        }
    }
}
