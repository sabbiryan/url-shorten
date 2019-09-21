using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShorten.EntityFrameworkCore
{
    public class HitLog: EntityBase<string>
    {
        public string UrlMapId { get; set; }
        [ForeignKey("UrlMapId")] public virtual UrlMap UrlMap { get; set; }

        
    }
}