using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nhanvien.DataAccess
{
    public class NhanVienDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MyDatabase"].ConnectionString;

        public List<NhanVien> GetAllNhanVien()
        {
            List<NhanVien> nhanViens = new List<NhanVien>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM VIEW_NHANVIEN";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    nhanViens.Add(new NhanVien
                    {
                        ID_NHANVIEN = (int)reader["ID_NHANVIEN"],
                        MANV = reader["MANV"].ToString(),
                        TAIKHOAN = reader["TAIKHOAN"].ToString(),
                        MATKHAU = reader["MATKHAU"].ToString(),
                        TENNV = reader["TENNV"].ToString(),
                        GIOITINH = (bool)reader["GIOITINH"],
                        CMND_CCCD = (int)reader["CMND_CCCD"],
                        DIACHI = reader["DIACHI"].ToString(),
                        EMAIL = reader["EMAIL"].ToString(),
                        SDT = (int)reader["SDT"],
                        DAXOA = (bool)reader["DAXOA"]
                    });
                }
            }
            return nhanViens;
        }

        // Thêm phương thức Insert
        public void InsertNhanVien(NhanVien nhanVien)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO NHANVIEN (MANV, TAIKHOAN, MATKHAU, TENNV, GIOITINH, CMND_CCCD, DIACHI, EMAIL, SDT, DAXOA) " +
                               "VALUES (@MANV, @TAIKHOAN, @MATKHAU, @TENNV, @GIOITINH, @CMND_CCCD, @DIACHI, @EMAIL, @SDT, @DAXOA)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MANV", nhanVien.MANV);
                cmd.Parameters.AddWithValue("@TAIKHOAN", nhanVien.TAIKHOAN);
                cmd.Parameters.AddWithValue("@MATKHAU", nhanVien.MATKHAU);
                cmd.Parameters.AddWithValue("@TENNV", nhanVien.TENNV);
                cmd.Parameters.AddWithValue("@GIOITINH", nhanVien.GIOITINH);
                cmd.Parameters.AddWithValue("@CMND_CCCD", nhanVien.CMND_CCCD);
                cmd.Parameters.AddWithValue("@DIACHI", nhanVien.DIACHI);
                cmd.Parameters.AddWithValue("@EMAIL", nhanVien.EMAIL);
                cmd.Parameters.AddWithValue("@SDT", nhanVien.SDT);
                cmd.Parameters.AddWithValue("@DAXOA", nhanVien.DAXOA);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Thêm phương thức Update
        public void UpdateNhanVien(NhanVien nhanVien)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE NHANVIEN SET TENNV = @TENNV, GIOITINH = @GIOITINH, CMND_CCCD = @CMND_CCCD, DIACHI = @DIACHI, EMAIL = @EMAIL, SDT = @SDT " +
                               "WHERE ID_NHANVIEN = @ID_NHANVIEN";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_NHANVIEN", nhanVien.ID_NHANVIEN);
                cmd.Parameters.AddWithValue("@TENNV", nhanVien.TENNV);
                cmd.Parameters.AddWithValue("@GIOITINH", nhanVien.GIOITINH);
                cmd.Parameters.AddWithValue("@CMND_CCCD", nhanVien.CMND_CCCD);
                cmd.Parameters.AddWithValue("@DIACHI", nhanVien.DIACHI);
                cmd.Parameters.AddWithValue("@EMAIL", nhanVien.EMAIL);
                cmd.Parameters.AddWithValue("@SDT", nhanVien.SDT);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Thêm phương thức Delete
        public void DeleteNhanVien(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE NHANVIEN SET DAXOA = 1 WHERE ID_NHANVIEN = @ID_NHANVIEN";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_NHANVIEN", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}