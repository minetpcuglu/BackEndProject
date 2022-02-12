using BusinessLayer.Services.Interface;
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
    }
}
