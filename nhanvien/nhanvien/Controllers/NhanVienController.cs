using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using nhanvien.Models;
using System.Data.SqlClient; 

namespace nhanvien.Controllers
{
    public class NhanVienController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["NhanVienDB"].ConnectionString;

        // Hiển thị danh sách nhân viên
        public ActionResult Index()
        {
            var nhanViens = new List<NhanVien>();
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM VIEW_NHANVIEN";
                var command = new SqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nhanViens.Add(new NhanVien
                        {
                            ID_NHANVIEN = reader.GetInt32(0),
                            MANV = reader.GetString(1),
                            TAIKHOAN = reader.GetString(2),
                            TENNV = reader.GetString(3),
                            GIOITINH = reader.GetBoolean(4),
                            CMND_CCCD = reader.GetInt32(5),
                            DIACHI = reader.GetString(6),
                            EMAIL = reader.GetString(7),
                            SDT = reader.GetInt32(8)
                        });
                    }
                }
            }
            return View(nhanViens);
        }

        // Tạo nhân viên mới
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NhanVien model)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "INSERT INTO NHANVIEN (MANV, TAIKHOAN, MATKHAU, TENNV, GIOITINH, CMND_CCCD, DIACHI, EMAIL, SDT) " +
                            "VALUES (@MANV, @TAIKHOAN, @MATKHAU, @TENNV, @GIOITINH, @CMND_CCCD, @DIACHI, @EMAIL, @SDT)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MANV", model.MANV);
                command.Parameters.AddWithValue("@TAIKHOAN", model.TAIKHOAN);
                command.Parameters.AddWithValue("@MATKHAU", model.MATKHAU);
                command.Parameters.AddWithValue("@TENNV", model.TENNV);
                command.Parameters.AddWithValue("@GIOITINH", model.GIOITINH);
                command.Parameters.AddWithValue("@CMND_CCCD", model.CMND_CCCD);
                command.Parameters.AddWithValue("@DIACHI", model.DIACHI);
                command.Parameters.AddWithValue("@EMAIL", model.EMAIL);
                command.Parameters.AddWithValue("@SDT", model.SDT);
                connection.Open();
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // Sửa nhân viên
        public ActionResult Edit(int id)
        {
            NhanVien nhanVien = null;
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM NHANVIEN WHERE ID_NHANVIEN = @ID";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        nhanVien = new NhanVien
                        {
                            ID_NHANVIEN = reader.GetInt32(0),
                            MANV = reader.GetString(1),
                            TAIKHOAN = reader.GetString(2),
                            TENNV = reader.GetString(3),
                            GIOITINH = reader.GetBoolean(4),
                            CMND_CCCD = reader.GetInt32(5),
                            DIACHI = reader.GetString(6),
                            EMAIL = reader.GetString(7),
                            SDT = reader.GetInt32(8)
                        };
                    }
                }
            }
            return View(nhanVien);
        }

        [HttpPost]
        public ActionResult Edit(NhanVien model)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "UPDATE NHANVIEN SET MANV = @MANV, TAIKHOAN = @TAIKHOAN, TENNV = @TENNV, GIOITINH = @GIOITINH, " +
                            "CMND_CCCD = @CMND_CCCD, DIACHI = @DIACHI, EMAIL = @EMAIL, SDT = @SDT WHERE ID_NHANVIEN = @ID";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", model.ID_NHANVIEN);
                command.Parameters.AddWithValue("@MANV", model.MANV);
                command.Parameters.AddWithValue("@TAIKHOAN", model.TAIKHOAN);
                command.Parameters.AddWithValue("@TENNV", model.TENNV);
                command.Parameters.AddWithValue("@GIOITINH", model.GIOITINH);
                command.Parameters.AddWithValue("@CMND_CCCD", model.CMND_CCCD);
                command.Parameters.AddWithValue("@DIACHI", model.DIACHI);
                command.Parameters.AddWithValue("@EMAIL", model.EMAIL);
                command.Parameters.AddWithValue("@SDT", model.SDT);
                connection.Open();
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // Xóa nhân viên
        public ActionResult Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "UPDATE NHANVIEN SET DAXOA = 1 WHERE ID_NHANVIEN = @ID";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}