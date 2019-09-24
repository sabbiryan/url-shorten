using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayController: ControllerBase
    {

        public PlayController()
        {
            
        }

        // GET: api/Play/s
        [HttpGet("{relativePath}", Name = "PlayGet")]
        public async Task Get([FromHeader]string relativePath)
        {
            await Task.Delay(0);
        }
    }
}