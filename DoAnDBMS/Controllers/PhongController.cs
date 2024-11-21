using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnDBMS.Controllers
{
    public class PhongController : Controller
    {
        // GET: Phong
        Models.QLKTXDataContext db = new Models.QLKTXDataContext();
        public ActionResult Phong()
        {
            return View(db.VIEW_Phongs);
        }
    }
}