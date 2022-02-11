using AutoMapper;
using DataAccessLayer.Models.DTO_s;
using EntityLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AutoMapper
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<Hobby, HobbyDTO>().ReverseMap();
        }
    }
}
