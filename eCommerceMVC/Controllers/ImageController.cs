using eCommerceClassLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceMVC.Controllers
{
    public class ImageController : Controller
    {
        private readonly eCommerceRepository _repository;

        public ImageController(eCommerceRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowImage(string SKU)
        {
            var imageData = _repository.ShowImages(SKU);
            return View(imageData);
        }

        public IActionResult Show(int id)
        {
            var imageData = _repository.ShowSingleImage(id);
            return File(imageData.Item3, "image/jpg");
        }
    }
}
