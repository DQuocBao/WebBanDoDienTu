using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ictshop.Models;

namespace Ictshop.Controllers
{
    public class SanphamController : Controller
    {
        Qlbanhang db = new Qlbanhang();

        // GET: Sanpham
        public ActionResult dtiphonepartial()
        {
            var ip = db.Sanphams.Where(n=>n.Mahang==2).Take(4).ToList();
            //var ip = db.Sanphams.Where(n => n.Mahang == "IP").Take(4).ToList(); 

            return PartialView(ip);
        }
        public ActionResult dtsamsungpartial()
        {
            var ss = db.Sanphams.Where(n => n.Mahang == 1).Take(4).ToList();
            //var ip = db.Sanphams.Where(n => n.Mahang == "SS").Take(4).ToList();  //lấy sản phẩm theo mã sp dạng chuỗi
            return PartialView(ss);
        }
        public ActionResult dtxiaomipartial()
        {
            var mi = db.Sanphams.Where(n => n.Mahang == 3).Take(4).ToList();
            //var ip = db.Sanphams.Where(n => n.Mahang == "XM").Take(4).ToList(); // trong đó 4 là số sản phẩm cần lây
            return PartialView(mi);
        }
        //public ActionResult dttheohang()
        //{
        //    var mi = db.Sanphams.Where(n => n.Mahang == 5).Take(4).ToList();
        //    return PartialView(mi);
        //}
        public ActionResult xemchitiet(int Masp)
        {
            var sanpham = db.Sanphams.FirstOrDefault(sp => sp.Masp == Masp);
            if (sanpham == null)
            {
                return HttpNotFound();
            }

            return View("xemchitiet", sanpham);
        }



        //lưu sản phẩm của user, chưa làm và sẽ làm nếu có time
        //public ActionResult SavedProducts(int id)   
        //{
        //    var savedProducts = db.Sanphams.Where(s => s.NguoiDungId == id).ToList();
        //    return View(savedProducts);
        //}

    }

}