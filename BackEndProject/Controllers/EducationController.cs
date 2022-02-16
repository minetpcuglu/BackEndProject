using BusinessLayer.Services.Interface;
using DataAccessLayer.Models.VMs;
using FluentValidation;
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
        private readonly IValidator<EducationVM> _educationValidator;
        public EducationController(IEducationService educationService, IValidator<EducationVM> educationValidator)
        {
            _educationValidator = educationValidator;
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
            var validateResult = _educationValidator.Validate(educationVM);
            if (validateResult.IsValid)
            {
                await _educationService.Add(educationVM);
                return RedirectToAction("GetList");
            }
            else
            {
                foreach (var error in validateResult.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(educationVM);
        }

        public async Task<IActionResult> DeleteEducation(int id)
        {
            await _educationService.Delete(id);
            return RedirectToAction("Getlist");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEducation(int id)
        {


            var value = await _educationService.GetById(id);
            return View(value);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateEducation(EducationVM educationVM)
        {
            var validateResult = _educationValidator.Validate(educationVM);
            if (validateResult.IsValid)
            {
                await _educationService.Update(educationVM);
                return RedirectToAction("GetList");
            }
            else
            {
                foreach (var error in validateResult.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(educationVM);
 
        }


    }
}
