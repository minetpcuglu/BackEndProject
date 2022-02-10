using EntityLayer.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities.Concrete
{
  public class About: BaseEntity<int>
    {
       
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string AgainPassword { get; set; }
        public string Adress { get; set; }
        public string TelephoneNumber { get; set; }
        public string Description { get; set; }
    }
}
