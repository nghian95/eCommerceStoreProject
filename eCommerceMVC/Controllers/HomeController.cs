using eCommerceMVC.Models;
using eCommerceClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace eCommerceMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly eCommerceRepository _repository;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, eCommerceRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public IActionResult HomePage()
        {
            CookieOptions option = new CookieOptions();
            Response.Cookies.Append("ChangePassword", "false", option);
            Response.Cookies.Append("Access", "" + -1, option);
            return View();
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

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CheckRole(IFormCollection frm)
        {
            string userName = frm["username"];
            string password = frm["password"];
            var tuple = _repository.ValidateCredentials(userName, password);
            int value = tuple.Item1;
            ViewBag.Message = tuple.Item2;
            var tuple2 = _repository.FindUser(userName);
            Users user = _mapper.Map<Users>(tuple2.Item1);
            switch (value)
            {
                case 1:
                    CookieOptions option = new CookieOptions();
                    Response.Cookies.Append("Access", ""+tuple.Item3, option);
                    if (user.FirstName != null)
                    {
                        Response.Cookies.Append("Name", user.FirstName, option);
                    } else
                    {
                        Response.Cookies.Append("Name", userName, option);
                    }
                    Response.Cookies.Append("UserName", userName, option);
                    string pw = Request.Cookies["ChangePassword"];
                    if (pw != "true") {
                        if (tuple.Item3 == 1)
                        {
                            return RedirectToAction("HomePage", "Admin");
                        } else
                        {
                            return RedirectToAction("HomePage", "Customer");
                        }
                    } else
                    {
                        Response.Cookies.Append("ChangePassword", "false", option);
                        return RedirectToAction("ChangePassword", "Customer", user);
                    }

                case -1: return RedirectToAction("Login");
                case -99: return RedirectToAction("Login");
                default: return RedirectToAction("Login");
            }
        }

        public IActionResult Categories()
        {
            var result = _repository.ViewCategories();
            List<eCommerceClassLibrary.Models.Categories> categories = result.Item1;
            ViewBag.Message = result.Item2;
            List<Categories> mvcCategories = new List<Categories>();
            Categories mvcCategory = new Categories();
            foreach(eCommerceClassLibrary.Models.Categories category in categories)
            {
                mvcCategory = _mapper.Map<Categories>(category);
                mvcCategories.Add(mvcCategory);
            }
            return View(mvcCategories);
        }

        public IActionResult Products()
        {
            var tuple = _repository.ViewAllProducts();
            List<eCommerceClassLibrary.Models.Products> products = tuple.Item1;
            ViewBag.Message = tuple.Item2;
            List<Products> modelProducts = new List<Products>();
            Products product = new Products();
            foreach (var prod in products)
            {
                product = _mapper.Map<Products>(prod);
                modelProducts.Add(product);
            }
            return View(modelProducts);
        }

        public IActionResult LogOut()
        {
            return View();
        }
    }
}
