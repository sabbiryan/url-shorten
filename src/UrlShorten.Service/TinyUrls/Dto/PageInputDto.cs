using System.Collections.Generic;

namespace UrlShorten.Service.TinyUrls.Dto
{
    public class PageInputDto
    {
        public string Sorting { get; set; }
        public int Page { get; set; } = 1;
        public int Take { get; set; } = 10;
        public int Skip => (Page > 0 ? (Page - 1) : 0 ) * Take;
    }
}