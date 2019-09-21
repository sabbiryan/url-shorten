using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.EntityFrameworkCore.Repositories;
using UrlShorten.Service.Helpers;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Service.TinyUrls
{
    public class TinyUrlService: ITinyUrlService
    {
        private readonly IRepository<UrlMap, string> _urlMaRepository;

        public TinyUrlService(IRepository<UrlMap, string> urlMaRepository)
        {
            _urlMaRepository = urlMaRepository;
        }


        public async Task<List<UrlMapOutput>> GetAll(UrlMapFilterInput input)
        {
            var urlMaps = await _urlMaRepository.GetAll().OrderByDescending(x=> x.CreationTime).Skip(input.Skip).Take(input.Take).ToListAsync();

            return urlMaps.ConvertAll(x => new UrlMapOutput(x)).ToList();
        }

        public async Task<UrlMapOutput> Get(string id)
        {
            var urlMap = await _urlMaRepository.GetAsync(id);

            return new UrlMapOutput(urlMap);
        }

        public async Task<UrlMapOutput> Create(UrlMapInput input)
        {
            var urlMap = await _urlMaRepository.CreateAsync(new UrlMap()
            {
                Id = Guid.NewGuid().ToString(),
                Title = input.Title,
                Description = input.Description,
                RawUrl = input.RawUrl
            });

            urlMap.ShortenUrl = ShortenUrlHelper.Encode(urlMap.Identity);
            urlMap = await _urlMaRepository.UpdateAsync(urlMap);

            return new UrlMapOutput(urlMap);
        }

        public async Task<UrlMapOutput> Update(UrlMapInput input)
        {
            var urlMap = await _urlMaRepository.GetAsync(input.Id);

            urlMap.Title = input.Title;
            urlMap.Description = input.Description;

            urlMap =await _urlMaRepository.UpdateAsync(urlMap);

            return new UrlMapOutput(urlMap);
        }

        public async Task Delete(string id)
        {
            await _urlMaRepository.DeleteAsync(id);
        }
    }
}
