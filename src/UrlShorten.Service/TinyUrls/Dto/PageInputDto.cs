namespace UrlShorten.Service.TinyUrls.Dto
{
    public class PageInputDto
    {
        public string Sorting { get; set; }
        public int Take { get; set; } = 10;
        public int Skip { get; set; }
    }
}