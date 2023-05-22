using Fullstack.Contracts.Documents;
using Fullstack.DocumentsApi.Features.Documents.Repositories;
using Fullstack.DocumentsApi.Features.Documents.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fullstack.DocumentsApi.Features.Controllers
{
  [ApiController]
  [Route("Documents")]
  public class DocumentController : ControllerBase
  {
    private readonly IDocumentService _documentsServicedocumentsService;

    public DocumentController(IDocumentService documentsServicedocumentsService)
    {
      _documentsServicedocumentsService = documentsServicedocumentsService;
    }

    /// <summary>
    /// Returns a list of filtered documents.
    /// </summary>
    /// <returns>Returns list of document</returns>
    /// <remarks>
    /// <response code="200">Returns the list of documents</response>
    /// <response code="400">Bad request</response>
    /// <response code="404">Document was not found</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GetDocumentsRequest))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAsync([FromBody] GetDocumentsRequest request)
    {
      if (!request.IsValid())
      {
        return BadRequest();
      }
      ResultDetails<DocumentDto> paginatedResult = await _documentsServicedocumentsService.GetDocumentsAsync(request);
      return Ok(new GetDocumentsResponse(paginatedResult.Items, request.CurrentPage, request.Take.Amount, paginatedResult.TotalCount));
    }
  }
}
