using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class TbPrices
    {
        [Key]
        public int Id { get; set; }
        public int Price { get; set; }
    }
}
