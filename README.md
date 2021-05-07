**#README#**

    Description: An eCommerce store project in the Fashion industry that utilizes the .NET Core, ASP.NET and MVC frameworks. 

**#TO DO#**
*Finished*
- Error when viewing productlanding page with no images
 - Either hide empty categories or solve the exception for Customer
    - Basic Checkout Functionality


*IMPORTANT*
    - Copy changes from Customer Pages to other Views as well

    - Order History page
    - Logoff functionality
    - Register Account page
    - Have increment, unique TransactionGroupIds that work properly. Add TransactionGroupIds to the ConfirmPurchase method
    - Adjust Edit Address Html.Textbox to be bigger to see the entire address.
    - Make shipping addresses saved with Transaction Groups
        
    - Discount price vs Total Price visible
    - Upload images for products
    - Admin page of Product Landing should have rearrange images, delete product, duplicate product.
    - Explore dropdown menus (i.e. for CategoryId when adding product)
    - Create Pages
        - Customer Pages
    - Create sessions that store user info.
    - Differentiate no-user pages and customer pages
    - Set up Data Annotations for min/max string length, type, etc. for models
    - Logout
    - Users can leave reviews
    - Coupon / News Banner at top
    - newsletter sign up for customer / new unregistered user
    - Wishlist
    - Table of Past Transactions
    - Account History / Order History
    -

*BUGS*

    - With 10 images, sometimes the product landing page doesn't load the ImageIds fast enough for the page and causes an error
    - Name for Categories is one step behind. 

*OPTIONAL*
    - Learn how to backup the sql server by having the create script in a separate file so I can work from my main PC as well
    - How to have images all the same size/layout.
    - Redirect from Success after 5 seconds.
    - Cookie-based Authentication / Identity-based Authentication
    - Different themes admin can choose from
    - Add popups instead of new pages for simple edits/views

**#DOUBLECHECK#**


**#IDEAS#**

    1) For simplistic edits, rather than leading to another view, perhaps use a partial view or even have a pop-up.
    2) Overriding Equals to compare Products object's values

**#TECHNICAL DOCUMENTATION#**

    Products have Primary Key SKU [VarChar(20)]
    CategoryID is a Foreign Key 

    SKU example: PoloT01-R for Red Polo Shirt

    Transaction Status 0 = Wish list
                       1 = Add to Cart
                       2 = Purchased
                       -1 = Return/Refund
    
    TransactionGroup is for Purchases that have multiple items.