using AutoMapper;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Models.VMs;
using DataAccessLayer.UnitOfWorks.Interface;
using EntityLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Concrete
{
    public class EducationService : IEducationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EducationService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        public async Task Add(EducationVM entity)
        {
            var addEducation = _mapper.Map<EducationVM, Education>(entity);
            await _unitOfWork.EducationRepository.insert(addEducation);
            await _unitOfWork.Commit();
        }

      

        public async Task Delete(int id)
        {
            var deleteEducation = await _unitOfWork.EducationRepository.Get(x => x.Id == id);
            _unitOfWork.EducationRepository.Delete(deleteEducation);
            await _unitOfWork.Commit();
        }

     

        public async Task<List<EducationVM>> GetAll()
        {
            var educationList = await _unitOfWork.EducationRepository.GetAll();
            var list = _mapper.Map<List<EducationVM>>(educationList);
            await _unitOfWork.Commit();
            return list;
        }

        public async Task<EducationVM> GetById(int id)
        {
            var educationGet = await _unitOfWork.EducationRepository.GetById(id);
            return _mapper.Map<EducationVM>(educationGet);
        }

        public Task Update(EducationVM entity)
        {
            throw new NotImplementedException();
        }
    }
}
