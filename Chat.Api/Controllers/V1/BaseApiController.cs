using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
} 