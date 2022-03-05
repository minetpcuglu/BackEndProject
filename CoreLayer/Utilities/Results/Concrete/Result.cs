using CoreLayer.Utilities.Results.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Results.Concrete //araçlar
{
    public class Result : IResult
    {
        // **** Getter lar readonlydir ve yalnızca getterlar ctor da set edilebilir ****///

        //onay + mesaj 
        public Result(bool success, string messsage) : this(success)  //kendini tekrar etmemek için this(class'ın kendisi yani result) kullanarak diger diğer ctordan success alınarak çalıştırıldı    
        {
            //business service de return de succes yapmamak için oluşturuldu
            Message = messsage; //set edildi

        }

        public Result(bool success)  //yanlızca onay 
        {
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
