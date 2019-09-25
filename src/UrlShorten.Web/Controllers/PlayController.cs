using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.Service.TinyUrls;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayController: ControllerBase
    {
        private readonly ITinyUrlService _tinyUrlService;

        public PlayController(ITinyUrlService tinyUrlService)
        {
            _tinyUrlService = tinyUrlService;
        }

        // GET: api/Play/s
        [HttpGet("{relativePath}", Name = "PlayGet")]
        public async Task Get([FromRoute]string relativePath)
        {

            string redirectUrl = await _tinyUrlService.GetRedirectUrl(relativePath);

            if (!string.IsNullOrWhiteSpace(redirectUrl)) RedirectPermanent(redirectUrl);
        }
    }
}