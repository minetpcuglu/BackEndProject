using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities.Interface
{
   public interface IBaseEntity
    {
        DateTime CreateDate { get; set; }
    }
}
