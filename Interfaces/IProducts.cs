using ecommerce.Models;

namespace ecommerce.Interfaces
{
    public interface IProducts
    {
        public CartItemModel GetItemInCartById(int id);
    }
}
