namespace MicroUrl.Web
{
    using System.Linq;
    using System.Reflection;
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MicroUrl.Common;
    using MicroUrl.Storage;
    using MicroUrl.Web.Configuration;
    using MicroUrl.Web.Keys;
    using MicroUrl.Web.Keys.Implementation;
    using MicroUrl.Web.Markdown;
    using MicroUrl.Web.Markdown.Implementation;
    using MicroUrl.Web.Middlewares;
    using MicroUrl.Web.Raw;
    using MicroUrl.Web.Raw.Implementation;
    using MicroUrl.Web.Redirects;
    using MicroUrl.Web.Redirects.Implementation;
    using MicroUrl.Web.Stats;
    using MicroUrl.Web.Stats.Implementation;
    using MicroUrl.Web.Text;
    using MicroUrl.Web.Text.Implementation;
    using MicroUrl.Web.Urls;
    using MicroUrl.Web.Urls.Implementation;
    using MicroUrl.Web.Visit;
    using MicroUrl.Web.Visit.Implementation;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddMvcCore(x =>
                {
                    x.EnableEndpointRouting = false;
                })
                .AddDataAnnotations()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.Formatting = Formatting.Indented;
                    x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
            
            services.Configure<UrlConfig>(_configuration.GetSection(nameof(UrlConfig)));
            
            var executingAssembly = Assembly.GetExecutingAssembly();
            var referenced = executingAssembly.GetReferencedAssemblies().Where(x => x.Name.StartsWith("MicroUrl"));
            var loadedReferences = referenced.Select(Assembly.Load).ToList();
            loadedReferences.Add(executingAssembly);
            services.AddAutoMapper(loadedReferences, ServiceLifetime.Singleton);
            
            services.AddCommon();

            services.AddStorage(_configuration);

            services.AddSingleton<IGoogleAnalyticsTracker, GoogleAnalyticsTracker>();
            services.AddSingleton<IKeyValidationService, KeyValidationService>();

            services.AddScoped<IVisitorTracker, VisitorTracker>();

            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<ITextService, TextService>();
            services.AddScoped<IRawService, RawService>();
            services.AddScoped<IMarkdownService, MarkdownService>();
            services.AddScoped<IRedirectService, RedirectService>();
            services.AddScoped<IMicroUrlKeyGenerator, MicroUrlKeyGenerator>();

            services.AddSingleton<IClientUrlService, ClientUrlService>();

            services.AddScoped<IStatsService, StatsService>();

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = false);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseSpaStaticFiles(new StaticFileOptions { ServeUnknownFileTypes = true });

            // TODO: consider cors
            app.UseMvc();

            // TODO: don't redirect SPA routes
            app.UseMiddleware<RedirectMiddleware>();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}