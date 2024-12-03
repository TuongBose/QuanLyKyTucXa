using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnDBMS.Models;

namespace DoAnDBMS.Controllers
{
    public class NhanVienController : Controller
    {
        // GET: NhanVien
        Models.QLKTXDataContext db = new Models.QLKTXDataContext();

        public ActionResult HoaDonDienNuoc()
        {
            return View(db.VIEW_HoaDonDienNuocs);
        }

        public ActionResult HoaDonPhong()
        {
            var list_viewhoadondiennuoc = db.VIEW_HoaDonDienNuocs.ToList();
            var list_hoadonphong = list_viewhoadondiennuoc.Select(item => new VIEW_HOADONPHONG
            {
                MADAYPHONG = item.DayPhong,
                MAPHONG = item.MaPhong,
                NAM = item.NAM,
                KY = item.THANG,
                DONGIA = item.DonGiaPhong,
                THANHTIEN_HOADON_DIENNUOC = ((item.ChiSoCuoiDien - item.ChiSoDauDien) * item.DonGiaDien) +
                ((item.ChiSoCuoiNuoc - item.ChiSoDauNuoc) * item.DonGiaNuoc),
                THANHTIEN_DONGIAPHONG_HDDN = (((item.ChiSoCuoiDien - item.ChiSoDauDien) * item.DonGiaDien) +
                ((item.ChiSoCuoiNuoc - item.ChiSoDauNuoc) * item.DonGiaNuoc)) + item.DonGiaPhong,
                TRANGTHAI = item.TRANGTHAI
            }).ToList();
            return View(list_hoadonphong);
        }

        public ActionResult DayPhong()
        {
            return View(db.VIEW_DayPhongs);
        }

        public ActionResult Phong()
        {
            return View(db.VIEW_Phongs);
        }

        public ActionResult NhanVien()
        {
            return View(db.VIEW_NHANVIENs);
        }

        public ActionResult DonGia()
        {
            return View(db.VIEW_DONGIAs);
        }

        public ActionResult HoTro()
        {
            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "MaPhong", "MaPhong");
            return View(db.VIEW_HOTROs);
        }

        [HttpPost]
        public ActionResult ThemHoTro(FormCollection Data)
        {
            var maphong = Data["MaPhong"];
            var noidung = Data["NoiDung"];
            var ngaygui = Data["NgayGui"];

            DateTime Chuanhoa_ngaygui;
            if (!DateTime.TryParse(ngaygui, out Chuanhoa_ngaygui))
            {
                TempData["Message"] = "Ngày gửi không hợp lệ";
                return RedirectToAction("HoTro", "NhanVien");
            }

            bool hasError = false;

            if (String.IsNullOrEmpty(maphong))
            {
                TempData["Message"] = "Mã phòng không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(noidung))
            {
                TempData["Message"] = "Nội dung không được bỏ trống";
                hasError = true;
            }

            if (hasError)
                return RedirectToAction("HoTro", "NhanVien");
            else
            {
                Models.PHONG idphong = db.PHONGs.FirstOrDefault(id => id.MAPHONG == maphong);
                if (idphong != null)
                {
                    Models.HOTRO NewHoTro = new Models.HOTRO
                    {
                        ID_PHONG = idphong.ID_PHONG,
                        NOIDUNG = noidung,
                        NGAYGUI = Chuanhoa_ngaygui,
                        TRANGTHAI = false,
                        DAXOA = false
                    };

                    db.HOTROs.InsertOnSubmit(NewHoTro);
                    db.SubmitChanges();
                    return RedirectToAction("HoTro", "NhanVien");
                }
                else
                {
                    TempData["Message"] = "Không tìm thấy mã phòng này";
                    return RedirectToAction("HoTro", "NhanVien");
                }
            }
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
                ViewData["LoiID"] = "ID Nhân viên không được bỏ trống";
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
                Models.NHANVIEN hasAccount = db.NHANVIENs.FirstOrDefault(x => x.ID_NHANVIEN == int.Parse(id) && x.MATKHAU == pass);
                if (hasAccount != null)
                {
                    Session["Account"] = hasAccount;
                    return RedirectToAction("TrangChuNhanVien", "TrangChu");
                }
                else
                    ViewBag.TB = "Sai ID hoặc sai mật khẩu, vui lòng nhập lại";

                return View();
            }
        }

        public ActionResult DangXuat()
        {
            Session["Account"] = null;
            return RedirectToAction("DangNhap", "NhanVien");
        }

