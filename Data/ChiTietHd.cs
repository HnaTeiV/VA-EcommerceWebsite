using System;
using System.Collections.Generic;

namespace VA_EcommerceWebsite.Data;

public partial class ChiTietHd
{
    public int MaHd { get; set; }

    public int MaHh { get; set; }

    public int? TongTien { get; set; }

    public virtual HoaDon MaHdNavigation { get; set; } = null!;

    public virtual HangHoa MaHhNavigation { get; set; } = null!;
}
