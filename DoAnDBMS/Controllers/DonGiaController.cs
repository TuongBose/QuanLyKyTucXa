using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnDBMS.Controllers
{
    public class DonGiaController : Controller
    {
        // GET: DonGia
        Models.QLKTXDataContext db = new Models.QLKTXDataContext();

        public ActionResult DonGia()
        {
            return View(db.VIEW_DONGIAs);
        }

    }
}