using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Site.Models;
using Site.Models.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICommonDataManager _service;

        public HomeController(ILogger<HomeController> logger, ICommonDataManager commonDataManager)
        {
            _logger = logger;
            _service = commonDataManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_service.GetAllData());
        }

        [HttpGet]
        public IActionResult ShowDetailsCar(Guid Id)
        {
            return View(_service.GetAllDataForVechicle(Id));
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

        public IActionResult Portfolio()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }
    }
}
