using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Movies.Data.ViewComponents
{
    
    public class ShoppingCartSummary: ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }
        
        public IViewComponentResult Invoke()
        {
            var item = _shoppingCart.GetShoppingcartItems();
            return View(item.Count);
        }
    }
}
