using System;
using System.Collections.Generic;

namespace VA_EcommerceWebsite.Data;

public partial class NhanVien
{
    public int MaNv { get; set; }

    public string HoTen { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? MatKhau { get; set; }

    public string? Sdt { get; set; }

    public bool? GioiTinh { get; set; }

    public string MaPb { get; set; } = null!;

    public string ChucVu { get; set; } = null!;

    public DateTime NgayVaoLam { get; set; }

    public decimal Luong { get; set; }

    public bool? IsActive { get; set; }

    public virtual PhongBan MaPbNavigation { get; set; } = null!;
}
