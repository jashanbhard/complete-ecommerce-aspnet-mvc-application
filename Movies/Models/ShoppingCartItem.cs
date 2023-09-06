using System.ComponentModel.DataAnnotations;

namespace Movies.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int id { get; set; }
        public double Amount { get; set; }
        public Movie Movie{ get; set; }

        public string ShoppingCartId { get; set; }
    }
}
