using System;
using System.Collections.Generic;
using System.Linq;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.Service.HitLogs.Dto;

namespace UrlShorten.Service.TinyUrls.Dto
{
    public class UrlMapDetailOutput
    {
        public string Id { get; set; }
        public int Identity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortenUrl { get; set; }
        public string ShortenUrlView { get; set; }
        public string RawUrl { get; set; }
        public int HitCount { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? ModificationTime { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletionTime { get; set; }

        public string IpAddress { get; set; }

        public string DeviceInfo { get; set; }

        public List<HitLogOutput> HitLogs { get; set; }


        public UrlMapDetailOutput()
        {
            
        }


        public UrlMapDetailOutput(UrlMap model)
        {
            Id = model.Id;
            Identity = model.Identity;
            Title = model.Title;
            Description = model.Description;
            ShortenUrl = model.ShortenUrl;
            RawUrl = model.RawUrl;
            HitCount = model.HitCount;

            CreationTime = model.CreationTime;
            ModificationTime = model.ModificationTime;
            IsDeleted = model.IsDeleted;
            DeletionTime = model.DeletionTime;
            IpAddress = model.IpAddress;
            DeviceInfo = model.DeviceInfo;

            if (model.HitLogs != null) HitLogs = model.HitLogs.ToList().ConvertAll(x => new HitLogOutput(x));
        }
    }
}