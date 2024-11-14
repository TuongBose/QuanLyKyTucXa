using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnDBMS.Models;

namespace DoAnDBMS.Controllers
{
    public class HoTroController : Controller
    {
        // GET: HoTro
        Models.QLKTXDataContext db = new Models.QLKTXDataContext();

        public ActionResult HoTro()
        {
            return View(db.VIEW_HOTROs);
        }
    }
}