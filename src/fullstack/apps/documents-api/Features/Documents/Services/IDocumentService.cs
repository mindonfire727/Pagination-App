using ErrorOr;
using Fullstack.Contracts.Documents;
using Fullstack.DocumentsApi.Features.Documents.Repositories;

namespace Fullstack.DocumentsApi.Features.Documents.Services
{
  public interface IDocumentService
  {
    Task<ResultDetails<DocumentDto>> GetDocumentsAsync(GetDocumentsRequest request);
  }
}
