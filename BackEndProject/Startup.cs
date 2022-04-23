using BusinessLayer.AutoMapper;
using BusinessLayer.Services.Concrete;
using BusinessLayer.Services.Interface;
using BusinessLayer.Validation.CustomValidation;
using BusinessLayer.Validation.FluentValidation;
using DataAccessLayer.Context;
using DataAccessLayer.Models.DTOs;
using DataAccessLayer.Models.VMs;
using EntityLayer.Entities.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
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
            #region context
            services.AddTransient<ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); //uygulamaya geliştirdiğimiz context nesnesi DbContext olarak tanıtılmaktadır.
            #endregion

            #region Cache
            services.AddMemoryCache();
            services.AddResponseCaching();
            #endregion

            #region Identity ValidatorRules
            services.AddIdentity<AppUser, AppRole>
               (x =>
               {
                   x.Password.RequireNonAlphanumeric = false; //Alfanumerik zorunluluğunu kaldırıyoruz.
                   x.Password.RequireLowercase = false; //Küçük harf zorunluluğunu kaldırıyoruz.
                   x.Password.RequireUppercase = false; //Büyük harf zorunluluğunu kaldırıyoruz.
                   x.Password.RequireDigit = false; //0-9 arası sayısal karakter zorunluluğu 
                   x.User.RequireUniqueEmail = false; //Email adreslerini tekilleştiriyoruz.

                   //x.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+"; //Kullanıcı adında geçerli olan karakterleri belirtiyoruz.

               }).AddPasswordValidator<CustomPasswordValidation>()
         .AddUserValidator<CustomUserValidation>()
         .AddErrorDescriber<CustomIdentityErrorDescriber>()
         .AddEntityFrameworkStores<ApplicationDbContext>();//(şifremi unuttum!)  //identity yapılanmasına dair gerekli entegrasyonu “AddIdentity” metodu ile gerçekleştirmekteyiz.

            //böylece hem password hemde user temelli custom validasyon yapılanması sağlanmış bulunmaktadır.
            #endregion

         

            #region Cookie
            //services.ConfigureApplicationCookie(x =>
            //{
            //    x.LoginPath = new PathString("/Login/UserLogin");
            //    x.Cookie = new CookieBuilder
            //    {
            //        Name = "AspNetCoreIdentityExampleCookie", //Oluşturulacak Cookie'yi isimlendiriyoruz.
            //        HttpOnly = false, //Kötü niyetli insanların client-side tarafından Cookie'ye erişmesini engelliyoruz.
            //        Expiration = TimeSpan.FromMinutes(2), //Oluşturulacak Cookie'nin vadesini belirliyoruz.
            //        SameSite = SameSiteMode.Lax, //Top level navigasyonlara sebep olmayan requestlere Cookie'nin gönderilmemesini belirtiyoruz.
            //        SecurePolicy = CookieSecurePolicy.Always //HTTPS üzerinden erişilebilir yapıyoruz.
            //    };
            //    x.SlidingExpiration = true; //Expiration süresinin yarısı kadar süre zarfında istekte bulunulursa eğer geri kalan yarısını tekrar sıfırlayarak ilk ayarlanan süreyi tazeleyecektir.
            //    x.ExpireTimeSpan = TimeSpan.FromMinutes(2); //CookieBuilder nesnesinde tanımlanan Expiration değerinin varsayılan değerlerle ezilme ihtimaline karşın tekrardan Cookie vadesi burada da belirtiliyor.
            //});

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //}).AddCookie(options =>
            //{
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            //    options.Cookie.MaxAge = options.ExpireTimeSpan; // optional
            //    options.SlidingExpiration = true;
            //});
            #endregion


            services.AddControllersWithViews();
            #region FluentValidation
            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
            #endregion

            #region IoC
            services.AddScoped<IHobbyService, HobbyService>(); /// dı 
            services.AddScoped<IEducationService, EducationService>(); /// dı 
            services.AddScoped<IAppUserService, AppUserService>(); /// dı 

            services.AddTransient<UserManager<AppUser>>();
            services.AddTransient<UserManager<AppRole>>();

            #endregion

            #region Automapper
            services.AddAutoMapper(typeof(HobbyMapping));
            services.AddAutoMapper(typeof(UserMapping));
            services.AddAutoMapper(typeof(EducationMapping));

            #endregion

            #region FluentValidation
            services.AddSingleton<IValidator<HobbyDTO>, HobbyValidation>(); // constructor injection kullanacağımız için Validator sınıfımızı ve servisimizi inject ediyoruz. 
            services.AddSingleton<IValidator<EducationVM>, EducationValidation>();
            #endregion

            #region ajax
            services.AddControllers().AddNewtonsoftJson();
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddRazorPages().AddNewtonsoftJson();
            #endregion


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

            app.UseStatusCodePages(); //* 400-599 hata durum kodu ayarladığında, durum kodunu ve boş bir yanıt gövdesini döndürür.
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();  //identity
            app.UseAuthorization();   //IoC


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
