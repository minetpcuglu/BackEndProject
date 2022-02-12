using AutoMapper;
using DataAccessLayer.Models.DTOs;
using EntityLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AutoMapper
{
   public class HobbyMapping:Profile
    {
        public HobbyMapping()
        {
            CreateMap<Hobby, HobbyDTO>().ReverseMap();
            CreateMap<HobbyDTO, Hobby>().ReverseMap();
        }
    }
}
