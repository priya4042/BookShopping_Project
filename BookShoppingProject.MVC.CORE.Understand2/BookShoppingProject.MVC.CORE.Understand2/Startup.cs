using BookShopping.DataAccess.Repository;
using BookShopping.DataAccess.Repository.IRepository;
using BookShopping.Utility;
using BookShoppingProject.MVC.CORE.Understand2.Data;
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

namespace BookShoppingProject.MVC.CORE.Understand2
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
            services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();
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
                options.AppId = "584217692966216";
                options.AppSecret = "a74a88700f54297b38061d5df724bb18";
            });

            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "793183225838-nh7kth1at7amg3aklo6miu7nf2s80hoq.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddControllersWithViews();

            services.AddSession(Options =>
            {
                Options.IdleTimeout = TimeSpan.FromMinutes(30);
                Options.Cookie.HttpOnly = true;
                Options.Cookie.IsEssential = true;
            });
            //services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            //services.AddControllersWithViews();
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
