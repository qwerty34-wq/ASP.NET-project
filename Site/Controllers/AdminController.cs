using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Site.Models.Entities;
using Site.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICommonDataManager _service;

        public AdminController(ICommonDataManager commonDataManager)
        {
            _service = commonDataManager;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.RouteData.Values["action"].ToString() == "Index" || HttpContext.Session.Keys.Contains("ADMIN_LOG_IN") || HttpContext.Request.Method == "POST")
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = Content("No Access");
            }
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.Keys.Contains("ADMIN_LOG_IN"))
            {
                return RedirectToAction("AdminPanel", new { id = HttpContext.Session.GetString("ADMIN_LOG_IN") });
            }

            return View();
        }

        [HttpPost]
        public IActionResult LogIn(string login, string password)
        {

            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
            {
                return Content("Enter all fields");
            }

            var id = _service.LogIn(login, password);

            if (id == null)
            {
                return Content("No Access");
            }

            HttpContext.Session.SetString("ADMIN_LOG_IN", $"{id}");
            return RedirectToAction("AdminPanel", new { id = id });
            //return "Log in Succesfully :)";
            //return View();
        }


        public IActionResult AdminPanel(Guid id)
        {
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("ADMIN_LOG_IN");
            return RedirectToAction("Index");
        }

        // 
        public IActionResult ShowUsers()
        {
            return View(_service.GetUsers());
        }

        [HttpGet]
        public IActionResult CreateUsers()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUsers(User model)
        {

            if (model.Hash == null)
            {
                ModelState.AddModelError("hash", "Password is required");
            }
            else if (model.Hash.Length > 50 || model.Hash.Length < 2)
            {
                ModelState.AddModelError("hash", "2 < Password < 50");
            }


            if (ModelState.IsValid)
            {
                model.SetGuid();
                model.SetHash();

                _service.AddUser(model);
                return RedirectToAction("ShowUsers");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditUsers(Guid Id)
        {
            var user = _service.GetUserById(Id);

            if (user is null)
            {
                throw new Exception($"User with id equals {Id} not found");
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult EditUsers(Guid Id, User model)
        {
            if (ModelState.IsValid)
            {
                _service._UpdateUser(Id, model);
                return RedirectToAction("ShowUsers");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult DetailsUsers(Guid Id)
        {
            var user = _service.GetUserById(Id);
            return View(user);
        }

        [HttpGet]
        public IActionResult DeleteUsers(Guid Id)
        {
            var user = _service.GetUserById(Id);

            if (user is null)
            {
                throw new Exception($"User with Id equals {Id} not found");
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult DeleteUsers(Guid Id, bool? isDelete)
        {
            _service.DeleteUser(Id);
            return RedirectToAction("ShowUsers");
        }


        // Car
        [HttpGet]
        public IActionResult CarIndex()
        {
            return View(_service.GetVechicles());
        }

        [HttpGet]
        public IActionResult CreateVechicle()
        {
            //var vechicles = _service.GetVechicles();
            //return View(vechicles);

            //SelectList sl= new SelectList(_service.GetUsers());
            //IList<SelectListItem> ls = new List<SelectListItem>();
            //foreach (var item in _service.GetUsers())
            //{
            //    SelectListItem i = new SelectListItem()
            //    {
            //        Value = item.Id.ToString(),
            //        Text = $"{item.Name} {item.Surname}"
            //    };
            //    ls.Add(i);
            //}
            //ViewData["Vechicles"] = ls;
            return View();
        }

        [HttpPost]
        public IActionResult CreateVechicle(Vechicle vechicle)
        {
            if (ModelState.IsValid)
            {
                vechicle.Id = Guid.NewGuid();
                _service.AddVechicle(vechicle);
                return RedirectToAction("CarIndex");
            }

            return View(vechicle);
        }

        [HttpGet]
        public IActionResult DetailsVechicle(Guid Id)
        {
            var vechicle = _service.GetVechicleById(Id);
            return View(vechicle);
        }

        [HttpGet]
        public IActionResult EditVechicle(Guid Id)
        {
            var v =_service.GetVechicleById(Id);

            if (v is null)
            {
                throw new Exception($"Vechicle id equals {Id} not found");
            }

            return View(v);
        }

        [HttpPost]
        public IActionResult EditVechicle(Guid Id, Vechicle vechicle)
        {

            if (ModelState.IsValid)
            {
                _service.UpdateVechicle(Id, vechicle);
                return RedirectToAction("CarIndex");
            }

            return View(vechicle);
        }

        [HttpGet]
        public IActionResult DeleteVechicle(Guid Id)
        {
            var vechicle = _service.GetVechicleById(Id);

            if (vechicle is null)
            {
                throw new Exception($"Vechicle with id equals {Id} not found");
            }

            return View(vechicle);
        }

        [HttpPost]
        public IActionResult DeleteVechicle(Guid id, bool? isDelete)
        {
            _service.DeleteVechicle(id);
            return RedirectToAction("CarIndex");
        }
    }
}
