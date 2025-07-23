using Ictshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.UI;
using System.Web.Routing;

namespace Ictshop.Controllers
{
    public class LaptopController : Controller
    {
        // GET: Laptop
        private readonly Qlbanhang db = new Qlbanhang(); // Đổi tên context nếu cần

        // Hiển thị danh sách laptop
        public ActionResult Index(int? page)
        {
            int pageSize = 12;  // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang mặc định là 1


            var sanphamList = db.Sanphams
             .Where(s => s.LoaiSanPham.Trim().Contains("Laptop")) // Lọc sản phẩm có "Laptop" trong cột PCLaptop_Nhucau
             .OrderBy(s => s.Masp)
             .ToPagedList(pageNumber, pageSize); // Phân trang

            return View(sanphamList);
        }

        // Hiển thị chi tiết sản phẩm
        public ActionResult Details(int id)
        {
            return RedirectToAction("xemchitiet", "Sanpham", new { Masp = id });
        }

        //Tìm kiếm sản phẩm ở Laptop
        public ActionResult Search(string query, int? Mahang, int? Mahdh, decimal? MinPrice, decimal? MaxPrice, int? Thesim,
            string Ram, string Dungluong, string SortOrder, int? page, string PhanKhucGia)
        //,string GPUVGA, string CPU, string battery_capacity, string screen_size)
        {
            var searchResults = db.Sanphams
    .Where(s => s.LoaiSanPham.Trim().Contains("Laptop"));

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
            if (Mahdh.HasValue)
            {
                searchResults = searchResults.Where(s => s.Mahdh == Mahdh);
                var heDieuHanh = db.Hedieuhanhs.FirstOrDefault(h => h.Mahdh == Mahdh)?.Tenhdh ?? "Hệ điều hành không xác định";
                filterDescriptions.Add($"Hệ điều hành: {heDieuHanh}");

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

            if (!string.IsNullOrEmpty(Ram))
            {
                allProducts = allProducts.Where(s =>
                SearchHelperController.GetMainMemoryValue(s.RAM).Equals(Ram, StringComparison.OrdinalIgnoreCase)
            ).ToList();

                filterDescriptions.Add($"RAM: {Ram}");
            }

            if (!string.IsNullOrEmpty(Dungluong))
            {
                allProducts = allProducts.Where(s =>
                {
                    var storageDict = SearchHelperController.GetFirstSSDAndHDD(s.Dungluong);
                    return storageDict.ContainsValue(Dungluong);
                }).ToList();

                filterDescriptions.Add($"Dung lượng ổ cứng: {Dungluong}");
            }

            switch (PhanKhucGia)
            {
                case "re":
                    allProducts = allProducts.Where(s => s.Giatien < 8000000).ToList();
                    filterDescriptions.Add("Phân khúc: Giá rẻ (Dưới 8 triệu)");
                    break;
                case "phothong":
                    allProducts = allProducts.Where(s => s.Giatien >= 8000000 && s.Giatien < 15000000).ToList();
                    filterDescriptions.Add("Phân khúc: Phổ thông (8 - 15 triệu)");
                    break;
                case "trungbinh":
                    allProducts = allProducts.Where(s => s.Giatien >= 15000000 && s.Giatien < 30000000).ToList();
                    filterDescriptions.Add("Phân khúc: Trung bình (15 - 30 triệu)");
                    break;
                case "caocap":
                    allProducts = allProducts.Where(s => s.Giatien >= 30000000 && s.Giatien < 50000000).ToList();
                    filterDescriptions.Add("Phân khúc: Cao cấp (15 - 25 triệu)");
                    break;
                case "sieu cao cap":
                    allProducts = allProducts.Where(s => s.Giatien >= 50000000).ToList();
                    filterDescriptions.Add("Phân khúc: Siêu cao cấp (Trên 50 triệu)");
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
                { "Mahdh", Mahdh },
                { "MinPrice", MinPrice },
                { "MaxPrice", MaxPrice },
                { "Ram", Ram },
                { "Dungluong", Dungluong },
                { "SortOrder", SortOrder },
                { "PhanKhucGia", PhanKhucGia }
            };

            // Ghi nhớ bộ lọc đã sử dụng vào TempData
            TempData["SearchQuery"] = filterDescriptions.Any() ? string.Join(" | ", filterDescriptions) : "Không có bộ lọc nào được áp dụng";


            int pageSize = 12;
            int pageNumber = (page ?? 1);

            ViewBag.Title = "Laptop";
            ViewBag.ControllerName = "Laptop";


            return View("~/Views/Shared/SearchResults.cshtml", allProducts.ToPagedList(pageNumber, pageSize));

        }
    }
}