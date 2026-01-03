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
        private BUS.LichDatBUS busLichDat = new BUS.LichDatBUS();
        private DichVuBUS busDichVu = new DichVuBUS();
        private HoaDonBUS busHoaDon = new HoaDonBUS();

        private string tenKH = "";
        private string sdtKH = "";
        private string maLich = "";
        private List<DAL.CT_HoaDon_DichVu> listChiTiet = new List<DAL.CT_HoaDon_DichVu>();
        private decimal tongTienDichVu = 0;
        private decimal tienSan = 0;

        public bool IsThanhToanThanhCong { get; set; } = false;

        public CT_HoaDon_DichVu()
        {
            InitializeComponent();
            this.Load += CT_HoaDon_DichVu_Load;
            btnThanhToan.Click += BtnThanhToan_Click;
            btnHuy.Click += BtnHuy_Click;
        }

        public void SetKhachHang(string ten, string sdt)
        {
            tenKH = ten;
            sdtKH = sdt;
        }

        public void SetMaLich(string maLichDat)
        {
            maLich = maLichDat;
        }

        public void SetChiTietDichVu(List<DAL.CT_HoaDon_DichVu> list, decimal tongTienDV)
        {
            listChiTiet = list ?? new List<DAL.CT_HoaDon_DichVu>();
            tongTienDichVu = tongTienDV;
        }

        private void CT_HoaDon_DichVu_Load(object sender, EventArgs e)
        {

                LoadDanhSachLichDat();
                HienThiThongTinKhachHang();
                if (!string.IsNullOrWhiteSpace(maLich))
                {
                    grpTienSan.Visible = true;
                    LoadThongTinLichDat();
                }else{
                    grpTienSan.Visible = false;
                    tienSan = 0;
                }
                LoadChiTietDichVu();
                CapNhatTongTien();
        }

        private void LoadDanhSachLichDat()
        {
            try
            {
                cbxMaLich.SelectedIndexChanged -= CbxMaLich_SelectedIndexChanged;

                DateTime homNay = DateTime.Now.Date;

                var allLichs = busLichDat.GetAll();

                var filteredLichs = allLichs?.Where(l => l.TrangThai == "Đã đặt" && l.NgayDat.HasValue && l.NgayDat.Value.Date >= homNay).OrderBy(l => l.NgayDat).ThenBy(l => l.GioBD).ToList() ?? new List<DAL.LichDat>();

                if (filteredLichs.Count == 0)
                {
                    MessageBox.Show("⚠️ Không có lịch đặt nào từ hôm nay trở đi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbxMaLich.DataSource = null; return;
                }

                var displayList = filteredLichs.Select(l => new
                {
                    MaLich = l.MaLich,
                    Display = $"{l.MaLich} - {l.MaSan} ({l.NgayDat:dd/MM/yyyy}) {l.GioBD}:00-{l.GioKT}:00 ({l.TenKH})"
                }).ToList();
                cbxMaLich.DataSource = displayList;
                cbxMaLich.DisplayMember = "Display";
                cbxMaLich.ValueMember = "MaLich";

                // ===== NẾU ĐÃ CÓ MALÍCH ĐƯỢC SET SẴN THÌ CHỌN LẠI =====
                if (!string.IsNullOrWhiteSpace(maLich))
                {
                    // Tìm xem MaLich có tồn tại trong danh sách không
                    var existingLich = displayList.FirstOrDefault(l => l.MaLich == maLich);
                    if (existingLich != null)
                    {
                        cbxMaLich.SelectedValue = maLich;
                    }
                    else
                    {
                        cbxMaLich.SelectedIndex = 0;
                    }
                }
                else if (cbxMaLich.Items.Count > 0)
                {
                    cbxMaLich.SelectedIndex = 0;
                }
                cbxMaLich.SelectedIndexChanged += CbxMaLich_SelectedIndexChanged;
            }
            catch (Exception ex){ }
        }

        // ===== SỰ KIỆN KHI CHỌN LỊCH TRONG COMBOBOX =====
        private void CbxMaLich_SelectedIndexChanged(object sender, EventArgs e)
        {
                if (cbxMaLich.SelectedValue == null || string.IsNullOrWhiteSpace(cbxMaLich.SelectedValue.ToString()))
                    return;
                string selectedMaLich = cbxMaLich.SelectedValue.ToString();
                maLich = selectedMaLich;
                LoadThongTinLichDat();
                CapNhatTongTien();          
        }

        private void HienThiThongTinKhachHang()
        {
                txtTenKH.Text = tenKH;
                txtSDT.Text = sdtKH;
        }

        private void LoadThongTinLichDat()
        {

                if (string.IsNullOrWhiteSpace(maLich))
                {
                    return;
                }

                var lichDat = busLichDat.GetAll().FirstOrDefault(l => l.MaLich == maLich);

                if (lichDat != null)
                {
                    cbxMaLich.Text = lichDat.MaLich;
                    txtTenSan.Text = lichDat.MaSan;
                    txtKhungGio.Text = $"{lichDat.GioBD}:00 - {lichDat.GioKT}:00";

                    tienSan = lichDat.DonGiaThucTe ?? 0;
                    txtTienSan.Text = tienSan.ToString("N0");

                    lblTongTienSan_Value.Text = tienSan.ToString("N0") + " VNĐ";
                }

        }

        private void LoadChiTietDichVu()
        {

                DataTable dt = new DataTable();
                dt.Columns.Add("MaDV", typeof(string));
                dt.Columns.Add("TenDV", typeof(string));
                dt.Columns.Add("SoLuong", typeof(int));
                dt.Columns.Add("DonGia", typeof(decimal));
                dt.Columns.Add("ThanhTien", typeof(decimal));

                var listWithDichVu = busDichVu.LoadChiTietWithDichVu(listChiTiet);

                if (listWithDichVu != null && listWithDichVu.Count > 0)
                {
                    foreach (var ct in listWithDichVu)
                    {
                        string tenDV = ct.DichVu?.TenDV ?? "Không xác định";
                        decimal donGia = ct.DichVu?.DonGia ?? 0;
                        int soLuong = ct.SoLuong ?? 1;
                        decimal thanhTien = ct.ThanhTien ?? 0;

                        dt.Rows.Add(ct.MaDV, tenDV, soLuong, donGia, thanhTien);
                    }
                }
                dgvCTDichVu.DataSource = dt;
                FormatDichVuColumn();
                lblTongTienDV_Value.Text = tongTienDichVu.ToString("N0") + " VNĐ";

        }

        private void FormatDichVuColumn()
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

        private void CapNhatTongTien()
        {
            try
            {
                decimal tongCong = tienSan + tongTienDichVu;
                lblTongThanhToan.Text = tongCong.ToString("N0") + " VNĐ";
            }
            catch { }
        }

       

       

        private void HienThiQRCode()
        {
            try
            {
                decimal tongTien = tienSan + tongTienDichVu;

                try
                {
                    Bitmap qrBitmap = BUS.QRCodeHelper.GenerateQRCode(tongTien, "Thanh toan dich vu");
                    picQRCode.Image = qrBitmap;
                    picQRCode.Visible = true;
                    lblQRCode.Visible = true;
                }
                catch (Exception qrEx)
                {
                    MessageBox.Show("⚠️ Không thể tạo mã QR. Lỗi: " + qrEx.Message,
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    picQRCode.Visible = false;
                    lblQRCode.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi hiển thị QR Code:\n" + ex.Message);
            }
        }

        private void BtnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                // ===== KIỂM TRA LỊCH ĐẶT TRƯỚC KHI THANH TOÁN =====
                if (string.IsNullOrWhiteSpace(maLich))
                {
                    MessageBox.Show("❌ Vui lòng chọn lịch đặt trước khi thanh toán!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string trangThaiHienTai = busHoaDon.GetTrangThaiLichDat(maLich);
                if (trangThaiHienTai != "Đã đặt")
                {
                    MessageBox.Show($"❌ Không thể thanh toán!\n\n" +$"💡 Chỉ có thể thanh toán lịch ở trạng thái 'Đã đặt'.","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string tongTienText = lblTongThanhToan.Text.Replace(" VNĐ", "").Replace(",", "");
                decimal.TryParse(tongTienText, out decimal tongTien);

                string hinhThucTT = cbxHinhThucTT.SelectedItem?.ToString() ?? "Tiền mặt";

                string message = $"✔ XÁC NHẬN THANH TOÁN\n\n" + $"Mã lịch: {maLich}\n" +$"Hình thức TT: {hinhThucTT}\n" + $"Tổng tiền: {tongTien:N0} VNĐ\n\n" + $"Bạn có muốn xác nhận?";

                DialogResult result = MessageBox.Show(message, "Xác Nhận Thanh Toán",MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DAL.HoaDon hoaDon = new DAL.HoaDon
                    {
                        MaLich = maLich,
                        TongTien = tongTien,
                        ThoiGianThanhToan = DateTime.Now,
                        HinhThucTT = hinhThucTT
                    };

                    List<DAL.CT_HoaDon_DichVu> listChiTietNew = new List<DAL.CT_HoaDon_DichVu>();

                    foreach (var item in listChiTiet)
                    {
                        listChiTietNew.Add(new DAL.CT_HoaDon_DichVu
                        {
                            MaCT = item.MaCT,
                            MaDV = item.MaDV,
                            SoLuong = item.SoLuong,
                            ThanhTien = item.ThanhTien
                        });
                    }

                    bool success = busHoaDon.ThanhToan(hoaDon, listChiTietNew);

                    if (success)
                    {
                       
                        // ===== GỬI EMAIL NẾU CÓ =====
                        string emailKH = txtEmaill.Text.Trim();
                        if (!string.IsNullOrWhiteSpace(emailKH))
                        {
                            try
                            {
                                bool emailSent = EmailBUS.SendPaymentNotification(
                                    toEmail: emailKH,
                                    tenKH: tenKH,
                                    maLich: maLich,
                                    maSan: txtTenSan.Text,
                                    khungGio: txtKhungGio.Text,
                                    tienSan: tienSan,
                                    tienDichVu: tongTienDichVu,
                                    tongTien: tongTien,
                                    hinhThucTT: hinhThucTT,
                                    thoiGianTT: DateTime.Now
                                );

                                if (emailSent)
                                {
                                    MessageBox.Show($"📧 Email xác nhận đã được gửi đến:\n{emailKH}",
                                        "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show($"⚠️ Lỗi gửi email đến {emailKH}.\n\n💡 Có thể do email không hợp lệ hoặc lỗi kết nối mạng.",
                                        "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            catch (Exception emailEx)
                            {
                                MessageBox.Show($"⚠️ Lỗi gửi email: {emailEx.Message}",
                                    "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        IsThanhToanThanhCong = true;
                        this.Close();
                    }
                    else
                    {
                        // ✅ KIỂM TRA LẠI TRẠNG THÁI ĐỂ XÁC ĐỊNH NGUYÊN NHÂN
                        string trangThaiSau = busHoaDon.GetTrangThaiLichDat(maLich);
                        
                        string errorDetail = "";
                        if (trangThaiSau == "Đã thanh toán" || trangThaiSau == "Hoàn Thành")
                        {
                            errorDetail = $"\n\n💡 Lịch đặt này có thể đã được thanh toán trước đó!";
                        }
                        else if (trangThaiSau == "Đã hủy")
                        {
                            errorDetail = $"\n\n💡 Lịch đặt này đã bị hủy!";
                        }
                        
                        MessageBox.Show($"❌ Lỗi lưu hóa đơn vào database!{errorDetail}\n\nVui lòng kiểm tra lại và thử lại.",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex){}
        }

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


        private void cbxHinhThucTT_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                string hinhThuc = cbxHinhThucTT.SelectedItem?.ToString() ?? "";

                if (hinhThuc == "Chuyển khoản")
                {
                    HienThiQRCode();
                }
                else
                {
                    picQRCode.Visible = false;
                    lblQRCode.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi chọn hình thức thanh toán:\n" + ex.Message);
            }
        }

        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
    }
}