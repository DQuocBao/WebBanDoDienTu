using Ictshop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ictshop.Controllers
{
    public class PhuKienController : Controller
    {
        // GET: PhuKien

        private readonly Qlbanhang db = new Qlbanhang(); // Đổi tên context nếu cần

        // Hiển thị danh sách laptop
        public ActionResult Index(int? page)
        {
            int pageSize = 12;  // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang mặc định là 1


            var sanphamList = db.Sanphams
             .Where(s => s.NhomSanPham.Trim().Contains("PhuKien")) // Lọc sản phẩm có "Laptop" trong cột PCLaptop_Nhucau
             .OrderBy(s => s.Masp)
             .ToPagedList(pageNumber, pageSize); // Phân trang

            return View(sanphamList);
        }

        // Hiển thị chi tiết sản phẩm
        public ActionResult Details(int id)
        {
            return RedirectToAction("xemchitiet", "Sanpham", new { Masp = id });
        }

        public ActionResult Search(string query, int? Mahang, int? Mahdh, decimal? MinPrice, decimal? MaxPrice, int? Thesim,
            string Ram, string Dungluong, string SortOrder, int? page, string PhanKhucGia)
        //,string GPUVGA, string CPU, string battery_capacity, string screen_size)
        {
            var searchResults = db.Sanphams
    .Where(s => s.NhomSanPham.Trim().Equals("PhuKien", StringComparison.OrdinalIgnoreCase));


            List<string> filterDescriptions = new List<string>();

            // Các bộ lọc vẫn như cũ nhưng áp dụng trên danh sách điện thoại
            if (!string.IsNullOrEmpty(query))
            {
                searchResults = searchResults.Where(s => s.Tensp.Contains(query));
                filterDescriptions.Add($"Từ khóa: '{query}'");
            }
            if (Mahang.HasValue)
            {
                searchResults = searchResults.Where(s => s.Mahang == Mahang);
                var hangSX = db.Hangsanxuats.FirstOrDefault(h => h.Mahang == Mahang)?.Tenhang ?? "Hãng không xác định";
                filterDescriptions.Add($"Hãng: {hangSX}");
            }           
            if (MinPrice.HasValue)
            {
                searchResults = searchResults.Where(s => s.Giatien >= MinPrice);
                filterDescriptions.Add($"Giá từ {MinPrice.Value:N0} VND");
            }
            if (MaxPrice.HasValue)
            {
                searchResults = searchResults.Where(s => s.Giatien <= MaxPrice);
                filterDescriptions.Add($"Giá đến {MaxPrice.Value:N0} VND");
            }

            var allProducts = searchResults.ToList();
           
            switch (PhanKhucGia)
            {
                case "re":
                    allProducts = allProducts.Where(s => s.Giatien < 200000).ToList();
                    filterDescriptions.Add("Phân khúc: Giá rẻ (Dưới 200 nghìn)");
                    break;
                case "phothong":
                    allProducts = allProducts.Where(s => s.Giatien >= 200000 && s.Giatien < 500000).ToList();
                    filterDescriptions.Add("Phân khúc: Phổ thông (200 - 500 nghìn)");
                    break;
                case "trungbinh":
                    allProducts = allProducts.Where(s => s.Giatien >= 500000 && s.Giatien < 1500000).ToList();
                    filterDescriptions.Add("Phân khúc: Trung bình (500 nghìn - 1.5 triệu)");
                    break;
                case "caocap":
                    allProducts = allProducts.Where(s => s.Giatien >= 1500000 && s.Giatien < 5000000).ToList();
                    filterDescriptions.Add("Phân khúc: Cao cấp (1.5 triệu - 5 triệu)");
                    break;
                case "sieu cao cap":
                    allProducts = allProducts.Where(s => s.Giatien >= 5000000).ToList();
                    filterDescriptions.Add("Phân khúc: Siêu cao cấp (Trên 5 triệu)");
                    break;
            }

            // Sắp xếp kết quả
            switch (SortOrder)
            {
                case "price-asc":
                    allProducts = allProducts.OrderBy(s => s.Giatien ?? 0).ToList();
                    filterDescriptions.Add("Sắp xếp: Giá tăng dần");
                    break;
                case "price-desc":
                    allProducts = allProducts.OrderByDescending(s => s.Giatien ?? 0).ToList();
                    filterDescriptions.Add("Sắp xếp: Giá giảm dần");
                    break;
                case "name-asc":
                    allProducts = allProducts.OrderBy(s => s.Tensp).ToList();
                    filterDescriptions.Add("Sắp xếp: Tên A → Z");
                    break;
                case "name-desc":
                    allProducts = allProducts.OrderByDescending(s => s.Tensp).ToList();
                    filterDescriptions.Add("Sắp xếp: Tên Z → A");
                    break;
                case "newest":
                    allProducts = allProducts.OrderByDescending(s => s.NgayThem).ToList();
                    filterDescriptions.Add("Sắp xếp: Mới nhất");
                    break;
                case "oldest":
                    allProducts = allProducts.OrderBy(s => s.NgayThem).ToList();
                    filterDescriptions.Add("Sắp xếp: Cũ nhất");
                    break;
            }

            ViewBag.SearchParams = new RouteValueDictionary
            {
                { "query", query },
                { "Mahang", Mahang },
                { "MinPrice", MinPrice },
                { "MaxPrice", MaxPrice },
                { "SortOrder", SortOrder },
                { "PhanKhucGia", PhanKhucGia }
            };

            // Ghi nhớ bộ lọc đã sử dụng vào TempData
            TempData["SearchQuery"] = filterDescriptions.Any() ? string.Join(" | ", filterDescriptions) : "Không có bộ lọc nào được áp dụng";


            int pageSize = 12;
            int pageNumber = (page ?? 1);

            ViewBag.Title = "PhuKien";
            ViewBag.ControllerName = "PhuKien";


            return View("~/Views/Shared/SearchResults.cshtml", allProducts.ToPagedList(pageNumber, pageSize));

        }
    }
}