using eCommerceClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace eCommerceClassLibrary
{
    public class eCommerceRepository
    {
        public eCommerceDBContext _context;

        public eCommerceRepository(eCommerceDBContext context)
        {
            _context = context;
        }

        public (int, string) AddProduct(Products product)
        {
            int status = 0;
            string message = "";
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                if (_context.Products.Find(product.Sku) != null)
                {
                    status = 1;
                } else
                {
                    status = -1;
                    message = "Product was not added successfully.";
                }
            } catch (Exception ex)
            {
                status = -99;
                message = ex.Message;
            }
             return (status, message);
        }

        public (List<Categories>, string) ViewCategories()
        {
            List<Categories> categories = null;
            string message = "";
            try
            {
                categories = _context.Categories.ToList();
                //if (categories.Count == 0)
                //{
                //    categories = null;
                //}
            } catch (Exception ex)
            {
                categories = null;
                message = ex.Message;
            }
            return (categories, message);
        }

        public (List<Products>, string) ViewProductsInCategory(Categories category)
        {
            List<Products> products = null;
            string message = "";
            try
            {
                products = _context.Products
                            .Where(x => x.CategoryId == category.CategoryId)
                            .ToList();
            } catch (Exception ex)
            {
                message = ex.Message;
                products = null;
            }
            return (products, message);
        }

        public (List<Products>, string) ViewAllProducts()
        {
            List<Products> products = null;
            string message = "";
            try
            {
                products = _context.Products
                            .ToList();
            }
            catch (Exception ex)
            {
                products = null;
                message = ex.Message;
            }
            return (products, message);
        }

        public (int, string) EditProduct(Products newProduct)
        {
            int value = 0;
            string message = "";
            try
            {
                Products products = _context.Products.Find(newProduct.Sku);
                products.Sku = newProduct.Sku;
                products.Name = newProduct.Name;
                products.Quantity = newProduct.Quantity;
                products.Description = newProduct.Description;
                products.Price = newProduct.Price;
                products.CategoryId = newProduct.CategoryId;
                products.Sale = newProduct.Sale;
                //Console.WriteLine(newProduct.Sku);
                //using (var newContext = new eCommerceDBContext())
                //{
                //    newContext.Products.Update(newProduct);
                //    newContext.SaveChanges();
                //}
                _context.Products.Update(products);
                _context.SaveChanges();
                Products product = _context.Products.Find(newProduct.Sku);
                bool equal = (newProduct.Sku.Equals(product.Sku) && newProduct.Name.Equals(product.Name) && newProduct.Quantity.Equals(product.Quantity) &&
                  newProduct.Description.Equals(product.Description) && newProduct.Price.Equals(product.Price) && newProduct.CategoryId.Equals(product.CategoryId) &&
                  newProduct.Sale.Equals(product.Sale));
                if (equal)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                    message = "Edited product was not saved properly.";
                }
            } catch (Exception ex)
            {
                message = ex.Message;
                value = -99;
            }
            return (value, message);
        }

        public (int, string) DeleteProduct(string Sku)
        {
            int value = 0;
            string message = "";
            try
            {
                Products product = _context.Products.Find(Sku);
                _context.Products.Remove(product);
                _context.SaveChanges();
                if (_context.Products.Find(Sku) == null)
                {
                    value = 1;
                } else
                {
                    value = -1;
                    message = "Product was not deleted.";
                }
            } catch (Exception ex)
            {
                message = ex.Message;
                value = -99;
            }
            return (value, message);
        } 

        //public int AddCategory(string categoryName)
        //{
        //    int value = 0;
        //    try
        //    {
        //        Categories category = new Categories();
        //        category.Name = categoryName;
        //        _context.Add(category);
        //        _context.SaveChanges();
        //        if (_context.Categories.Find(category.CategoryId) != null)
        //        {
        //            value = 1;
        //        } else
        //        {
        //            value = -1;
        //        }
        //    } catch (Exception ex)
        //    {
        //        value = -99;
        //    }
        //    return value;
        //}

        public (int, string) AddCategory(Categories category)
        {
            int value = 0;
            string message = "";
            try
            {
                if (_context.Categories.Find(category.CategoryId) != null)
                {
                    return (2, "There is already a Category with that Id");
                }
                _context.Categories.Add(category);
                _context.SaveChanges();
                if (_context.Categories.Find(category.CategoryId) != null)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                    message = "Category was not added successfully.";
                }
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
                //message = ex.ToString();
                
            }
            return (value, message);
        }

        public (int, string) GetNextCategoryId()
        {
            int value = 0;
            string message = "";
            try
            {
                value = _context.Categories.OrderByDescending(x => x.CategoryId).First().CategoryId + 1;
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
            }
            return (value, message);
        }

        public (int, string) RenameCategory(int categoryId, string categoryName)
        {
            int value = 0;
            string message = "";
            try
            {
                Categories category = new Categories();
                category = _context.Categories.Find(categoryId);
                category.Name = categoryName;
                _context.Update(category);
                _context.SaveChanges();
                if (_context.Categories.Find(category.CategoryId).Name == categoryName)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                    message = "Category Name was not saved properly.";
                }
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
            }
            return (value, message);
        }

        public (int,string) DeleteCategory(int categoryId)
        {
            int value = 0;
            string message = "";
            try
            {
                Categories category = new Categories();
                category = _context.Categories.Find(categoryId);
                _context.Remove(category);
                _context.SaveChanges();
                if (_context.Categories.Find(category.CategoryId) == null)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                    message = "Category was not deleted.";
                }
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
            }
            return (value, message);
        }

        public (int, string, int) ValidateCredentials(string username, string password)
        {
            int value = 0;
            string message = "";
            int access = -1;
            try
            {
                Users user = _context.Users.Find(username);
                if (user != null && user.Password.Equals(password))
                {
                    value = 1;
                    access = user.Access;
                }
                else
                {
                    value = -1;
                    message = "Login credentials do not match.";
                }
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
            }
            return (value, message, access);
        }

        public (Categories, string) FindCategory(int categoryId)
        {
            Categories category = null;
            string message = "";
            try
            {
                category = _context.Categories.Find(categoryId);
            }
            catch (Exception ex)
            {
                category = null;
                message = ex.Message;
            }
            return (category, message);
        }

        //public int DummyMethod(string variable)
        //{
        //    int value = 0;
        //    try
        //    {
        //        if (true)
        //        {
        //            value = 1;
        //        }
        //        else
        //        {
        //            value = -1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        value = -99;
        //    }
        //    return value;
        //}
    }
}
