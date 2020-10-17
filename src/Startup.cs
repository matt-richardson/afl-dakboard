using System;
using Certes;
using FluffySpoon.AspNet.EncryptWeMust;
using FluffySpoon.AspNet.EncryptWeMust.Certes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace afl_dakboard
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddFluffySpoonLetsEncrypt(new LetsEncryptOptions
            {
                Email = Configuration["FluffySpoonLetsEncrypt:EmailAddress"], //LetsEncrypt will send you an e-mail here when the certificate is about to expire
                UseStaging = bool.Parse(Configuration["FluffySpoonLetsEncrypt:UseStaging"]), //switch to true for testing
                Domains = new[] { Configuration["FluffySpoonLetsEncrypt:Domain"] },
                TimeUntilExpiryBeforeRenewal = TimeSpan.FromDays(30), //renew automatically 30 days before expiry
                CertificateSigningRequest = new CsrInfo //these are your certificate details
                {
                    CountryName = "Australia",
                    Locality = "AU",
                    Organization = Configuration["FluffySpoonLetsEncrypt:Domain"],
                    OrganizationUnit = "afl",
                    State = "VIC"
                }
            });
            services.AddFluffySpoonLetsEncryptFileCertificatePersistence("FluffySpoonLetsEncrypt:CertificatePersistenceFilePath");
            services.AddFluffySpoonLetsEncryptFileChallengePersistence("FluffySpoonLetsEncrypt:ChallengePersistenceFilePath");

            services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseFluffySpoonLetsEncrypt();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
