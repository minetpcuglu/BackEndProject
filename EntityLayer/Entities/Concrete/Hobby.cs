using EntityLayer.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities.Concrete
{
   public class Hobby : BaseEntity<int>
    {
        public string MyHobby { get; set; }
    }
}
