using AutoMapper;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Models.VMs;
using DataAccessLayer.UnitOfWorks.Interface;
using EntityLayer.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Concrete
{
    public class AppUserService : IAppUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public AppUserService(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager)

        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
            this._signInManager = signInManager;

        }

        public async Task EditUser(EditProfileViewModel editProfileViewModel)
        {
            var user = await _unitOfWork.AppUserRepository.GetById(editProfileViewModel.Id);
            if (user != null)
            {
                if (editProfileViewModel.Email != user.Email)
                {
                    var mail = await _userManager.FindByEmailAsync(editProfileViewModel.Email); //email varmı
                    if (mail == null) //yokise
                    {
                        await _userManager.SetEmailAsync(user, editProfileViewModel.Email);
                    }

                }

                if (editProfileViewModel.UserName != user.UserName)
                {
                    var userName = await _userManager.FindByNameAsync(editProfileViewModel.UserName); //varmı
                    if (userName == null) //yokise
                    {
                        await _userManager.SetUserNameAsync(user, editProfileViewModel.UserName);
                        user.UserName = editProfileViewModel.UserName;
                        await _signInManager.SignInAsync(user, isPersistent: true);

                    }
                }
                if (editProfileViewModel.Password != null)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, editProfileViewModel.Password);
                    await _userManager.UpdateAsync(user);
                }
                if (editProfileViewModel.Adress != user.Adress)
                {
                    user.Adress = editProfileViewModel.Adress;
                    await _unitOfWork.AppUserRepository.Update(user);
                    await _unitOfWork.Commit();
                }
            }
        }

        public async Task<EditProfileViewModel> GetById(int id)
        {
            var user = await  _unitOfWork.AppUserRepository.GetById(id);

            return _mapper.Map<EditProfileViewModel>(user);
        }

        public async Task<SignInResult> LogIn(LoginViewModel loginVM)
        {
            var user = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, loginVM.Persistent, loginVM.Lock);

            //var user = await _userManager.FindByNameAsync(loginVM.Email);
            //var password = await _userManager.CheckPasswordAsync(user, loginVM.Password);

            //if(password)
            //{
            //    await _signInManager.SignInAsync(user, false);
            //    return true;
            //}

            return user;


            //Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(_userManager, loginVM.Password, loginVM.Persistent, loginVM.Lock);


        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> SingIn(AppUserViewModel appUserView)
        {
            var user = _mapper.Map<AppUser>(appUserView);

            var result = await _userManager.CreateAsync(user, appUserView.Sifre);

            if (result.Succeeded) await _signInManager.SignInAsync(user, isPersistent: false);// isPersistent =>Tarayıcı kullanıcı giriş bilgilerini hafıza da tutmsun mu diye sorar

            return result;
        }





    }
}
