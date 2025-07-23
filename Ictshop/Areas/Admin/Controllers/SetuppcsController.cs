using Ictshop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ictshop.Areas.Admin.Controllers
{
    public class SetuppcsController : Controller
    {
        // GET: Admin/Setuppcs
        Qlbanhang db = new Qlbanhang();

        public ActionResult Index(int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int( số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Masp mới có thể phân trang.
            var sp = db.Sanphams
                .Where(x => x.NhomSanPham.Contains("SetupPC"))
                .OrderBy(x => x.Masp);

            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
            int pageSize = 5;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(sp.ToPagedList(pageNumber, pageSize));

        }

        // Xem chi tiết người dùng GET: Admin/Sanphammaytins/Details/5 
        public ActionResult Details(int id)
        {
            var dt = db.Sanphams.Find(id);
            return View(dt);
        }

        // Tạo sản phẩm mới phương thức GET: Admin/Sanphammaytins/Create
        public ActionResult Create()
        {
            ViewBag.Mahang = new SelectList(db.Hangsanxuats, "Mahang", "Tenhang");
            ViewBag.Mahdh = new SelectList(db.Hedieuhanhs, "Mahdh", "Tenhdh");
            return View();
        }

        // POST: Admin/Sanphams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Masp,Tensp,Giatien,Soluong,Mota,RAM,Dungluong,Sanphammoi,Mahang,Mahdh,ChipCPU,GPU,KetNoi,Anhbia,Pin,Manhinh,Camera,Trongluong,Kichthuoc,Mainboard,Nguon,Tannhiet,LoaiCase,Thongtinkhac,Tenhangsx,Tenhedieuhanh,NhomSanPham,LoaiSanPham")] Sanpham sanpham)
        public ActionResult Create([Bind(Include = "Masp,Tensp,Giatien,Soluong,Mota,Anhbia,KichCo,Chatlieu,Trongluong,Kichthuoc,MauSac,Tenhangsx,NhomSanPham,LoaiSanPham")] Sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                db.Sanphams.Add(sanpham);
                db.SaveChanges();
                sanpham.NgayThem = DateTime.Now;
                return RedirectToAction("Index");
            }

            ViewBag.Mahang = new SelectList(db.Hangsanxuats, "Mahang", "Tenhang", sanpham.Mahang);
            ViewBag.Mahdh = new SelectList(db.Hedieuhanhs, "Mahdh", "Tenhdh", sanpham.Mahdh);
            return View(sanpham);
        }


        // GET: Admin/Sanphammaytins/Edit/5
        public ActionResult Edit(int id)
        {
            // Hiển thị dropdownlist
            var dt = db.Sanphams.Find(id);
            var hangselected = new SelectList(db.Hangsanxuats, "Mahang", "Tenhang", dt.Mahang);
            ViewBag.Mahang = hangselected;
            var hdhselected = new SelectList(db.Hedieuhanhs, "Mahdh", "Tenhdh", dt.Mahdh);
            ViewBag.Mahdh = hdhselected;

            return View(dt);
        }

        // POST: Admin/Sanphammaytins/Edit/5
        [HttpPost]
        public ActionResult Edit(Sanpham sanpham)
        {
            try
            {
                // Tìm sản phẩm theo mã
                var oldItem = db.Sanphams.Find(sanpham.Masp);
                if (oldItem != null)
                {
                    oldItem.Tensp = sanpham.Tensp;
                    oldItem.Giatien = sanpham.Giatien;
                    oldItem.Soluong = sanpham.Soluong;
                    oldItem.Mota = sanpham.Mota;
                    oldItem.Anhbia = sanpham.Anhbia;
                    oldItem.Sanphammoi = sanpham.Sanphammoi;
                    oldItem.KichCo = sanpham.KichCo;
                    oldItem.Chatlieu = sanpham.Chatlieu;
                    oldItem.Trongluong = sanpham.Trongluong;
                    oldItem.Kichthuoc = sanpham.Kichthuoc;
                    oldItem.MauSac = sanpham.MauSac;
                    oldItem.Tenhangsx = sanpham.Tenhangsx;
                    oldItem.NhomSanPham = sanpham.NhomSanPham;
                    oldItem.LoaiSanPham = sanpham.LoaiSanPham;

                    // Cập nhật thời gian sửa
                    oldItem.NgaySua = DateTime.Now;

                    // Lưu lại
                    db.SaveChanges();
                }

                // Chuyển hướng về Index
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // Xoá sản phẩm phương thức GET: Admin/Sanphammaytins/Delete/5
        public ActionResult Delete(int id)
        {
            var dt = db.Sanphams.Find(id);
            return View(dt);
        }

        // Xoá sản phẩm phương thức POST: Admin/Sanphammaytins/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //Lấy được thông tin sản phẩm theo ID(mã sản phẩm)
                var dt = db.Sanphams.Find(id);
                // Xoá
                db.Sanphams.Remove(dt);
                // Lưu lại
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}