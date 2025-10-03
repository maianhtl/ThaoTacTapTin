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
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Lop {  get; set; }
        
        public string SoCMND {  get; set; }
        public string SoDT {  get; set; }
        public string DiaChi {  get; set; }
        public List<string> MonHocDK { get; set; }

        public student()
        {
           
        }

        public student(string mssv, string hoVaTenLot, string ten, string lop, string gioitinh,
               DateTime ngaySinh, string soCCCD, string soDT, string diaChi, List<string> monHocDK)
        {
            Mssv = mssv;
            HoVaTenLot = hoVaTenLot;
            Ten = ten;
            Lop = lop;
            GioiTinh = gioitinh;
            NgaySinh = ngaySinh;
            SoCMND = soCCCD;
            SoDT = soDT;
            DiaChi = diaChi;
            MonHocDK = monHocDK;
        }

        
    }
}
