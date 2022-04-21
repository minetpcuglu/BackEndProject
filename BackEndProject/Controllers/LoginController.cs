using BusinessLayer.Services.Interface;
using DataAccessLayer.Models.VMs;
using EntityLayer.Entities.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackEndProject.Controllers
{
    public class LoginController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        private readonly IAppUserService _appUser;


        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAppUserService appUser)
        {
            _appUser = appUser;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult UserLogin(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(UserController.Index), "User");
            }
            //kullanıcının yetkisinin olmadığı sayfalara erişmeye  çalıştığında  direkt olarak “Login” actionına yönlendirecektir
            ViewData["ReturnUrl"] = returnUrl; //temt data kontrolu atandi
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(LoginViewModel loginView, string returnUrl)
        {
            var captchaImage = HttpContext.Request.Form["g-recaptcha-response"];
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(captchaImage))
                {
                    ModelState.AddModelError(String.Empty, "Dogrulama Doldurulmadı..!");
                    //return Content("Doldurulmadı");
                }

                var verified = await CheckCaptcha();
                if (!verified)
                {
                    ModelState.AddModelError(String.Empty, "Geçersiz Dogrulama..!");
                    //return Content("Dogrulanmadı");
                }
                if (verified)
                {

                    var result = await _appUser.LogIn(loginView);
                    if (result.Succeeded) return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError(String.Empty, "Hatalı Kullanıcı Adı veya Şifre..!");
                return View();


            }

            return View();
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            else return RedirectToAction(nameof(UserController.Index), "User");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _appUser.LogOut();

            return RedirectToAction("UserLogin");
        }
        public async Task<bool> CheckCaptcha()
        {
            var postData = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("secret", "6LeGiIsfAAAAAFbdoFqhmnV41kiSCRJFRtM8FRvu"),
                new KeyValuePair<string, string>("response", HttpContext.Request.Form["g-recaptcha-response"])

            };

            var client = new HttpClient();
            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(postData));
            var o = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
            return (bool)o["success"];
        }

        #region Loginpost
        //[HttpPost]
        //public async Task<IActionResult> UserLogin(LoginViewModel loginView, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _appUser.LogIn(loginView);

        //        if (result.Succeeded) return RedirectToLocal(returnUrl);

        //        ModelState.AddModelError(String.Empty, "Invalid login attempt..!");
        //    }

        //    return View();
        //}
        #endregion
        #region LogOut
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "User");
        //}
        #endregion
        #region UserLogin
        //if (ModelState.IsValid)
        //{
        //    var result = await _appUser.LogIn(loginView);
        //    if (result.Succeeded)
        //    {
        //        TempData["success"] = "Giriş İşlemi Başarılı Yönlendiriliyorsunuz";
        //        return RedirectToAction(nameof(HomeController.Index), "Home"); // Eğer giriş başarılı olursa HomeController'daki Home Action'a yönlendir.
        //    }
        //    ModelState.AddModelError(String.Empty, "Geçersiz giriş denemesi..!");
        //    TempData["failed"] = "Lütfen Tekrar Deneyiniz";
        //}
        #endregion
    }
}
