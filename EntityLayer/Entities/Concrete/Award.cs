using EntityLayer.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities.Concrete
{
   public class Award : BaseEntity<int>
    { 
        public string AwardName { get; set; }
    }
}
