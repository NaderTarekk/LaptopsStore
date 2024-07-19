using ecommerce.Models;

namespace ecommerce.Bl
{
    public class ClsProducts : IClsProducts
    {
        LapShopProjectContext ctx = new LapShopProjectContext();
        public List<TbLaptop> GetAllLaptops()
        {
            return ctx.TbLaptops.ToList();
        }
        public List<TbLaptop> GetAllLaptopsByCategory(string? cat)
        {
            var filteredLaptops = ctx.TbLaptops.Where(lap => lap.Category == cat).ToList();
            return filteredLaptops;
        }
        public List<TbLaptop> GetAllLaptopsByPrice(int? price)
        {
            var filteredLaptops = ctx.TbLaptops.Where(lap => lap.Price <= price).ToList();
            return filteredLaptops;
        }

        public TbLaptop? GetProductById(int id)
        {
            var laptop = ctx.TbLaptops.FirstOrDefault(lap => lap.Id == id);
            return laptop;
        }
    }
}
