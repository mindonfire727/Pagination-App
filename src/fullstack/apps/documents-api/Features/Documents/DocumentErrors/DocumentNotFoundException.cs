namespace Fullstack.DocumentsApi.Features.Documents.DocumentErrors
{
  public class DocumentNotFoundException : ServiceException
  {
    public override int HttpStatusCode => StatusCodes.Status404NotFound;

    public override string ErrorMessage => "Ups, für deine Suche gab es leider Treffer";
  }
}
