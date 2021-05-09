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
        private readonly CookieOptions _option = new CookieOptions()
        {
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/",
            HttpOnly = false,
            Expires = DateTime.Now.AddMonths(1)
        };

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
            Response.Cookies.Append("BackLink", "AccountInfo", _option);
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
            Response.Cookies.Append("ChangePassword", "true", _option);
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

        public IActionResult ViewProductLanding(Models.Products product)
        {
            if (Request.Cookies["Access"] != "0")
            {
                return RedirectToAction("ViewProductLanding", "Home");
            }
            Response.Cookies.Append("BackLink", "ViewProductLanding", _option);
            //if (product.Name == null || product.Name == "")
            //{
                product = _mapper.Map<Models.Products>(_repository._context.Products.Find(product.Sku));
            //}
            TempData["Name"] = product.Name;
            TempData["Sku"] = product.Sku;
            //var tuple = _repository.ShowImages(product.SKU);
            var tuple = _repository.FindImageIds(product.Sku);
            int[] ids = tuple.Item1;
            ViewBag.Message = tuple.Item2;
            int count = (ids.Count(s => s != 0) - 1);
            Response.Cookies.Append("Count", Convert.ToString(count), _option);
            TempData["Count"] = count;
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

            if (Request.Cookies["Access"] != "0")
            {
                return RedirectToAction("ViewProductsInCategory", "Home");
            }
            //if (value == 0)
            //{
            //    Categories entityCategory = _repository._context.Categories.Find(category.CategoryId);
            //    Response.Cookies.Append("Category", entityCategory.Name, _option);
            //    return RedirectToAction("ViewProductsInCategory", "Customer", new
            //    {
            //        @category = category, @value = 1
            //    });
            //} else
            //{
            //CookieOptions cookieOptions = new CookieOptions();
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

            Categories entityCategory = _mapper.Map<Categories>(category);
            var result = _repository.ViewProductsInCategory(entityCategory);
            List<Products> entityProducts = result.Item1;
            ViewBag.Message += result.Item2;
            List<Models.Products> modelProducts = new List<Models.Products>();
            Models.Products modelProduct = null;
            foreach (var product in entityProducts)
            {
                modelProduct = _mapper.Map<Models.Products>(product);
                modelProducts.Add(modelProduct);
            }
            var categoryName0 = category.Name;
            var categoryID0 = category.CategoryId;
            TempData["Category"] = category.Name;
            TempData["CategoryId"] = category.CategoryId;
            Response.Cookies.Append("Category",category.Name, _option);
            Response.Cookies.Append("CategoryId",""+category.CategoryId, _option);
            //if (Request.Cookies["Category"] != category.Name)
            //{
            //    ViewProductsInCategory(category);
            //}
            var categoryName = Request.Cookies["Category"];
            var categoryID = Request.Cookies["CategoryId"];
            if (modelProducts.Count == 0)
            {
                List<Models.Products> products = new List<Models.Products>();
                return View(products);
            }
            else
            {
                return View(modelProducts);
            }
            //}
        }

        //public IActionResult RedirectToViewEmptyProductsInCategory(Models.Products)
        //{
        //    return 
        //}

        public IActionResult Products()
        {
            if (Request.Cookies["Access"] != "0")
            {
                return RedirectToAction("Products", "Home");
            }
            Response.Cookies.Append("BackLink", "Products", _option);

            var result = _repository.ViewAllProducts();
            List<Products> products = result.Item1;
            ViewBag.Message = result.Item2;
            List<Models.Products> modelProducts = new List<Models. Products>();
            Models.Products product = new Models.Products();
            foreach (var prod in products)
            {
                product = _mapper.Map<Models.Products>(prod);
                modelProducts.Add(product);
            }
            return View(modelProducts);
        }

        public IActionResult Categories()
        {
            if (Request.Cookies["Access"] != "0")
            {
                return RedirectToAction("Login", "Home");
            }
            Response.Cookies.Append("BackLink", "Categories", _option);
            //Response.Cookies.Append("Category", "Empty", _option);
            var result = _repository.ViewCategories();
            List<Categories> categories = result.Item1;
            ViewBag.Message = result.Item2;
            List<Models.Categories> mvcCategories = new List<Models.Categories>();
            Models.Categories mvcCategory = new Models.Categories();
            foreach (Categories category in categories)
            {
                mvcCategory = _mapper.Map<Models.Categories>(category);
                mvcCategories.Add(mvcCategory);
            }
            return View(mvcCategories);
        }

        public IActionResult AddToCart(Models.Products product, IFormCollection frm)
        {
            if (Request.Cookies["Access"] != "0")
            {
                return RedirectToAction("Login", "Home");
            }
            Transactions transaction = new Transactions();
            transaction.Sku = product.Sku;
            transaction.UserName = Request.Cookies["UserName"];
            transaction.Status = 1;
            transaction.Quantity = Convert.ToInt32(frm["amount"]);
            transaction.Price = product.Price;
            transaction.Name = product.Name;
            _repository.AddTransaction(transaction);
            var tuple = _repository.RetrieveTransactions(1);
            List<Transactions> transactions = tuple.Item1;
            ViewBag.Message = tuple.Item2;
            Models.Transactions modelTransaction = new Models.Transactions();
            List<Models.Transactions> modelTransactions = new List<Models.Transactions>();
            foreach (var model in transactions)
            {
                modelTransaction = _mapper.Map<Models.Transactions>(transaction);
                modelTransactions.Add(modelTransaction);
            }
            return RedirectToAction("ViewCart") ; // could also just make it redirect to view cart
        }

        public IActionResult SaveEditedQuantity(Models.Transactions transaction, IFormCollection frm)
        {
            if (Request.Cookies["Access"] != "0")
            {
                return RedirectToAction("Login", "Home");
            }
            int transactionID = transaction.TransactionId;
            int quantity = Convert.ToInt32(frm["Quantity"]);
            _repository.EditTransactionQuantity(transactionID, quantity);
            
            
            return RedirectToAction("ViewCart");
        }

        public IActionResult ViewCart()
        {
            if (Request.Cookies["Access"] != "0")
            {
                return RedirectToAction("Login", "Home");
            }
            Response.Cookies.Append("BackLink", "Cart", _option);
            var tuple = _repository.RetrieveTransactions(1);
            List<Transactions> transactions = tuple.Item1;
            if (transactions.Count == 0)
            {
                return View();
            }
            TempData["Subtotal"] = tuple.Item3;
            List<Models.Transactions> modelTransactions = new List<Models.Transactions>();
            Models.Transactions modTransac = new Models.Transactions();
            foreach (var item in transactions)
            {
                modTransac = _mapper.Map<Models.Transactions>(item);
                modelTransactions.Add(modTransac);
            }
            ViewBag.Message = tuple.Item2;
            return View(modelTransactions);
            
        }

        public IActionResult ViewOrderHistory(List<Models.Transactions> transactions)
        {
            if (Request.Cookies["Access"] != "0")
            {
                return RedirectToAction("Login", "Home");
            }
            return View(transactions);
        }

        public IActionResult ViewCheckout(IFormCollection frm)
        {
            if (Request.Cookies["Access"] != "0")
            {
                return RedirectToAction("Login", "Home");
            }
            string shippingAddress = frm["ShippingAddress"];
            var tuple = _repository.RetrieveTransactions(1);
            List<Transactions> transactions = tuple.Item1;
            TempData["Subtotal"] = tuple.Item3;
            List<Models.Transactions> modelTransactions = new List<Models.Transactions>();
            Models.Transactions modTransac = new Models.Transactions();
            foreach (var item in transactions)
            {
                modTransac = _mapper.Map<Models.Transactions>(item);
                modelTransactions.Add(modTransac);
            }
            ViewBag.Message = tuple.Item2;
            var tuple2 = _repository.FindUser(Request.Cookies["UserName"]);
            if (shippingAddress == null)
            {
                if (HttpContext.Session.GetString("Address") == null)
                {
                    HttpContext.Session.SetString("Address", tuple2.Item1.Address);
                }
                //TempData["Address"] = tuple2.Item1.Address;
            } else
            {
                HttpContext.Session.SetString("Address", shippingAddress);
            }
            return View(modelTransactions);
        }

        //[System.Web.Mvc.ChildActionOnly]
        public IActionResult EditAddress()
        {
            if (Request.Cookies["Access"] != "0")
            {
                return RedirectToAction("Login", "Home");
            }
            return PartialView();
        }

        public IActionResult DeleteTransaction(Transactions transaction)
        {
            if (Request.Cookies["Access"] != "0")
            {
                return RedirectToAction("Login", "Home");
            }
            eCommerceClassLibrary.Models.Transactions entityTransaction = _mapper.Map<eCommerceClassLibrary.Models.Transactions>(transaction);
            var tuple = _repository.DeleteTransaction(entityTransaction);
            int value = tuple.Item1;
            ViewBag.Message = tuple.Item2;
            if (value == 1)
            {
                return View("Success");
            } else
            {
                return View("Failed");
            }
        }

        public IActionResult ConfirmPurchase()
        {
            if (Request.Cookies["Access"] != "0")
            {
                return RedirectToAction("Login", "Home");
            }
            var tuple = _repository.ConfirmPurchase();
            ViewBag.Message = tuple.Item2;
            int value = tuple.Item1;
            if (value == 1)
            {
                return View("Success");
            } else
            {
                return View("Failed");
            }
        }

        public IActionResult LogOut()
        {
            Response.Cookies.Append("Access", "-1", _option);
            if (Request.Cookies["Access"] == "-1")
            {
                return View("LogOut");
            }
            return View();
        }
    }
}
