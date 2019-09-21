using System;
using System.Collections.Generic;
using System.Text;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Service.HitLogs.Dto
{
    public class HitLogOutput
    {
        public string UrlMapId { get; set; }
        public UrlMapOutput UrlMap { get; set; }

        public DateTime CreationTime { get; set; }

        public string IpAddress { get; set; }

        public string DeviceInfo { get; set; }

        public HitLogOutput(HitLog model)
        {
            UrlMapId = model.UrlMapId;
            if(model.UrlMap != null) UrlMap = new UrlMapOutput(model.UrlMap);

            CreationTime = model.CreationTime;
            IpAddress = model.IpAddress;
            DeviceInfo = model.DeviceInfo;
        }
    }
}
