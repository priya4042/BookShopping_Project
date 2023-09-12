using BookShoping_Data_Access.Repository;
using BookShoping_Data_Access.Repository.IRepository;
using BookShoping_Utility;
using BookShopingProject.Data_Access.Data;

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

namespace BookShopingProject_11
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

           // services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
              //  .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentity<IdentityUser, IdentityRole>(/*options => options.SignIn.RequireConfirmedAccount = true*/).AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddTransient<IEmailSender, EmailSender>();

            // services.AddScoped<IcategoryRepository, CategoryRepository>();
            //
            //services.AddScoped<ICoverTypeRepository, CoverTypeRepository>();

            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.ConfigureApplicationCookie(options =>
            {
               options.LoginPath = $"/Identity/Account/Login";
                options.AccessDeniedPath = $"/Identity/Account/AccessDeniedPath";
                options.LogoutPath = $"/Identity/Account/Logout";
            });

            services.AddAuthentication().AddFacebook(Option =>
            {
                Option.AppId = "584217692966216";
                Option.AppSecret = "a74a88700f54297b38061d5df724bb18";
            });
            services.AddAuthentication().AddGoogle(Option =>
            {
                Option.ClientId = "500792857035-qnd8mebqp2nmbknimvkglo31erajeg7q.apps.googleusercontent.com";
                Option.ClientSecret = "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";
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
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
                    pattern: "{area=customer}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            
        }

        
    }
}
