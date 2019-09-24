using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShorten.Service.TinyUrls;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinyUrlController : ControllerBase
    {
        private readonly ITinyUrlService _tinyUrlService;

        public TinyUrlController(ITinyUrlService tinyUrlService)
        {
            _tinyUrlService = tinyUrlService;
            //_tinyUrlService.SetUserAgent(Request.Headers["User-Agent"]);
        }

        // GET: api/TinyUrl
        [HttpGet]
        public async Task<UrlMapPageOutput> Get([FromQuery] UrlMapFilterInput input)
        {
            return await _tinyUrlService.GetAll(input);
        }

        // GET: api/TinyUrl/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<UrlMapOutput> Get(string id)
        {
            return await _tinyUrlService.Get(id);
        }

        // POST: api/TinyUrl
        [HttpPost]
        public async Task<UrlMapOutput> Post([FromBody] UrlMapInput input)
        {
            return await _tinyUrlService.Create(input);
        }

        // PUT: api/TinyUrl/5
        [HttpPut("{id}")]
        public async Task<UrlMapOutput> Put(string id, [FromBody] UrlMapInput input)
        {
            if (id != input.Id) throw new Exception("Item does not exist!");

            return await _tinyUrlService.Update(input);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _tinyUrlService.Delete(id);
        }
    }
}
