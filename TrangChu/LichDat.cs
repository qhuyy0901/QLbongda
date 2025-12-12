using BUS;
using DAL; // Bắt buộc
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrangChu
{
    public partial class LichDat : Form
    {
        // --- 1. KHAI BÁO ---
        private LichDatBUS busLichDat = new LichDatBUS();
        private SanBongBUS busSanBong = new SanBongBUS();

        // List để quản lý 6 nút sân cho dễ tô màu
        private List<Button> listBtnSan = new List<Button>();

        public LichDat()
        {
            InitializeComponent();
            this.Load += LichDat_Load;
        }

        // --- 2. LOAD FORM ---
        private void LichDat_Load(object sender, EventArgs e)
        {
            try
            {
                // Gán Property cho cột
                clMaLich.DataPropertyName = "MaLich";
                clMaSan.DataPropertyName = "MaSan";
                clSDT_KH.DataPropertyName = "SDT_KH";
                clTenKH.DataPropertyName = "TenKH";
                clNgayDat.DataPropertyName = "NgayDat";
                clGioBatDau.DataPropertyName = "GioBD";
                clGioKetThuc.DataPropertyName = "GioKT";
                clTrangThai.DataPropertyName = "TrangThai";
                clDonGiaThucTe.DataPropertyName = "DonGiaThucTe";

                // Cài đặt ngày giờ mặc định
                dtpNgayDat.Format = DateTimePickerFormat.Custom;
                dtpNgayDat.CustomFormat = "dd/MM/yyyy";
                dtpNgayDat.Value = DateTime.Now;

                // Gom nút sân
                KhoiTaoNutSan();

                // Load dữ liệu
                LoadComboBoxSan();
                RefreshData();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // --- 3. CÁC HÀM XỬ LÝ GIAO DIỆN ---

        private void KhoiTaoNutSan()
        {
            listBtnSan.Clear();
            // Tag phải trùng với Mã Sân trong SQL (San1, San2...)
            SetupButton(btnSan1, "San1");
            SetupButton(btnSan2, "San2");
            SetupButton(btnSan3, "San3");
            SetupButton(btnSan4, "San4");
            SetupButton(btnSan5, "San5");
            SetupButton(btnSan6, "San6");
        }

        private void SetupButton(Button btn, string maSan)
        {
            if (btn != null)
            {
                btn.Tag = maSan;
                btn.Click -= BtnSan_Click;
                btn.Click += BtnSan_Click;
                listBtnSan.Add(btn);
            }
        }

        private void LoadComboBoxSan()
        {
            cbxMaSan.DataSource = busSanBong.GetListSanBong();
            cbxMaSan.DisplayMember = "MaSan";
            cbxMaSan.ValueMember = "MaSan";
        }

        // Hàm làm mới dữ liệu (Đồng bộ SQL lên giao diện)
        private void RefreshData()
        {
            var data = busLichDat.GetAll();

            // 👉 Popup hiện số lượng dòng lấy được từ DB
            //MessageBox.Show("Số dòng lấy được: " + data.Count);

            dgvDatSan.DataSource = null;
            dgvDatSan.DataSource = data;
        }



        // Logic tô màu: Quét lịch ngày đang chọn để xem giờ nào bận
        private void CapNhatMauSacSan()
        {
            DateTime ngayDangChon = dtpNgayDat.Value.Date;
            int gioHienTai = DateTime.Now.Hour; // Chỉ dùng cho ngày hôm nay
            bool xemHomNay = (ngayDangChon == DateTime.Today);

            // Lấy lịch của ngày đang chọn
            var listLich = busLichDat.GetByDate(ngayDangChon);

            foreach (var btn in listBtnSan)
            {
                string maSan = btn.Tag.ToString();
                bool dangDa = false;

                // Logic: Nếu đang xem ngày hôm nay, sân nào có giờ đá trùng giờ hiện tại thì ĐỎ
                if (xemHomNay)
                {
                    foreach (var item in listLich)
                    {
                        if (item.MaSan == maSan &&
                            gioHienTai >= item.GioBD && gioHienTai < item.GioKT)
                        {
                            dangDa = true;
                            break;
                        }
                    }
                }

                // Đổi màu
                if (dangDa)
                {
                    btn.BackColor = Color.Red;
                    // btn.Text = maSan + "\n(Đang đá)"; // Tùy chọn hiện chữ
                }
                else
                {
                    btn.BackColor = Color.LimeGreen;
                    // btn.Text = maSan + "\n(Trống)";
                }
            }
        }

        // Sự kiện khi click vào hình cái Sân
        private void BtnSan_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string maSan = btn.Tag.ToString();

            // Đổ thông tin lên form
            cbxMaSan.SelectedValue = maSan;
            cbxMaSan.Text = maSan;

            // Logic loại sân (Ví dụ)
            if (maSan == "San5" || maSan == "San6") cbxLoaiSan.Text = "Sân 7";
            else cbxLoaiSan.Text = "Sân 5";

            // Nếu sân đang đỏ (đang đá) -> báo bận
            if (btn.BackColor == Color.Red)
            {
                txtTrangThai.Text = "Đang đá";
            }
            else
            {
                txtTrangThai.Text = "Trống";
                // Reset form để sẵn sàng đặt mới
                txtMaDat.Clear();
                txtTenKhachHang.Clear();
                txtSDT.Clear();
                // Tự tính tiền (nếu có hàm)
            }
        }

        // --- 4. CÁC NÚT CHỨC NĂNG (CRUD) ---

        private void btnDatSAn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSDT.Text) || string.IsNullOrEmpty(cbxMaSan.Text))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin!");
                    return;
                }

                // Tạo đối tượng Dữ liệu (DAL)
                DAL.LichDat lich = new DAL.LichDat();

                lich.MaSan = cbxMaSan.Text;
                lich.SDT_KH = txtSDT.Text;
                lich.TenKH = txtTenKhachHang.Text;
                lich.NgayDat = dtpNgayDat.Value;
                lich.GioBD = dtpGioBatDau.Value.Hour;
                lich.GioKT = dtpGioKetThuc.Value.Hour;
                lich.TrangThai = "Đã đặt"; // Hoặc lấy từ txtTrangThai.Text

                decimal gia = 0;
                decimal.TryParse(cbxDonGia.Text.Replace(",", ""), out gia);
                lich.DonGiaThucTe = gia;

                if (busLichDat.Insert(lich))
                {
                    MessageBox.Show("Đặt sân thành công!");
                    RefreshData(); // Đồng bộ lại SQL và Giao diện
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Thất bại! Sân đã kín giờ này.");
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaDat.Text)) return;

                // Tạo đối tượng cập nhật
                DAL.LichDat lich = new DAL.LichDat();
                lich.MaLich = txtMaDat.Text;

                // Các thông tin cần sửa
                lich.MaSan = cbxMaSan.Text;
                lich.SDT_KH = txtSDT.Text;
                lich.TenKH = txtTenKhachHang.Text;
                lich.NgayDat = dtpNgayDat.Value;
                lich.GioBD = dtpGioBatDau.Value.Hour;
                lich.GioKT = dtpGioKetThuc.Value.Hour;
                lich.TrangThai = txtTrangThai.Text;

                decimal gia = 0;
                decimal.TryParse(cbxDonGia.Text, out gia);
                lich.DonGiaThucTe = gia;

                if (busLichDat.Update(lich))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    RefreshData();
                    ResetForm();
                }
                else MessageBox.Show("Cập nhật thất bại!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaDat.Text))
                {
                    MessageBox.Show("Chọn lịch cần xóa!");
                    return;
                }

                if (MessageBox.Show("Bạn muốn xóa lịch này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = int.Parse(txtMaDat.Text);
                    if (busLichDat.Delete(id))
                    {
                        MessageBox.Show("Đã xóa!");
                        RefreshData();
                        ResetForm();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnHuySan_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetForm()
        {
            txtMaDat.Clear();
            txtTenKhachHang.Clear();
            txtSDT.Clear();
            cbxMaSan.SelectedIndex = -1;
            cbxDonGia.Text = "";
            txtTrangThai.Text = "";
        }

        // Sự kiện đổi ngày -> Cập nhật lại màu sân
        private void dtpNgayDat_ValueChanged(object sender, EventArgs e)
        {
            CapNhatMauSacSan();
        }

        // Click vào bảng -> Đổ dữ liệu lên ô nhập
        private void dgvDatSan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu click vào dòng tiêu đề (Index < 0) thì không làm gì
            if (e.RowIndex < 0) return;

            try
            {
                // Lấy dòng hiện tại đang chọn
                DataGridViewRow row = dgvDatSan.Rows[e.RowIndex];

                // Đổ dữ liệu lên các ô nhập
                // Kiểm tra null trước khi ToString() để tránh lỗi
                if (row.Cells["clMaLich"].Value != null)
                    txtMaDat.Text = row.Cells["clMaLich"].Value.ToString();

                if (row.Cells["clMaSan"].Value != null)
                {
                    cbxMaSan.Text = row.Cells["clMaSan"].Value.ToString();
                    cbxMaSan.SelectedValue = row.Cells["clMaSan"].Value.ToString(); // Chọn item trong combo
                }

                if (row.Cells["clSDT_KH"].Value != null)
                    txtSDT.Text = row.Cells["clSDT_KH"].Value.ToString();

                if (row.Cells["clTenKH"].Value != null)
                    txtTenKhachHang.Text = row.Cells["clTenKH"].Value.ToString();

                if (row.Cells["clDonGiaThucTe"].Value != null)
                {
                    // Format tiền cho đẹp (VD: 200,000)
                    decimal gia = Convert.ToDecimal(row.Cells["clDonGiaThucTe"].Value);
                    cbxDonGia.Text = gia.ToString("N0");
                }

                if (row.Cells["clTrangThai"].Value != null)
                    txtTrangThai.Text = row.Cells["clTrangThai"].Value.ToString();

                // Xử lý ngày giờ (Cần cẩn thận vì format ngày tháng)
                if (row.Cells["clNgayDat"].Value != null)
                    dtpNgayDat.Value = Convert.ToDateTime(row.Cells["clNgayDat"].Value);

                // Lưu ý: DateTimePicker không set được giờ riêng lẻ dễ dàng nếu format là ngày
                // Bạn có thể cần xử lý thêm nếu muốn hiển thị giờ lên NumericUpDown hoặc ComboBox giờ
            }
            catch (Exception ex)
            {
                // Bỏ qua lỗi nhỏ khi click nhầm vùng
            }
        }

        private void dgvDatSan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}