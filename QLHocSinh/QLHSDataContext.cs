using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace QLHocSinh
{
    // Lớp dữ liệu: ánh xạ trực tiếp các bảng SQL Server bằng LINQ to SQL.
    public class QLHSDataContext : DataContext
    {
        public QLHSDataContext() : base(ConfigurationManager.ConnectionStrings["QLHS"].ConnectionString) { }
        public Table<Lop> Lops => GetTable<Lop>();
        public Table<HocSinh> HocSinhs => GetTable<HocSinh>();
    }

    [Table(Name = "dbo.Lop")]
    public class Lop
    {
        [Column(IsPrimaryKey = true)] public string MaLop { get; set; }
        [Column] public string TenLop { get; set; }
    }

    [Table(Name = "dbo.HocSinh")]
    public class HocSinh
    {
        [Column(IsPrimaryKey = true)] public string MaHS { get; set; }
        [Column] public string TenHS { get; set; }
        [Column] public DateTime NgaySinh { get; set; }
        [Column] public string DiaChi { get; set; }
        [Column] public string MaLop { get; set; }
        [Column] public float DTB { get; set; }
    }
}

