using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShorten.Service.TinyUrls;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Web.Pages.Components.TinyUrlGrid
{
    public class TinyUrlGridViewComponent : ViewComponent
    {
        private readonly ITinyUrlService _tinyUrlService;

        public TinyUrlGridViewComponent(ITinyUrlService tinyUrlService)
        {
            _tinyUrlService = tinyUrlService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string sorting, int currentPage = 1, int take =10)
        {
            var urlMapOutputs = await _tinyUrlService.GetAll(new UrlMapFilterInput()
            {
                Sorting = sorting,
                Page = currentPage,
                Take = take,
            });

            return View(urlMapOutputs);
        }
    }
}
