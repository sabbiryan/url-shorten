using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace UrlShorten.Web.Pages
{
    public class LogModel : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public LogModel(IWebHostEnvironment hostingEnvironment, 
            IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }


        public string Message { get; set; }
        public Array Logs { get; set; }


        public async Task OnGetAsync()
        {
            await Task.Delay(0);

            var value = _configuration.GetValue<string>("Log:FilePath");
            var path = value.Replace("{shortdate}", $"{DateTime.Now:yyyy-MM-dd}");

            if (System.IO.File.Exists(path))
            {
                Logs = System.IO.File.ReadAllLines(path);
                if (Logs == null)
                {
                    Message = "The file is empty.";
                }
            }
            else
            {
                Message = "The file does not exist.";
            }

        }
    }
}