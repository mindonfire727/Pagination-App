using Fullstack.Contracts;
using Fullstack.Contracts.Documents;
using Fullstack.DocumentsApi.Features.Documents.DocumentErrors;
using Fullstack.DocumentsApi.Features.Documents.Models;
using Fullstack.DocumentsApi.Features.Documents.Repositories;
using Fullstack.DocumentsApi.Features.Documents.Services;
using Moq;

namespace Fullstack.DocumentsApi.Test.Documents
{
  public class DocumentTests
  {
    private Mock<IDocumentRepository> _mockRepository;
    private IDocumentService _documentService;

    [Fact]
    public async Task GetDocumentsAsync_ReturnsEmpty()
    {
      // Arrange
      _mockRepository = new Mock<IDocumentRepository>();
      _documentService = new DocumentService(_mockRepository.Object);
      string expectedErrorMessage = "Ups, für deine Suche gab es leider Treffer";

      GetDocumentsRequest request = new(new Take(10), "Author", 1);
      _mockRepository.Setup(repo => repo.GetDocumentsAsync(request.SearchQueryMeta, request.Take.Amount, 1))
        .ReturnsAsync(new ResultDetails<Document>(0, new List<Document> ()));

      // Act
      Func<Task<ResultDetails<DocumentDto>>> act = () => _documentService.GetDocumentsAsync(request);

      // Assert
      var exception = await Assert.ThrowsAsync<DocumentNotFoundException>(act);
      Assert.Equal(exception.ErrorMessage, expectedErrorMessage);
    }
  }
}
