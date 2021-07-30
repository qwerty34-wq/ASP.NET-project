using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Site.Models;
using Site.Models.Entities;
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
        public IActionResult Index(FilterViewModel model)
        {

            var filter = new FilterViewModel();
            var vechicles = new List<Vechicle>();
            var users = new List<User>();

            filter = new FilterViewModel();
            vechicles = _service.GetVechicles().ToList();
            users = _service.GetUsers().ToList();

            //if (String.IsNullOrEmpty(model.Search) && model.VechicleState == null && model.VechicleType == null)
            //{
            //    //return RedirectToAction("Index", new { filterName = filterName, filterValue = filterValue });
            //    //ViewData["RES"] = $"{filterName} - {filterValue}";

            //    filter = new FilterViewModel();
            //    vechicles = _service.GetVechicles().ToList();
            //    users = _service.GetUsers().ToList();
            //}
            if (!String.IsNullOrEmpty(model.Search) || model.VechicleState != null || model.VechicleType != null)
            {
                if (!String.IsNullOrEmpty(model.Search))
                {
                    filter.Search = model.Search;
                    vechicles = vechicles.Where(x => (x.Name.ToLower() + x.Model.ToLower() + x.Country.ToLower()).Replace(" ", "").Contains(model.Search.ToLower().Trim().Replace(" ", ""))).ToList();
                }

                if (model.VechicleState != null)
                {
                    filter.VechicleState = model.VechicleState;
                    vechicles = vechicles.Where(x => x.VechicleState == model.VechicleState).ToList();
                }

                if (model.VechicleType != null)
                {
                    filter.VechicleType = model.VechicleType;
                    vechicles = vechicles.Where(x => x.VechicleType == model.VechicleType).ToList();
                }
            }

            MainViewModel main = new MainViewModel()
            {
                Filter = filter,
                Vechicles = vechicles,
                Users = users
            };

            return View(main);
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

        //[HttpGet]
        //public IActionResult Filter(FilterViewModel model)
        //{
            //string filterName = Request.Form.Keys.First();
            //string filterValue = Request.Form.FirstOrDefault(p => p.Key == filterName).Value;

            //return RedirectToAction("Index", new { filterName = filterName, filterValue = filterValue });
            //return Content($"{filterName} - {filterValue}");
        //}

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
