using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnDBMS.Controllers
{
    public class NhanVienController : Controller
    {
        // GET: NhanVien
        Models.QLKTXDataContext db = new Models.QLKTXDataContext(); 

        public ActionResult NhanVien()
        {
            return View(db.VIEW_NHANVIENs);
        }
    }
}