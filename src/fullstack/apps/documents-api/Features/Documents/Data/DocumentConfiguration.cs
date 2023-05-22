using Fullstack.DocumentsApi.Features.Documents.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fullstack.DocumentsApi.Features.Documents.Data;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
  public void Configure(EntityTypeBuilder<Document> builder)
  {
    builder.HasKey(ba => ba.Id);
    builder.Property(x => x.Number).IsRequired();
    builder.Property(x => x.CreatedAt).IsRequired();
    builder.Property(x => x.Author).IsRequired();

    builder.HasData(DocumentSeedGenerator.Seed());
  }
}
