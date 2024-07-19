using System;
using System.Collections.Generic;

namespace ecommerce.Models;

public partial class TbSetting
{
    public int Id { get; set; }

    public string? WebsiteName { get; set; }

    public string? Description { get; set; }

    public string? Logo { get; set; }

    public string? TwitterLink { get; set; }

    public string? InstgramLink { get; set; }

    public string? YoutubeLink { get; set; }

    public string? MiddleBanner { get; set; }
}
