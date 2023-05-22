using Fullstack.Contracts.Documents;

namespace Fullstack.DocumentsApi.Features.Documents.Models;

public class Document
{
  public Guid Id { get; }
  public string Number { get; init; }
  public DateTimeOffset CreatedAt { get; init; }
  public string Author { get; init; }
  public DocumentType Type { get; init; }

  public Document(string number, DateTimeOffset createdAt, string author, DocumentType type)
  {
    Id = Guid.NewGuid();
    Number = number;
    CreatedAt = createdAt;
    Author = author;
    Type = type;
  }
}
