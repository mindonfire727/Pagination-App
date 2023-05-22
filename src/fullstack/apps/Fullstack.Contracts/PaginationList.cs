using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fullstack.Contracts
{
  public class PaginationList<T>
  {
    public int TotalCount { get; private set; }
    public int PageSize { get; private set; }
    public int CurrentPage { get; private set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public IEnumerable<T> Items { get; private set; }

    public PaginationList(IEnumerable<T> items, int totalCount, int currentPage, int pageSize)
    {
      TotalCount = totalCount;
      CurrentPage = currentPage;
      PageSize = pageSize;
      Items = items;
    }

    public static PaginationList<T> Create(IReadOnlyList<T> items, int total, int pageNumber, int pageSize)
    {
      var totalCount = total;
      //var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

      return new PaginationList<T>(items, totalCount, pageNumber, pageSize);
    }
  }
}
