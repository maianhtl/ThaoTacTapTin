using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTap
{
    public partial class fTimKiem : Form
    {
        public fTimKiem()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void fTimKiem_Load(object sender, EventArgs e)
        {
            cboTimKiem.SelectedItem = "MSSV";

        }

        private void cboTimKiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTimKiem.SelectedItem.ToString() == "Tên")
                label2.Text = "Nhập tên:";
            if (cboTimKiem.SelectedItem.ToString() == "MSSV")
                label2.Text = "Nhập MSSV:";
            if (cboTimKiem.SelectedItem.ToString() == "Lớp")
                label2.Text = "Nhập lớp:";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        public List<student> TimKiem(List<student> ds)
        {
            string text = txtTimKiem.Text.ToUpper();
            List<student> result = new List<student>();

            if (cboTimKiem.SelectedItem.ToString() == "MSSV")
            {
                foreach (var sv in ds)
                {
                    if (sv.Mssv == txtTimKiem.Text) result.Add(sv);
                }
            }
            if (cboTimKiem.SelectedItem.ToString() == "Tên")
            {
                foreach (var sv in ds)
                {
                    if (sv.Ten == txtTimKiem.Text) result.Add(sv);
                }
            }
            if (cboTimKiem.SelectedItem.ToString() == "Lớp")
            {
                foreach (var sv in ds)
                {
                    if (sv.Lop == text) result.Add(sv);
                }
            }


            return result;
        }

        private void fTimKiem_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
