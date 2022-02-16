
using AutoMapper;
using BusinessLayer.Services.Interface;
using BusinessLayer.Validation.FluentValidation;
using DataAccessLayer.Models.DTOs;
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
            var list = await _hobbyServices.GetAll();
            return View(list);
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
             await   _hobbyServices.Add(hobbyDTO);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in validateResult.Errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(hobbyDTO);
        }


        public async Task<IActionResult> DeleteHobby(int id)
        {
            await _hobbyServices.Delete(id);
            return RedirectToAction("Index");
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
