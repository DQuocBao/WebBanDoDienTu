//namespace Ictshop.Models
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Web;
//    using System.ComponentModel.DataAnnotations;
//    using System.ComponentModel.DataAnnotations.Schema;
//    using System.Data.Entity.Spatial;

//    [Table("SanphamMayTinh")]
//    public partial class SanphamMayTinh
//    {
//        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
//        public SanphamMayTinh()
//        {
//            Chitietdonhang = new HashSet<Chitietdonhang>();
//        }

//        [Key]
//        public int Masp { get; set; }

//        [StringLength(50)]
//        [Display(Name = "Tên sản phẩm")]
//        public string Tensp { get; set; }
        
//        [Display(Name = "Giá tiền")]
//        public decimal? Giatien { get; set; }
        
//        [Display(Name = "Số lượng")]
//        public int? Soluong { get; set; }
        
//        [Display(Name = "Mô tả")]
//        [Column(TypeName = "ntext")]
//        public string Mota { get; set; }

//        [StringLength(100)]
//        [Display(Name = "RAM")]
//        public string RAM { get; set; }

//        [StringLength(250)]
//        [Display(Name = "Dung lượng ổ cứng (SSD, HDD, ...)")]
//        [Column("Dungluongocung(SSD,HDD,...)")]
//        public string DungLuongOCung { get; set; } // "=" với lại Bonhotrong ở Bảng Sản phẩm(cùng 1 loại kiểu nhưng khác tên)

//        [Display(Name = "Là sản phẩm mới")]
//        public bool? Sanphammoi { get; set; }
        
//        [Display(Name = "Ảnh sản phẩm")]
//        [StringLength(50)]
//        public string Anhbia { get; set; }
        
//        [Display(Name = "Hãng sản xuất")]
//        public int? Mahang { get; set; }
        
//        [Display(Name = "Hệ điều hành")]
//        public int? Mahdh { get; set; }

//        [StringLength(150)]
//        [Display(Name = "CPU")]
//        public string CPU { get; set; }

//        [StringLength(150)]
//        [Display(Name = "GPU - VGA")]
//        [Column("GPU-VGA")]
//        public string GPUVGA { get; set; }

//        [StringLength(300)]
//        [Display(Name = "Kết nối (Mạng, Bluetooth, Cổng Sạc, Cổng Kết Nối)")]
//        [Column("Ketnoi(Mang,Bluetooth,CongSac,CongKetNoi)")]
//        public string KetNoi { get; set; }

//        [StringLength(10)]
//        [Display(Name = "Pin")]
//        public string Pin { get; set; }

//        [StringLength(200)]
//        [Display(Name = "Màn hình Laptop")]
//        public string Manhinhlaptop { get; set; }

//        [StringLength(10)]
//        [Display(Name = "Trọng lượng")]
//        public string Trongluong { get; set; }

//        [StringLength(50)]
//        [Display(Name = "Kích thước máy ( W x H x D )")]
//        public string Kichthuocmay { get; set; }

//        [StringLength(50)]
//        [Display(Name = "Mainboard")]
//        public string Mainboard { get; set; }

//        [StringLength(50)]
//        [Display(Name = "Nguồn PSU")]
//        public string NguonPSU { get; set; }

//        [StringLength(200)]
//        [Display(Name = "Tản nhiệt")]
//        public string Tannhiet { get; set; }

//        [StringLength(50)]
//        [Display(Name = "Loại Case")]
//        public string LoaiCase { get; set; }

//        [Display(Name = "Thông tin khác (Tính năng nổi bật, Công nghệ màn hình, Âm thanh, Chất liệu,...)")]
//        [Column("Thongtinkhac(TinhNangNoiBat,AmThanh,ChatLieu,.....)", TypeName = "ntext")]
//        public string Thongtinkhac { get; set; }


//        [StringLength(50)]
//        [Display(Name = "Tên hãng - Nhập tên hãng dạng chuỗi")]
//        public string Tenhangsx { get; set; }

//        [StringLength(50)]
//        [Display(Name = "Tên hệ điều hành - Nhập tên HĐH dạng chuỗi")]
//        public string Tenhedieuhanh { get; set; }

//        [StringLength(50)]
//        [Display(Name = "PC hay Laptop, Nhu cầu")]
//        [Column("Loaithietbi_Nhucau")] // Ánh xạ với tên cột trong database
//        public string Loaithietbi_Nhucau { get; set; }

//        [StringLength(100)]
//        [Display(Name = "Camera Laptop")]
//        public string CameraLaptop { get; set; }

//        [StringLength(100)]
//        [Display(Name = "Socket PC")]
//        public string SocketPC { get; set; }



//        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
//        [Display(Name = "Ngày Thêm")]
//        public DateTime? NgayThem { get; set; }

//        [Display(Name = "Ngày Sửa")]
//        public DateTime? NgaySua { get; set; }



//        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
//        public virtual ICollection<Chitietdonhang> Chitietdonhang { get; set; }

//        public virtual Hangsanxuat Hangsanxuat { get; set; }

//        public virtual Hedieuhanh Hedieuhanh { get; set; }
//    }
//}