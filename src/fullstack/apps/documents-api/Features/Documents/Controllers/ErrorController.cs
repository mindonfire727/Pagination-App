using Fullstack.DocumentsApi.Features.Documents.DocumentErrors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Fullstack.DocumentsApi.Features.Documents.Controllers
{
  public class ErrorController : ControllerBase
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ErrorController(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("Error")]
    public IActionResult Error()
    {
      Exception? exception = _httpContextAccessor.HttpContext?
    .Features.Get<IExceptionHandlerFeature>()?
    .Error;

      return exception is ServiceException e
          ? Problem(
              title: e.ErrorMessage,
              statusCode: e.HttpStatusCode)
          : Problem(
              title: "An error occurred while processing your request.",
              statusCode: StatusCodes.Status500InternalServerError);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/Error-development")]
    public IActionResult HandleErrorDevelopment(
    [FromServices] IHostEnvironment hostEnvironment)
    {
      if (!hostEnvironment.IsDevelopment())
      {
        return NotFound();
      }

      var exceptionHandlerFeature =
          HttpContext.Features.Get<IExceptionHandlerFeature>()!;

      return Problem(
          detail: exceptionHandlerFeature.Error.StackTrace,
          title: exceptionHandlerFeature.Error.Message);
    }
  }
}
