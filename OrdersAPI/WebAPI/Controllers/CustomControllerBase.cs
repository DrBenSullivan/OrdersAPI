#pragma warning disable 1591

using Microsoft.AspNetCore.Mvc;

namespace OrdersAPI.WebAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {
    }
}
