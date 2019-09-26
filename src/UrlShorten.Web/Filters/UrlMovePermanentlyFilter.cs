using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.EntityFrameworkCore.Repositories;
using ILogger = NLog.ILogger;

namespace UrlShorten.Web.Filters
{
    public class UrlMovePermanentlyFilter : IAsyncActionFilter
    {
        private readonly IRepository<UrlMap, string> _urlMapRepository;
        private readonly IRepository<HitLog, string> _hitLogRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UrlMovePermanentlyFilter> _logger;

        public UrlMovePermanentlyFilter(IRepository<UrlMap, string> urlMapRepository, IConfiguration configuration, 
            IRepository<HitLog, string> hitLogRepository, 
            ILogger<UrlMovePermanentlyFilter> logger)
        {
            _urlMapRepository = urlMapRepository;
            _configuration = configuration;
            _hitLogRepository = hitLogRepository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Do something before the action executes.
            var requestUrl =
                $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}{context.HttpContext.Request.Path}{context.HttpContext.Request.QueryString.Value}";

            _logger.LogTrace($"Requested URL: {requestUrl}");

            var value = _configuration.GetValue<string>("App:ShortenWebRootPathReplaceValue");

            var relativeValue = requestUrl.Replace(value, "");

            _logger.LogTrace($"Relative Path: {requestUrl}");

            var urlMap = await _urlMapRepository.GetAll().FirstOrDefaultAsync(x => string.Equals(x.ShortenUrl, relativeValue, StringComparison.InvariantCulture));

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
