using CatalogService.Api.Core.Application.Domain;
using CatalogService.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Api.Infrastructure.EntityConfigurations
{
    public class CatalogTypeEntityTypeConfiguration : IEntityTypeConfiguration<CatalogType>
    {
        public void Configure(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("CatalogType", CatalogContext.DEFAULT_SCHEMA);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).UseHiLo("catalog_type_hilo").IsRequired();

            builder.Property(p => p.Type).IsRequired().HasMaxLength(128);
        }
    }
}
