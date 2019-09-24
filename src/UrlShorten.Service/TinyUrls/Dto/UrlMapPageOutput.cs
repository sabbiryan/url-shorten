using System.Collections.Generic;

namespace UrlShorten.Service.TinyUrls.Dto
{
    public class UrlMapPageOutput : PageOutputDto<UrlMapOutput>
    {
        public UrlMapPageOutput(List<UrlMapOutput> items, int totalCount) : base(items, totalCount)
        {

        }
    }
}