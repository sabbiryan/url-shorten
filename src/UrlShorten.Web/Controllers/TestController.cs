using Microsoft.AspNetCore.Mvc;

namespace UrlShorten.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController: ControllerBase
    {

        public TestController()
        {
            
        }

        [HttpGet]
        public void Get([FromHeader] string relativePath)
        {

        }
    }
}