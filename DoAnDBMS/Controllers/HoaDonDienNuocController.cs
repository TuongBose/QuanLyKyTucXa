using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnDBMS.Controllers
{
    public class HoaDonDienNuocController : Controller
    {
        Models.QLKTXDataContext db = new Models.QLKTXDataContext();

        public ActionResult HoaDonDienNuoc()
        {
            DoAnDBMS.Models.CANBO CB = Session["Account"] as DoAnDBMS.Models.CANBO;

            if (CB != null)
            {
                return View("HoaDonDienNuocCanBo", db.VIEW_HoaDonDienNuocs);
            }
            else
            {
                return View("HoaDonDienNuocNhanVien", db.VIEW_HoaDonDienNuocs);
            }
        }
    }
}
