using System;
using System.Collections.Generic;

namespace ecommerce.Models;

public partial class TbLaptop
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Ram { get; set; }

    public decimal? ScreenSize { get; set; }

    public string? OperationSystem { get; set; }

    public string? Gpu { get; set; }

    public string? Proccessor { get; set; }

    public string? Type { get; set; }

    public int? Quality { get; set; }

    public bool? IsAvailable { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public string? Category { get; set; }

    public string? LaptopImg { get; set; }

    public virtual ICollection<TbImage> TbImages { get; set; } = new List<TbImage>();
}
