using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WeLinkUp.Data;
using WeLinkUp.Models;
using Microsoft.AspNetCore.Identity;
using Amazon.Runtime.CredentialManagement;
using Amazon;


namespace WeLinkUp
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
              .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();

            //Navigate users to login page if they are trying to access to authorized page while not logged in
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Home/LogIn");
            //method to register the profile.
            var options = new CredentialProfileOptions
            {
                //AccessKey = "AKIAVMUZV2K6MFVWGKOK",
                //SecretKey = "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"
                AccessKey = Configuration.GetConnectionString("AWSAccessKey"),
                SecretKey = Configuration.GetConnectionString("AWSSecretKey"),


            };
            var profile = new CredentialProfile("default", options);
            profile.Region = RegionEndpoint.USEast1;
            var netSDKFile = new NetSDKCredentialsFile();
            netSDKFile.RegisterProfile(profile);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
