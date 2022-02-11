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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HobbyService( IHobbyRepository hobbyRepository, IMapper mapper,IUnitOfWork unitOfWork)
        {
            _hobbyRepository = hobbyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddHobby(HobbyDTO hobbyDTO)
        { 
            var addHobby = _mapper.Map<HobbyDTO, Hobby>(hobbyDTO);
       
            await _unitOfWork.HobbyRepository.insert(addHobby);
   
            await _unitOfWork.Commit();
        }

        public async Task<List<HobbyDTO>> GetAll()
        {
          

            var hobbyList = await _unitOfWork.HobbyRepository.GetAll();
            var list = _mapper.Map<List<HobbyDTO>>(hobbyList);
            await _unitOfWork.Commit();

            //var newList = hobbyList.AsQueryable().Select(x => new HobbyDTO { Id = x.Id, MyHobby = x.MyHobby }); automapper kullanmazsak
            return list;
        }
    }
}