        [HttpGet]
        public ActionResult ThemChiSoDienNuocCongTo()
        {
            ViewBag.ID_PHONG = new SelectList(db.PHONGs, "MaPhong", "MaPhong");
            return View();
        }

        [HttpPost]
        public ActionResult ThemChiSoDienNuocCongTo(FormCollection Data)
        {
            var maphong = Data["MaPhong"];
            var nuocthangtruoc = Data["NuocThangTruoc"];
            var nuocthangnay = Data["NuocThangNay"];
            var dienthangtruoc = Data["DienThangTruoc"];
            var dienthangnay = Data["DienThangNay"];
            var thangdien = Data["ThangDien"];
            var namdien = Data["NamDien"];
            var thangnuoc = Data["ThangNuoc"];
            var namnuoc = Data["NamNuoc"];

            bool hasError = false;

            if (String.IsNullOrEmpty(maphong))
            {
                TempData["Message"] = "Mã phòng không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(nuocthangnay))
            {
                TempData["Message"] = "Nước tháng này không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(nuocthangtruoc))
            {
                TempData["Message"] = "Nước tháng trước không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(dienthangnay))
            {
                TempData["Message"] = "Điện tháng này không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(dienthangtruoc))
            {
                TempData["Message"] = "Điện tháng trước không được bỏ trống";
                hasError = true;
            }

            if (hasError)
                return RedirectToAction("ThemChiSoDienNuocCongTo", "NhanVien");
            else
            {
                Models.PHONG hasPhong = db.PHONGs.FirstOrDefault(x => x.MAPHONG == maphong);
                if (hasPhong != null)
                {
                    Models.HOADON_DIENNUOC hasHDDN = db.HOADON_DIENNUOCs.FirstOrDefault(x => x.ID_PHONG == hasPhong.ID_PHONG && x.TRANGTHAI == false);
                    if (hasHDDN != null)
                    {
                        if (hasHDDN.THANG == int.Parse(thangdien) && hasHDDN.NAM == int.Parse(namdien)
                            && hasHDDN.THANG == int.Parse(thangnuoc) && hasHDDN.NAM == int.Parse(namnuoc)) 
                        {
                            TempData["Message"] = "Năm hoặc tháng hóa đơn của phòng này đã được lập";
                            return RedirectToAction("ThemChiSoDienNuocCongTo", "NhanVien");
                        }
                        else
                        {
                            Models.CONGTODIEN CTD = db.CONGTODIENs.FirstOrDefault(x => x.ID_PHONG == hasPhong.ID_PHONG);
                            if (CTD != null)
                            {
                                CTD.CHISODAU = int.Parse(dienthangtruoc);
                                CTD.CHISOCUOI = int.Parse(dienthangnay);
                                CTD.THANG = int.Parse(thangdien);
                                CTD.NAM = int.Parse(namdien);
                                CTD.TRANGTHAI = false;

                                db.SubmitChanges();
                            }

                            Models.CONGTONUOC CTN = db.CONGTONUOCs.FirstOrDefault(x => x.ID_PHONG == hasPhong.ID_PHONG);
                            if (CTN != null)
                            {
                                CTN.CHISODAU = int.Parse(nuocthangtruoc);
                                CTN.CHISOCUOI = int.Parse(nuocthangnay);
                                CTN.THANG = int.Parse(thangnuoc);
                                CTN.NAM = int.Parse(namnuoc);
                                CTN.TRANGTHAI = false;

                                db.SubmitChanges();
                            }

                            Models.DONGIA DG = db.DONGIAs.Where(x => x.TRANGTHAI == true).FirstOrDefault();
                            if (DG != null)
                            {
                                Models.HOADON_DIENNUOC NewHDDN = new Models.HOADON_DIENNUOC
                                {
                                    ID_PHONG = hasPhong.ID_PHONG,
                                    ID_DONGIA = DG.ID_DONGIA,
                                    ID_DIEN = CTD.ID_DIEN,
                                    ID_NUOC = CTN.ID_NUOC,
                                    THANG = int.Parse(DateTime.Now.Month.ToString()),
                                    NAM = int.Parse(DateTime.Now.Year.ToString()),
                                    TRANGTHAI = false
                                };
                                db.HOADON_DIENNUOCs.InsertOnSubmit(NewHDDN);
                                db.SubmitChanges();
                                TempData["Message"] = "Thêm thành công";
                                return RedirectToAction("HoaDonDienNuoc", "NhanVien");
                            }
                        }
                    }
                }

                return RedirectToAction("ThemChiSoDienNuocCongTo", "NhanVien");
            }
        }
    }
}