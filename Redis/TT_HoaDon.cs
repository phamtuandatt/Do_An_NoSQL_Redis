using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    class TT_HoaDon
    {
        string  maNV, maKH, tens;
        int id, soLuong, giaBan, thanhTien;

        public int SoLuong { get => soLuong; set => soLuong = value; }
        public int GiaBan { get => giaBan; set => giaBan = value; }
        public int ThanhTien { get => thanhTien; set => thanhTien = value; }
        public int Id { get => id; set => id = value; }
        public string MaNV { get => maNV; set => maNV = value; }
        public string MaKH { get => maKH; set => maKH = value; }
        public string Tens { get => tens; set => tens = value; }
    }
}
