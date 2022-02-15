using BusinessLayer.Services.Interface;
using DataAccessLayer.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Controllers
{
    public class EducationController : Controller
    {
        private readonly IEducationService _educationService;
        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetList()
        {
            var value = await _educationService.GetAll();
            return View(value);
        }


        [HttpGet]
        public IActionResult AddEducation()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddEducation(EducationVM educationVM)
        {
             await _educationService.Add(educationVM);
            return RedirectToAction("GetList");
        }

        public async Task<IActionResult> DeleteEducation(int id)
        {
            await _educationService.Delete(id);
            return RedirectToAction("Getlist");
        }

        [HttpGet]
        public async Task<IActionResult> EditEducation(int id)
        {
            var value =  await _educationService.GetById(id);
            return View(value);
        }


        [HttpPost]
        public async Task<IActionResult> EditEducation(EducationVM educationVM)
        {
            await _educationService.Update(educationVM);
            return RedirectToAction("GetList");
        }


    }
}
