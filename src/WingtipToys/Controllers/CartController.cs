using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WingtipToys.Logic;
using WingtipToys.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WingtipToys.Controllers
{
    
    public class CartController : Controller
    {
            private readonly ApplicationDbContext _db;
            private readonly UserManager<ApplicationUser> _userManager;
            public void AddToCart(int id)
            {
                ShoppingCartActions newShoppingCart = new ShoppingCartActions();
                newShoppingCart.ShoppingCartId = GetCartId();
            //_db.ShoppingCartActions.Add(newShoppingCart);
            //_db.SaveChanges();
            // Retrieve the product from the database.  
            try
            {
                var cars = _db.Products.ToList();
                int num = 4 + 5;
                num++;
            }
            catch(NullReferenceException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
                CartItem cartItem = null;     
                try
                {
                    cartItem = _db.CartItems.Include(p => p.Product).SingleOrDefault(
                        c => c.CartId == newShoppingCart.ShoppingCartId
                        && c.Product.ProductId == id);
                }
                catch(NullReferenceException e)
                {
                System.Diagnostics.Debug.WriteLine(e);
                }

                if (cartItem == null)
                {
                    // Create a new cart item if no cart item exists.                 
                    cartItem = new CartItem
                    {
                        ItemId = Guid.NewGuid().ToString(),
                        CartId = newShoppingCart.ShoppingCartId,
                        Product = _db.Products.SingleOrDefault(
                       p => p.ProductId == id),
                        Quantity = 1,
                        DateCreated = DateTime.Now
                    };

                    _db.CartItems.Add(cartItem);
                }
                else
                {
                    // If the item does exist in the cart,                  
                    // then add one to the quantity.                 
                    cartItem.Quantity++;
                }
                _db.SaveChanges();
            }


            public string GetCartId()
            {
                
                ISession current = HttpContext.Session;
                if (current.Id == null)
                {
                    if (!string.IsNullOrWhiteSpace(HttpContext.User.Identity.Name))
                    {
                        SessionExtensions.SetString(current, ShoppingCartActions.CartSessionKey, HttpContext.User.Identity.Name);
                    }
                    else
                    {
                        // Generate a new random GUID using System.Guid class.     
                        Guid tempCartId = Guid.NewGuid();
                        SessionExtensions.SetString(current, ShoppingCartActions.CartSessionKey, tempCartId.ToString());
                    }
                }
                return current.Id;
            }

            public List<CartItem> GetCartItems()
            {
            ShoppingCartActions newCart = new ShoppingCartActions();
                newCart.ShoppingCartId = GetCartId();

                return _db.CartItems.Where(
                    c => c.CartId == newCart.ShoppingCartId).ToList();
            }
            [HttpPost]
            public IActionResult Index(int ProductId)
            {

                AddToCart(ProductId);
                return RedirectToAction("ShoppingCart");
            }

            public IActionResult ShoppingCart()
            {
                ShoppingCartActions userShoppingCart = new ShoppingCartActions();
                return View(GetCartItems().ToList());
            }
        }
}

