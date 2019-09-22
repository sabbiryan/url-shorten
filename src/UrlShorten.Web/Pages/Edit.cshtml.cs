using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.Service.TinyUrls;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Web.Pages
{
    public class EditModel : PageModel
    {
        private readonly ITinyUrlService _tinyUrlService;

        public EditModel(ITinyUrlService tinyUrlService)
        {
            _tinyUrlService = tinyUrlService;
            //_tinyUrlService.SetUserAgent(Request.Headers["User-Agent"]);
        }

        [BindProperty]
        public UrlMapInput UrlMap { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urlMapOutput = await _tinyUrlService.Get(id);

            UrlMap = new UrlMapInput()
            {
                Id = urlMapOutput.Id,
                Title = urlMapOutput.Title,
                Description = urlMapOutput.Description,
                RawUrl = urlMapOutput.RawUrl,
            };

            if (UrlMap == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {

                await _tinyUrlService.Update(UrlMap);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UrlMapExists(UrlMap.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> UrlMapExists(string id)
        {
            return await _tinyUrlService.IsExist(id);
        }
    }
}
