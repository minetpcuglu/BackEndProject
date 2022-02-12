using DataAccessLayer.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Interface
{
  public interface IEducationService
    {
        Task<List<EducationVM>> GetAll();
    }
}
