using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace nhanvien.Models
{
    public class NhanVien
    {
        public int ID_NHANVIEN { get; set; }
        public string MANV { get; set; }
        public string TAIKHOAN { get; set; }
        public string MATKHAU { get; set; }
        public string TENNV { get; set; }
        public bool GIOITINH { get; set; }
        public int CMND_CCCD { get; set; }
        public string DIACHI { get; set; }
        public string EMAIL { get; set; }
        public int SDT { get; set; }
        public bool DAXOA { get; set; }
    }
} 