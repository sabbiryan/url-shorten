using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.Service.TinyUrls;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Web.Pages
{
    public class CreateModel : PageModel
    {

        private readonly ITinyUrlService _tinyUrlService;

        public CreateModel(ITinyUrlService tinyUrlService)
        {
            _tinyUrlService = tinyUrlService;
            //_tinyUrlService.SetUserAgent(Request.Headers["User-Agent"]);
        }


        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UrlMapInput UrlMap { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _tinyUrlService.Create(UrlMap);

            return RedirectToPage("./Index");
        }
    }
}