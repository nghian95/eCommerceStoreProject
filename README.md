**#README#**

    Description: An eCommerce store project in the Fashion industry that utilizes the .NET Core, ASP.NET and MVC frameworks. 

**#TO DO#**

    - Set the correct properties for add product as required.
    - Explore dropdown menus (i.e. for CategoryId when adding product)
    - Create Pages
    - Authenticate the user. Make sure Admin is only accessible to admins, and customer info is only available to that specific customer or the admin.
    - Create sessions that store user info.
    - Create Customer pages.
    - Differentiate no-user pages and customer pages
    - For Failed pages, print out the error message.
    - Redirect from Success/Failure after 5 seconds.

**#DOUBLECHECK#**
    How to pass properties into Action Methods (do we need asp-route-id or the parameter Models.Products or do we need IFormCollection frm)

**#IDEAS#**

    1) For simplistic edits, rather than leading to another view, perhaps use a partial view or even have a pop-up.
    2) After Success or Failure, automatically redirect to appropriate page.
    3) Overriding Equals to compare Products object's values

**#TECHNICAL DOCUMENTATION#**

    Products have Primary Key SKU [VarChar(20)]
    CategoryID is a Foreign Key 

    SKU example: BLUEPOLO0001