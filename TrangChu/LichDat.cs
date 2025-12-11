using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TrangChu
{
    public partial class LichDat : Form
    {
        SanBongBUS sanBongBUS = new SanBongBUS();
        LichDatBUS lichDatBUS = new LichDatBUS();
        private User currentUser;
        List<Button> listBtnSan = new List<Button>();

        public LichDat() { InitializeComponent(); }
        public LichDat(User user) { InitializeComponent(); this.currentUser = user; }

        private void LichDat_Load(object sender, EventArgs e)
        {
            KhoiTaoNutSan();
            LoadComboBoxSan();
            LoadDataStatic();
            LoadDGV();

            // SỬA: Bảng Users mới dùng cột ID, không phải UserName
            if (currentUser != null)
                lblTenNguoiDung.Text = "Người Thực Hiện Đặt: " + (currentUser.TenNguoiDung ?? currentUser.ID);

            cbxMaSan.SelectedIndexChanged += (s, ev) => TinhTien();
            dtpNgayDat.ValueChanged += (s, ev) => { TinhTien(); CapNhatMauSacSan(); };
            dtpGioBatDau.ValueChanged += (s, ev) => CapNhatMauSacSan();
            CapNhatMauSacSan();
        }

        private void KhoiTaoNutSan()
        {
            listBtnSan.Clear();
            // CẬP NHẬT TAG CHO KHỚP VỚI DATABASE MỚI (SB01 -> SB07)
            if (btnSan1 != null) { btnSan1.Tag = "SB01"; listBtnSan.Add(btnSan1); }
            if (btnSan2 != null) { btnSan2.Tag = "SB02"; listBtnSan.Add(btnSan2); }
            if (btnSan3 != null) { btnSan3.Tag = "SB03"; listBtnSan.Add(btnSan3); }
            if (btnSan4 != null) { btnSan4.Tag = "SB04"; listBtnSan.Add(btnSan4); }
            if (btnSan5 != null) { btnSan5.Tag = "SB05"; listBtnSan.Add(btnSan5); }
            if (btnSan6 != null) { btnSan6.Tag = "SB06"; listBtnSan.Add(btnSan6); }
            if (btnSan7 != null) { btnSan7.Tag = "SB07"; listBtnSan.Add(btnSan7); }

            foreach (var btn in listBtnSan)
            {
                btn.Click -= NutSan_Click;
                btn.Click += NutSan_Click;
            }
        }

        private void NutSan_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                string maSan = btn.Tag.ToString(); // Lấy "SB01", "SB02"...

                // 1. Điền Mã Sân
                if (cbxMaSan.Items.Count > 0)
                {
                    cbxMaSan.SelectedValue = maSan;
                }

                // 2. Điền Loại Sân (Logic nhận diện theo mã mới)
                // SB01 -> SB06 là Sân 5, SB07 là Sân 7
                if (maSan == "SB07")
                {
                    cbxLoaiSan.Text = "Sân 7"; // Hoặc "Sân cỏ tự nhiên" tùy dữ liệu bạn nhập
                }
                else
                {
                    cbxLoaiSan.Text = "Sân 5";
                }

                // 3. Thông báo
                if (btn.BackColor == Color.Red)
                    MessageBox.Show($"Sân {maSan} đang bận!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if (btn.BackColor == Color.Yellow)
                    MessageBox.Show($"Sân {maSan} đang bảo trì!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }

        private void CapNhatMauSacSan()
        {
            DateTime ngayChon = dtpNgayDat.Value.Date;
            int gioXem = dtpGioBatDau.Value.Hour;
            var listLich = lichDatBUS.GetLichDatByDate(ngayChon);
            var listSanInfo = sanBongBUS.GetListSanBong();

            foreach (var btn in listBtnSan)
            {
                string maSan = btn.Tag.ToString();
                var sanInfo = listSanInfo.FirstOrDefault(s => s.MaSan == maSan);

                if (sanInfo != null && (sanInfo.TrangThai == "Bảo trì" || sanInfo.TrangThai == "Hỏng"))
                {
                    btn.BackColor = Color.Yellow; continue;
                }

                bool dangDa = listLich.Any(l => l.MaSan == maSan && (l.GioBD <= gioXem && l.GioKT > gioXem));
                if (dangDa) btn.BackColor = Color.Red;
                else btn.BackColor = Color.LimeGreen;
            }
        }

        private void TinhTien()
        {
            try
            {
                if (cbxMaSan.SelectedValue != null)
                {
                    string maSan = cbxMaSan.SelectedValue.ToString();
                    decimal gia = sanBongBUS.GetDonGia(maSan, dtpNgayDat.Value);
                    cbxDonGia.Text = gia.ToString("N0");
                }
            }
            catch { }
        }

        private void btnDatSAn_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentUser == null) { MessageBox.Show("Lỗi: Mất phiên đăng nhập!"); return; }
                if (cbxMaSan.SelectedValue == null) { MessageBox.Show("Vui lòng chọn sân!"); return; }
                if (string.IsNullOrEmpty(txtSDT.Text) || string.IsNullOrEmpty(txtTenKhachHang.Text)) { MessageBox.Show("Thiếu thông tin khách!"); return; }

                DAL.LichDat lich = new DAL.LichDat();
                lich.SDT_KH = txtSDT.Text.Trim();
                lich.TenKH = txtTenKhachHang.Text.Trim();
                lich.MaSan = cbxMaSan.SelectedValue.ToString();

                // SỬA: Bảng Users dùng cột ID làm khóa chính (không phải UserName)
                // Tuy nhiên trong bảng LichDat bạn lại không có cột ID User mà chỉ có ràng buộc?
                // Dựa theo SQL: Bạn không tạo cột ID User trong LichDat??? 
                // À, xem kỹ lại bảng LichDat trong SQL mới của bạn: KHÔNG CÓ CỘT USERNAME/ID 
                // -> Code sẽ lỗi ở đây nếu Model EF chưa cập nhật.
                // GIẢ ĐỊNH: Bạn vẫn muốn lưu người đặt, nhưng trong SQL bạn quên cột này.
                // Tạm thời tôi comment dòng này lại để code chạy được với SQL hiện tại.
                // lich.UserName = currentUser.ID; 

                lich.NgayDat = dtpNgayDat.Value.Date;
                lich.GioBD = dtpGioBatDau.Value.Hour;
                lich.GioKT = dtpGioKetThuc.Value.Hour;
                lich.TrangThai = "Đã Đặt";

                string kq = lichDatBUS.ThemLichDat(lich, txtTenKhachHang.Text.Trim());
                if (kq == "Success")
                {
                    MessageBox.Show("Đặt thành công!");
                    LoadDGV(); CapNhatMauSacSan();
                }
                else MessageBox.Show(kq);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void LoadComboBoxSan()
        {
            cbxMaSan.DataSource = sanBongBUS.GetListSanBong();
            cbxMaSan.DisplayMember = "TenSan";
            cbxMaSan.ValueMember = "MaSan";
        }

        private void LoadDataStatic()
        {
            cbxLoaiSan.Items.Clear();
            cbxLoaiSan.Items.Add("Sân 5");
            cbxLoaiSan.Items.Add("Sân 7");
        }

        private void LoadDGV()
        {
            dgvDatSan.DataSource = lichDatBUS.GetListLichDat();
            // Ẩn cột thừa
            string[] hide = { "SanBong", "Users", "KhachHang", "HoaDons" };
            foreach (var c in hide) if (dgvDatSan.Columns[c] != null) dgvDatSan.Columns[c].Visible = false;
        }

        private void dgvDatSan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvDatSan.Rows[e.RowIndex];
                txtSDT.Text = row.Cells["SDT_KH"].Value?.ToString();
                txtTenKhachHang.Text = row.Cells["TenKH"].Value?.ToString();
                if (row.Cells["MaLich"].Value != null)
                {
                    int id = Convert.ToInt32(row.Cells["MaLich"].Value);
                    btnHuySan.Tag = id; btnXoa.Tag = id; txtMaDat.Text = id.ToString();
                }
            }
        }

        private void btnHuySan_Click(object sender, EventArgs e)
        {
            if (btnHuySan.Tag != null && MessageBox.Show("Hủy?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lichDatBUS.HuyLichDat((int)btnHuySan.Tag);
                LoadDGV(); CapNhatMauSacSan();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (btnXoa.Tag != null && MessageBox.Show("Xóa?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string kq = lichDatBUS.XoaLichDat((int)btnXoa.Tag);
                if (kq == "Success") { LoadDGV(); CapNhatMauSacSan(); }
                else MessageBox.Show(kq);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e) { this.Close(); }
        private void btnSan1_Click(object sender, EventArgs e) { }
        private void btnSan2_Click(object sender, EventArgs e) { }
        private void btnSan3_Click(object sender, EventArgs e) { }
        private void btnSan4_Click(object sender, EventArgs e) { }
        private void btnSan5_Click(object sender, EventArgs e) { }
        private void btnSan6_Click(object sender, EventArgs e) { }
        private void btnSan7_Click(object sender, EventArgs e) { }
        private void btnSua_Click(object sender, EventArgs e) { }
        private void btnThanhToan_Click(object sender, EventArgs e) { }
    }
}