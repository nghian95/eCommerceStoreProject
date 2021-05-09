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

        public (int, string, List<byte[]>) ShowImages(string SKU)
        {
            int value = 0;
            string message = "";
            byte[] imgData = null;
            List<byte[]> imageBytes = new List<byte[]>();
            try
            {
                List<Images> images = _context.Images.Where(x => x.Sku == SKU).ToList();
                foreach (var image in images)
                {
                    imageBytes.Add(image.ImageFile);
                }
                if (imageBytes != null)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                    message = "Images were not found successfully.";
                }
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
            }
            return (value, message, imageBytes);
        }

        public (int, string, byte[]) ShowSingleImage(int id)
        {
            int value = 0;
            string message = "";
            byte[] imgData = null;
            try
            {
                imgData = _context.Images.Find(id).ImageFile;
                if (imgData != null)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                    message = "Image was not found successfully.";
                }
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
            }
            return (value, message, imgData);
        }

        public (int[], string) FindImageIds(string SKU)
        {
            int[] value = new int[10];
            string message = "";
            try
            {
                List<Images> images = _context.Images.Where(x => x.Sku == SKU).ToList();
                for (int i = 0; i < images.Count; i++)
                {
                    value[i] = images[i].ImageId;
                }
            } catch (Exception ex)
            {
                value = null;
                message = ex.Message;
            }
            return (value, message);
        }

        public (List<int>,string) FindImageIdsForCategory(int categoryId)
        {
            List<int> imageIds = new List<int>();
            string message = "";
            try
            {
                List<string> skus = _context.Products.Where(x => x.CategoryId == categoryId).Select(x => x.Sku).ToList();
                foreach (string sku in skus)
                {
                    imageIds.Add(_context.Images.Where(x => x.Sku == sku).Select(x => x.ImageId).First());
                }
            }
            catch (Exception ex)
            {
                imageIds = null;
                message = ex.Message;
            }
            return (imageIds, message);
        }

        public (int, string) UploadImage(Images image)
        {
            int value = 0;
            string message = "";
            try
            {
                _context.Images.Add(image);
                _context.SaveChanges();
                if (_context.Images.Find(image.ImageId) != null)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                    message = "Image was not found in database.";
                }
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
            }
            return (value, message);
        }

        public (int, string) DeleteImage(int id)
        {
            int value = 0;
            string message = "";
            try
            {
                Images image = _context.Images.Find(id);
                _context.Images.Remove(image);
                _context.SaveChanges();
                if (_context.Images.Find(id) == null)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                    message = "Image was not deleted.";
                }
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
            }
            return (value, message);
        }

        public (Users, string) FindUser(string userName)
        {
            string message = "";
            Users user = null;
            try
            {
                 user = _context.Users.Find(userName);
                if (user.UserName == userName)
                {
                    message = "User found successfully.";
                } else
                {
                    message = "User not found.";
                }
            }
            catch (Exception ex)
            {
                user = null;
                message = ex.Message;
            }
            return (user, message);
        }

        public (int, string) EditUser(Users user)
        {
            int value = 0;
            string message = "";
            try
            {
                Users entUser = _context.Users.Find(user.UserName);
                entUser.Address = user.Address;
                entUser.FirstName = user.FirstName;
                entUser.LastName = user.LastName;
                entUser.Phone = user.Phone;
                entUser.Email = user.Email;
                _context.Update(entUser);
                _context.SaveChanges();
                entUser = _context.Users.Find(user.UserName);
                if (entUser.FirstName == user.FirstName && entUser.LastName == user.LastName && entUser.Address == user.Address && entUser.Phone == user.Phone && entUser.Email == user.Email)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                    message = "Failed to Edit User";
                }
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
            }
            return (value, message);
        }

        public (int, string) ChangePassword(Users user)
        {
            int value = 0;
            string message = "";
            try
            {
                Users actualUser = _context.Users.Find(user.UserName);
                actualUser.Password = user.Password;
                _context.Update(actualUser);
                _context.SaveChanges();
                if (_context.Users.Find(user.UserName).Password == user.Password)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                    message = "Failed to change password.";
                }
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
            }
            return (value, message);
        }

        public (int, string) AddTransaction(Transactions transaction)
        {
            int value = 0;
            string message = "";
            try
            {
                //int max = Convert.ToInt32(_context.Transactions.Where(x => x.TransactionGroup != null).OrderByDescending(t => t.TransactionGroup).FirstOrDefault().TransactionGroup);
                //transaction.TransactionGroup = ++max;
                if (transaction.Status == 1)
                {
                    Transactions transac = _context.Transactions.Where(x => x.Sku == transaction.Sku && x.Status == 1).FirstOrDefault();
                    if (transac != null)
                    {
                        if (transac.Quantity != null)
                        {
                            transac.Quantity = transac.Quantity + transaction.Quantity;
                        }
                        _context.Transactions.Update(transac);
                    } else
                    {
                        _context.Transactions.Add(transaction);
                    }
                } else
                {
                    _context.Transactions.Add(transaction);
                }
                _context.SaveChanges();
                if (_context.Transactions.Find(transaction.TransactionId) != null)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                    message = "Failed to add transaction.";
                }
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
            }
            return (value, message);
        }

        public (List<Transactions>, string, decimal?) RetrieveTransactions(int status, string username)
        {
            List<Transactions> transactions = new List<Transactions>();
            string message = "";
            decimal? subtotal = 0;
            try
            {
                transactions = _context.Transactions.Where(x => x.Status == status && x.UserName == username).ToList();
                foreach (Transactions transac in transactions)
                {
                    subtotal += (transac.Quantity * transac.Price);
                }
                if (transactions != null)
                {
                    message = "Successful";
                }
                else
                {
                    message = "Failed to retrieve transactions.";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return (transactions, message, subtotal);
        }

        public (int, string) DeleteTransaction(Transactions transaction)
        {
            int value = 0;
            string message = "";
            try
            {
                Transactions testTransaction = _context.Transactions.Find(transaction.TransactionId);
                _context.Transactions.Remove(testTransaction);
                _context.SaveChanges();
                if (_context.Transactions.Find(transaction.TransactionId) == null)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                    message = "Failed to delete transaction.";
                }
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
            }
            return (value, message);
        }

        public (int, string) EditTransactionQuantity(int transactionID, int quantity)
        {
            int value = 0;
            string message = "";
            try
            {
                Transactions transaction = _context.Transactions.Find(transactionID);
                if (quantity == 0)
                {
                    _context.Transactions.Remove(transaction);
                    _context.SaveChanges();
                }
                else
                {
                    transaction.Quantity = quantity;
                    _context.Transactions.Update(transaction);
                    _context.SaveChanges();
                    if (_context.Transactions.Find(transactionID).Quantity == quantity)
                    {
                        value = 1;
                    }
                    else
                    {
                        value = -1;
                        message = "Failed to Edit Transaction Quantity";
                    }
                }
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
            }
            return (value, message);
        }

        public (int, string) ConfirmPurchase()
        {
            int value = 0;
            string message = "";
            try
            {
                List<Transactions> justPurchased = _context.Transactions.Where(x => x.Status == 1).ToList();
                int? maxTransactionGroup = _context.Transactions.Max(x => x.TransactionGroup) + 1;
                foreach(Transactions transac in justPurchased)
                {
                    transac.TransactionGroup = maxTransactionGroup;
                    transac.Status = 2;
                    _context.Update(transac);
                }
                _context.SaveChanges();
                if (_context.Transactions.Find(justPurchased.FirstOrDefault().TransactionId).Status == 2)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                    message = "Failed to Checkout";
                }
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
            }
            return (value, message);
        }

        public (int, string) SaveRegisteredAccount(Users user)
        {
            int value = 0;
            string message = "";
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                if (_context.Users.Find(user.UserName) != null)
                {
                    value = 1;
                }
                else
                {
                    value = -1;
                    message = "Failed to register user";
                }
            }
            catch (Exception ex)
            {
                value = -99;
                message = ex.Message;
            }
            return (value, message);
        }

        //public (int, string) AddToCart(Transactions transaction)
        //{
        //    int value = 0;
        //    string message = "";
        //    try
        //    {
        //        _context.Add(transaction);
        //        _context.SaveChanges();
        //        if (_context.Transactions.Find(transaction.TransactionId) != null)
        //        {
        //            value = 1;
        //        }
        //        else
        //        {
        //            value = -1;
        //            message = "Failed to Add to Cart";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        value = -99;
        //        message = ex.Message;
        //    }
        //    return (value, message);
        //}

        //public (int, string) DummyMethod(string variable)
        //{
        //    int value = 0;
        //    string message = "";
        //    try
        //    {
        //        if (_context.)
        //        {
        //            value = 1;
        //        }
        //        else
        //        {
        //            value = -1;
        //            message = "Failed";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        value = -99;
        //        message = ex.Message;
        //    }
        //    return (value, message);
        //}
    }
}
