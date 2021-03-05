using AutoMapper;
using eCommerceClassLibrary;
using eCommerceMVC.Models;
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


        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult SaveAddedProduct()
        {
            return View();
        }
    }
}
