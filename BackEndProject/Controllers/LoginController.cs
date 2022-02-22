using BusinessLayer.Services.Interface;
using DataAccessLayer.Models.VMs;
using EntityLayer.Entities.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackEndProject.Controllers
{
    public class LoginController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        private readonly IAppUserService _appUser;
        private static Logger logger = LogManager.GetLogger("myAppLoggerRules");

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAppUserService appUser)
        {
            _appUser = appUser;
            _userManager = userManager;
            _signInManager = signInManager;
        }



        #region Loglama Example
        //public IActionResult UserLogin(string returnUrl)
        //{
        //    //kullanıcının yetkisinin olmadığı sayfalara erişmeye  çalıştığında  direkt olarak “Login” actionına yönlendirecektir
        //    TempData["returnUrl"] = returnUrl; //tempdata kontrolu atandi 
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> UserLogin(LoginViewModel model)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    AppUser appUser = await _userManager.FindByEmailAsync(model.Email); //mail uygun user varsa cekilir
        //    //    if (User != null)
        //    //    {
        //    //        //İlgili kullanıcının geçmişte oluşturulmuş bir Cookie varsa siliyoruz.
        //    //        await _signInManager.SignOutAsync();

        //    //        //kullanıcıya SignInManager(kullanıcı giriş ve çıkış kontrolu sınıfı) sınıfının PasswordSignInAsync metoduyla oturum açmasına izin verilir.
        //    //        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, model.Password, model.Persistent, model.Lock);
        //    //        return RedirectToAction("Index", "User");
        //    //    }
        //    //    else
        //    //    {
        //    //        ModelState.AddModelError("", "Hatalı Kullanıcı Adı/Şifre");
        //    //        return View();
        //    //    }
        //    //}

        //    return View(model);

        //    //logger.Info("Login Method");
        //    //try
        //    //{
        //    //    if (ModelState.IsValid)
        //    //    {

        //    //        AppUser appUser = await _userManager.FindByEmailAsync(model.Email); //mail uygun user varsa cekilir
        //    //        if (User != null)
        //    //        {

        //    //            //İlgili kullanıcının geçmişte oluşturulmuş bir Cookie varsa siliyoruz.
        //    //            await _signInManager.SignOutAsync();

        //    //            //kullanıcıya SignInManager(kullanıcı giriş ve çıkış kontrolu sınıfı) sınıfının PasswordSignInAsync metoduyla oturum açmasına izin verilir.
        //    //            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, model.Password, model.Persistent, model.Lock);
        //    //            logger.Info("Good.Login Succes");
        //    //            return View("LoginSuccess", model);
        //    //        }
        //    //        else
        //    //        {
        //    //            ModelState.AddModelError("", "Hatalı Kullanıcı Adı/Şifre");
        //    //            logger.Info("Sorry.Login Failed");
        //    //            return View("LoginFailed", model);
        //    //        }


        //    //    }


        //    //}
        //    //catch (Exception e)
        //    //{

        //    //    logger.Error("Exception ! " + e.Message);
        //    //    return Content("Exception in Login" + e.Message);

        //    //}

        //    //return View(model);



        //}
        #endregion 


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

            if (ModelState.IsValid)
            {
                var result = await _appUser.LogIn(loginView);

                if (result.Succeeded) return RedirectToLocal(returnUrl);

                ModelState.AddModelError(String.Empty, "Invalid login attempt..!");
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

            return RedirectToAction("LogIn");
        }

        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "User");
        //}
    }
}
