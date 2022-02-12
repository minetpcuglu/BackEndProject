using AutoMapper;
using DataAccessLayer.Models.VMs;
using EntityLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AutoMapper
{
    public class EducationMapping:Profile
    {
        public EducationMapping()
        {
            CreateMap<Education, EducationVM>().ReverseMap();
            CreateMap<EducationVM, Education>().ReverseMap();
        }
    }
}
