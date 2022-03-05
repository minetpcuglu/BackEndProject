using BusinessLayer.Services.BaseServices.Interface;
using CoreLayer.Utilities.Results.Interface;
using DataAccessLayer.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Interface
{
    public interface IHobbyService
    {
        Task<List<HobbyDTO>> GetAll();
        Task Add(HobbyDTO hobbyDTO);
        Task<HobbyDTO> GetById(int id);
        Task Update(HobbyDTO hobbyDTO);
        Task Delete(int id);
    }
}
