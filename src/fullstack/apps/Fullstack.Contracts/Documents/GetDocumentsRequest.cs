namespace Fullstack.Contracts.Documents
{
  public record struct GetDocumentsRequest
  {
    public Take Take { get;  set; }

    public int CurrentPage { get;  set; }

    public string SearchQuery { get;  set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public SearchQueryDetails SearchQueryMeta => new(SearchQuery);

    public GetDocumentsRequest(Take take, string searchQuery, int currentPage)
    {
      SearchQuery ??= searchQuery;
      CurrentPage = currentPage < 0 ? 1 : currentPage;
      Take = take;
    }
    public bool IsValid()
    {
      if (!string.IsNullOrWhiteSpace(SearchQuery))
      {
        return SearchQuery.Length < 50 && Take.IsValid();
      }

      return false;
    }

    public readonly struct SearchQueryDetails
    {
      public bool IsDateTimeType { get; }

      public string SearchQuery { get; }
      public SearchQueryDetails(string searchQuery)
      {
        IsDateTimeType = DateTimeOffset.TryParse(searchQuery, out DateTimeOffset searchQueryTime);
        SearchQuery = searchQuery;
      }
    }


  }
}
