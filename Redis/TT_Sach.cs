    using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    class TT_Sach
    {
        int id;
        string tens, theLoai, nhaXB;
        int soLuong, giaBan;
        string anhBia;
        Image anh;
        string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        public int Id { get => id; set => id = value; }
        public string Tens { get => tens; set => tens = value; }
        public string TheLoai { get => theLoai; set => theLoai = value; }
        public string NhaXB { get => nhaXB; set => nhaXB = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public int GiaBan { get => giaBan; set => giaBan = value; }
        public string AnhBia { get => anhBia; set => anhBia = value; }
        public Image Anh
        {
            get
            {
                if (AnhBia == null)
                {
                    return Image.FromFile(projectPath + "\\Image\\nocontentyet.jpg");
                }
                return Image.FromFile(AnhBia);
            }
        }
    }
}
