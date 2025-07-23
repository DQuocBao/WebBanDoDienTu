using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ictshop.Models
{
    public class SanphamViewModel
    {
        public int Masp { get; set; }
        public string Tensp { get; set; }
        public decimal? Giatien { get; set; }
        public int? Mahang { get; set; }
        public int? Mahdh { get; set; }
        public int? Thesim { get; set; } // Chỉ có trên Sanpham
        public string RAM { get; set; }
        public string Bonhotrong { get; set; } // Dùng chung cho cả Sanpham và Sanphammaytinh
        public string Loaithietbi_Nhucau { get; set; }
        public string TenHangSX { get; set; }
        public DateTime? NgayThem { get; set; }
        public string GPUVGA { get; set; }
        public string CPU { get; set; }
        public string Chip { get; set; }
        public string Pin { get; set; }
        public string KichThuocManHinh { get; set; }
    }
}