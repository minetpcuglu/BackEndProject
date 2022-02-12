
using AutoMapper;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Models.DTOs;
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

       [HttpGet]
        public async Task<IActionResult> AddHobby()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddHobby( HobbyDTO hobbyDTO)
        {
            await _hobbyServices.AddHobby(hobbyDTO);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> DeleteHobby(int id)
        {
            var value = await _hobbyServices.GetById(id);
           await _hobbyServices.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
