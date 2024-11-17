using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnDBMS.Controllers
{
    public class HoaDonPhongController : Controller
    {
        // GET: HoaDonPhong
        Models.QLKTXDataContext db = new Models.QLKTXDataContext();

        public ActionResult HoaDonPhong()
        {
            return View(db.VIEW_HOADONPHONGs);
        }
    }
}