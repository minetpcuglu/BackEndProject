using AutoMapper;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Models.DTOs;
using DataAccessLayer.Repositories.Interface.EntityTypeRepositories;
using DataAccessLayer.UnitOfWorks.Interface;
using EntityLayer.Entities.Concrete;
using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Concrete
{
    public class HobbyService : IHobbyService
    {
        private readonly IHobbyRepository _hobbyRepository;
        private readonly IMapper _mapper;

        public HobbyService( IHobbyRepository hobbyRepository, IMapper mapper)
        {
            _hobbyRepository = hobbyRepository;
            _mapper = mapper;
        }

        public async Task<List<HobbyDTO>> GetAll()
        {
          

            var hobbyList = await _hobbyRepository.GetAll();
            var list = _mapper.Map<List<HobbyDTO>>(hobbyList);
           

            //var newList = hobbyList.AsQueryable().Select(x => new HobbyDTO { Id = x.Id, MyHobby = x.MyHobby });

            return list;
        }
    }
}
