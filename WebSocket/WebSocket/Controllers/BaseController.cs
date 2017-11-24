using Microsoft.AspNetCore.Mvc;

namespace WebSocket
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        public BaseController() { }
    }
}
