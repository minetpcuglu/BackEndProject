using BusinessLayer.AutoMapper;
using BusinessLayer.Services.Concrete;
using BusinessLayer.Services.Interface;
using BusinessLayer.Validation.FluentValidation;
using DataAccessLayer.Context;
using DataAccessLayer.Models.DTOs;
using DataAccessLayer.Models.VMs;
using EntityLayer.Entities.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject
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
            services.AddTransient<ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); //uygulamaya geliştirdiğimiz context nesnesi DbContext olarak tanıtılmaktadır.
            services.AddIdentity<AppUser, AppRole>
                (x =>
                {
                    x.Password.RequireLowercase = false; // =>özelliði; þifre içerisinde en az 1 adet küçük harf zorunluluðu olmasý özelliðini false yaptýk.
                    x.Password.RequireUppercase = false; // => özelliði; þifre içerisinde en az 1 adet büyük harf zorunluluðu olmasýný false yaptýk.
                    x.Password.RequireNonAlphanumeric = false; // =>  özelliði; þifre içerisinde en az 1 adet alfanümerik karakter zorunluluðu olmasý özelliði false.
                })
            .AddEntityFrameworkStores<ApplicationDbContext>(); //identity yapılanmasına dair gerekli entegrasyonu “AddIdentity” metodu ile gerçekleştirmekteyiz.
            services.AddControllersWithViews();
            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
            });



            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            //    options.LoginPath = “/Identity/Account/Login”;
            //    options.AccessDeniedPath = “/Identity/Account/AccessDenied”;
            //    options.SlidingExpiration = true;
            //});

            services.AddScoped<IHobbyService, HobbyService>(); /// dı 
            services.AddScoped<IEducationService, EducationService>(); /// dı 

            services.AddSingleton<IValidator<HobbyDTO>, HobbyValidation>(); // constructor injection kullanacağımız için Validator sınıfımızı ve servisimizi inject ediyoruz. 
            services.AddSingleton<IValidator<EducationVM>, EducationValidation>();
            services.AddAutoMapper(typeof(HobbyMapping));
            services.AddAutoMapper(typeof(UserMapping));
            services.AddAutoMapper(typeof(EducationMapping));

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

            app.UseAuthorization();

            app.UseAuthentication(); //identity

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
