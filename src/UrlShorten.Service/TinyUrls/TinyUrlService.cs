using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.EntityFrameworkCore.Repositories;
using UrlShorten.Service.Helpers;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Service.TinyUrls
{
    public class TinyUrlService: ITinyUrlService
    {
        private readonly IRepository<UrlMap, string> _urlMapRepository;
        private readonly IRepository<HitLog, string> _hitLogRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;

        private string UserAgent { get; set; }

        public TinyUrlService(IRepository<UrlMap, string> urlMapRepository,
            IConfiguration configuration, 
            IHttpContextAccessor httpContext, 
            IRepository<HitLog, string> hitLogRepository)
        {
            _urlMapRepository = urlMapRepository;
            _configuration = configuration;
            _httpContext = httpContext;
            _hitLogRepository = hitLogRepository;
        }


        public void SetUserAgent(StringValues userAgent)
        {
            UserAgent = userAgent;
        }


        public async Task<UrlMapPageOutput> GetAll(UrlMapFilterInput input)
        {
            var queryable = _urlMapRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                queryable = queryable.Where(x => x.RawUrl.Contains(input.Filter) || x.Title.Contains(input.Filter));
            }

            var totalCount = await queryable.CountAsync();

            var urlMaps = await queryable.OrderByDescending(x=> x.CreationTime).Skip(input.Skip).Take(input.Take).ToListAsync();

            var outputs = urlMaps.ConvertAll(x => new UrlMapOutput(x)).ToList();

            var shortenWebRootPath = _configuration.GetValue<string>("App:ShortenWebRootPath");
            foreach (var output in outputs)
            {
                output.ShortenUrlView = $"{shortenWebRootPath}{output.ShortenUrl}";
            }

            return new UrlMapPageOutput(outputs, totalCount);
        }

        public async Task<UrlMapOutput> Get(string id)
        {
            var urlMap = await _urlMapRepository.GetAsync(id);

            return new UrlMapOutput(urlMap);
        }

        public async Task<UrlMapOutput> Create(UrlMapInput input)
        {
            var existingUrlMap = await _urlMapRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(x => x.RawUrl == input.RawUrl);

            if(existingUrlMap != null)
            {
                //throw new Exception($"An existing item with same url already exist"); 
                return new UrlMapOutput(existingUrlMap);
            }

            var urlMap = await _urlMapRepository.CreateAsync(new UrlMap()
            {
                Id = Guid.NewGuid().ToString(),
                Title = input.Title,
                Description = input.Description,
                RawUrl = input.RawUrl,
                DeviceInfo = UserAgent
            });

            urlMap.ShortenUrl = ShortenUrlHelper.Encode(urlMap.Identity);
            urlMap = await _urlMapRepository.UpdateAsync(urlMap);

            return new UrlMapOutput(urlMap);
        }

        public async Task<UrlMapOutput> Update(UrlMapInput input)
        {
            var existingUrlMap = await _urlMapRepository.GetAll()
                .FirstOrDefaultAsync(x => x.RawUrl == input.RawUrl);

            if (existingUrlMap != null)
            {
                existingUrlMap.Title = input.Title;
                existingUrlMap.Description = input.Description;

                existingUrlMap = await _urlMapRepository.UpdateAsync(existingUrlMap);

                return new UrlMapOutput(existingUrlMap);
            }


            var urlMap = await _urlMapRepository.GetAsync(input.Id);

            urlMap.Title = input.Title;
            urlMap.Description = input.Description;
            urlMap.DeviceInfo = UserAgent;
            urlMap.ShortenUrl = ShortenUrlHelper.Encode(urlMap.Identity);

            urlMap = await _urlMapRepository.UpdateAsync(urlMap);

            return new UrlMapOutput(urlMap);
        }

        public async Task Delete(string id)
        {
            await _urlMapRepository.DeleteAsync(id);
        }


        public async Task<bool> IsUrlExist(string url, string id)
        {
            return await _urlMapRepository.GetAll().AnyAsync(x => x.RawUrl == url && x.Id != id);
        }

        public async Task<bool> IsExist(string id)
        {
            return await _urlMapRepository.GetAll().AnyAsync(x => x.Id == id);
        }

        public async Task<UrlMapDetailOutput> GetDetail(string id)
        {
            var urlMap = await _urlMapRepository.GetAll().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return new UrlMapDetailOutput(urlMap);
        }



        public async Task<string> GetRedirectUrl(string relativePath)
        {
            var requestUrl =
                $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}{_httpContext.HttpContext.Request.Path}{_httpContext.HttpContext.Request.QueryString.Value}";


            var value = _configuration.GetValue<string>("App:ShortenWebRootPath");

            var relativeValue = requestUrl.Replace(value, "");

            var urlMap = await _urlMapRepository.GetAll().FirstOrDefaultAsync(x => x.ShortenUrl == relativeValue);

            if (urlMap != null)
            {
                var hitLog = new HitLog()
                {
                    Id = Guid.NewGuid().ToString(),
                    UrlMapId = urlMap.Id
                };
                await _hitLogRepository.CreateAsync(hitLog);

                urlMap.HitCount += 1;
                await _urlMapRepository.UpdateAsync(urlMap);

                return urlMap.RawUrl;
            }

            throw new Exception($"The requested url does not exist!!");
        }

    }
}
