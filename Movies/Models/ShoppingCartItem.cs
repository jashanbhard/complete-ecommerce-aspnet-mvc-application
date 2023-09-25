using System.ComponentModel.DataAnnotations;

namespace Movies.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public double Amount { get; set; }
        public Movie? Movie{ get; set; }

        public string ShoppingCartId { get; set; }
    }
}
