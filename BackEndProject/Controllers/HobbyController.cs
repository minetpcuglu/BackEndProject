
using AutoMapper;
using BusinessLayer.Services.Interface;
using BusinessLayer.Validation.FluentValidation;
using DataAccessLayer.Models.DTOs;
using DataAccessLayer.Models.VMs;
using FluentValidation;
using FluentValidation.Results;
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
        private readonly IValidator<HobbyDTO> _hobbyValidator;

        public HobbyController(IHobbyService hobbyService, IValidator<HobbyDTO> hobbyValidator)
        {
            _hobbyServices = hobbyService;
            _hobbyValidator = hobbyValidator;

        }

        public async Task<IActionResult> Index()
        {
            var result = await _hobbyServices.GetAll();

            return View(result);
        }

        [HttpGet]
        public IActionResult AddHobby()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddHobby(HobbyDTO hobbyDTO)
        {
            var validateResult = _hobbyValidator.Validate(hobbyDTO);
            if (validateResult.IsValid)
            {
                await _hobbyServices.Add(hobbyDTO);
                return Json(data: new { success = true, message = "your request has been successfuly added,." }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                foreach (var error in validateResult.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(hobbyDTO);
        }




        [HttpPost]
        public async Task<IActionResult> DeleteHobby(int id)
        {
            if (id != 0)
            {
                var result = await _hobbyServices.Delete(id);
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
            return BadRequest();
        }



        [HttpGet]
        public async Task<IActionResult> UpdateHobby(int id)
        {
            var value = await _hobbyServices.GetById(id);
            return View(value);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateHobby(HobbyDTO hobbyDTO)
        {


            var validateResult = _hobbyValidator.Validate(hobbyDTO);
            if (validateResult.IsValid)
            {
                await _hobbyServices.Update(hobbyDTO);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in validateResult.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(hobbyDTO);

        }

    }
}
