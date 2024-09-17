using Microsoft.AspNetCore.Mvc;
using Sample.Application;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        /// <summary>
        /// Creates an Microsoft.AspNetCore.Mvc.BadRequestObjectResult that produces a Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest response.
        /// </summary>
        [NonAction]
        public BadRequestObjectResult BadRequest(Error error)
        {
            return base.BadRequest((ApiError)error);
        }

        /// <summary>
        /// Creates an Microsoft.AspNetCore.Mvc.ConflictObjectResult that produces a Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict response.
        /// </summary>
        [NonAction]
        public ConflictObjectResult Conflict(Error error)
        {
            return base.Conflict((ApiError)error);
        }

        /// <summary>
        /// Creates an Microsoft.AspNetCore.Mvc.NotFoundObjectResult that produces a Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound response.
        /// </summary>
        [NonAction]
        public NotFoundObjectResult NotFound(Error error)
        {
            return base.NotFound((ApiError)error);
        }

        /// <summary>
        /// Creates an Microsoft.AspNetCore.Mvc.NotFoundObjectResult that produces a Microsoft.AspNetCore.Http.StatusCodes.Status405MethodNotAllowed response.
        /// </summary>
        [NonAction]
        public ObjectResult NotAllowed(Error error)
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed, (ApiError)error);
        }

        /// <summary>
        /// Creates an Microsoft.AspNetCore.Mvc.NotFoundObjectResult that produces a Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError response.
        /// </summary>
        [NonAction]
        public ObjectResult UnexpectedError()
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ApiError.Unexpected());
        }

        /// <summary>
        /// Creates an Microsoft.AspNetCore.Mvc.NotFoundObjectResult that produces a Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError response.
        /// </summary>
        [NonAction]
        public ObjectResult UnexpectedError(Error error)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, (ApiError)error);
        }

        /// <summary>
        /// Creates an Microsoft.AspNetCore.Mvc.NotFoundObjectResult that produces a Microsoft.AspNetCore.Http.StatusCodes.Status417ExpectationFailed response.
        /// </summary>
        [NonAction]
        public ObjectResult ExpectedError(Error error)
        {
            return StatusCode(StatusCodes.Status417ExpectationFailed, (ApiError)error);
        }
    }
}