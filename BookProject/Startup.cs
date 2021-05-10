using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookProject.Data;
using BookProject.Models;
using BookProject.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using BookProject.Service;
using BookProject.Helper;

namespace BookProject
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>();

        #if DEBUG
            services.AddRazorPages().
                AddRazorRuntimeCompilation();
                // disable client-side validation
                // AddViewOptions(option =>
                //    option.HtmlHelperOptions.ClientValidationEnabled = false
                // );
        #endif
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.Configure<IdentityOptions>(option => {
                option.Password.RequiredLength = 3;
                option.Password.RequireDigit = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;

                option.SignIn.RequireConfirmedEmail = true;
            });

            services.ConfigureApplicationCookie(config => 
            {
                config.LoginPath = _configuration["Application:LoginPath"];
            });

            services.AddSingleton<IMessageRepository, MessageRepository>();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IEmailService, EmailService>();


            services.Configure<SMTPConfigModel>
                (_configuration.GetSection("SMTPConfig"));

            services.Configure<NewBookAlertConfig>
                ("InternalBook",_configuration.GetSection("NewBookAlert"));

            services.Configure<NewBookAlertConfig>
                ("ThirdPartyBook", _configuration.GetSection("ThirdPartyBook"));
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello From Banngladesh");
            //    await next();
            //    await context.Response.WriteAsync("Hello From Banngladesh Response");
            //});

            app.UseStaticFiles();

            //app.UseStaticFiles(new StaticFileOptions() {
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
            //    RequestPath = "/StaticFiles"
            //});

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync(env.EnvironmentName);
                //});

                //endpoints.MapDefaultControllerRoute();

                //endpoints.MapControllerRoute(
                //    name: "Default",
                //    //pattern: "{controller}/{action}/{id?}/{name?}"); // first way
                //    pattern: "{controller=home}/{action=Index}/{id?}"); //second way with query string like home/index/12?name=irfan

                // create a different pattern for a particular page below
                //endpoints.MapControllerRoute( // this is for multiple routing of a particular page
                //    name: "AboutUs",
                //    pattern: "about-us/{id?}",
                //    defaults: new { controller = "Home", action = "AboutUs" });
            });
        }
    }
}
