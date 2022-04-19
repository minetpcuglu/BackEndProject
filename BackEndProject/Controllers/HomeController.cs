﻿using BackEndProject.Models;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Context;
using DataAccessLayer.Models.VMs;
using EntityLayer.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IEducationService _educationService;
        private readonly ApplicationDbContext _context;

        //private readonly ILogger<HomeController> _logger;

        public HomeController(/*ILogger<HomeController> logger*/ IMemoryCache memoryCache , IEducationService educationService , ApplicationDbContext context)
        {
            //_logger = logger;
            _memoryCache = memoryCache;
            _educationService = educationService;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Education> educations;
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            if (!_memoryCache.TryGetValue("Educations",out educations))
            {
                _memoryCache.Set("Educations", _context.Educations.ToList());
            }
            educations = _memoryCache.Get("Educations") as List<Education>;
            stopWatch.Stop();
            ViewBag.TotelTime = stopWatch.Elapsed;
            ViewBag.TotelRows = educations.Count;
          
            return View(educations);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
