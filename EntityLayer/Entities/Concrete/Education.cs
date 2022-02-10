using EntityLayer.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities.Concrete
{
   public class Education : BaseEntity<int>
    {
        public string SchollName { get; set; }
        public string Section { get; set; }
        public string NoteAverage { get; set; }
        public string Date { get; set; }
    }
}
