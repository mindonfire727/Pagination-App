using Fullstack.Contracts.Documents;
using Fullstack.DocumentsApi.Features.Documents.DocumentErrors;
using Fullstack.DocumentsApi.Features.Documents.Models;
using Fullstack.DocumentsApi.Features.Documents.Repositories;

namespace Fullstack.DocumentsApi.Features.Documents.Services
{
  public class DocumentService : IDocumentService
  {
    private readonly IDocumentRepository _documentsRepository;

    public DocumentService(IDocumentRepository documentsRepository)
    {
      _documentsRepository = documentsRepository;
    }

    public async Task<ResultDetails<DocumentDto>> GetDocumentsAsync(GetDocumentsRequest request)
    {
      ResultDetails<Document> paginatedResult = await _documentsRepository.GetDocumentsAsync(request.SearchQueryMeta, request.Take.Amount, request.CurrentPage);

      if (paginatedResult.TotalCount == 0)
      {
        throw new DocumentNotFoundException();
      }

      return new ResultDetails<DocumentDto>(paginatedResult.TotalCount, paginatedResult.Items.Select(MapToDto).ToList());
    }

    private static DocumentDto MapToDto(Document document)
    {
      return new DocumentDto(document.Id, document.Number, document.CreatedAt, document.Author, document.Type);
    }

  }
}
