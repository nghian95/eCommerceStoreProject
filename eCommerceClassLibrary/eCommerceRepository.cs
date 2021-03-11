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

        public int AddProduct(Products product)
        {
            int status = 0;
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
                }
            } catch (Exception ex)
            {
                status = -99;
            }
             return status;
        }

        public List<Categories> ViewCategories()
        {
            List<Categories> categories = null;
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
            }
            return categories;
        }

        public List<Products> ViewProductsInCategory(Categories category)
        {
            List<Products> products = null;
            try
            {
                products = _context.Products
                            .Where(x => x.CategoryId == category.CategoryId)
                            .ToList();
            } catch (Exception ex)
            {
                products = null;
            }
            return products;
        }

        public List<Products> ViewAllProducts()
        {
            List<Products> products = null;
            try
            {
                products = _context.Products
                            .ToList();
            }
            catch (Exception ex)
            {
                products = null;
            }
            return products;
        }

        public int EditProduct(Products newProduct)
        {
            int value = 0;
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
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                value = -99;
            }
            return value;
        }

        public int DeleteProduct(string Sku)
        {
            int value = 0;
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
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                value = -99;
            }
            return value;
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

        public int AddCategory(Categories category)
        {
            int value = 0;
            try
            {
                if (_context.Categories.Find(category.CategoryId) != null)
                {
                    return 2;
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
                }
            }
            catch (Exception ex)
            {
                value = -99;
            }
            return value;
        }

        public int GetNextCategoryId()
        {
            int value = 0;
            try
            {
                value = _context.Categories.OrderByDescending(x => x.CategoryId).First().CategoryId + 1;
            }
            catch (Exception ex)
            {
                value = -99;
            }
            return value;
        }

        public int RenameCategory(int categoryId, string categoryName)
        {
            int value = 0;
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
                }
            }
            catch (Exception ex)
            {
                value = -99;
            }
            return value;
        }

        public int DeleteCategory(int categoryId)
        {
            int value = 0;
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
                }
            }
            catch (Exception ex)
            {
                value = -99;
            }
            return value;
        }

        public int ValidateCredentials(string username, string password)
        {
            int value = 0;
            try
            {
                Users user = _context.Users.Find(username);
                if (user != null && user.Password.Equals(password))
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                }
            }
            catch (Exception ex)
            {
                value = -99;
            }
            return value;
        }

        public Categories FindCategory(int categoryId)
        {
            Categories category = null;
            try
            {
                category = _context.Categories.Find(categoryId);
            }
            catch (Exception ex)
            {
                category = null;
            }
            return category;
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
