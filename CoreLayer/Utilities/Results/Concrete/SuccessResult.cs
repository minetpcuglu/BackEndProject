using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities.Results.Concrete
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message) //yapılan işlem basarılı //base(result)
        {

        }

        public SuccessResult() : base(true) //result
        {

        }
    }
}
