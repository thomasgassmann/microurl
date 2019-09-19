namespace MicroUrl
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MicroUrl.Filters;
    using MicroUrl.Infrastructure.Settings;
    using MicroUrl.Middlewares;
    using MicroUrl.Stats;
    using MicroUrl.Stats.Implementation;
    using MicroUrl.Storage;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddMvcCore(x =>
                {
                    x.Filters.Add<ModelStateValidationFilter>();
                    x.EnableEndpointRouting = false;
                })
                .AddDataAnnotations()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.Formatting = Formatting.Indented;
                    x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.Configure<MicroUrlSettings>(Configuration.GetSection(nameof(MicroUrlSettings)));

            services.AddScoped<IUrlStorageService, UrlStorageService>();
            services.AddScoped<ITextStorageService, TextStorageService>();
            services.AddSingleton<IStorageFactory, StorageFactory>();
            services.AddSingleton<IGoogleAnalyticsTracker, GoogleAnalyticsTracker>();
            services.AddScoped<IVisitorTracker, VisitorTracker>();
            services.AddScoped<IVisitStorageService, VisitStorageService>();
            services.AddScoped<IMicroUrlKeyGenerator, MicroUrlKeyGenerator>();

            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<ITextService, TextService>();

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

            app.UseSpaStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true
            });

            // TODO: consider cors
            app.UseMvc();

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