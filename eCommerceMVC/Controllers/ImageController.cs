using eCommerceClassLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Http;
using eCommerceClassLibrary.Models;
using AutoMapper;

namespace eCommerceMVC.Controllers
{
    public class ImageController : Controller
    {
        private readonly eCommerceRepository _repository;
        private readonly IMapper _mapper;

        public ImageController(eCommerceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

        //public IActionResult UploadFile(HttpPostedFileBase file)
        //{

        //    return View();
        //}

        [HttpPost]
        public IActionResult Index(IFormFile files)
        {
            var tuple = (0,"");
            if (Convert.ToInt32(Request.Cookies["Count"]) >= 9)
            {
                ViewBag.Message = "Max amount of photos has been reached (10).";
                return View("Failed");
            }
            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = Convert.ToString(Guid.NewGuid());

                    var objfiles = new Images()
                    {
                        ImageName = newFileName,
                        OriginalFormat = fileExtension,
                        Sku = (string)TempData["Sku"]
                    };

                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        objfiles.ImageFile = target.ToArray();
                    }
                    tuple = _repository.UploadImage(objfiles);
                }

            }
            int result = tuple.Item1;
            if (result == 1) {
                return View("Success");
            } else
            {
                return View("Failed");
            }
        }

        public IActionResult Products()
        {
            return RedirectToAction("Products", "Admin");
        }

        public IActionResult ViewProductLanding(string Sku)
        {
            Products product = _repository._context.Products.Find(Sku);
            eCommerceMVC.Models.Products modelProduct = _mapper.Map<eCommerceMVC.Models.Products>(product);
            return RedirectToAction("ViewProductLanding", "Admin", modelProduct);
        }

        public IActionResult Categories()
        {
            return RedirectToAction("Categories", "Admin");
        }
    }
}
