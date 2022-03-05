using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        //Bu extension method authentication olmuş kullanıcının username'minden Id'sini yakalayıp bize teslim edecek.
        public static int GetUserId(this ClaimsPrincipal principal) => Convert.ToInt32(principal.FindFirstValue(ClaimTypes.NameIdentifier)); //kullanıcı kullanıcı kimliğini verecek
        // principal tipinde gönderilen deger , sorgu sonucunda bana ilgili kullanıcının userId sını int tipinde vericektir
        public static string GetUserName(this ClaimsPrincipal principal) => principal.FindFirstValue(ClaimTypes.Name);
    }
}
