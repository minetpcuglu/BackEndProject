using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Results.Interface
{
   public interface IDataResult<T> : IResult  //hem mesaj hem işlem sonucu hemde data içersin bunun için ıresult impelemnte al
                                              // IResultdan gelir result true false durumu haricinde birde datayı burda tanımlıcaz   <T> Data 
    {
        //resulttan iplemente ile alınanlar

        //bool Success { get; } 
        //string Message { get; } 

        T Data { get; }

    }
}
