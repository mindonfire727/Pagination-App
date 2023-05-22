namespace Fullstack.DocumentsApi.Features.Documents.DocumentErrors
{
  [Serializable]
  public abstract class ServiceException : Exception
  {
    public abstract int HttpStatusCode { get; }
    public abstract string ErrorMessage { get; }
  }
}
