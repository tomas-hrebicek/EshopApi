using Microsoft.AspNetCore.Mvc;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApiController : ControllerBase
    {
    }
}
