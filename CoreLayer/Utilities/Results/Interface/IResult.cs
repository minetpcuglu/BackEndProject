using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Results.Interface
{
    //Temel voidler için
   public interface IResult
    {
        bool Success { get; } //işlem sonucu 
        string Message { get; } //basarılı ve ya değilse bilgilendirme gönderilir
    }
}
