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
   public class UserMapping:Profile
    {
        public UserMapping()
        {
            CreateMap<AppUser,AppUserViewModel>().ReverseMap();
            CreateMap<AppUser,EditProfileViewModel>().ReverseMap();
            CreateMap<AppUserViewModel, AppUser>().ReverseMap();
            CreateMap<EditProfileViewModel, AppUser>().ReverseMap();
        }
    }
}
