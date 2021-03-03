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

        public IActionResult Categories()
        {
            List<eCommerceClassLibrary.Models.Categories> categories = _repository.ViewCategories();
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
            return View();
        }
    }
}
