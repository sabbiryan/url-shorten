using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.Service.TinyUrls;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Web.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ITinyUrlService _tinyUrlService;

        public DeleteModel(ITinyUrlService tinyUrlService)
        {
            _tinyUrlService = tinyUrlService;
            //_tinyUrlService.SetUserAgent(Request.Headers["User-Agent"]);
        }

        [BindProperty]
        public UrlMapDetailOutput UrlMap { get; set; } = new UrlMapDetailOutput();

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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (await _tinyUrlService.IsExist(id))
            {
                await _tinyUrlService.Delete(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
