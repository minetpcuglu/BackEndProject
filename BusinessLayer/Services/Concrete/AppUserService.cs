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


        //public async Task<EditProfileViewModel> GetById(int id) //çalışıyor
        //{
        //    var user = await _unitOfWork.AppUserRepository.GetById(id);

        //    return _mapper.Map<EditProfileViewModel>(user);
        //}

        public async Task<EditProfileViewModel> GetById(int id)  //ikiside calısıyor
        {
            var user = await _unitOfWork.AppUserRepository.GetFilteredFirstOrDefault(
                selector: x => new EditProfileViewModel
                {
                    Id = x.Id,
                    Adress = x.Adress,
                    UserName = x.UserName,
                    Email = x.Email
                },
                expression: x => x.Id == id);

            return user;
        }

        public async Task EditUser(EditProfileViewModel model)
        {
            var user = await _unitOfWork.AppUserRepository.GetById(model.Id);
            if (user != null)
            {

                if (model.Password != null)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                    await _userManager.UpdateAsync(user);
                }
                if (model.UserName != user.UserName)
                {
                    var isUserNameExist = await _userManager.FindByNameAsync(model.UserName);

                    if (isUserNameExist == null)
                    {
                        await _userManager.SetUserNameAsync(user, model.UserName);
                        user.UserName = model.UserName;
                        await _signInManager.SignInAsync(user, isPersistent: true);
                    }
                }
                if (model.Adress != user.Adress)
                {
                    user.Adress = model.Adress;
                    await _unitOfWork.AppUserRepository.Update(user);
                    await _unitOfWork.Commit();
                }

                if (model.Email != user.Email)
                {
                    var isEmailExist = await _userManager.FindByEmailAsync(model.Email);
                    if (isEmailExist == null)
                        await _userManager.SetEmailAsync(user, model.Email);
                }

            }
        }

        public async Task<EditProfileViewModel> GetUserName(string userName)
        {
            var user = await _unitOfWork.AppUserRepository.GetFilteredFirstOrDefault(
                selector: y => new EditProfileViewModel
                {
                    Email = y.Email,
                    Adress = y.Adress,
                    Password = y.PasswordHash,
                    UserName = y.UserName
                },
                expression: x => x.UserName == userName
                );
            return user;
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
