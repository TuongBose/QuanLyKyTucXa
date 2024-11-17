using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnDBMS.Controllers
{
    public class NguoiDungNhanVienController : Controller
    {
        // GET: NguoiDungNhanVien
        Models.QLKTXDataContext db = new Models.QLKTXDataContext(); 

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap()
        {
            return View();
        }
    }
}