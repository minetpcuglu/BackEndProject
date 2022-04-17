using BusinessLayer.Services.Interface;
using DataAccessLayer.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbyController : ControllerBase
    {
        IHobbyService _hobbyService;

        public HobbyController(IHobbyService hobbyService)
        {
            _hobbyService = hobbyService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _hobbyService.GetAll();
            return Ok(result);
        }


        [HttpPost("Add")]
        public IActionResult Add(HobbyDTO hobbyDTO)
        {
            var result = _hobbyService.Add(hobbyDTO); //toast and notify

            return Ok(result);
        }

    }
}
