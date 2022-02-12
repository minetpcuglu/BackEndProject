using AutoMapper;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Models.VMs;
using DataAccessLayer.UnitOfWorks.Interface;
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

        public async Task<List<EducationVM>> GetAll()
        {
            var educationList = await _unitOfWork.EducationRepository.GetAll();
            var list = _mapper.Map<List<EducationVM>>(educationList);
            await _unitOfWork.Commit();
            return list;
        }
    }
}
