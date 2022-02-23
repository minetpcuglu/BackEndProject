using AutoMapper;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Models.VMs;
using EntityLayer.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace BackEndProject.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        readonly SignInManager<AppUser> _signInManager;
        private readonly IAppUserService _appUser;
    
        public UserController(UserManager<AppUser> userManager, IMapper mapper, IAppUserService appUser)
        {
            _mapper = mapper;
            _userManager = userManager;
            _appUser = appUser;
      
        }

      
        //[Authorize]
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }



        [HttpGet]
        public IActionResult SıgnIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SıgnIn(AppUserViewModel appUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var appUser = _mapper.Map<AppUserViewModel, AppUser>(appUserViewModel);
                //AppUser appUser = new AppUser
                //{
                //    UserName = appUserViewModel.UserName,
                //    Email = appUserViewModel.Email
                //};
                IdentityResult result = await _userManager.CreateAsync(appUser, appUserViewModel.Sifre); //identity kendi kütüphanesi create
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));

            }
            return View();


        }

        #region  MailOnayı
        public IActionResult PasswordReset()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordReset(ResetPasswordViewModel model)
        {
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user); //kullanıcıya özel token üretildi

                //   MailMessage mail = new MailMessage();
                //    mail.IsBodyHtml = true;
                //    mail.To.Add(user.Email);
                //    mail.From = new MailAddress("******@gmail.com", "Şifre Güncelleme", System.Text.Encoding.UTF8); //kodlamak ve bunları bir bayt dizisinde depolamak için nesne kullanır.
                //    mail.Subject = "Şifre Güncelleme Talebi";
                //    mail.Body = $"<a target=\"_blank\" href=\"https://localhost:5001{Url.Action("UpdatePassword", "User", new { userId = user.Id, token = HttpUtility.UrlEncode(resetToken) })}\">Yeni şifre talebi için tıklayınız</a>";
                //    mail.IsBodyHtml = true;
                //    SmtpClient smp = new SmtpClient();
                //    smp.Credentials = new NetworkCredential("*****@gmail.com", "******");
                //    smp.Port = 587;
                //    smp.Host = "smtp.gmail.com";
                //    smp.EnableSsl = true;
                //    smp.Send(mail);

                //    mailmessage mail = new mailmessage();
                //    smtpclient smtpserver = new smtpclient("smtp.gmail.com");
                //    mail.from = new mailaddress("fromaddress@gmail.com");
                //    mail.to.add("toaddress1@gmail.com");
                //    mail.to.add("toaddress2@gmail.com");
                //    mail.subject = "password recovery ";
                //    mail.body += " <html>";
                //    mail.body += "<body>";
                //    mail.body += "<table>";
                //    mail.body += "<tr>";
                //    mail.body += "<td>user name : </td><td> hai </td>";
                //    mail.body += "</tr>";

                //    mail.body += "<tr>";
                //    mail.body += "<td>password : </td><td>aaaaaaaaaa</td>";
                //    mail.body += "</tr>";
                //    mail.body += "</table>";
                //    mail.body += "</body>";
                //    mail.body += "</html>";
                //    mail.ısbodyhtml = true;
                //    smtpserver.port = 587;
                //    smtpserver.credentials = new system.net.networkcredential("sendfrommailaddress.com", "password");
                //    smtpserver.enablessl = true;
                //    smtpserver.send(mail);

                //    viewbag.state = true;
                //}
                //else
                //    viewbag.state = false;

            }

            return View();
        }


        [HttpGet("[action]/{userId}/{token}")]
        public IActionResult UpdatePassword(string userId, string token)
        {
            return View();
        }
        //query string URL üzerindeki stringler ile verileri taşımaya yarar
        // e-postadaki urle tıkladıgında  query string olarak belirtilen userId ve token değerleri “UpdatePassword” action metodu tarafından yakalanmakta ve TempData‘ya atılmaktadır
        [HttpPost("[action]/{userId}/{token}")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel model, string userId, string token)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(token), model.Password);
            if (result.Succeeded)
            {
                ViewBag.State = true;
                await _userManager.UpdateSecurityStampAsync(user);
            }
            else
                ViewBag.State = false;
            return View();
        }
        #endregion MailOnayı Mail

        //[HttpGet]
        //public async Task<IActionResult> UpdateUser(string userName)
        //{
        //    if (userName == User.Identity.Name)
        //    {
        //        var user = await 

        //        if (user == null) return NotFound();

        //        return View(user);
        //    }
        //    else return RedirectToAction(nameof(HomeController.Index), "Home");
        //}





    }
}
