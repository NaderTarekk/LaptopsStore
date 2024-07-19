namespace ecommerce.Models
{
    public interface IClsProducts
    {
        public List<TbLaptop> GetAllLaptops();
        public List<TbLaptop> GetAllLaptopsByCategory(string? cat);
        public List<TbLaptop> GetAllLaptopsByPrice(int? price);
        public TbLaptop? GetProductById(int id);
    }
}
