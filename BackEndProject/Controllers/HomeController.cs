using BackEndProject.Models;
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

        public IActionResult Index( int startPage = 0)
        {
            var pageCount = 3;
            var startFrom = startPage + pageCount;
            ViewBag.NextPage = startPage + 1;
            ViewBag.PreviousPage = startPage - 1;

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

            educations = educations.Skip(startFrom).Take(pageCount).ToList();
            SlidingExpiration();
            CachePriority();
            
            return View(educations);
        }

        private void RemoveKey()
        {
            _memoryCache.Remove("Educations");   //key silinir
        }

        private void SlidingExpiration()
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.SlidingExpiration = TimeSpan.FromSeconds(5);
            _memoryCache.Set<string>("Educations", "System", options);
        }
        private void CachePriority()
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.Priority = CacheItemPriority.High;
            //High : Ram dolarsa en son bunu sil 
            //Low : Ram dolarsa ilk bunu sil 
            //NeverRemove : Ram dolsa da silme Not: Exception'a düşme ihtimali var Ram dolarsa
            //Normal : High ile Low arasında
            _memoryCache.Set<string>("username", "System", options);
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
