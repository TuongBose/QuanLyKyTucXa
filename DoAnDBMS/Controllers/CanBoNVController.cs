using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnDBMS.Controllers
{
    public class CanBoNVController : Controller
    {
        //
        // GET: /CanBoNV/
        Models.QLKTXDataContext db = new Models.QLKTXDataContext();

        public ActionResult CanBoNV()
        {
            return View(db.VIEW_NHANVIENs);
        }
	}
}