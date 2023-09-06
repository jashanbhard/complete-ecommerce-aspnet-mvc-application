using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Data
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public void AddItemToCart(Movie movie)
        {
            var shoppingcartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if (shoppingcartItem == null)
            {
                shoppingcartItem = new ShoppingCartItem()
                {
                    Movie = movie,
                    ShoppingCartId = ShoppingCartId,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(shoppingcartItem);
            }
            else
            {
                shoppingcartItem.Amount++;
            }
            _context.SaveChanges();
        }
        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingcartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if (shoppingcartItem != null)
            {
                if (shoppingcartItem.Amount>1)
                {
                    shoppingcartItem.Amount--;
                }
                _context.ShoppingCartItems.Remove(shoppingcartItem);
            }  
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShippingcartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId ==
            ShoppingCartId).Include(n => n.Movie).ToList());
        }
        public double GetShippingcartTotal() => _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n =>
            n.Movie.Price * n.Amount).Sum();

        //below condition is similar as above, this is just another method of writing it
        //public double GetShippingcartTotal()
        //{
        //    var total= _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n =>
        //    n.Movie.Price * n.Amount).Sum();
        //}

    }
}
