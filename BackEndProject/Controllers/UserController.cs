using AutoMapper;
using DataAccessLayer.Models.VMs;
using EntityLayer.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                //var appUser = _mapper.Map<AppUserViewModel, AppUser>(appUserViewModel);
                AppUser appUser = new AppUser
                {
                    UserName = appUserViewModel.UserName,
                    Email = appUserViewModel.Email
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, appUserViewModel.Sifre);
                if (result.Succeeded)
                    return RedirectToAction("Index");
            }
            return View();
            
        }
    }
}
