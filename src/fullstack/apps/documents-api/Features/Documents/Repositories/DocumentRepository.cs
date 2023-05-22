using Fullstack.DocumentsApi.Features.Documents.Data;
using Fullstack.DocumentsApi.Features.Documents.Models;
using Microsoft.EntityFrameworkCore;
using static Fullstack.Contracts.Documents.GetDocumentsRequest;

namespace Fullstack.DocumentsApi.Features.Documents.Repositories
{
  public class DocumentRepository : IDocumentRepository, IDisposable
  {
    private readonly DocumentDbContext _dbContext;
    public DocumentRepository(DocumentDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<ResultDetails<Document>> GetDocumentsAsync(SearchQueryDetails searchQuery, int take, int pageNumber)
    {
      IQueryable<Document> source = _dbContext.Documents;

      if (!string.IsNullOrWhiteSpace(searchQuery.SearchQuery))
      {
        source = searchQuery.IsDateTimeType ?
          source = source.Where(x => x.CreatedAt.Date == DateTimeOffset.Parse(searchQuery.SearchQuery))
        : source.Where(x => x.Number == searchQuery.SearchQuery || x.Author.Contains(searchQuery.SearchQuery));
      }
      int totalRecords = await source.CountAsync();
      source = ApplyPagination(source, take, pageNumber, totalRecords);

      return new ResultDetails<Document>(totalRecords, await source.ToListAsync());
    }

    private static IQueryable<Document> ApplyPagination(IQueryable<Document> source, int pageSize, int pageNumber, int totalRecords)
    {
      pageNumber = pageSize > totalRecords ? 1 : pageNumber;
      return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        if (disposing)
        {
          _dbContext.Dispose();
        }
      }
      this.disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }

  public record ResultDetails<T>(int TotalCount, IReadOnlyList<T> Items);

}
