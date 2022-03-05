using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Results.Concrete
{
   public class SuccessDataResult<T> : DataResult<T>  //base dataresult
    {
        public SuccessDataResult(T data, string message) : base(data, true, message) //data mesaj işlem sonucu true
        {

        }

        public SuccessDataResult(T data) : base(data, true) //yalnızca data  işllrm sonucu true
        {

        }

        public SuccessDataResult(string message) : base(default, true, message) //yalnızca mesaj çok yaygın kullanılmaz
        {

        }
        //public SuccessDataResult() : base(default, true) //hiç bir şey verilmezse
        //{

        //}
    }
}
