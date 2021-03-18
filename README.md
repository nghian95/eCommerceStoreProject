**#README#**

    Description: An eCommerce store project in the Fashion industry that utilizes the .NET Core, ASP.NET and MVC frameworks. 

**#TO DO#**

*IMPORTANT*

    - Add to Cart functionality
    - Discount price vs Total Price visible
    - Upload images for products
    - Admin page of Product Landing should have rearrange images, delete product, duplicate product.
    - Explore dropdown menus (i.e. for CategoryId when adding product)
    - Create Pages
    - Create sessions that store user info.
    - Create Customer pages.
    - Differentiate no-user pages and customer pages
    - Set up Data Annotations for min/max string length, type, etc. for models
    - Hide empty fields in DeleteProduct
    - Logout
    - Users can leave reviews
    - Coupon / News Banner at top
    - newsletter sign up for customer / new unregistered user
    - Wishlist
    - Table of Past Transactions
    - Account History / Order History

*BUGS*
    - With 10 images, sometimes the product landing page doesn't load the ImageIds fast enough for the page and causes an error
    - Name for Categories is one step behind. 

*OPTIONAL*

    - Redirect from Success/Failure after 5 seconds.
    - Cookie-based Authentication / Identity-based Authentication
    - Different themes admin can choose from
**#DOUBLECHECK#**
    How to pass properties into Action Methods (do we need asp-route-id or the parameter Models.Products or do we need IFormCollection frm)

**#IDEAS#**

    1) For simplistic edits, rather than leading to another view, perhaps use a partial view or even have a pop-up.
    2) After Success or Failure, automatically redirect to appropriate page.
    3) Overriding Equals to compare Products object's values

**#TECHNICAL DOCUMENTATION#**

    Products have Primary Key SKU [VarChar(20)]
    CategoryID is a Foreign Key 

    SKU example: PoloT01-R for Red Polo Shirt