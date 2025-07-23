namespace Ictshop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sanpham")]
    public partial class Sanpham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sanpham()
        {
            Chitietdonhang = new HashSet<Chitietdonhang>();
        }

        [Key]
        public int Masp { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên sản phẩm")]

        public string Tensp { get; set; }
        [Display(Name = "Giá tiền")]

        public decimal? Giatien { get; set; }
        [Display(Name = "Số lượng")]

        public int? Soluong { get; set; }
        [Display(Name = "Mô tả")]

        [Column(TypeName = "ntext")]
        public string Mota { get; set; }
        [Display(Name = "Thẻ sim")]

        public int? Thesim { get; set; }


        [StringLength(10)]
        [Display(Name = "Dung lượng")]
        public string Dungluong { get; set; }

        [StringLength(10)]
        [Display(Name = "RAM")]
        public string RAM { get; set; }



        [Display(Name = "Là sản phẩm mới")]

        public bool? Sanphammoi { get; set; }

        [Display(Name = "Ảnh sản phẩm")]

        [StringLength(50)]
        public string Anhbia { get; set; }
        [Display(Name = "Hãng sản xuất")]

        public int? Mahang { get; set; }
        [Display(Name = "Hệ điều hành")]

        public int? Mahdh { get; set; }

        







        [StringLength(200)]
        [Display(Name = "Màn hình")]
        public string Manhinh { get; set; }

        [StringLength(300)]
        [Display(Name = "Camera")]
        public string Camera { get; set; }

        [StringLength(10)]
        [Display(Name = "Pin")]
        public string Pin { get; set; }

        [StringLength(100)]
        [Display(Name = "Chip CPU")]
        public string ChipCPU { get; set; }

        [StringLength(300)]
        [Display(Name = "Kết nối")]
        public string Ketnoi { get; set; }

        [StringLength(200)]
        [Display(Name = "Chất liệu")]
        public string Chatlieu { get; set; }

        [StringLength(150)]
        [Display(Name = "Trọng lượng")]
        public string Trongluong { get; set; }

        [StringLength(50)]
        [Display(Name = "Kích thước")]
        public string Kichthuoc { get; set; }

        [Display(Name = "Thông tin khác")]
        [Column(TypeName = "ntext")]
        public string Thongtinkhac { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên hãng - Nhập tên hãng dạng chuỗi")]
        public string Tenhangsx { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên hệ điều hành - Nhập tên HĐH dạng chuỗi")]
        public string Tenhedieuhanh { get; set; }

        [StringLength(100)]
        [Display(Name = "Nhóm sản phẩm")]
        [Column("NhomSanPham")] 
        public string NhomSanPham { get; set; }

        [StringLength(100)]
        [Display(Name = "Loại sản phẩm")]
        [Column("LoaiSanPham")]
        public string LoaiSanPham { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Ngày Thêm")]
        public DateTime? NgayThem { get; set; }

        [Display(Name = "Ngày Sửa")]
        public DateTime? NgaySua { get; set; }

        [StringLength(50)]
        [Display(Name = "Mainboard")]
        public string Mainboard { get; set; }

        [StringLength(50)]
        [Display(Name = "Nguồn")]
        public string Nguon { get; set; }

        [StringLength(50)]
        [Display(Name = "Tản nhiệt")]
        public string Tannhiet { get; set; }

        [StringLength(150)]
        [Display(Name = "GPU")]
        public string GPU { get; set; }

        [StringLength(50)]
        [Display(Name = "Loại Case")]
        public string LoaiCase { get; set; }

        [StringLength(50)]
        [Display(Name = "Tương Thích")]
        public string Tuongthich { get; set; }

        [StringLength(50)]
        [Display(Name = "Loại Kết Nối")]
        public string LoaiKetNoi { get; set; }

        [StringLength(50)]
        [Display(Name = "Công Suất")]
        public string CongSuat { get; set; }

        [StringLength(50)]
        [Display(Name = "Kích Cỡ")]
        public string KichCo { get; set; }

        [StringLength(50)]
        [Display(Name = "Màu Sắc")]
        public string MauSac { get; set; }





        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chitietdonhang> Chitietdonhang { get; set; }

        public virtual Hangsanxuat Hangsanxuat { get; set; }

        public virtual Hedieuhanh Hedieuhanh { get; set; }
    }
}
