using System;
using System.Collections.Generic;

namespace ecommerce.Models;

public partial class TbImage
{
    public int Id { get; set; }

    public string? ImagePath { get; set; }

    public int ModelType { get; set; }

    public int? LaptopId { get; set; }

    public virtual TbLaptop? Laptop { get; set; }
}
