using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Service.TinyUrls
{
    public interface ITinyUrlService
    {
        Task<List<UrlMapOutput>> GetAll(UrlMapFilterInput input);
        Task<UrlMapOutput> Get(string id);
        Task<UrlMapOutput> Create(UrlMapInput input);
        Task<UrlMapOutput> Update(UrlMapInput input);
        Task Delete(string id);
    }
}