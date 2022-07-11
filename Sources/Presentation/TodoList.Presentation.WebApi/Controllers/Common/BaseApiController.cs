using Microsoft.AspNetCore.Mvc;

namespace TodoList.Presentation.WebApi.Controllers.Common
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
