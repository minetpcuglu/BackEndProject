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
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public EducationService(IMapper mapper, IUnitOfWork unitOfWork, IEducationRepository educationRepository, ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _educationRepository = educationRepository;

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

        //public async Task<bool> Update(EducationVM entity)
        //{
        //    var educationGet = await _unitOfWork.EducationRepository.FirstOrDefault(x => x.Id == entity.Id);
        //    if(educationGet != null)
        //    {
               
        //        await _educationRepository.Update(educationGet);

        //        await _unitOfWork.Commit();
        //        return true;
        //    }

        //    return false;
        //}

        public async Task Update(EducationVM entity)
        {
            var education = await _unitOfWork.EducationRepository.GetById(entity.Id);

            var educationUpdate = _mapper.Map<Education>(entity);

            if (entity.SchollName != education.SchollName )
            {
                education.SchollName = entity.SchollName;
                await _unitOfWork.EducationRepository.Update(education);
                await _unitOfWork.Commit();
            }
            if (entity.Section != education.Section)
            {
                education.Section = entity.Section;
                await _unitOfWork.EducationRepository.Update(education);
                await _unitOfWork.Commit();
            }
            if (entity.NoteAverage != education.NoteAverage)
            {
                education.NoteAverage = entity.NoteAverage;
                await _unitOfWork.EducationRepository.Update(education);
                await _unitOfWork.Commit();
            }
            if (entity.Date != education.Date)
            {
                education.Date = entity.Date;
                await _unitOfWork.EducationRepository.Update(education);
                await _unitOfWork.Commit();
            }

        }


    }
}
