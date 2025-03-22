using System;
using System.Collections.Generic;

namespace VA_EcommerceWebsite.Data;

public partial class PhongBan
{
    public string MaPb { get; set; } = null!;

    public string TenPhongBan { get; set; } = null!;

    public DateTime? NgayThanhLap { get; set; }

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
