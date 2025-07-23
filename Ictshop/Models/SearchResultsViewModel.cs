using Ictshop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ictshop.ViewModels
{
    public class SearchResultsViewModel
    {
        public IPagedList<Sanpham> Sanphams { get; set; }
        //public IPagedList<SanphamMayTinh> SanphamsMayTinh { get; set; }
        public string FilterDescription { get; set; }

    }
}