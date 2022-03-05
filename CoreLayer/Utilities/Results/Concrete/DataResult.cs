using CoreLayer.Utilities.Results.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Results.Concrete
{
   public class DataResult<T> : Result, IDataResult<T> //sen bir resultsın ve o sınıfın ctorları tanımla
    {
        public DataResult(T data, bool success, string message) : base(success, message) //mesaj+onay  //base=result
        {
            Data = data;
        }
        public DataResult(T data, bool success) : base(success) //yalnızca onay
        {
            Data = data;
        }

        //public DataResult(List<T> data, bool success) : base(success) //yalnızca onay
        //{
        //    Datas = data;
        //}
        public T Data { get; }

        
   
    }
}
