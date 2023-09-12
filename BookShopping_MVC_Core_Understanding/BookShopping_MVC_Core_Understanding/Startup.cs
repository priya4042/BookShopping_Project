using Bookshopping.DataAccess.Repository;
using Bookshopping.DataAccess.Repository.IRepository;
using Bookshopping.Utility;
using BookShopping_MVC_Core_Understanding.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopping_MVC_Core_Understanding
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddIdentity<IdentityUser, IdentityRole>().
                AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddScoped<IEmailSender, EmailSender>();
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                options.LogoutPath = $"/Identity/Account/Logout";
            });

            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "1217787405646210";
                options.AppSecret = "66ce589e9f13202199597a254cf32ecc";
            });

            services.AddAuthentication().AddGoogle(Option =>
            {

                Option.ClientId = "500792857035-qnd8mebqp2nmbknimvkglo31erajeg7q.apps.googleusercontent.com";
                Option.ClientSecret = "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";
            });

            services.AddSession(Options =>
            {
                Options.IdleTimeout = TimeSpan.FromMinutes(30);
                Options.Cookie.HttpOnly = true;
                Options.Cookie.IsEssential = true;
            });
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{Area=Customers}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
