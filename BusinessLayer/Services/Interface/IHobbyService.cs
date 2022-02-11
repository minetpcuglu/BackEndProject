using DataAccessLayer.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Interface
{
  public  interface IHobbyService
    {
        Task<IQueryable<HobbyDTO>> GetAll();
    }
}
