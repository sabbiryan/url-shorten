using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.Service.TinyUrls;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Web.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ITinyUrlService _tinyUrlService;

        public DetailsModel(ITinyUrlService tinyUrlService)
        {
            _tinyUrlService = tinyUrlService;
        }

        public UrlMapDetailOutput UrlMap { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UrlMap = await _tinyUrlService.GetDetail(id);

            if (UrlMap == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
