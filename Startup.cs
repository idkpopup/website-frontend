using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.AWS.Logger;
using NLog.Config;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Site
{
    public class Startup
    {

        Logger logger = LogManager.GetLogger("aws");

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddRazorPages().AddNewtonsoftJson(setup => {
                setup.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var config = new LoggingConfiguration();

            var awsTarget = new AWSTarget()
            {
                LogGroup = "idkpopup-website",
                Region = "us-west-1"
            };
            config.AddTarget("aws", awsTarget);

            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, awsTarget));

            LogManager.Configuration = config;

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            
            //Wire endpoings
            app.UseEndpoints(endpoints =>
            {                
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                 name: "default",
                 pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                "register",
                "api/register",
                new { controller = "Register", action = "Post" });

                endpoints.MapControllerRoute(
                "register",
                "playbook/{action=playbook}",
                new { controller = "Playbook" });
            });
            
            logger.Info("Routes configured, starting up");

        }
    }
}
