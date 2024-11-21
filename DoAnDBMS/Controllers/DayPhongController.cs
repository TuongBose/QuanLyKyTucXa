using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnDBMS.Controllers
{
    public class DayPhongController : Controller
    {
        // GET: DayPhong
        Models.QLKTXDataContext db = new Models.QLKTXDataContext();

        public ActionResult DayPhong()
        {
            return View(db.VIEW_DayPhongs);
        }
    }
}