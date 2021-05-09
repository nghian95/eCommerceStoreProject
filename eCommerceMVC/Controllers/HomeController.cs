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
        private CookieOptions _option = new CookieOptions();

        public HomeController(ILogger<HomeController> logger, eCommerceRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public IActionResult HomePage()
        {
            Response.Cookies.Append("ChangePassword", "false", _option);
            Response.Cookies.Append("Access", "" + -1, _option);
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
                    Response.Cookies.Append("Access", ""+tuple.Item3, _option);
                    if (user.FirstName != null)
                    {
                        Response.Cookies.Append("Name", user.FirstName, _option);
                    } else
                    {
                        Response.Cookies.Append("Name", userName, _option);
                    }
                    Response.Cookies.Append("UserName", userName, _option);
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
                        Response.Cookies.Append("ChangePassword", "false", _option);
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

        public IActionResult ViewProductLanding(Models.Products product)
        {
            Response.Cookies.Append("BackLink", "ViewProductLanding", _option);
            if (product.Name == null)
            {
                product = _mapper.Map<Products>(_repository._context.Products.Find(product.Sku));
            }
            TempData["Name"] = product.Name;
            TempData["Sku"] = product.Sku;
            //var tuple = _repository.ShowImages(product.SKU);
            var tuple = _repository.FindImageIds(product.Sku);
            int[] ids = tuple.Item1;
            ViewBag.Message = tuple.Item2;
            int count = (ids.Count(s => s != 0) - 1);
            Response.Cookies.Append("Count", Convert.ToString(count), _option);
            for (int num = 0; num <= count; num++)
            {
                ViewData["ImageID" + num] = ids[num];
            }
            //TempData["Count"] = ids.Count(s => s != 0) - 1;
            //ViewData["ImageID"] = id;
            //var image = _repository.ShowSingleImage(id);
            return View(product);
        }

        public IActionResult ViewProductsInCategory(Models.Categories category)
        {
            Response.Cookies.Append("BackLink", "SpecificCategory", _option);

            var tuple = _repository.FindImageIdsForCategory(category.CategoryId);
            List<int> imageIds = tuple.Item1;
            if (imageIds != null)
            {
                int num = 0;
                foreach (var id in tuple.Item1)
                {
                    ViewData["Image" + num] = id;
                    num++;
                }
            }
            ViewBag.Message = tuple.Item2;

            eCommerceClassLibrary.Models.Categories entityCategory = _mapper.Map<eCommerceClassLibrary.Models.Categories>(category);
            var result = _repository.ViewProductsInCategory(entityCategory);
            List<eCommerceClassLibrary.Models.Products> entityProducts = result.Item1;
            ViewBag.Message += result.Item2;
            List<Models.Products> modelProducts = new List<Models.Products>();
            Models.Products modelProduct = null;
            foreach (var product in entityProducts)
            {
                modelProduct = _mapper.Map<Models.Products>(product);
                modelProducts.Add(modelProduct);
            }
            Response.Cookies.Append("Category", category.Name, _option);
            Response.Cookies.Append("CategoryId", "" + category.CategoryId, _option);
            if (modelProducts.Count == 0)
            {
                List<Products> products = new List<Products>();
                return View(products);
            }
            else
            {
                return View(modelProducts);
            }
        }

        public IActionResult Register()
        {
            Response.Cookies.Append("BackLink", "Login", _option);
            return View();
        }

        public IActionResult SaveRegisteredAccount(Users user)
        {
            //TempData["BackLink"] = "Login";
            eCommerceClassLibrary.Models.Users entityUser = _mapper.Map<eCommerceClassLibrary.Models.Users>(user);
            var tuple = _repository.SaveRegisteredAccount(entityUser);
            int value = tuple.Item1;
            if (value == 1)
            {
                return View("Success");
            } else
            {
                return View("Failed");
            }

        }

    }
}
