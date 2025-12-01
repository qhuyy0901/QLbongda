using DAL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TrangChu
{

    public partial class TrangChu : Form
    {
        private string tenNguoiDung;

        public TrangChu(string ten)
        {
            InitializeComponent();
            tenNguoiDung = ten;
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            lblXinChao.Text = "Xin chào, " + tenNguoiDung + "!";
        }
    }



}
