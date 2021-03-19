using AutoMapper;
using eCommerceClassLibrary;
using eCommerceClassLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly eCommerceRepository _repository;
        private readonly IMapper _mapper;
        private CookieOptions option = new CookieOptions();

        public CustomerController(eCommerceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            return View();
        }

        public IActionResult AccountInfo()
        {
            Response.Cookies.Append("BackLink", "AccountInfo", option);
            var tuple = _repository.FindUser(Request.Cookies["UserName"]);
            Users user = tuple.Item1;
            Models.Users modelUser = _mapper.Map<Models.Users>(user);
            ViewBag.Message = tuple.Item2;
            return View(modelUser);
        }

        public IActionResult Edit(Models.Users user)
        {
            return View(user);
        }

        public IActionResult SaveEdit(Models.Users user)
        {
            Users entUser = _mapper.Map<Users>(user);
            var tuple = _repository.EditUser(entUser);
            int value = tuple.Item1;
            ViewBag.Message = tuple.Item2;
            if (value == 1)
            {
                return View("Success");
            }
            else
            {
                return View("Failed");
            }
        }
        
        public IActionResult ChangePassword(Models.Users user)
        {
            Response.Cookies.Append("ChangePassword", "true", option);
            return View();
        }

        public IActionResult SaveChangedPassword(Models.Users user)
        {
            Users entUser = _mapper.Map<Users>(user);
            var tuple = _repository.ChangePassword(entUser);
            ViewBag.Message = tuple.Item2;
            if (tuple.Item1 == 1)
            {
                return View("Success");
            } else
            {
                return View("Failed");
            }
        }

        public IActionResult AddToCart(Models.Products product)
        {
            return View();
        }

        public IActionResult ViewCart()
        {
            return View();
        }

        public IActionResult ViewOrderHistory()
        {
            return View();
        }
    }
}
