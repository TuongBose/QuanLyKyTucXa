using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnDBMS.Models;

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

        [HttpPost]
        public ActionResult ThemCanBo(FormCollection Data, Models.CANBO NewCanBo)
        {
            var macb = Data["MACB"];
            var taikhoan = Data["TaiKhoan"];
            var matkhau = Data["MatKhau"];
            var tencb = Data["TenCB"];
            var sdt = Data["SDT"];
            var email = Data["Email"];

            bool hasError = false;

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
            if (String.IsNullOrEmpty(matkhau))
            {
                TempData["Message"] = "Mật khẩu không được bỏ trống";
                hasError = true;
            }

            if (hasError)
                return View();
            else
            {
                NewCanBo.MACB = macb;
                NewCanBo.TAIKHOAN = taikhoan;
                NewCanBo.MATKHAU = matkhau;
                NewCanBo.TENCB = tencb;
                NewCanBo.SDT = sdt;
                NewCanBo.EMAIL = email;
                NewCanBo.DAXOA = false;

                Models.CANBO hasCanbo = db.CANBOs.FirstOrDefault(x => x.MACB == macb);
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

        [HttpGet]
        public ActionResult ChinhSuaCanBo(int id)
        {
            var canbo = db.CANBOs.FirstOrDefault(x => x.ID_CANBO == id);

            List<SelectListItem> sex = new List<SelectListItem>
            {
                new SelectListItem{ Text="Nam", Value="1" },
                new SelectListItem{ Text="Nữ", Value="0" }
            };
            ViewBag.GioiTinhList = sex;
            ViewBag.QuanTri = canbo.QUANTRI;

            return View(canbo);
        }

        [HttpPost]
        public ActionResult ChinhSuaCanBo(FormCollection Data)
        {
            var id = Data["ID"];
            var macb = Data["MACB"];
            var taikhoan = Data["TaiKhoan"];
            var matkhau = Data["MatKhau"];
            var tencb = Data["TenCB"];
            var gioitinh = Data["GioiTinh"];
            var cccd = Data["CCCD"];
            var diachi = Data["DiaChi"];
            var email = Data["Email"];
            var sdt = Data["SDT"];
            bool quantri;
            if (Data["QianTri"] == "true")
            {
                quantri = true;
            }
            else
            {
                quantri = false;
            }

            bool hasError = false;

            if (String.IsNullOrEmpty(macb))
            {
                TempData["Message"] = "Mã cán bộ không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(taikhoan))
            {
                TempData["Message"] = "Tài khoản không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                TempData["Message"] = "Mật khẩu không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(cccd))
            {
                TempData["Message"] = "CCCD cán bộ không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(diachi))
            {
                TempData["Message"] = "Địa chỉ không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(email))
            {
                TempData["Message"] = "Email không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(sdt))
            {
                TempData["Message"] = "Số điện thoại không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(tencb))
            {
                TempData["Message"] = "Tên cán bộ không được bỏ trống";
                hasError = true;
            }

            if (hasError)
                return View();
            else
            {
                Models.CANBO hasCanbo = db.CANBOs.FirstOrDefault(x => x.ID_CANBO == int.Parse(id));
                if (hasCanbo != null)
                {
                    hasCanbo.MACB = macb;
                    hasCanbo.TAIKHOAN = taikhoan;
                    hasCanbo.MATKHAU = matkhau;
                    hasCanbo.TENCB = tencb;
                    if (gioitinh == "1")
                        hasCanbo.GIOITINH = true;
                    if (gioitinh == "0")
                        hasCanbo.GIOITINH = false;
                    hasCanbo.CMND_CCCD = int.Parse(cccd);
                    hasCanbo.DIACHI = diachi;
                    hasCanbo.EMAIL = email;
                    hasCanbo.SDT = sdt;
                    hasCanbo.QUANTRI = quantri;

                    db.SubmitChanges();
                    return RedirectToAction("CanBo", "CanBo");
                }
                else
                {
                    TempData["Message"] = "Không tìm thấy cán bộ";
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult NhanVien()
        {
            List<SelectListItem> sex = new List<SelectListItem>
            {
                new SelectListItem{ Text="Nam", Value="1" },
                new SelectListItem{ Text="Nữ", Value="0" }
            };
            ViewBag.GioiTinhList = sex;
            return View(db.VIEW_NHANVIENs);
        }

        [HttpPost]
        public ActionResult ThemNhanVien(FormCollection Data)
        {
            var manv = Data["MANV"];
            var taikhoan = Data["TaiKhoan"];
            var matkhau = Data["MatKhau"];
            var tennv = Data["TenNV"];
            var gioitinh = Data["GioiTinh"];
            var cmnd = Data["CMND"];
            var diachi = Data["DiaChi"];
            var email = Data["Email"];
            var sdt = Data["SDT"];

            bool hasError = false;

            if (String.IsNullOrEmpty(manv))
            {
                TempData["Message"] = "Mã nhân viên không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(taikhoan))
            {
                TempData["Message"] = "Tài khoản không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                TempData["Message"] = "Mật khẩu không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(tennv))
            {
                TempData["Message"] = "Tên nhân viên không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(gioitinh))
            {
                TempData["Message"] = "Giới tính không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(cmnd))
            {
                TempData["Message"] = "CMND_CCCD không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(diachi))
            {
                TempData["Message"] = "Địa chỉ không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(email))
            {
                TempData["Message"] = "Email không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(sdt))
            {
                TempData["Message"] = "Số điện thoại không được bỏ trống";
                hasError = true;
            }

            if(hasError)
            return RedirectToAction("NhanVien", "CanBo");
            else
            {
                Models.NHANVIEN hasNV = db.NHANVIENs.FirstOrDefault(x => x.MANV == manv);
                if (hasNV == null)
                {
                    Models.NHANVIEN NewNV = new Models.NHANVIEN
                    {
                        MANV = manv,
                        TENNV = tennv,
                        TAIKHOAN = taikhoan,
                        MATKHAU = matkhau,
                        CMND_CCCD = int.Parse(cmnd),
                        DIACHI = diachi,
                        EMAIL = email,
                        SDT = sdt,
                        DAXOA=false
                    };
                    if (gioitinh == "0")
                        NewNV.GIOITINH = false;
                    else
                        NewNV.GIOITINH = true;

                    db.NHANVIENs.InsertOnSubmit(NewNV);
                    db.SubmitChanges();
                    TempData["Message"] = "Thêm thành công";
                    return RedirectToAction("NhanVien", "CanBo");
                }
                else
                {
                    TempData["Message"] = "Mã nhân viên này đã tồn tại";
                    return RedirectToAction("NhanVien", "CanBo");
                }
            }
        }

        [HttpGet]
        public ActionResult ChiTietNhanVien(string manv)
        {
            List<SelectListItem> sex = new List<SelectListItem>
            {
                new SelectListItem{ Text="Nam", Value="1" },
                new SelectListItem{ Text="Nữ", Value="0" }
            };
            ViewBag.GioiTinhList = sex;
            return View(db.NHANVIENs.FirstOrDefault(x => x.MANV == manv));
        }

        [HttpPost]
        public ActionResult SuaNhanVien(FormCollection Data)
        {
            var id = Data["ID"];
            var manv = Data["MANV"];
            var taikhoan = Data["TaiKhoan"];
            var matkhau = Data["MatKhau"];
            var tennv = Data["TenNV"];
            var gioitinh = Data["GioiTinh"];
            var cmnd = Data["CMND"];
            var diachi = Data["DiaChi"];
            var email = Data["Email"];
            var sdt = Data["SDT"];

            bool hasError = false;

            if (String.IsNullOrEmpty(manv))
            {
                TempData["Message"] = "Mã nhân viên không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(taikhoan))
            {
                TempData["Message"] = "Tài khoản không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                TempData["Message"] = "Mật khẩu không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(tennv))
            {
                TempData["Message"] = "Tên nhân viên không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(gioitinh))
            {
                TempData["Message"] = "Giới tính không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(cmnd))
            {
                TempData["Message"] = "CMND_CCCD không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(diachi))
            {
                TempData["Message"] = "Địa chỉ không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(email))
            {
                TempData["Message"] = "Email không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(sdt))
            {
                TempData["Message"] = "Số điện thoại không được bỏ trống";
                hasError = true;
            }

            if (hasError)
                return RedirectToAction("NhanVien", "CanBo");
            else
            {
                Models.NHANVIEN hasNV = db.NHANVIENs.FirstOrDefault(x => x.ID_NHANVIEN == int.Parse(id));
                if (hasNV != null)
                {
                    hasNV.MANV = manv;
                    hasNV.TENNV = tennv;
                    hasNV.TAIKHOAN = taikhoan;
                    hasNV.MATKHAU = matkhau;
                      hasNV.CMND_CCCD = int.Parse(cmnd);
                    hasNV.DIACHI = diachi;
                    hasNV.EMAIL = email;
                    hasNV.SDT = sdt;
                    if (gioitinh == "0")
                        hasNV.GIOITINH = false;
                    else
                        hasNV.GIOITINH = true;

                    db.SubmitChanges();
                    TempData["Message"] = "Sửa thành công";
                    return RedirectToAction("NhanVien", "CanBo");
                }
                else
                {
                    TempData["Message"] = "Mã nhân viên này không tìm thấy";
                    return RedirectToAction("NhanVien", "CanBo");
                }
            }
        }
        [HttpGet]
        public ActionResult DayPhong()
        {
            return View(db.VIEW_DayPhongs);
        }

        [HttpPost]
        public ActionResult ThemDayPhong(FormCollection Data)
        {
            var madayphong = Data["MaDayPhong"];

            bool hasError = false;
            if (String.IsNullOrEmpty(madayphong))
            {
                TempData["Message"] = "Mã dãy phòng không được bỏ trống";
                hasError = true;
            }

            if (hasError)
                return RedirectToAction("DayPhong", "CanBo");
            else
            {
                Models.DAYPHONG hasDP = db.DAYPHONGs.FirstOrDefault(x => x.MADAYPHONG == madayphong);
                if (hasDP == null)
                {
                    Models.DAYPHONG NewDP = new Models.DAYPHONG
                    {
                        MADAYPHONG = madayphong,
                        DAXOA = false
                    };
                    db.DAYPHONGs.InsertOnSubmit(NewDP);
                    db.SubmitChanges();
                    TempData["Message"] = "Thêm thành công";
                    return RedirectToAction("DayPhong", "CanBo");
                }
                else
                {
                    TempData["Message"] = "Mã dãy phòng đã tồn tại";
                    return RedirectToAction("DayPhong", "CanBo");
                }
            }
        }

        [HttpGet]
        public ActionResult Phong()
        {
            ViewBag.ID_DAY = new SelectList(db.DAYPHONGs, "MaDayPhong", "MaDayPhong");
            return View(db.VIEW_Phongs);
        }

        [HttpPost]
        public ActionResult ThemPhong(FormCollection Data)
        {
            var madayphong = Data["MaDayPhong"];
            var maphong = Data["MaPhong"];
            var taikhoan = Data["TaiKhoan"];
            var matkhau = Data["MatKhau"];
            var soluongsinhvien = Data["SLSV"];
            var dongia = Data["DonGia"];
            var tinhtrang = Data["TinhTrang"];
            var motakhac = Data["MoTaKhac"];

            bool hasError = false;
            if (String.IsNullOrEmpty(madayphong))
            {
                TempData["Message"] = "Mã dãy phòng không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(maphong))
            {
                TempData["Message"] = "Mã phòng không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(taikhoan))
            {
                TempData["Message"] = "Tài khoản không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                TempData["Message"] = "Mật khẩu không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(soluongsinhvien))
            {
                TempData["Message"] = "Số lượng sinh viên không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(dongia))
            {
                TempData["Message"] = "Đơn giá phòng không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(tinhtrang))
            {
                TempData["Message"] = "Tình trạng không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(motakhac))
            {
                TempData["Message"] = "Mô tả khác không được bỏ trống";
                hasError = true;
            }

            if (hasError)
                return RedirectToAction("Phong", "CanBo");
            else
            {
                Models.DAYPHONG hasDP = db.DAYPHONGs.FirstOrDefault(x => x.MADAYPHONG == madayphong);
                if (hasDP != null)
                {
                    Models.PHONG hasPhong = db.PHONGs.FirstOrDefault(x => x.MAPHONG == maphong && x.ID_DAY == hasDP.ID_DAY);
                    if (hasPhong == null)
                    {
                        Models.PHONG NewPhong = new Models.PHONG
                        {
                            ID_DAY = hasDP.ID_DAY,
                            MAPHONG = maphong,
                            TAIKHOAN = taikhoan,
                            MATKHAU = matkhau,
                            SOLUONGSV = int.Parse(soluongsinhvien),
                            DONGIA = int.Parse(dongia),
                            TINHTRANG = tinhtrang,
                            MOTAKHAC = motakhac,
                            TRANGTHAI = false,
                            DAXOA = false
                        };

                        db.PHONGs.InsertOnSubmit(NewPhong);
                        db.SubmitChanges();
                        TempData["Message"] = "Thêm thành công";
                        return RedirectToAction("Phong", "CanBo");
                    }
                    else
                    {
                        TempData["Message"] = "Mã phòng đã tồn tại";
                        return RedirectToAction("Phong", "CanBo");
                    }
                }
                else
                {
                    TempData["Message"] = "Mã dãy phòng không tồn tại";
                    return RedirectToAction("Phong", "CanBo");
                }
            }
        }

        [HttpGet]
        public ActionResult ThongTinPhong(string maphong)
        {
            ViewBag.ID_DAY = new SelectList(db.DAYPHONGs, "MaDayPhong", "MaDayPhong");
            var ThongTinPhong = db.PHONGs.FirstOrDefault(x => x.MAPHONG == maphong);
            Models.DAYPHONG dayphong = db.DAYPHONGs.FirstOrDefault(x => x.ID_DAY == ThongTinPhong.ID_DAY);
            ViewBag.ThongTinDayPhong = dayphong.MADAYPHONG;
            return View(ThongTinPhong);
        }

        [HttpPost]
        public ActionResult SuaThongTinPhong(FormCollection Data)
        {
            var idphong = Data["ID_PHONG"];
            var madayphong = Data["MaDayPhong"];
            var Maphong = Data["MaPhong"];
            var taikhoan = Data["TaiKhoan"];
            var matkhau = Data["MatKhau"];
            var soluongsinhvien = Data["SLSV"];
            var dongia = Data["DonGia"];
            var tinhtrang = Data["TinhTrang"];
            var motakhac = Data["MoTaKhac"];
            bool trangthai;
            if (Data["TrangThai"] == "true")
            {
                trangthai = true;
            }
            else
            {
                trangthai = false;
            }

            bool hasError = false;
            if (String.IsNullOrEmpty(madayphong))
            {
                TempData["Message"] = "Mã dãy phòng không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(Maphong))
            {
                TempData["Message"] = "Mã phòng không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(taikhoan))
            {
                TempData["Message"] = "Tài khoản không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                TempData["Message"] = "Mật khẩu không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(soluongsinhvien))
            {
                TempData["Message"] = "Số lượng sinh viên không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(dongia))
            {
                TempData["Message"] = "Đơn giá phòng không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(tinhtrang))
            {
                TempData["Message"] = "Tình trạng không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(motakhac))
            {
                TempData["Message"] = "Mô tả khác không được bỏ trống";
                hasError = true;
            }

            if (hasError)
                return RedirectToAction("Phong", "CanBo");
            else
            {
                Models.DAYPHONG hasDP = db.DAYPHONGs.FirstOrDefault(x => x.MADAYPHONG == madayphong);
                {
                    if (hasDP != null)
                    {
                        Models.PHONG hasPhong = db.PHONGs.FirstOrDefault(x => x.ID_PHONG == int.Parse(idphong));
                        if (hasPhong != null)
                        {
                            hasPhong.ID_DAY=hasDP.ID_DAY;
                            hasPhong.MAPHONG = Maphong;
                            hasPhong.TAIKHOAN = taikhoan;
                            hasPhong.MATKHAU = matkhau;
                            hasPhong.SOLUONGSV = int.Parse(soluongsinhvien);
                            hasPhong.DONGIA = int.Parse(dongia);
                            hasPhong.TINHTRANG = tinhtrang;
                            hasPhong.MOTAKHAC = motakhac;
                            hasPhong.DAXOA = false;
                            hasPhong.TRANGTHAI = trangthai;

                            db.SubmitChanges();
                            TempData["Message"] = "Sửa thành công";
                            return RedirectToAction("Phong", "CanBo");
                        }
                        else
                        {
                            TempData["Message"] = "Mã phòng không tồn tại";
                            return RedirectToAction("Phong", "CanBo");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Mã dãy phòng không tồn tại";
                        return RedirectToAction("Phong", "CanBo");
                    }
                }
            }
        }

        public ActionResult DonGia()
        {
            return View(db.VIEW_DONGIAs);
        }

        [HttpPost]
        public ActionResult ThemDonGia(FormCollection Data, Models.DONGIA NewDG)
        {
            var madongia = Data["MaDonGia"];
            var dongiadien = Data["DonGiaDien"];
            var dongianuoc = Data["DonGiaNuoc"];
            var ngayapdung = Data["NgayApDung"];

            DateTime Chuanhoa_ngayapdung;
            if (!DateTime.TryParse(ngayapdung, out Chuanhoa_ngayapdung))
            {
                TempData["Message"] = "Ngày áp dụng không hợp lệ";
                return RedirectToAction("DonGia", "CanBo");
            }

            bool hasError = false;

            if (String.IsNullOrEmpty(madongia))
            {
                TempData["Message"] = "Mã đơn giá không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(dongiadien))
            {
                TempData["Message"] = "Đơn giá điện không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(dongianuoc))
            {
                TempData["Message"] = "Đơn giá nước không được bỏ trống";
                hasError = true;
            }

            if (hasError)
                return RedirectToAction("DonGia", "CanBo");
            else
            {
                NewDG.MADONGIA = madongia;
                NewDG.DONGIADIEN = int.Parse(dongiadien);
                NewDG.DONGIANUOC = int.Parse(dongianuoc);
                NewDG.NGAYAPDUNG = Chuanhoa_ngayapdung;
                NewDG.DAXOA = false;

                Models.DONGIA hasDG = db.DONGIAs.FirstOrDefault(x => x.MADONGIA == madongia);
                if (hasDG == null)
                {
                    db.DONGIAs.InsertOnSubmit(NewDG);
                    db.SubmitChanges();
                    return RedirectToAction("DonGia", "CanBo");
                }
                else
                {
                    TempData["Message"] = "Mã đơn giá này đã tồn tại";
                    return RedirectToAction("DonGia", "CanBo");
                }
            }
        }

        [HttpGet]
        public ActionResult ChinhSuaDonGia(string madongia)
        {
            return View(db.DONGIAs.FirstOrDefault(x => x.MADONGIA == madongia));
        }

        [HttpPost]
        public ActionResult ChinhSuaDonGia(FormCollection Data)
        {
            var id = Data["ID"];
            var madongia = Data["MaDonGia"];
            var dongiadien = Data["DonGiaDien"];
            var dongianuoc = Data["DonGiaNuoc"];
            var ngayapdung = Data["NgayApDung"];
            bool apdung;
            if (Data["ApDung"] == "true")
            {
                apdung = true;
            }
            else
            {
                apdung = false;
            }


            DateTime Chuanhoa_ngayapdung;
            if (!DateTime.TryParse(ngayapdung, out Chuanhoa_ngayapdung))
            {
                TempData["Message"] = "Ngày áp dụng không hợp lệ";
                return RedirectToAction("DonGia", "CanBo");
            }

            bool hasError = false;

            if (String.IsNullOrEmpty(madongia))
            {
                TempData["Message"] = "Mã đơn giá không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(dongiadien))
            {
                TempData["Message"] = "Đơn giá điện không được bỏ trống";
                hasError = true;
            }
            if (String.IsNullOrEmpty(dongianuoc))
            {
                TempData["Message"] = "Đơn giá nước không được bỏ trống";
                hasError = true;
            }
            if (apdung == true)
            {
                Models.DONGIA hasDG_DangApDung = db.DONGIAs.FirstOrDefault(x => x.TRANGTHAI == true);
                if (hasDG_DangApDung != null)
                {
                    TempData["Message"] = "Đã có đơn giá khác đang áp dụng";
                    hasError = true;
                }
            }

            if (hasError)
                return RedirectToAction("DonGia", "CanBo");
            else
            {
                Models.DONGIA hasDG = db.DONGIAs.FirstOrDefault(x => x.ID_DONGIA == int.Parse(id));
                if (hasDG != null)
                {
                    hasDG.MADONGIA = madongia;
                    hasDG.DONGIADIEN = int.Parse(dongiadien);
                    hasDG.DONGIANUOC = int.Parse(dongianuoc);
                    hasDG.NGAYAPDUNG = Chuanhoa_ngayapdung;
                    hasDG.TRANGTHAI = apdung;
                    hasDG.DAXOA = false;

                    db.SubmitChanges();
                    return RedirectToAction("DonGia", "CanBo");
                }
                else
                {
                    TempData["Message"] = "Mã đơn giá này đã tồn tại";
                    return RedirectToAction("DonGia", "CanBo");
                }
            }
        }

        public ActionResult HoTro()
        {
            return View(db.VIEW_HOTROs);
        }

        public ActionResult ChinhSuaHotro(int idhotro)
        {
            Models.HOTRO hasHT = db.HOTROs.FirstOrDefault(x => x.ID_HOTRO == idhotro);
            if (hasHT!=null)
            {
                hasHT.TRANGTHAI = true;
                db.SubmitChanges();
                return RedirectToAction("HoTro", "CanBo");
            }
            else
            {
                TempData["Message"] = "Không tìm thấy hỗ trợ";
                return RedirectToAction("HoTro", "CanBo");
            }
        }

        public ActionResult HoaDonDienNuoc()
        {
            return View(db.VIEW_HoaDonDienNuocs);
        }

        [HttpGet]
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

        [HttpPost]
        public ActionResult ChinhSua_HDP_HDDN(FormCollection Data)
        {
            var madayphong = Data["MaDayPhong"];
            var maphong = Data["MaPhong"];
            var nam = Data["Nam"];
            var ky = Data["Ky"];

            Models.PHONG hasPhong = db.PHONGs.FirstOrDefault(x => x.MAPHONG == maphong);
            if (hasPhong != null)
            {
                Models.HOADON_DIENNUOC hasHDDN = db.HOADON_DIENNUOCs.FirstOrDefault(x => x.ID_PHONG == hasPhong.ID_PHONG
                && x.TRANGTHAI == false && x.THANG == int.Parse(ky) && x.NAM == int.Parse(nam));
                if (hasHDDN != null)
                {
                    Models.HOADON_PHONG hasHDP = db.HOADON_PHONGs.FirstOrDefault(x => x.ID_PHONG == hasPhong.ID_PHONG
                    && x.TRANGTHAI == false && x.ID_DONGIA == hasHDDN.ID_DONGIA && x.KY == int.Parse(ky) && x.NAM == int.Parse(nam));
                    if (hasHDP != null)
                    {
                        hasHDDN.TRANGTHAI = true;
                        hasHDP.TRANGTHAI = true;
                        db.SubmitChanges();
                        TempData["Message"] = "Xác nhận thành công";
                        return RedirectToAction("HoaDonPhong", "CanBo");
                    }
                }
            }

            TempData["Message"] = "Không thành công";
            return RedirectToAction("HoaDonPhong", "CanBo");
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

        public ActionResult SinhVien()
        {
            return View(db.VIEW_SINHVIENs);
        }

        public ActionResult ThanNhan()
        {
            return View(db.VIEW_THANNHANs);
        }
    }
}