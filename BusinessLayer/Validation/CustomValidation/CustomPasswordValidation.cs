
using EntityLayer.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation.CustomValidation
{
   public class CustomPasswordValidation:IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (password.Length < 5)  //Password karakter sayısı
                errors.Add(new IdentityError { Code = "PasswordLength", Description = "Lütfen şifreyi en az 5 karakter giriniz." });

            if (password.ToLower().Contains(user.UserName.ToLower())) //Password içerisinde username kontrolü
                errors.Add(new IdentityError { Code = "PasswordContainsUserName", Description = "Lütfen şifre içerisinde kullanıcı adını yazmayınız." });

            if (!errors.Any())
                return Task.FromResult(IdentityResult.Success);
            else
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
        }


    }
}
