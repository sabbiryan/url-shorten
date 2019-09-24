using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        
        public string Filter { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }

        public UrlMapPageOutput Result { get;set; }

        public async Task OnGetAsync(string filter, int currentPage = 1, int pageSize = 10)
        {
            var input = new UrlMapFilterInput()
            {
                Filter = filter,
                Page = currentPage,
                Take = pageSize,
            };

            Result = await _tinyUrlService.GetAll(input);

            CurrentPage = currentPage;
            TotalPages = (int) Math.Ceiling(decimal.Divide(Result.TotalCount, pageSize));
        }
    }
}
