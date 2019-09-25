using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using UrlShorten.Service.TinyUrls.Dto;

namespace UrlShorten.Service.TinyUrls
{
    public interface ITinyUrlService
    {
        /// <summary>
        /// Get all items as page
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UrlMapPageOutput> GetAll(UrlMapFilterInput input);

        /// <summary>
        /// Get an item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UrlMapOutput> Get(string id);

        /// <summary>
        /// Create an item
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UrlMapOutput> Create(UrlMapInput input);

        /// <summary>
        /// Update an existing item. Url can not be updated.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UrlMapOutput> Update(UrlMapInput input);

        /// <summary>
        /// Delete an existing item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(string id);

        /// <summary>
        /// Check the inputted url exists or not
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsUrlExist(string url, string id);

        /// <summary>
        /// Check the item is exists or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExist(string id);

        /// <summary>
        /// Get item details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UrlMapDetailOutput> GetDetail(string id);

        /// <summary>
        /// Set user agent
        /// </summary>
        /// <param name="userAgent"></param>
        void SetUserAgent(StringValues userAgent);


        /// <summary>
        /// Get redirect url from relative path
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        Task<string> GetRedirectUrl(string relativePath);
    }
}