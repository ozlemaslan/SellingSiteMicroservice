﻿using CatalogService.Api.Core.Application.Domain;
using CatalogService.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Api.Infrastructure.EntityConfigurations
{
    public class CatalogBrandEntityTypeConfiguration : IEntityTypeConfiguration<CatalogBrand>
    {
        public void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToTable("CatalogBrand", CatalogContext.DEFAULT_SCHEMA);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).UseHiLo("catalog_brand_hilo").IsRequired(); //hilo algoritmasına göre sql de automatic arttıran algo

            builder.Property(p => p.Brand).IsRequired().HasMaxLength(128);
        }
    }
}
