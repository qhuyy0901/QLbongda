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
        private void TuTinhDonGia()
        {
            if (string.IsNullOrEmpty(cbxMaSan.Text)) return;

            DateTime ngay = dtpNgayDat.Value.Date;
            int gioBD = dtpGioBatDau.Value.Hour;
            int gioKT = dtpGioKetThuc.Value.Hour;

            if (gioBD >= gioKT)
            {
                txtDonGia.Text = "";
                return;
            }

            int soGio = gioKT - gioBD;
            decimal donGia1Gio;

            // Giá theo ngày
            if (ngay.DayOfWeek == DayOfWeek.Saturday ||
                ngay.DayOfWeek == DayOfWeek.Sunday)
            {
                donGia1Gio = 199000;
            }
            else
            {
                donGia1Gio = 149000;
            }

            // +50k nếu là sân 7 người (San5, San6)
            if (cbxMaSan.Text == "San5" || cbxMaSan.Text == "San6")
            {
                donGia1Gio += 50000;
            }

            decimal tongTien = soGio * donGia1Gio;
            txtDonGia.Text = tongTien.ToString("N0");
        }

        // --- 2. LOAD FORM ---
        private void LichDat_Load(object sender, EventArgs e)
        {
            try
            {
                // ===== 1. GÁN PROPERTY CHO DATAGRIDVIEW =====
                clMaLich.DataPropertyName = "MaLich";
                clMaSan.DataPropertyName = "MaSan";
                clSDT_KH.DataPropertyName = "SDT_KH";
                clTenKH.DataPropertyName = "TenKH";
                clNgayDat.DataPropertyName = "NgayDat";
                clGioBatDau.DataPropertyName = "GioBD";
                clGioKetThuc.DataPropertyName = "GioKT";
                clTrangThai.DataPropertyName = "TrangThai";
                clDonGiaThucTe.DataPropertyName = "DonGiaThucTe";

                // ===== 2. SET DATE PICKER (NGÀY) =====
                dtpNgayDat.Format = DateTimePickerFormat.Custom;
                dtpNgayDat.CustomFormat = "dd/MM/yyyy";
                dtpNgayDat.Value = DateTime.Now;

                // ===== 3. FIX GIỜ ĐẶT SÂN (CỰC KỲ QUAN TRỌNG) =====
                dtpGioBatDau.Format = DateTimePickerFormat.Custom;
                dtpGioBatDau.CustomFormat = "HH:00";
                dtpGioBatDau.ShowUpDown = true;

                dtpGioKetThuc.Format = DateTimePickerFormat.Custom;
                dtpGioKetThuc.CustomFormat = "HH:00";
                dtpGioKetThuc.ShowUpDown = true;

                // Giờ mặc định
                dtpGioBatDau.Value = DateTime.Today.AddHours(7);
                dtpGioKetThuc.Value = DateTime.Today.AddHours(9);

                // ===== 4. GOM NÚT SÂN =====
                KhoiTaoNutSan();

                // ===== 5. LOAD DỮ LIỆU =====
                LoadComboBoxSan();
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
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



            // Nếu sân đang đỏ (đang đá) -> báo bận
            if (btn.BackColor == Color.Red)
            {
                //txtTrangThai.Text = "Đang đá";
            }
            else
            {
                //txtTrangThai.Text = "Trống";
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
            if (string.IsNullOrWhiteSpace(txtSDT.Text) || string.IsNullOrWhiteSpace(cbxMaSan.Text))
            {
                MessageBox.Show("Nhập thiếu thông tin!");
                return;
            }

            int gioBD = dtpGioBatDau.Value.Hour;
            int gioKT = dtpGioKetThuc.Value.Hour;

            if (gioBD >= gioKT)
            {
                MessageBox.Show("Giờ kết thúc phải lớn hơn giờ bắt đầu!");
                return;
            }
            //string maLich = "LD" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DAL.LichDat lich = new DAL.LichDat
            {
                MaLich = txtMaDat.Text.Trim(),
                MaSan = cbxMaSan.Text.Trim(),
                SDT_KH = txtSDT.Text.Trim(),
                TenKH = txtTenKhachHang.Text.Trim(),
                NgayDat = dtpNgayDat.Value.Date,
                GioBD = dtpGioBatDau.Value.Hour,
                GioKT = dtpGioKetThuc.Value.Hour,
                TrangThai = "Đã đặt",
                DonGiaThucTe = decimal.TryParse(txtDonGia.Text, out decimal gia) ? gia : 0
            };

            Log("===== DATA GỬI XUỐNG DB =====");
            Log($"MaLich: {lich.MaLich}");
            Log($"MaSan: {lich.MaSan}");
            Log($"SDT: {lich.SDT_KH}");
            Log($"TenKH: {lich.TenKH}");
            Log($"NgayDat: {lich.NgayDat:dd/MM/yyyy}");
            Log($"GioBD: {lich.GioBD}");
            Log($"GioKT: {lich.GioKT}");
            Log($"DonGia: {lich.DonGiaThucTe}");
            Log("=============================");

            try
            {
                Log("▶ Bắt đầu Insert lịch đặt");

                if (busLichDat.Insert(lich))
                {
                    Log("✔ Insert thành công");

                    RefreshData();
                    CapNhatMauSacSan();
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                Log("❌ LỖI: " + ex.Message);
            }

        }

        private void Log(string message)
        {
            rtbLog.AppendText(
                $"[{DateTime.Now:HH:mm:ss}] {message}\n"
            );
            rtbLog.ScrollToCaret();
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
                //lich.TrangThai = txtTrangThai.Text;

                decimal gia = 0;
                //decimal.TryParse(cbxDonGia.Text, out gia);
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
            txtDonGia.Text = "";
            //txtTrangThai.Text = "";
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
                    //cbxDonGia.Text = gia.ToString("N0");
                }

                //if (row.Cells["clTrangThai"].Value != null)
                //    txtTrangThai.Text = row.Cells["clTrangThai"].Value.ToString();

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