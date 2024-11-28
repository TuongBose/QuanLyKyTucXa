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

        public ActionResult ThemCanBo(FormCollection Data, Models.CANBO NewCanBo)
        {
            var macb = Data["MACB"];
            var taikhoan = Data["TaiKhoan"];
            var tencb = Data["TenCB"];
            var sdt = Data["SDT"];
            var email = Data["Email"];

            bool hasError=false;

            if (String.IsNullOrEmpty(macb))
            {
                TempData["Message"] = "Mã cán bộ không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(taikhoan))
            {
                TempData["Message"] = "Tài khoản cán bộ không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(tencb))
            {
                TempData["Message"] = "Tên cán bộ không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(sdt))
            {
                TempData["Message"] = "Số điện thoại không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(email))
            {
                TempData["Message"] = "Email không được bỏ trống";
                hasError = true;
            }

            if(hasError)
            return View();
            else
            {
                NewCanBo.MACB = macb;
                NewCanBo.TAIKHOAN = taikhoan;
                NewCanBo.TENCB = tencb;
                NewCanBo.SDT = sdt;
                NewCanBo.EMAIL = email;

                Models.CANBO hasCanbo=db.CANBOs.FirstOrDefault(x=>x.MACB== macb);
                if (hasCanbo == null)
                {
                    db.CANBOs.InsertOnSubmit(NewCanBo);
                    db.SubmitChanges();
                    return RedirectToAction("CanBo", "CanBo");
                }
                else
                {
                    TempData["Message"] = "Mã cán bộ đã tồn tại";
                    return View();
                }
            }
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