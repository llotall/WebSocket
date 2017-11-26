using Microsoft.AspNetCore.Mvc;

namespace WebSocketApi
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        public BaseController() { }
    }
}
