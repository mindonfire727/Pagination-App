using Fullstack.DocumentsApi.Features.Documents.Models;
using static Fullstack.Contracts.Documents.GetDocumentsRequest;

namespace Fullstack.DocumentsApi.Features.Documents.Repositories
{
  // Implement InternalsVisibleTo and make interface internal
  public interface IDocumentRepository
  {
    Task<ResultDetails<Document>> GetDocumentsAsync(SearchQueryDetails searchQuery, int take, int pageNumber);
  }
}
