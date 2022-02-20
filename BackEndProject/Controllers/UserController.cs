using AutoMapper;
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
        public UserController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
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
                IdentityResult result = await _userManager.CreateAsync(appUser, appUserViewModel.Sifre);
                if (result.Succeeded)       
                    return RedirectToAction("Index");
                else
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
               
            }
            return View();
            
            
        }


        public IActionResult PasswordReset()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordReset(ResetPasswordViewModel model)
        {
            //AppUser user = await _userManager.FindByEmailAsync(model.Email);
            //if (user != null)
            //{
            //    string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            //    MailMessage mail = new MailMessage();
            //    mail.IsBodyHtml = true;
            //    mail.To.Add(user.Email);
            //    mail.From = new MailAddress("******@gmail.com", "Şifre Güncelleme", System.Text.Encoding.UTF8);
            //    mail.Subject = "Şifre Güncelleme Talebi";
            //    mail.Body = $"<a target=\"_blank\" href=\"https://localhost:5001{Url.Action("UpdatePassword", "User", new { userId = user.Id, token = HttpUtility.UrlEncode(resetToken) })}\">Yeni şifre talebi için tıklayınız</a>";
            //    mail.IsBodyHtml = true;
            //    SmtpClient smp = new SmtpClient();
            //    smp.Credentials = new NetworkCredential("*****@gmail.com", "******");
            //    smp.Port = 587;
            //    smp.Host = "smtp.gmail.com";
            //    smp.EnableSsl = true;
            //    smp.Send(mail);

            //    MailMessage mail = new MailMessage();
            //    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //    mail.From = new MailAddress("fromaddress@gmail.com");
            //    mail.To.Add("toaddress1@gmail.com");
            //    mail.To.Add("toaddress2@gmail.com");
            //    mail.Subject = "Password Recovery ";
            //    mail.Body += " <html>";
            //    mail.Body += "<body>";
            //    mail.Body += "<table>";
            //    mail.Body += "<tr>";
            //    mail.Body += "<td>User Name : </td><td> HAi </td>";
            //    mail.Body += "</tr>";

            //    mail.Body += "<tr>";
            //    mail.Body += "<td>Password : </td><td>aaaaaaaaaa</td>";
            //    mail.Body += "</tr>";
            //    mail.Body += "</table>";
            //    mail.Body += "</body>";
            //    mail.Body += "</html>";
            //    mail.IsBodyHtml = true;
            //    SmtpServer.Port = 587;
            //    SmtpServer.Credentials = new System.Net.NetworkCredential("sendfrommailaddress.com", "password");
            //    SmtpServer.EnableSsl = true;
            //    SmtpServer.Send(mail);

            //    ViewBag.State = true;
            //}
            //else
            //    ViewBag.State = false;

            

            return View();
        }


        [HttpGet("[action]/{userId}/{token}")]
        public IActionResult UpdatePassword(string userId, string token)
        {
            return View();
        }
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




    }
}
