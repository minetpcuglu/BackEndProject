
using AutoMapper;
using BusinessLayer.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Controllers
{
    public class HobbyController : Controller
    {
        private readonly IHobbyService _hobbyServices;
   
        public HobbyController(IHobbyService hobbyService )
        {
            _hobbyServices = hobbyService;
           
        }

        public async Task<IActionResult> Index()
        {
            var list = await _hobbyServices.GetAll();
            return View(list);
        }
    }
}
