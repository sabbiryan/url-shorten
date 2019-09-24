using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.EntityFrameworkCore.Repositories;

namespace UrlShorten.Web.Filters
{
    public class UrlMovePermanentlyFilter : IAsyncActionFilter
    {
        private readonly IRepository<UrlMap, string> _urlMapRepository;
        private readonly IRepository<HitLog, string> _hitLogRepository;
        private readonly IConfiguration _configuration;

        public UrlMovePermanentlyFilter(IRepository<UrlMap, string> urlMapRepository, IConfiguration configuration, 
            IRepository<HitLog, string> hitLogRepository)
        {
            _urlMapRepository = urlMapRepository;
            _configuration = configuration;
            _hitLogRepository = hitLogRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Do something before the action executes.
            var requestUrl =
                $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}{context.HttpContext.Request.Path}{context.HttpContext.Request.QueryString.Value}";


            var value = _configuration.GetValue<string>("App:ShortenWebRootPath");

            var relativeValue = requestUrl.Replace(value, "");

            var urlMap = await _urlMapRepository.GetAll().FirstOrDefaultAsync(x => x.ShortenUrl == relativeValue);

            if (urlMap != null)
            {
                var hitLog = new HitLog()
                {
                    Id =  Guid.NewGuid().ToString(),
                    UrlMapId= urlMap.Id
                };
                await _hitLogRepository.CreateAsync(hitLog);

                urlMap.HitCount += 1;
                await _urlMapRepository.UpdateAsync(urlMap);

                context.Result = new RedirectResult(urlMap.RawUrl, true);


                return;
            }

            // next() calls the action method.
            var resultContext = await next();
            // resultContext.Result is set.
            // Do something after the action executes.
        }
    }
}
