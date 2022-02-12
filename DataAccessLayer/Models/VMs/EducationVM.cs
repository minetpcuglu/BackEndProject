using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.VMs
{
   public class EducationVM
    {
        public int Id { get; set; }
        public string SchollName { get; set; }
        public string Section { get; set; }
        public string NoteAverage { get; set; }
        public string Date { get; set; }
    }
}
