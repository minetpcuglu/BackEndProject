using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.VMs
{
   public class LoginViewModel
    {
        [Required(ErrorMessage = "Lütfen e-posta adresini boş geçmeyiniz.")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Lütfen uygun formatta e-posta adresi giriniz.")]
        [Display(Name = "E-Posta ")]
        public string UserName { get; set; }
        //[Required(ErrorMessage = "Lütfen şifreyi boş geçmeyiniz.")]
        //[DataType(DataType.Password, ErrorMessage = "Lütfen uygun formatta şifre giriniz.")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        /// <summary>
        /// Beni hatırla...
        /// </summary>
        //[Display(Name = "Beni Hatırla")]
        //public bool RememberMe { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool Persistent { get; set; } //cookie için geçerli/aktif olup olmamasını beliler
        public bool Lock { get; set; } //belirli saydıa hatalı giriş yaptıgında hesabın kitlenip kitlenmeyecegini belirler
    }
}
