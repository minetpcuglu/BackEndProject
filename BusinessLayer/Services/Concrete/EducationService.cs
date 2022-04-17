using AutoMapper;
using BusinessLayer.Services.BaseServices.Interface;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Context;
using DataAccessLayer.Models.VMs;
using DataAccessLayer.Repositories.Interface.EntityTypeRepositories;
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
        private readonly IEducationRepository _educationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EducationService(IMapper mapper, IUnitOfWork unitOfWork, IEducationRepository educationRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _educationRepository = educationRepository;
        }

        public async Task Add(EducationVM entity)
        {
            var addEducation = _mapper.Map<EducationVM, Education>(entity);
            await _unitOfWork.EducationRepository.Insert(addEducation);
            await _unitOfWork.Commit();
        }

        public async Task<bool> Delete(int id)
        {
            if (id != 0)
            {
                var deleteEducation = await _unitOfWork.EducationRepository.Get(x => x.Id == id);
                _unitOfWork.EducationRepository.Delete(deleteEducation);

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<EducationVM>> GetAll()
        {
            var educationList = await _unitOfWork.EducationRepository.GetAll();
            var list = _mapper.Map<List<EducationVM>>(educationList);
           
            return list;
        }

        public async Task<EducationVM> GetById(int id)
        {
            var educationGet = await _unitOfWork.EducationRepository.GetById(id);
            return _mapper.Map<EducationVM>(educationGet);
        }

        public async Task Update(EducationVM entity)
        {
            var educationUpdate = _mapper.Map<EducationVM,Education>(entity);
            if (educationUpdate.Id !=0)
            {
                await _educationRepository.Update(educationUpdate);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
