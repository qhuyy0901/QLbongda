using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DAL;

namespace TrangChu
{
    public partial class CT_HoaDon_DichVu : Form
    {
        private LichDatBUS busLichDat = new LichDatBUS();
        private DichVuBUS busDichVu = new DichVuBUS();

        // ===== BIẾN LƯU TRỮ THÔNG TIN KHÁCH HÀNG =====
        private string tenKH = "";
        private string sdtKH = "";
        private string loaiKhach = "VangLai"; // VangLai hoặc DatSan
        private string maLich = "";
        private List<DAL.CT_HoaDon_DichVu> listChiTiet = new List<DAL.CT_HoaDon_DichVu>();
        private decimal tongTienDichVu = 0;
        private decimal tienSan = 0;

        // ===== FLAG KIỂM TRA THANH TOÁN THÀNH CÔNG =====
        public bool IsThanhToanThanhCong { get; set; } = false;

        public CT_HoaDon_DichVu()
        {
            InitializeComponent();
            this.Load += CT_HoaDon_DichVu_Load;
            btnThanhToan.Click += BtnThanhToan_Click;
            btnHuy.Click += BtnHuy_Click;
            radKhachVangLai.CheckedChanged += RadKhach_CheckedChanged;
            radKhachDatSan.CheckedChanged += RadKhach_CheckedChanged;
        }

        // ===== NHẬN THÔNG TIN KHÁCH HÀNG =====
        public void SetKhachHang(string ten, string sdt)
        {
            tenKH = ten;
            sdtKH = sdt;
        }

        // ===== NHẬN LOẠI KHÁCH HÀNG =====
        public void SetLoaiKhach(string loai)
        {
            loaiKhach = loai;
        }

        // ===== NHẬN MÃ LỊCH ĐẶT SÂN =====
        public void SetMaLich(string maLichDat)
        {
            maLich = maLichDat;
        }

        // ===== NHẬN CHI TIẾT DỊCH VỤ =====
        public void SetChiTietDichVu(List<DAL.CT_HoaDon_DichVu> list, decimal tongTienDV)
        {
            listChiTiet = list ?? new List<DAL.CT_HoaDon_DichVu>();
            tongTienDichVu = tongTienDV;
        }

        private void CT_HoaDon_DichVu_Load(object sender, EventArgs e)
        {
            try
            {
                // ===== THIẾT LẬP HÌNH THỨC THANH TOÁN =====
                cbxHinhThucTT.Items.Clear();
                cbxHinhThucTT.Items.Add("Tiền mặt");
                cbxHinhThucTT.Items.Add("Chuyển khoản");
                cbxHinhThucTT.Items.Add("Thẻ tín dụng");
                cbxHinhThucTT.SelectedIndex = 0;

                // ===== NGĂN CHẶN CHỈNH SỬA DATAGRIDVIEW =====
                dgvCTDichVu.ReadOnly = true;
                dgvCTDichVu.AllowUserToAddRows = false;
                dgvCTDichVu.AllowUserToDeleteRows = false;
                dgvCTDichVu.AllowUserToResizeRows = false;
                dgvCTDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // ===== HIỂN THỊ THÔNG TIN KHÁCH HÀNG =====
                HienThiThongTinKhachHang();

                // ===== HIỂN THỊ THÔNG TIN LỊCH ĐẶT (NẾU CÓ) =====
                if (loaiKhach == "DatSan" && !string.IsNullOrWhiteSpace(maLich))
                {
                    grpTienSan.Visible = true;
                    LoadThongTinLichDat();
                }
                else
                {
                    grpTienSan.Visible = false;
                    radKhachVangLai.Checked = true;
                    tienSan = 0;
                }

                // ===== HIỂN THỊ CHI TIẾT DỊCH VỤ =====
                LoadChiTietDichVu();

                // ===== CẬP NHẬT TỔNG TIỀN =====
                CapNhatTongTien();

                // ===== KHÓA TOÀN BỘ TRỊ NHẬP LIỆU =====
                KhoaTatCaControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khởi tạo form:\n" + ex.Message, 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== HIỂN THỊ THÔNG TIN KHÁCH HÀNG =====
        private void HienThiThongTinKhachHang()
        {
            try
            {
                txtTenKH.Text = tenKH;
                txtSDT.Text = sdtKH;

                if (loaiKhach == "DatSan")
                {
                    radKhachDatSan.Checked = true;
                }
                else
                {
                    radKhachVangLai.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi hiển thị khách hàng:\n" + ex.Message);
            }
        }

        // ===== LOAD THÔNG TIN LỊCH ĐẶT =====
        private void LoadThongTinLichDat()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maLich))
                {
                    return;
                }

                var lichDat = busLichDat.GetAll().FirstOrDefault(l => l.MaLich == maLich);

                if (lichDat != null)
                {
                    // ===== HIỂN THỊ THÔNG TIN SÂN =====
                    cbxMaLich.Text = lichDat.MaLich;
                    txtTenSan.Text = lichDat.MaSan;
                    txtKhungGio.Text = $"{lichDat.GioBD}:00 - {lichDat.GioKT}:00";

                    // ===== HIỂN THỊ TIỀN SÂN =====
                    tienSan = lichDat.DonGiaThucTe ?? 0;
                    txtTienSan.Text = tienSan.ToString("N0");

                    lblTongTienSan_Value.Text = tienSan.ToString("N0") + " VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi load thông tin lịch:\n" + ex.Message);
            }
        }

