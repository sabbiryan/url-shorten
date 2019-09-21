using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShorten.Service.TinyUrls;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Web.Pages.Components.TinyUrlGrid
{
    public class GetTinyUrlViewComponent : ViewComponent
    {
        private readonly ITinyUrlService _tinyUrlService;

        public GetTinyUrlViewComponent(ITinyUrlService tinyUrlService)
        {
            _tinyUrlService = tinyUrlService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var urlMapOutput = await _tinyUrlService.Get(id);

            return View(urlMapOutput);
        }
    }
}
