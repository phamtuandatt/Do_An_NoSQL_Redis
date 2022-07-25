using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceStack.Redis;
using StackExchange.Redis;
using ServiceStack.Redis.Generic;

namespace Redis
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            using (RedisClient client = new RedisClient("localhost"))
            {
                IRedisTypedClient<Phone3> phone = client.As<Phone3>();
                phone3BindingSource.DataSource = phone.GetAll();
            }
        }

        private void txtAdd_Click(object sender, EventArgs e)
        {
            phone3BindingSource.Add(new Phone3());
            phone3BindingSource.MoveLast();
        }

        private void txtEdit_Click(object sender, EventArgs e)
        {

        }

        private void txtDelete_Click(object sender, EventArgs e)
        {
            Phone3 p = phone3BindingSource.Current as Phone3;
            if (p != null)
            {
                using (RedisClient client = new RedisClient("localhost"))
                {
                    phone3BindingSource.EndEdit();
                    IRedisTypedClient<Phone3> phone = client.As<Phone3>();
                    phone.DeleteById(p.Id);
                    phone3BindingSource.RemoveCurrent();

                    MetroFramework.MetroMessageBox.Show(this, "Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtSave_Click(object sender, EventArgs e)
        {
            using (RedisClient client = new RedisClient("localhost"))
            {
                phone3BindingSource.EndEdit();
                IRedisTypedClient<Phone3> phone = client.As<Phone3>();
                phone.StoreAll(phone3BindingSource.DataSource as List<Phone3>);

                MetroFramework.MetroMessageBox.Show(this, "Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
