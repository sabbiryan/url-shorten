using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using UrlShorten.EntityFrameworkCore;
using UrlShorten.EntityFrameworkCore.Repositories;
using UrlShorten.Service.TinyUrls;
using UrlShorten.Web.Extensions;
using UrlShorten.Web.Filters;

namespace UrlShorten.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string RoutePrfix = "swagger";
        private const string SwaggerEndpoint = "/swagger/v1/swagger.json";
        private const string SwaggerTitle = "UrlShorten API";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContextPool<AppDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll));

            services.AddMvc(service =>
                {
                    service.Filters.Add(typeof(UrlMovePermanentlyFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddLogging();

            services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddTransient<ITinyUrlService, TinyUrlService>();

            // Inject an implementation of ISwaggerProvider with defaulted settings applied
            services.AddSwaggerGen(options => { options.SwaggerDoc("v1", new Info {Title = SwaggerTitle, Version = "v1"}); });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "text/plain";

                await context.HttpContext.Response.WriteAsync(
                    "Status code page, status code: " +
                    context.HttpContext.Response.StatusCode);
            });

            if (env.IsDevelopment())
            {
                app.UseDatabaseErrorPage();
                app.UseDeveloperExceptionPage();

                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/html";

                        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                        await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();

                        // Use exceptionHandlerPathFeature to process the exception (for example, 
                        // logging), but do NOT expose sensitive error information directly to 
                        // the client.

                        if (exceptionHandlerPathFeature?.Error != null)
                        {
                            await context.Response.WriteAsync($"{exceptionHandlerPathFeature?.Error.Message}<br><br>\r\n");
                        }

                        await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
                        await context.Response.WriteAsync("</body></html>\r\n");
                        await context.Response.WriteAsync(new string(' ', 512)); // IE padding
                    });
                });
                app.UseHsts();
            }
            else
            {
                //app.UseExceptionHandler("/Error");

                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/html";

                        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                        await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();

                        // Use exceptionHandlerPathFeature to process the exception (for example, 
                        // logging), but do NOT expose sensitive error information directly to 
                        // the client.

                        if (exceptionHandlerPathFeature?.Error != null)
                        {
                            await context.Response.WriteAsync($"{exceptionHandlerPathFeature?.Error.Message}<br><br>\r\n");
                        }

                        await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
                        await context.Response.WriteAsync("</body></html>\r\n");
                        await context.Response.WriteAsync(new string(' ', 512)); // IE padding
                    });
                });
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();


            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = RoutePrfix;
                c.SwaggerEndpoint(SwaggerEndpoint, SwaggerTitle);
            });
        }
    }
}
