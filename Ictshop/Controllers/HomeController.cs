using Ictshop.Models;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;

namespace Ictshop.Controllers
{
    public class HomeController : Controller
    {
        private Qlbanhang db = new Qlbanhang(); // Giả sử bạn sử dụng Entity Framework để kết nối database

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult SlidePartial()
        {
            return PartialView();
        }

        public ActionResult Search(string query, int? Mahang, int? Mahdh, decimal? MinPrice, decimal? MaxPrice, int? Thesim
            , string Ram, string Dungluong, string SortOrder, string NhomSanPham, string PhanKhucGia, int? page)
            //,string GPUVGA, string CPU, string battery_capacity, string screen_size, string Chip)
        {
            var searchResults = db.Sanphams.AsQueryable();


            List<string> filterDescriptions = new List<string>();

            // Các bộ lọc vẫn giữ nguyên
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

            if (Thesim.HasValue)
            {
                searchResults = searchResults.Where(s => s.Thesim != null && s.Thesim == Thesim);
                filterDescriptions.Add($"Thẻ SIM: {Thesim} SIM");
            }

            // **Lấy dữ liệu ra khỏi database trước khi gọi phương thức C#**
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

                filterDescriptions.Add($"Bộ nhớ trong / Dung lượng ổ cứng: {Dungluong}");
            }

            // **Lọc theo loại thiết bị**
            if (!string.IsNullOrEmpty(NhomSanPham))
            {
                if (NhomSanPham.Equals("PhuKien", StringComparison.OrdinalIgnoreCase) ||
                    NhomSanPham.Equals("SetupPC", StringComparison.OrdinalIgnoreCase))
                {
                    allProducts = allProducts.Where(s => s.NhomSanPham.Equals(NhomSanPham)).ToList();
                }
                else
                {
                    allProducts = allProducts
    .Where(s => (s.LoaiSanPham ?? "").Trim().ToLower().Contains(NhomSanPham.ToLower()))
    .ToList();
                }

                filterDescriptions.Add($"Nhóm / Loại: {NhomSanPham}");
            }


            // **Lọc theo phân khúc giá**
            switch (PhanKhucGia)
            {
                case "re":
                    allProducts = allProducts.Where(s => s.Giatien < 3000000).ToList();
                    filterDescriptions.Add("Phân khúc: Giá rẻ (Dưới 3 triệu)");
                    break;
                case "phothong":
                    allProducts = allProducts.Where(s => s.Giatien >= 3000000 && s.Giatien < 7000000).ToList();
                    filterDescriptions.Add("Phân khúc: Phổ thông (3 - 7 triệu)");
                    break;
                case "trungbinh":
                    allProducts = allProducts.Where(s => s.Giatien >= 7000000 && s.Giatien < 15000000).ToList();
                    filterDescriptions.Add("Phân khúc: Trung bình (7 - 15 triệu)");
                    break;
                case "caocap":
                    allProducts = allProducts.Where(s => s.Giatien >= 15000000 && s.Giatien < 25000000).ToList();
                    filterDescriptions.Add("Phân khúc: Cao cấp (15 - 25 triệu)");
                    break;
                case "sieu cao cap":
                    allProducts = allProducts.Where(s => s.Giatien >= 25000000).ToList();
                    filterDescriptions.Add("Phân khúc: Siêu cao cấp (Trên 25 triệu)");
                    break;
            }

            // Sắp xếp theo tùy chọn SortOrder
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

            // Lưu thông tin bộ lọc vào ViewBag để sử dụng khi phân trang
            ViewBag.SearchParams = new RouteValueDictionary
            {
                { "query", query },
                { "Mahang", Mahang },
                { "Mahdh", Mahdh },
                { "MinPrice", MinPrice },
                { "MaxPrice", MaxPrice },
                { "Thesim", Thesim },
                { "Ram", Ram },
                { "Dungluong", Dungluong },
                { "SortOrder", SortOrder },
                { "NhomSanPham", NhomSanPham },
                { "PhanKhucGia", PhanKhucGia }
            };

            // Ghi nhớ bộ lọc đã sử dụng vào TempData
            TempData["SearchQuery"] = filterDescriptions.Any() ? string.Join(" | ", filterDescriptions) : "Không có bộ lọc nào được áp dụng";

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            ViewBag.Title = "Tất cả sản phẩm";
            ViewBag.ControllerName = "Home"; // Quan trọng để phân trang không bị lỗi

            return View("SearchResults", allProducts.ToPagedList(pageNumber, pageSize));
        }
    }
}


