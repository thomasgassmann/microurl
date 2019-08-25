namespace MicroUrl
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MicroUrl.Infrastructure.Settings;
    using MicroUrl.Middlewares;
    using MicroUrl.Storage;
    using MicroUrl.Storage.Implementation;
    using MicroUrl.Urls;
    using MicroUrl.Urls.Implementation;
    using MicroUrl.Urls.Visit;
    using MicroUrl.Urls.Visit.Implementation;
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
            services.AddMvcCore()
                .AddDataAnnotations()
                .AddJsonFormatters()
                .AddJsonOptions(x =>
                {
                    x.SerializerSettings.Formatting = Formatting.Indented;
                    x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.Configure<MicroUrlSettings>(Configuration.GetSection(nameof(MicroUrlSettings)));

            services.AddScoped<IUrlStorageService, UrlStorageService>();
            services.AddScoped<IUrlService, UrlService>();
            services.AddSingleton<IStorageFactory, StorageFactory>();
            services.AddSingleton<IGoogleAnalyticsTracker, GoogleAnalyticsTracker>();
            services.AddScoped<IVisitorTracker, VisitorTracker>();

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseSpaStaticFiles();

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