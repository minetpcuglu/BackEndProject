using DataAccessLayer.Models.VMs;
using EntityLayer.Entities.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult UserLogin(string returnUrl)
        {
            //kullanıcının yetkisinin olmadığı sayfalara erişmeye  çalıştığında  direkt olarak “Login” actionına yönlendirecektir
            TempData["returnUrl"] = returnUrl; //tempdata kontrolu atandi 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByEmailAsync(model.Email); //mail uygun user varsa cekilir
                if (User != null)
                {
                    //İlgili kullanıcının geçmişte oluşturulmuş bir Cookie varsa siliyoruz.
                    await _signInManager.SignOutAsync();

                    //kullanıcıya SignInManager(kullanıcı giriş ve çıkış kontrolu sınıfı) sınıfının PasswordSignInAsync metoduyla oturum açmasına izin verilir.
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, model.Password, model.Persistent, model.Lock);
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Hatalı Kullanıcı Adı/Şifre");
                    return View();
                }
            }
          
            return View(model);
               
            





        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "User");
        }

    }
}
