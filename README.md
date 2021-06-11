**#README#**

    Description: An eCommerce store project in the Fashion industry that utilizes the .NET Core, ASP.NET and MVC frameworks. 

**#TO DO#**

*FINISHED*
    - Update database to properly fit the models in the project.


*IMPORTANT*
    - Copy changes from Customer Pages to other Views as well

    - Explore dropdown menus (i.e. for CategoryId when adding product)
        - Dropdown menu for customer to see all their options (Account into AccountInfo, OrderHistory)
    - Promo codes
    - Order History should be restructured to separate the different purchases more clearly and show the subtotal.

    - Upload images for products
    - Admin page of Product Landing should have rearrange images, delete product, duplicate product.
    - Make it look nice
    - Users can leave reviews
    - Coupon / News Banner at top
    - newsletter sign up for customer / new unregistered user
    - Wishlist
    - Demo for credit card/bank info/checking info
    - Mobile friendly
    - deploy to server
    - Small photos of the product next to the name in checkout/cart
    - Purchases shoud lower stock of products. 
        - Customer should not be able to buy more than there is in stock.

*BUGS*

    - With 10 images, sometimes the product landing page doesn't load the ImageIds fast enough for the page and causes an error
    - Name for Categories is one step behind. 

*OPTIONAL*
    - How to have images all the same size/layout.
    - Redirect from Success after 5 seconds.
    - Cookie-based Authentication / Identity-based Authentication
    - Different themes admin can choose from
    - Add popups instead of new pages for simple edits/views
    - Maybe have a "save address as default" option when editing address in Checkout
    - Learn jquery
    - Add plugin for tracking 

**#DOUBLECHECK#**
    - Customer checkout makes it not have the customer layout signed in.
        - Not able to replicate

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
                       3 = Just deleted
                       -1 = Return/Refund
    
    TransactionGroup is for Purchases that have multiple items.

**#RESTORING DATABASE**
    In SSMS SQL:
    
        CREATE DATABASE eCommerceDB

        USE eCommerceDB

    Right click eCommerceDB > Tasks > Restore > Add eCommerceDB.bak > Click Options and check Overwrite the Existing Database (With replace) > Select Close existing connections to destination database
