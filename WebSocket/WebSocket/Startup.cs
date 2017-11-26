using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Storage;
using BusinessLogic;
using Microsoft.AspNetCore.Http;
using WebSocketManager;
using System;

namespace WebSocketApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();
            Configuration = builder.Build();

            builder.AddEnvironmentVariables();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddCors();

            services.AddNHibernate(Configuration);

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //            .AddCookie(o => o.LoginPath = new PathString("/login"));

            services.AddBuisnessServices();
            services.AddWebSocketManager();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            var culture = CultureInfo.CreateSpecificCulture("ru-RU");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            
            app.UseCors(policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowCredentials();
            });

            app.UseAuthentication();

            app.UseStaticFiles(new StaticFileOptions()
            {
                ServeUnknownFileTypes = true
            });

            app.UseMvc();

            app.UseWebSockets();

            app.MapWebSocketManager("/websocket", serviceProvider.GetService<NotificationsMessageHandler>());
        }
    }
}
