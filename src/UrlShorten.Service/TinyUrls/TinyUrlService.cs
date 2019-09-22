using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IRepository<UrlMap, string> _urlMaRepository;
        private readonly IConfiguration _configuration;

        private string UserAgent { get; set; }

        public TinyUrlService(IRepository<UrlMap, string> urlMaRepository,
            IConfiguration configuration)
        {
            _urlMaRepository = urlMaRepository;
            _configuration = configuration;
        }


        public void SetUserAgent(StringValues userAgent)
        {
            UserAgent = userAgent;
        }

        public async Task<List<UrlMapOutput>> GetAll(UrlMapFilterInput input)
        {
            var urlMaps = await _urlMaRepository.GetAll().OrderByDescending(x=> x.CreationTime).Skip(input.Skip).Take(input.Take).ToListAsync();

            var outputs = urlMaps.ConvertAll(x => new UrlMapOutput(x)).ToList();

            var shortenWebRootPath = _configuration.GetValue<string>("App:ShortenWebRootPath");
            foreach (var output in outputs)
            {
                output.ShortenUrlView = $"{shortenWebRootPath}{output.ShortenUrl}";
            }

            return outputs;
        }

        public async Task<UrlMapOutput> Get(string id)
        {
            var urlMap = await _urlMaRepository.GetAsync(id);

            return new UrlMapOutput(urlMap);
        }

        public async Task<UrlMapOutput> Create(UrlMapInput input)
        {
            var existingUrlMap = await _urlMaRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(x => x.RawUrl == input.RawUrl);

            if(existingUrlMap != null) return new UrlMapOutput(existingUrlMap);

            var urlMap = await _urlMaRepository.CreateAsync(new UrlMap()
            {
                Id = Guid.NewGuid().ToString(),
                Title = input.Title,
                Description = input.Description,
                RawUrl = input.RawUrl,
                DeviceInfo = UserAgent
            });

            urlMap.ShortenUrl = ShortenUrlHelper.Encode(urlMap.Identity);
            urlMap = await _urlMaRepository.UpdateAsync(urlMap);

            return new UrlMapOutput(urlMap);
        }

        public async Task<UrlMapOutput> Update(UrlMapInput input)
        {
            var existingUrlMap = await _urlMaRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(x => x.RawUrl == input.RawUrl);

            if (existingUrlMap != null) return new UrlMapOutput(existingUrlMap);


            var urlMap = await _urlMaRepository.GetAsync(input.Id);

            urlMap.Title = input.Title;
            urlMap.Description = input.Description;
            urlMap.DeviceInfo = UserAgent;
            urlMap.ShortenUrl = ShortenUrlHelper.Encode(urlMap.Identity);

            urlMap =await _urlMaRepository.UpdateAsync(urlMap);

            return new UrlMapOutput(urlMap);
        }

        public async Task Delete(string id)
        {
            await _urlMaRepository.DeleteAsync(id);
        }

        public async Task<bool> IsExist(string id)
        {
            return await _urlMaRepository.GetAll().AnyAsync(x => x.Id == id);
        }

        public async Task<UrlMapDetailOutput> GetDetail(string id)
        {
            var urlMap = await _urlMaRepository.GetAll().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return new UrlMapDetailOutput(urlMap);
        }

        
    }
}
