using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using DentAppointment.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DentAppointment.Data.Entity;
using Microsoft.CodeAnalysis.Options;

namespace DentAppointment
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
                    Configuration.GetConnectionString("DefaultConnection"))); //db connection string reading from appsettings.json



            services.AddIdentity<AppUser, AppRole>(
                            options =>
                            {
                                options.User.RequireUniqueEmail = true;
                                options.SignIn.RequireConfirmedAccount = false;
                                options.SignIn.RequireConfirmedPhoneNumber = false;
                                options.Password.RequireDigit = false;
                                options.Password.RequiredLength = 6;
                                options.Password.RequireLowercase = false;
                                options.Password.RequireUppercase = false;
                                //options.SignIn.RequireConfirmedEmail = false;
                                options.Password.RequireNonAlphanumeric = false;

                            }
                         )
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders(); //cookie verisi vasıtasıyla bir tokena sahip olmasını sağlıcak oturum acan kullanıcının

            //cookie settings
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login"; //eğer oturum sahibi değilse bu actiona yönlendiririz
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/Denied";
                options.Cookie.Name = "Dentist.Cookie";
                options.SlidingExpiration = true; //kullanıcının süresi varken tekrar giriş yaparsa sisteme süresini yenilemek
            });

            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
                endpoints.MapRazorPages();
            });
        }
    }
}
