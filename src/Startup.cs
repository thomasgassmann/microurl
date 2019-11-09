namespace MicroUrl
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MicroUrl.Infrastructure.Settings;
    using MicroUrl.Markdown;
    using MicroUrl.Markdown.Implementation;
    using MicroUrl.Middlewares;
    using MicroUrl.Raw;
    using MicroUrl.Raw.Implementation;
    using MicroUrl.Stats;
    using MicroUrl.Stats.Implementation;
    using MicroUrl.Storage;
    using MicroUrl.Storage.Abstractions;
    using MicroUrl.Storage.Abstractions.CloudDatastore;
    using MicroUrl.Storage.Abstractions.Implementation;
    using MicroUrl.Storage.Abstractions.Shared;
    using MicroUrl.Storage.Abstractions.Shared.Implementation;
    using MicroUrl.Storage.Implementation;
    using MicroUrl.Text;
    using MicroUrl.Text.Implementation;
    using MicroUrl.Urls;
    using MicroUrl.Urls.Implementation;
    using MicroUrl.Visit;
    using MicroUrl.Visit.Implementation;
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

            services.Configure<MicroUrlSettings>(_configuration.GetSection(nameof(MicroUrlSettings)));

            services.AddSingleton<IGoogleAnalyticsTracker, GoogleAnalyticsTracker>();

            services.AddSingleton<IEntityAnalyzer, EntityAnalyzer>();
            services.AddSingleton<IKeyFactory, DefaultKeyFactory>();
            services.AddScoped<IStorageFactory, CloudDatastoreStorageFactory>();

            services.AddScoped<IUrlStorageService, UrlStorageService>();
            services.AddScoped<ITextStorageService, TextStorageService>();
            services.AddScoped<IVisitorTracker, VisitorTracker>();
            services.AddScoped<IVisitStorageService, VisitStorageService>();
            services.AddScoped<IMicroUrlKeyGenerator, MicroUrlKeyGenerator>();

            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<ITextService, TextService>();
            services.AddScoped<IRawService, RawService>();
            services.AddScoped<IMarkdownService, MarkdownService>();

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