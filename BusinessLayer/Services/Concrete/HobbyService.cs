using AutoMapper;
using BusinessLayer.Services.BaseServices.Interface;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Context;
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
    public class HobbyService :IHobbyService
    {
        private readonly IHobbyRepository _hobbyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public HobbyService(IMapper mapper, IUnitOfWork unitOfWork, IHobbyRepository hobbyRepository, ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _hobbyRepository = hobbyRepository;

        }

        public async Task Add(HobbyDTO hobbyDTO)
        { 
            var addHobby = _mapper.Map<HobbyDTO, Hobby>(hobbyDTO);
            await _unitOfWork.HobbyRepository.insert(addHobby);
            await _unitOfWork.Commit();
        }

        public async Task Delete (int id)
        {
            var deleteHobby = await _unitOfWork.HobbyRepository.Get(x => x.Id == id);
            _unitOfWork.HobbyRepository.Delete(deleteHobby);
            await _unitOfWork.Commit();
        }

        public async Task Update(HobbyDTO entity)
        {
            var hobbyUpdate = _mapper.Map<HobbyDTO,Hobby>(entity);

            if (hobbyUpdate.Id != 0)
            {
                await _hobbyRepository.Update(hobbyUpdate);
                await _unitOfWork.SaveChangesAsync();
            }


        }

   

        public async Task<List<HobbyDTO>> GetAll()
        {
            var hobbyList = await _unitOfWork.HobbyRepository.GetAll();
            var list = _mapper.Map<List<HobbyDTO>>(hobbyList);
            await _unitOfWork.Commit();

            //var newList = hobbyList.AsQueryable().Select(x => new HobbyDTO { Id = x.Id, MyHobby = x.MyHobby }); automapper kullanmazsak
            return list;
        }
    

        public async Task<HobbyDTO> GetById(int id)
        {
           var hobby  = await _unitOfWork.HobbyRepository.GetById(id);
            return _mapper.Map<HobbyDTO>(hobby);
        }

       
    }
}
