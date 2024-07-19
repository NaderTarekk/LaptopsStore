using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class CartModel
    {
        public CartModel() {
            lstItems = new List<CartItemModel>();
        }
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserLocation { get; set; }
        public ICollection<CartItemModel> lstItems { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }

    }
}
