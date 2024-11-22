using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnDBMS.Controllers
{
    public class CanBoController : Controller
    {
        // GET: CanBo
        Models.QLKTXDataContext db = new Models.QLKTXDataContext();

        public ActionResult CanBo()
        {
            return View(db.VIEW_CANBOs);
        }

        public ActionResult NhanVien()
        {
            return View(db.VIEW_NHANVIENs);
        }

        public ActionResult DayPhong()
        {
            return View(db.VIEW_DayPhongs);
        }

        public ActionResult Phong()
        {
            return View(db.VIEW_Phongs);
        }

        public ActionResult DonGia()
        {
            return View(db.VIEW_DONGIAs);
        }

        public ActionResult HoTro()
        {
            return View(db.VIEW_HOTROs);
        }

        public ActionResult HoaDonDienNuoc()
        {
            return View(db.VIEW_HoaDonDienNuocs);
        }

        public ActionResult HoaDonPhong()
        {
            return View(db.VIEW_HOADONPHONGs);
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection Data)
        {
            var id = Data["ID"];
            var pass = Data["Pass"];

            bool hasError = false;

            if (String.IsNullOrEmpty(id))
            {
                ViewData["LoiID"] = "ID Cán bộ không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(pass))
            {
                ViewData["LoiPass"] = "Mật khẩu không được bỏ trống";
                hasError = true;
            }

            if (hasError)
                return View();
            else
            {
                Models.CANBO hasAccount = db.CANBOs.FirstOrDefault(x => x.ID_CANBO == int.Parse(id) && x.MATKHAU == pass);
                if (hasAccount != null)
                {
                    Session["Account"] = hasAccount;
                    return RedirectToAction("TrangChuCanBo", "TrangChu");
                }
                else
                    ViewBag.TB = "Sai ID hoặc sai mật khẩu, vui lòng nhập lại";

                return View();
            }
        }

        public ActionResult DangXuat()
        {
            Session["Account"] = null;
            return RedirectToAction("DangNhap", "CanBo");
        }
    }
}