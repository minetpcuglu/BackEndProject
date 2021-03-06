using BusinessLayer.Services.Interface;
using BusinessLayer.Validation.FluentValidation;
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
        public EducationController(IEducationService educationService,  IValidator<EducationVM> educationValidator)
        {
            _educationValidator = educationValidator;
            _educationService = educationService;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        public async Task<IActionResult> GetList(int startPage = 0)
        {
            var pageCount = 5;
            var startFrom = startPage + pageCount;
            ViewBag.NextPage = startPage + 1;
            ViewBag.PreviousPage = startPage - 1;
            var value = await _educationService.GetAll();
            value = value.Skip(startFrom).Take(pageCount).ToList();
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

        [HttpPost]
        public async Task<IActionResult> DeleteEducation(int id)
        {
            if (id != 0)
            {
                var result = await _educationService.Delete(id);
                if (result)
                {
                    return Json(new ToastViewModel
                    {
                        Message = "silindi.",
                        Success = true
                    });
                }
                else
                {
                    return Json(new ToastViewModel
                    {
                        Message = "İşlem Başarısız.",
                        Success = false
                    });
                }
            }
            return View();
          
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
