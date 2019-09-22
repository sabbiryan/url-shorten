using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.Service.TinyUrls;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITinyUrlService _tinyUrlService;

        public IndexModel(ITinyUrlService tinyUrlService)
        {
            _tinyUrlService = tinyUrlService;
        }

        public IList<UrlMapOutput> UrlMap { get;set; }

        public async Task OnGetAsync()
        {
            UrlMap = await _tinyUrlService.GetAll(new UrlMapFilterInput());
        }
    }
}
