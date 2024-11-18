using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnDBMS.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: TrangChu
        public ActionResult TrangChuNhanVien()
        {
            return View();
        }

        public ActionResult TrangChuCanBo()
        {
            return View();
        }
    }
}