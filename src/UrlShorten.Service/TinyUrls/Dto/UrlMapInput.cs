using System;
using System.Text;

namespace UrlShorten.Service.TinyUrls.Dto
{
    public class UrlMapInput
    {

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public string ShortenUrl { get; set; }
        public string RawUrl { get; set; }

    }
}
