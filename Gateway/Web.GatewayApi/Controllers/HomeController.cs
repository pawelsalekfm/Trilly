using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.GatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            //_logger.Information("{MessageType} - {Body}", "HomeController", "Hello from CategoryApi!");
            return Content($"Hello from WebGateway: {DateTime.Now}");
        }
    }
}
