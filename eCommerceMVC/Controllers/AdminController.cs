using AutoMapper;
using eCommerceClassLibrary;
using eCommerceMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly eCommerceRepository _repository;
        private readonly IMapper _mapper;

        public AdminController(ILogger<AdminController> logger, eCommerceRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public IActionResult HomePage()
        {
            return View();
        }

        public IActionResult Categories()
        {
            List<eCommerceClassLibrary.Models.Categories> categories = _repository.ViewCategories();
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
            List<eCommerceClassLibrary.Models.Products> products = _repository.ViewAllProducts();
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
            ViewBag.NextCategoryId = _repository.GetNextCategoryId();
            return View();
        }
        public IActionResult SaveAddedCategory(Models.Categories category)
        {
            eCommerceClassLibrary.Models.Categories entityCategory;
            entityCategory = _mapper.Map<eCommerceClassLibrary.Models.Categories>(category);
            int value = _repository.AddCategory(entityCategory);
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
            return View(category);
        }

        public IActionResult SaveRenamedCategory(Models.Categories category)
        {
            //eCommerceClassLibrary.Models.Categories entityCategory;
            //entityCategory = _mapper.Map<eCommerceClassLibrary.Models.Categories>(category);
            int value = _repository.RenameCategory(category.CategoryId, category.Name);
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
            return View(category);
        }

        public IActionResult SaveDeletedCategory(Models.Categories category)
        {
            int value = _repository.DeleteCategory(category.CategoryId);
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
            eCommerceClassLibrary.Models.Categories entityCategory = _mapper.Map<eCommerceClassLibrary.Models.Categories>(category);
            List<eCommerceClassLibrary.Models.Products> entityProducts = _repository.ViewProductsInCategory(entityCategory);
            List<Models.Products> modelProducts = new List<Models.Products>();
            Models.Products modelProduct = null;
            foreach (var product in entityProducts)
            {
                modelProduct = _mapper.Map<Models.Products>(product);
                modelProducts.Add(modelProduct);
            }
            ViewBag.Category = category.Name.Substring(0, category.Name.Length-1);
            ViewBag.Id = category.CategoryId;
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
            eCommerceClassLibrary.Models.Categories category = _repository.FindCategory(categoryId);
            ViewBag.Category = category.Name.Substring(0, category.Name.Length - 1);
            return View();
        }

        public IActionResult SaveAddedProduct(/*IFormCollection frm*/Models.Products products)
        {
            Products product = new Products();
            //product.SKU = frm["SKU"];
            //product.Name = frm["Name"];
            //product.Quantity = Convert.ToInt32(frm["Quantity"]);
            //product.CategoryId = Convert.ToInt32(frm["CategoryId"]);
            eCommerceClassLibrary.Models.Products entityProduct = new eCommerceClassLibrary.Models.Products();
            entityProduct = _mapper.Map<eCommerceClassLibrary.Models.Products>(products);
            int value = _repository.AddProduct(entityProduct);
            switch (value)
            {
                case 1: return View("Success");
                case 0:
                case -1:
                case -99: return View("Failed");
                default: return View("DeleteCategory");
            }
        }

        public IActionResult EditProduct(Models.Products product)
        {
            return View(product);
        }

        public IActionResult SaveEditedProduct(Models.Products product)
        {
            eCommerceClassLibrary.Models.Products entityProduct = _mapper.Map<eCommerceClassLibrary.Models.Products>(product);
            int value = _repository.EditProduct(entityProduct);
            switch (value)
            {
                case 1: return View("Success");
                case 0:
                case -1:
                case -99: return View("Failed");
                default: return View("DeleteCategory");
            }
        }
        public IActionResult DeleteProduct()
        {
            return View();
        }

        public IActionResult SaveDeletedProduct()
        {
            return View();
        }

        public IActionResult DetailsProduct()
        {
            return View();
        }

    }
}