        // ===== LOAD CHI TIẾT DỊCH VỤ VÀO DATAGRIDVIEW =====
        private void LoadChiTietDichVu()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("MaDV", typeof(string));
                dt.Columns.Add("TenDV", typeof(string));
                dt.Columns.Add("SoLuong", typeof(int));
                dt.Columns.Add("DonGia", typeof(decimal));
                dt.Columns.Add("ThanhTien", typeof(decimal));

                // ===== LOAD DỊCH VỤ TỪ DATABASE (EAGER LOADING) =====
                var listWithDichVu = busDichVu.LoadChiTietWithDichVu(listChiTiet);

                if (listWithDichVu != null && listWithDichVu.Count > 0)
                {
                    foreach (var ct in listWithDichVu)
                    {
                        // ===== LẤY THÔNG TIN DỊCH VỤ =====
                        string tenDV = ct.DichVu?.TenDV ?? "Không xác định";
                        decimal donGia = ct.DichVu?.DonGia ?? 0;
                        int soLuong = ct.SoLuong ?? 1;
                        decimal thanhTien = ct.ThanhTien ?? 0;

                        dt.Rows.Add(ct.MaDV, tenDV, soLuong, donGia, thanhTien);
                    }
                }

                dgvCTDichVu.DataSource = dt;

                // ===== ĐỊNH DẠNG CỘT TIỀN =====
                FormatDichVuColumn();

                // ===== HIỂN THỊ TỔNG TIỀN DỊCH VỤ =====
                lblTongTienDV_Value.Text = tongTienDichVu.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi load dịch vụ:\n" + ex.Message);
            }
        }

        // ===== ĐỊNH DẠNG CỘT TIỀN =====
        private void FormatDichVuColumn()
        {
            try
            {
                if (dgvCTDichVu.Columns["DonGia"] != null)
                {
                    dgvCTDichVu.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                    dgvCTDichVu.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (dgvCTDichVu.Columns["ThanhTien"] != null)
                {
                    dgvCTDichVu.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
                    dgvCTDichVu.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch { }
        }

        // ===== CẬP NHẬT TỔNG TIỀN =====
        private void CapNhatTongTien()
        {
            try
            {
                decimal tongCong = tienSan + tongTienDichVu;
                lblTongThanhToan.Text = tongCong.ToString("N0") + " VNĐ";
            }
            catch { }
        }

        // ===== KHÓA TOÀN BỘ CONTROL (KHI ĐÃ NHẬP DỮ LIỆU) =====
        private void KhoaTatCaControl()
        {
            try
            {
                // ===== KHÓA TÁT CẢ TEXTBOX =====
                txtTenKH.ReadOnly = true;
                txtSDT.ReadOnly = true;
                txtTenSan.ReadOnly = true;
                txtKhungGio.ReadOnly = true;
                txtTienSan.ReadOnly = true;

                // ===== KHÓA RADIOBUTTON (CÓ THỂ XEM NHƯNG KHÔNG CHỈNH) =====
                radKhachVangLai.Enabled = false;
                radKhachDatSan.Enabled = false;

                // ===== KHÓA COMBOBOX =====
                cbxMaLich.Enabled = false;

                // ===== DATAAGRIDVIEW ĐÃ ĐƯỢC SET READONLY TRONG LOAD =====
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khóa control:\n" + ex.Message);
            }
        }

        // ===== SỰ KIỆN THAY ĐỔI LOẠI KHÁCH (CHỈ ĐỂ HIỂN THỊ) =====
        private void RadKhach_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radKhachDatSan.Checked)
                {
                    grpTienSan.Visible = true;
                }
                else
                {
                    grpTienSan.Visible = false;
                }
            }
            catch { }
        }

        // ===== NÚT THANH TOÁN =====
        private void BtnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                // ===== LẤY TỔNG TIỀN =====
                string tongTienText = lblTongThanhToan.Text.Replace(" VNĐ", "").Replace(",", "");
                decimal.TryParse(tongTienText, out decimal tongTien);

                string hinhThucTT = cbxHinhThucTT.SelectedItem?.ToString() ?? "Tiền mặt";

                // ===== HIỂN THỊ XÁC NHẬN =====
                string message = $"✔ XÁC NHẬN THANH TOÁN\n\n" +
                    $"Khách hàng: {tenKH}\n" +
                    $"SĐT: {sdtKH}\n" +
                    $"Hình thức TT: {hinhThucTT}\n" +
                    $"━━━━━━━━━━━━━━━━\n" +
                    $"Tổng tiền: {tongTien:N0} VNĐ\n\n" +
                    $"Bạn có muốn xác nhận?";

                DialogResult result = MessageBox.Show(message, "Xác Nhận Thanh Toán", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("✔ THANH TOÁN THÀNH CÔNG!\n\nCảm ơn bạn đã mua hàng.", 
                        "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    IsThanhToanThanhCong = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thanh toán:\n" + ex.Message);
            }
        }

        // ===== NÚT HỦY / THOÁT =====
        private void BtnHuy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn hủy thanh toán?", 
                "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                IsThanhToanThanhCong = false;
                this.Close();
            }
        }
    }
}
