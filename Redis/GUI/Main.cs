using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StackExchange.Redis;
using System.IO;

namespace Redis.GUI
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        string file_path_PT;
        string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        public Main()
        {
            InitializeComponent();
            load_data();
        }

        public void load_data()
        {
            using (RedisClient client = new RedisClient("localhost"))
            {
                IRedisTypedClient<TT_Sach> sach = client.As<TT_Sach>();
                tTSachBindingSource.DataSource = sach.GetAll();

                IRedisTypedClient<TT_NhanVien> nv = client.As<TT_NhanVien>();
                tTNhanVienBindingSource.DataSource = nv.GetAll();

                IRedisTypedClient<TT_KhachHang> kh = client.As<TT_KhachHang>();
                tTKhachHangBindingSource2.DataSource = kh.GetAll();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //Add_Sach add = new Add_Sach(tTSachBindingSource);
            //add.ShowDialog();
            //load_data();
            gridSach.ReadOnly = false;
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            gridSach.ReadOnly = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost"))
            {
                tTSachBindingSource.EndEdit();
                IRedisTypedClient<TT_Sach> sach = client.As<TT_Sach>();
                sach.StoreAll(tTSachBindingSource.DataSource as List<TT_Sach>);
                MetroFramework.MetroMessageBox.Show(this, "Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gridSach.ReadOnly = true;
            }
        }

        private void btnSaveNV_Click(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost"))
            {
                tTNhanVienBindingSource.EndEdit();
                IRedisTypedClient<TT_NhanVien> nv = client.As<TT_NhanVien>();
                nv.StoreAll(tTNhanVienBindingSource.DataSource as List<TT_NhanVien>);
                MetroFramework.MetroMessageBox.Show(this, "Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gridNV.ReadOnly = true;
            }
        }

        private void btnSave_KH_Click(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost"))
            {
                tTKhachHangBindingSource2.EndEdit();
                IRedisTypedClient<TT_KhachHang> kh = client.As<TT_KhachHang>();
                kh.StoreAll(tTKhachHangBindingSource2.DataSource as List<TT_KhachHang>);
                MetroFramework.MetroMessageBox.Show(this, "Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gridKH.ReadOnly = true;
            }
        }

        private void btnXoa_Sach_Click(object sender, EventArgs e)
        {
            TT_Sach sach = tTSachBindingSource.Current as TT_Sach;
            if (MetroFramework.MetroMessageBox.Show(this, "Bạn có muốn Xóa sách " + sach.Tens + " không ?",
                    "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;
            if (sach != null)
            {
                using (RedisClient client = new RedisClient("localhost"))
                {
                    tTSachBindingSource.EndEdit();
                    IRedisTypedClient<TT_Sach> s = client.As<TT_Sach>();
                    s.DeleteById(sach.Id);
                    tTSachBindingSource.RemoveCurrent();

                    MetroFramework.MetroMessageBox.Show(this, "Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            TT_NhanVien nv = tTNhanVienBindingSource.Current as TT_NhanVien;
            if (MetroFramework.MetroMessageBox.Show(this, "Bạn có muốn Xóa nhân viên " + nv.TenNV + " không ?",
                    "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;
            if (nv != null)
            {
                using (RedisClient client = new RedisClient("localhost"))
                {
                    tTNhanVienBindingSource.EndEdit();
                    IRedisTypedClient<TT_NhanVien> n = client.As<TT_NhanVien>();
                    n.DeleteById(nv.Id);
                    tTNhanVienBindingSource.RemoveCurrent();

                    MetroFramework.MetroMessageBox.Show(this, "Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            TT_KhachHang kh = tTKhachHangBindingSource2.Current as TT_KhachHang;
            if (MetroFramework.MetroMessageBox.Show(this, "Bạn có muốn Xóa Khách hàng " + kh.TenKH + " không ?",
                    "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;
            if (kh != null)
            {
                using (RedisClient client = new RedisClient("localhost"))
                {
                    tTKhachHangBindingSource2.EndEdit();
                    IRedisTypedClient<TT_KhachHang> n = client.As<TT_KhachHang>();
                    n.DeleteById(kh.Id);
                    tTKhachHangBindingSource2.RemoveCurrent();

                    MetroFramework.MetroMessageBox.Show(this, "Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void gridSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                openAnh.Filter = "Pictures files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg; *.jpeg; *.jpe; *.jfif; *.png|All files (*.*)|*.*";
                openAnh.FilterIndex = 1;
                openAnh.RestoreDirectory = true;
                if (openAnh.ShowDialog() == DialogResult.OK)
                {
                    file_path_PT = openAnh.SafeFileName;
                    Image img = Image.FromFile(openAnh.FileName);

                    gridSach.Rows[e.RowIndex].Cells[6].Value = projectPath + "\\Image\\" + file_path_PT;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            gridNV.ReadOnly = false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            gridNV.ReadOnly = false;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            gridKH.ReadOnly = false;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            gridKH.ReadOnly = false;
        }

        private void btnTK_Click(object sender, EventArgs e)
        {

        }
    }
}
