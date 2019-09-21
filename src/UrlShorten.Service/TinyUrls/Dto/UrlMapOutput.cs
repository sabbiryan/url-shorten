using System.Collections.Generic;
using System.Linq;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.Service.HitLogs.Dto;

namespace UrlShorten.Service.TinyUrls.Dto
{
    public class UrlMapOutput 
    {
        public string Id { get; set; }
        public int Identity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortenUrl { get; set; }
        public string ShortenUrlView { get; set; }
        public string RawUrl { get; set; }
        public int HitCount { get; set; }

        public List<HitLogOutput> HitLogs { get; set; }


        public UrlMapOutput(UrlMap model)
        {
            Id = model.Id;
            Identity = model.Identity;
            Title = model.Title;
            Description = model.Description;
            ShortenUrl = model.ShortenUrl;
            RawUrl = model.RawUrl;
            HitCount = model.HitCount;

            if (model.HitLogs != null) HitLogs = model.HitLogs.ToList().ConvertAll(x => new HitLogOutput(x));
        }
    }
}