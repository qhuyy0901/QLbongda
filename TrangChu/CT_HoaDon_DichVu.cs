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
            cbxHinhThucTT.SelectedIndexChanged += CbxHinhThucTT_SelectedIndexChanged;
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
            try
            {
                cbxHinhThucTT.Items.Clear();
                cbxHinhThucTT.Items.Add("Tiền mặt");
                cbxHinhThucTT.Items.Add("Chuyển khoản");
                cbxHinhThucTT.SelectedIndex = 0;

                dgvCTDichVu.ReadOnly = true;
                dgvCTDichVu.AllowUserToAddRows = false;
                dgvCTDichVu.AllowUserToDeleteRows = false;
                dgvCTDichVu.AllowUserToResizeRows = false;
                dgvCTDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // ===== LOAD DANH SÁCH LỊCH ĐẶT VÀO COMBOBOX =====
                LoadDanhSachLichDat();

                HienThiThongTinKhachHang();

                if (!string.IsNullOrWhiteSpace(maLich))
                {
                    grpTienSan.Visible = true;
                    LoadThongTinLichDat();
                }
                else
                {
                    grpTienSan.Visible = false;
                    tienSan = 0;
                }

                LoadChiTietDichVu();

                CapNhatTongTien();

                KhoaTatCaControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khởi tạo form:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== LOAD DANH SÁCH LỊCH ĐẶT TỪngày hiện tại trở đi =====
        private void LoadDanhSachLichDat()
        {
            try
            {
                // ===== HỦY SỰ KIỆN CŨ NẾU ĐÃ CÓ =====
                cbxMaLich.SelectedIndexChanged -= CbxMaLich_SelectedIndexChanged;

                DateTime homNay = DateTime.Now.Date;

                // ===== LẤY TẤT CẢ LỊCH ĐẶT =====
                var allLichs = busLichDat.GetAll();

                // ===== LỌC LỊCH ĐẶT CÓ TRẠNG THÁI "ĐÃ ĐẶT" VÀ NGÀY >= HÔM NAY =====
                var filteredLichs = allLichs
   ?.Where(l => l.TrangThai == "Đã đặt" && l.NgayDat.HasValue && l.NgayDat.Value.Date >= homNay)
   .OrderBy(l => l.NgayDat)
.ThenBy(l => l.GioBD)
     .ToList() ?? new List<DAL.LichDat>();

        // ===== NẾU KHÔNG CÓ LỊCH NÀO THÌ HIỂN THỊ THÔNG BÁO =====
          if (filteredLichs.Count == 0)
      {
         MessageBox.Show("⚠️ Không có lịch đặt nào từ hôm nay trở đi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
          cbxMaLich.DataSource = null;
 return;
       }

         // ===== TẠO DANH SÁCH HIỂN THỊ =====
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
      // Nếu không tìm thấy thì chọn lịch đầu tiên
       cbxMaLich.SelectedIndex = 0;
 }
           }
   else if (cbxMaLich.Items.Count > 0)
     {
     cbxMaLich.SelectedIndex = 0;
         }

     // ===== THÊM SỰ KIỆN CHANGE LẦN DUY NHẤT =====
                cbxMaLich.SelectedIndexChanged += CbxMaLich_SelectedIndexChanged;
     }
  catch (Exception ex)
   {
   MessageBox.Show("❌ Lỗi load danh sách lịch đặt:\n" + ex.Message,
            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
       }
    }

        // ===== SỰ KIỆN KHI CHỌN LỊCH TRONG COMBOBOX =====
        private void CbxMaLich_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbxMaLich.SelectedValue == null || string.IsNullOrWhiteSpace(cbxMaLich.SelectedValue.ToString()))
                    return;

                string selectedMaLich = cbxMaLich.SelectedValue.ToString();

                // ===== CẬP NHẬT MALÍCH =====
                maLich = selectedMaLich;

                // ===== LOAD THÔNG TIN LỊCH =====
                LoadThongTinLichDat();

                // ===== CẬP NHẬT TỔNG TIỀN =====
                CapNhatTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi chọn lịch:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiThongTinKhachHang()
        {
            try
            {
                txtTenKH.Text = tenKH;
                txtSDT.Text = sdtKH;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi hiển thị khách hàng:\n" + ex.Message);
            }
        }

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
                    cbxMaLich.Text = lichDat.MaLich;
                    txtTenSan.Text = lichDat.MaSan;
                    txtKhungGio.Text = $"{lichDat.GioBD}:00 - {lichDat.GioKT}:00";

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
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi load dịch vụ:\n" + ex.Message);
            }
        }

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

        private void CapNhatTongTien()
        {
            try
            {
                decimal tongCong = tienSan + tongTienDichVu;
                lblTongThanhToan.Text = tongCong.ToString("N0") + " VNĐ";
            }
            catch { }
        }

        private void KhoaTatCaControl()
        {
            try
            {
                txtTenKH.ReadOnly = true;
                txtSDT.ReadOnly = true;
                txtTenSan.ReadOnly = true;
                txtKhungGio.ReadOnly = true;
                txtTienSan.ReadOnly = true;
                // ===== KHÔNG KHÓA COMBOBOX LỊCH ĐẶT - CHO PHÉP CHỌN =====
                cbxMaLich.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khóa control:\n" + ex.Message);
            }
        }

        private void CbxHinhThucTT_SelectedIndexChanged(object sender, EventArgs e)
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
                string tongTienText = lblTongThanhToan.Text.Replace(" VNĐ", "").Replace(",", "");
                decimal.TryParse(tongTienText, out decimal tongTien);

                string hinhThucTT = cbxHinhThucTT.SelectedItem?.ToString() ?? "Tiền mặt";

                string message = $"✔ XÁC NHẬN THANH TOÁN\n\n" +
                    $"Hình thức TT: {hinhThucTT}\n" +
                    $"Tổng tiền: {tongTien:N0} VNĐ\n\n" +
                    $"Bạn có muốn xác nhận?";

                DialogResult result = MessageBox.Show(message, "Xác Nhận Thanh Toán",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // ===== ĐẢM BẢO MALÍCH CÓ GIÁ TRỊ HỢP LỆ =====
                    string maLichHD = string.IsNullOrWhiteSpace(maLich) ? null : maLich;

                    // ===== TẠO HÓA ĐƠN MỚI KHÔNG CÓ REFERENCE ĐẾN CONTEXT CŨ =====
                    DAL.HoaDon hoaDon = new DAL.HoaDon
                    {
                        MaLich = maLichHD,
                        TongTien = tongTien,
                        ThoiGianThanhToan = DateTime.Now,
                        HinhThucTT = hinhThucTT
                    };

                    // ===== TẠO DANH SÁCH CHI TIẾT MỚI KHÔNG LIÊN KẾT ĐẾN CONTEXT CŨ =====
                    List<DAL.CT_HoaDon_DichVu> listChiTietNew = new List<DAL.CT_HoaDon_DichVu>();

                    foreach (var item in listChiTiet)
                    {
                        // ===== TẠỌ OBJECT MỚI KHÔNG CÓ ENTITY TRACKING =====
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
                        MessageBox.Show("✔ THANH TOÁN THÀNH CÔNG!\n\n💰 Hóa đơn đã được lưu vào hệ thống.",
                            "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        IsThanhToanThanhCong = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("❌ Lỗi lưu hóa đơn vào database!\n\nVui lòng thử lại.",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thanh toán:\n" + ex.Message);
            }
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
    }
}