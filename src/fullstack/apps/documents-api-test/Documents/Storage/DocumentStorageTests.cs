using Fullstack.Contracts;
using Fullstack.Contracts.Documents;
using Fullstack.DocumentsApi.Features.Documents.Data;
using Fullstack.DocumentsApi.Features.Documents.Models;
using Fullstack.DocumentsApi.Features.Documents.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.DocumentsApi.Test.Documents.Storage
{
  public class DocumentStorageTests : IDisposable
  {
    private DocumentDbContext _dbContext;
    public DocumentStorageTests()
    {
      var dbContextOptions = new DbContextOptionsBuilder<DocumentDbContext>()
          .UseInMemoryDatabase(databaseName: "test")
          .Options;
      _dbContext = new DocumentDbContext(dbContextOptions);
    }

    public void Dispose()
    {
      GC.SuppressFinalize(this);
      _dbContext.Dispose();
    }

    [Theory()]
    [InlineData("Author")]
    [InlineData("1231231231232")]
    [InlineData("20.05.2022")]
    public async Task GetByValidSearchQuery_ReturnsDocument(string searchQuery)
    {
      // Arrange
      Document expectedDocument = new("1231231231232", new DateTimeOffset(new DateTime(2022, 05, 20)), "Author", DocumentType.Offer);
      List<Document> documents = new()
      { expectedDocument,
        new("1231231231234", DateTimeOffset.Now, "Tester", DocumentType.Invoice),
        new("1231231231235", DateTimeOffset.Now, "Logic", DocumentType.Offer)};
      _dbContext.Documents.AddRange(documents);
      await _dbContext.SaveChangesAsync();
      var repository = new DocumentRepository(_dbContext);
      GetDocumentsRequest request = new(new Take(10), searchQuery, 1);

      // Act
      var result = await repository.GetDocumentsAsync(request.SearchQueryMeta, request.Take.Amount, request.CurrentPage);

      // Assert
      Assert.NotNull(result);
      Assert.True(result.TotalCount > 0);
    }

    [Fact]
    public async Task GetByDocumentNumber_ReturnsDocument()
    {
      // Arrange
      Document expectedDocument = new("1231231231232", DateTimeOffset.Now, "Author", DocumentType.Offer);
      List<Document> documents = new()
      { expectedDocument,
        new("1231231231234", DateTimeOffset.Now, "Tester", DocumentType.Invoice),
        new("1231231231235", DateTimeOffset.Now, "Logic", DocumentType.Offer)};
      _dbContext.Documents.AddRange(documents);
      await _dbContext.SaveChangesAsync();
      var repository = new DocumentRepository(_dbContext);
      GetDocumentsRequest request = new(new Take(10), expectedDocument.Number, 1);

      // Act
      var result = await repository.GetDocumentsAsync(request.SearchQueryMeta, request.Take.Amount, request.CurrentPage);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(result.Items.FirstOrDefault().Number, expectedDocument.Number);
    }

    [Fact]
    public async Task GetByNonExistentDocumentNumber_ReturnsEmpty()
    {
      // Arrange
      Document expectedDocument = new("1231231231232", DateTimeOffset.Now, "Author", DocumentType.Offer);
      List<Document> documents = new()
      { expectedDocument,
        new("1231231231234", DateTimeOffset.Now, "Tester", DocumentType.Invoice),
        new("1231231231235", DateTimeOffset.Now, "Logic", DocumentType.Offer)};
      _dbContext.Documents.AddRange(documents);
      await _dbContext.SaveChangesAsync();
      var repository = new DocumentRepository(_dbContext);
      GetDocumentsRequest request = new(new Take(10), "test", 1);

      // Act
      var result = await repository.GetDocumentsAsync(request.SearchQueryMeta, request.Take.Amount, request.CurrentPage);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(result.TotalCount, 0);
    }


    // test pagination
  }
}
