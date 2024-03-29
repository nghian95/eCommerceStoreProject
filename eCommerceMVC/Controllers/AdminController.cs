﻿using AutoMapper;
using eCommerceClassLibrary;
using eCommerceMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerceMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly eCommerceRepository _repository;
        private readonly IMapper _mapper;
        private CookieOptions option = new CookieOptions()
        {
            IsEssential = true,
            Secure = true, 
            SameSite = SameSiteMode.None
        };



        public AdminController(ILogger<AdminController> logger, eCommerceRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public IActionResult HomePage()
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public IActionResult Categories()
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            Response.Cookies.Append("BackLink", "Categories", option);

            var result = _repository.ViewCategories();
            List<eCommerceClassLibrary.Models.Categories> categories = result.Item1;
            ViewBag.Message = result.Item2;
            List<Categories> mvcCategories = new List<Categories>();
            Categories mvcCategory = new Categories();
            foreach (eCommerceClassLibrary.Models.Categories category in categories)
            {
                mvcCategory = _mapper.Map<Categories>(category);
                mvcCategories.Add(mvcCategory);
            }
            return View(mvcCategories);
        }

        public IActionResult Products()
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            Response.Cookies.Append("BackLink", "Products", option);

            var result = _repository.ViewAllProducts();
            List<eCommerceClassLibrary.Models.Products> products = result.Item1;
            ViewBag.Message = result.Item2;
            List<Products> modelProducts = new List<Products>();
            Products product = new Products();
            foreach (var prod in products)
            {
                product = _mapper.Map<Products>(prod);
                modelProducts.Add(product);
            }
            return View(modelProducts);
        }

        public IActionResult AddCategory()
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            var tuple = _repository.GetNextCategoryId();
            ViewBag.NextCategoryId = tuple.Item1;
            ViewBag.Message = tuple.Item2;
            return View();
        }
        public IActionResult SaveAddedCategory(Models.Categories category)
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            eCommerceClassLibrary.Models.Categories entityCategory;
            entityCategory = _mapper.Map<eCommerceClassLibrary.Models.Categories>(category);
            var tuple = _repository.AddCategory(entityCategory);
            var value = tuple.Item1;
            ViewBag.Message = tuple.Item2;
            switch (value)
            {
                case 1: return View("Success");
                case 2:
                case -1:
                case -99: return View("Failed");
                default: return View("AddCategory");
            }
        }

        public IActionResult RenameCategory(Models.Categories category)
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            return View(category);
        }

        public IActionResult SaveRenamedCategory(Models.Categories category)
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            //eCommerceClassLibrary.Models.Categories entityCategory;
            //entityCategory = _mapper.Map<eCommerceClassLibrary.Models.Categories>(category);
            var tuple = _repository.RenameCategory(category.CategoryId, category.Name);
            int value = tuple.Item1;
            ViewBag.Message = tuple.Item2;
            switch (value)
            {
                case 1: return View("Success");
                case 0:
                case -1:
                case -99: return View("Failed");
                default: return View("RenameCategory");
            }
        }

        public IActionResult DeleteCategory(Models.Categories category)
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            return View(category);
        }

        public IActionResult SaveDeletedCategory(Models.Categories category)
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            var tuple = _repository.DeleteCategory(category.CategoryId);
            int value = tuple.Item1;
            ViewBag.Message = tuple.Item2;
            switch (value)
            {
                case 1: return View("Success");
                case 0:
                case -1:
                case -99: return View("Failed");
                default: return View("DeleteCategory");
            }
        }

        public IActionResult ViewProductsInCategory(Models.Categories category)
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            Response.Cookies.Append("BackLink", "SpecificCategory", option);

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
            Response.Cookies.Append("Category", category.Name, option);
            Response.Cookies.Append("CategoryId", ""+category.CategoryId, option);
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

        public IActionResult AddProduct(int categoryId)
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            var tuple = _repository.FindCategory(categoryId);
            eCommerceClassLibrary.Models.Categories category = tuple.Item1;
            ViewBag.Message = tuple.Item2;

            return View();
        }

        public IActionResult AddProductAlone()
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public IActionResult SaveAddedProduct(/*IFormCollection frm*/Models.Products products)
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            Products product = new Products();
            //product.SKU = frm["SKU"];
            //product.Name = frm["Name"];
            //product.Quantity = Convert.ToInt32(frm["Quantity"]);
            //product.CategoryId = Convert.ToInt32(frm["CategoryId"]);
            eCommerceClassLibrary.Models.Products entityProduct = new eCommerceClassLibrary.Models.Products();
            entityProduct = _mapper.Map<eCommerceClassLibrary.Models.Products>(products);
            var result = _repository.AddProduct(entityProduct);
            int value = result.Item1;
            ViewBag.Message = result.Item2;
            switch (value)
            {
                case 1: return View("Success");
                case 0:
                case -1:
                case -99: return View("Failed");
                default: return View("DeleteCategory");
            }
        }

        public IActionResult DuplicateProduct(Models.Products product)
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            return View(product);
        }

        public IActionResult EditProduct(Models.Products product)
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            return View(product);
        }


        public IActionResult SaveEditedProduct(Models.Products product)
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            eCommerceClassLibrary.Models.Products entityProduct = _mapper.Map<eCommerceClassLibrary.Models.Products>(product);
            var tuple = _repository.EditProduct(entityProduct);
            int value = tuple.Item1;
            ViewBag.Message = tuple.Item2;
            switch (value)
            {
                case 1: return View("Success");
                case 0:
                case -1:
                case -99: return View("Failed");
                default: return View("DeleteCategory");
            }
        }
        public IActionResult DeleteProduct(Models.Products product)
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            return View(product);
        }

        public IActionResult SaveDeletedProduct(Models.Products product)
        {
            if (Request.Cookies["Access"] != "1")
            {
                return RedirectToAction("Login", "Home");
            }
            var tuple = _repository.DeleteProduct(product.Sku);
            int value = tuple.Item1;
            ViewBag.Message = tuple.Item2;
            switch (value)
            {
                case 1: return View("Success");
                case 0:
                case -1:
                case -99: return View("Failed");
                default: return View("DeleteCategory");
            }
        }

        public IActionResult LogOut()
        {
            Response.Cookies.Append("Access", "-1", option);
            if (Request.Cookies["Access"] == "-1")
            {
                return View("LogOut");
            }
            return View();
        }

        //public async Task<IActionResult> SignInUser(Models.LoginModel user)
        //{
        //    var claims = new List<Claim>
        //    {   
        //        new Claim(ClaimTypes.NameIdentifier, user.UserID),
        //        new Claim(ClaimTypes.Email, user.UserID)
        //    };
        //    var identity = new ClaimsIdentity(claims);
        //    var principal = new ClaimsPrincipal(identity);
        //    await HttpContext.SignInAsync(principal);
        //    return RedirectToAction(“Index”, “Home”);
        //}

        public IActionResult ViewProductLanding(Models.Products product)
        {
            CookieOptions option = new CookieOptions();
            Response.Cookies.Append("BackLink", "ViewProductLanding", option);
            if (product.Name == null || product.Name == "")
            {
                product = _mapper.Map<Products>(_repository._context.Products.Find(product.Sku));
            }
            //HttpCookie myCookie = new HttpCookie("myCookie");
            TempData["Name"] = product.Name;
            TempData["Sku"] = product.Sku;
            //var tuple = _repository.ShowImages(product.SKU);
            var tuple = _repository.FindImageIds(product.Sku);
            int[] ids = tuple.Item1;
            ViewBag.Message = tuple.Item2;
            int count = (ids.Count(s => s != 0) - 1);
            string strCount = Convert.ToString(count);
            string cookieCount1 = Request.Cookies["Count"];
            HttpContext.Response.Cookies.Delete("Count");
            HttpContext.Response.Cookies.Append("Count", "1", option);
            string cookieCount2 = Request.Cookies["Count"];
            HttpContext.Response.Cookies.Delete("Count");
            HttpContext.Response.Cookies.Append("Count", strCount, option);
            string cookieCount = Request.Cookies["Count"];
            for (int num = 0; num <= count; num++)
            {
                ViewData["ImageID" + num] = ids[num];
            }
            TempData["Count"] = ids.Count(s => s != 0) - 1;
            //ViewData["ImageID"] = id;
            //var image = _repository.ShowSingleImage(id);
            return View(product);
        }

        //public IActionResult ViewProductLandingAfterBackToList(String Sku)
        //{
        //    eCommerceClassLibrary.Models.Products product = _repository._context.Products.Find(Sku);
        //    Products modelProduct = _mapper.Map<Products>(product);
        //    Response.Cookies.Append("BackLink", "ViewProductLanding", option);
        //    TempData["Name"] = modelProduct.Name;
        //    TempData["Sku"] = modelProduct.SKU;
        //    //var tuple = _repository.ShowImages(product.SKU);
        //    var tuple = _repository.FindImageIds(modelProduct.SKU);
        //    int[] ids = tuple.Item1;
        //    ViewBag.Message = tuple.Item2;
        //    int num = 0;
        //    foreach (var id in ids)
        //    {
        //        ViewData["ImageID" + num] = id;
        //        num++;
        //    }
        //    Response.Cookies.Append("Count", (ids.Count(s => s != 0) - 1).ToString(), option);
        //    //TempData["Count"] = ids.Count(s => s != 0) - 1;
        //    //ViewData["ImageID"] = id;
        //    //var image = _repository.ShowSingleImage(id);
        //    return View("ViewProductLanding");
        //}

        public IActionResult DeleteImage(Models.Products product)
        {
            var tuple = _repository.FindImageIds(product.Sku);
            int[] ids = tuple.Item1;
            int num = 0;
            TempData["Sku"] = product.Sku;
            foreach (var id in ids)
            {
                ViewData["ImageID" + num] = id;
                num++;
            }
            return View();
        }

        public IActionResult ConfirmDeletedImage(int id)
        {
            var tuple = _repository.DeleteImage(id);
            int value = tuple.Item1;
            ViewBag.Message = tuple.Item2;
            string tempValue = (string)TempData["Sku2"];
            TempData["Sku"] = tempValue;
            var tuple2 = _repository.FindImageIds((string)TempData["Sku"]);
            int[] ids = tuple2.Item1;

            switch (value)
            {
                case 1:
                    Response.Cookies.Append("Count", (ids.Count(s => s != 0) - 1).ToString(), option);
                    return View("Success");
                default: return View("Failed");
            }
        }

    }

}
