using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTap
{
    public class student
    {
        public string Mssv { get; set; }
        public string HoVaTenLot { get; set; }
        public string Ten { get; set; }
        public string Lop {  get; set; }
        public string GioiTinh {  get; set; }
        public DateTime NgaySinh { get; set; }
        public string SoCCCD {  get; set; }
        public string SoDT {  get; set; }
        public string DiaChi {  get; set; }
        public string[] MonHocDK { get; set; }

        public student()
        {
           
        }

        public student(string mssv, string hoVaTenLot, string ten, string lop, string gioiTinh,
               DateTime ngaySinh, string soCCCD, string soDT, string diaChi, string[] monHocDK)
        {
            Mssv = mssv;
            HoVaTenLot = hoVaTenLot;
            Ten = ten;
            Lop = lop;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            SoCCCD = soCCCD;
            SoDT = soDT;
            DiaChi = diaChi;
            MonHocDK = monHocDK;
        }

        public student(string line)
        {
            var parts = line.Split(',');
            Mssv = parts[0];
            HoVaTenLot = parts[1];
            Ten = parts[2];
            Lop = parts[3];
            GioiTinh = parts[4];
            NgaySinh = DateTime.ParseExact(parts[5], "dd/MM/yyyy", null);
            SoCCCD = parts[6];
            SoDT = parts[7];
            DiaChi = parts[8];
            MonHocDK = parts[9].Split(',');
        }

        public void GhepLine(string mssv, string hoVaTenLot, string ten, string lop, string gioiTinh,
               DateTime ngaySinh, string soCCCD, string soDT, string diaChi, string[] monHocDK)
        {

        }
    }
}
