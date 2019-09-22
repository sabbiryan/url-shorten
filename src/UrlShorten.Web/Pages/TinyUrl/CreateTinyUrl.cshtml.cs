using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UrlShorten.Service.TinyUrls;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Web.Pages.TinyUrl
{
    public class CreateTinyUrlModel : PageModel
    {
        private readonly ITinyUrlService _tinyUrlService;

        public CreateTinyUrlModel(ITinyUrlService tinyUrlService)
        {
            _tinyUrlService = tinyUrlService;
        }


        public void OnGet()
        {

        }


        public async Task<UrlMapOutput> OnPostAsync(UrlMapInput input)
        {
            return await _tinyUrlService.Create(input);
        }

    }
}
