using DataAccessLayer.Models.VMs;
using EntityLayer.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Interface
{
    public interface IAppUserService
    {
        Task<IdentityResult> SingIn(AppUserViewModel appUserView);
        Task<SignInResult> LogIn(LoginViewModel loginVM);
        Task EditUser(EditProfileViewModel editProfileViewModel);
        Task<EditProfileViewModel> GetById(int id);
        Task<EditProfileViewModel> GetUserName(string userName);
        Task LogOut();



        //Task<List<AppUser>> ListUser();
        //Task<int> GetUserIdFromName(string userName); // => Kullanıcının isminden Id yakalamak için kullanılır.
    
        //Task DeleteUser(int id);
       
        //Task<UpdateProfileVM> GetUserName(string userName);

    }
}
