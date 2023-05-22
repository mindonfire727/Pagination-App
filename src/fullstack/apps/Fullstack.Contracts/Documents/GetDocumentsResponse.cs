using Fullstack.Contracts.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fullstack.Contracts.Documents
{
  public record GetDocumentsResponse
  {
    public PaginationList<DocumentDto> PaginationList { get; private set; }

    public GetDocumentsResponse(IReadOnlyList<DocumentDto> list, int currentPage, int pageSize, int totalItems)
    {
      PaginationList = new PaginationList<DocumentDto>(list, totalItems, currentPage, pageSize);
    }
  }
}
