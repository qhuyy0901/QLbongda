using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TrangChu
{
    public partial class DichVu : Form
    {
        private DichVuBUS busDichVu = new DichVuBUS();
        private LichDatBUS busLichDat = new LichDatBUS();

        private User currentLoggedInUser;

        private DataTable dtDichVu = new DataTable();
        private DataTable dtGioHang = new DataTable();

        private List<LichDat> listLichDat = new List<LichDat>();

        public DichVu()
        {
            InitializeComponent();
            this.Load += DichVu_Load;
        }

        // ===== NHẬN USER TỪ FORM TRANG CHỦ =====
        public void SetCurrentUser(User user)
        {
            currentLoggedInUser = user;
        }

        private void DichVu_Load(object sender, EventArgs e)
        {
            try
            {
                InitTable();
                LoadDichVu();
                LoadLichDat();
                SetupEventHandlers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khởi tạo form Dịch vụ:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== THIẾT LẬP SỰ KIỆN =====
        private void SetupEventHandlers()
        {
            try
            {
                rdoKhachLe.CheckedChanged += RdoKhach_CheckedChanged;
                rdoKhachDatSan.CheckedChanged += RdoKhach_CheckedChanged;
                cbxLichDat.SelectedIndexChanged += CbxLichDat_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thiết lập sự kiện:\n" + ex.Message);
            }
        }

        // ===== SỰ KIỆN THAY ĐỔI LOẠI KHÁCH =====
        private void RdoKhach_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoKhachDatSan.Checked)
                {
                    // ===== KHÁCH ĐẶT SÂN: KHÓA NHẬP, MỞ COMBOBOX =====
                    cbxLichDat.Enabled = true;
                    txtTenKH.ReadOnly = true;
                    txtSDT.ReadOnly = true;
                    txtTenKH.BackColor = System.Drawing.Color.LightGray;
                    txtSDT.BackColor = System.Drawing.Color.LightGray;
                }
                else
                {
                    // ===== KHÁCH VÃNG LAI: MỞ NHẬP, KHÓA COMBOBOX =====
                    cbxLichDat.Enabled = false;
                    txtTenKH.ReadOnly = false;
                    txtSDT.ReadOnly = false;
                    txtTenKH.BackColor = System.Drawing.Color.White;
                    txtSDT.BackColor = System.Drawing.Color.White;
                    cbxLichDat.SelectedIndex = -1;
                    txtTenKH.Clear();
                    txtSDT.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thay đổi loại khách:\n" + ex.Message);
            }
        }

        // ===== LOAD DANH SÁCH LỊCH ĐẶT =====
        private void LoadLichDat()
        {
            try
            {
                // ===== LẤY DANH SÁCH LỊCH ĐẶT TỪ DATABASE (CHỈ NGÀY HÔM NAY TRỞ ĐI) =====
                List<DAL.LichDat> allLichDat = busLichDat.GetAll();
                
                // ===== LỌC CHỈ LẤY LỊCH TỪ NGÀY HÔM NAY TRỞ ĐI, CHỈ LẤY LỊCH "ĐÃ ĐẶT" =====
                DateTime homNay = DateTime.Now.Date;
                List<DAL.LichDat> lichDatList = allLichDat
                    .Where(l => l.NgayDat.HasValue && 
                                l.NgayDat.Value >= homNay && 
                                l.TrangThai == "Đã đặt")
                    .OrderBy(l => l.NgayDat)
                    .ThenBy(l => l.GioBD)
                    .ToList();

                // ===== THÊM PROPERTY TẠMTHỜI HIỂN THỊ =====
                foreach (var lich in lichDatList)
                {
                    lich.ThongTinLich = FormatLichDatDisplay(lich);
                }

                // ===== THIẾT LẬP COMBOBOX =====
                cbxLichDat.DataSource = null;
                cbxLichDat.Items.Clear();
                cbxLichDat.DisplayMember = "ThongTinLich";
                cbxLichDat.ValueMember = "MaLich";
                cbxLichDat.DataSource = lichDatList;

                if (lichDatList.Count == 0)
                {
                    MessageBox.Show("⚠️ Không có lịch đặt nào từ hôm nay trở đi!", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbxLichDat.Enabled = false;
                }
                else
                {
                    cbxLichDat.Enabled = true;
                    cbxLichDat.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi tải danh sách lịch đặt:\n" + ex.Message, 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== ĐỊNH DẠNG HIỂN THỊ LỊCH ĐẶT =====
        private string FormatLichDatDisplay(DAL.LichDat lich)
        {
            try
            {
                if (lich == null)
                    return "";

                string ngayDat = lich.NgayDat.HasValue ? lich.NgayDat.Value.ToString("dd/MM/yyyy") : "N/A";
                string gioChoi = $"{lich.GioBD}:00 - {lich.GioKT}:00";
                
                return $"[{lich.MaLich}] {lich.TenKH} | {lich.SDT_KH} | {ngayDat} | {gioChoi} | {lich.MaSan}";
            }
            catch
            {
                return lich.MaLich;
            }
        }

        // ===== SỰ KIỆN CHỌN LỊCH ĐẶT =====
        private void CbxLichDat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbxLichDat.SelectedIndex < 0)
                {
                    txtTenKH.Clear();
                    txtSDT.Clear();
                    return;
                }

                // ===== LẤY LỊCH ĐẶT ĐƯỢC CHỌN =====
                var selectedLich = cbxLichDat.SelectedItem as DAL.LichDat;

                if (selectedLich != null)
                {
                    // ===== TỰ ĐỘNG ĐIỀN THÔNG TIN KHÁCH HÀNG (KHÔNG CÓ THẺ SỬA) =====
                    txtTenKH.Text = selectedLich.TenKH ?? "";
                    txtSDT.Text = selectedLich.SDT_KH ?? "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi lấy thông tin lịch:\n" + ex.Message, 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= TABLE =================
        private void InitTable()
        {
            try
            {
                // ===== XÓA TOÀN BỘ CỘT CŨ TRƯỚC KHI THÊM CỘT MỚI =====
                dtDichVu.Columns.Clear();
                dtDichVu.Rows.Clear();

                // DỊCH VỤ
                dtDichVu.Columns.Add("MaDV", typeof(string));
                dtDichVu.Columns.Add("TenDV", typeof(string));
                dtDichVu.Columns.Add("DonGia", typeof(decimal));
                dgvDichVu.DataSource = dtDichVu;

                // ===== XÓA TOÀN BỘ CỘT CŨ TRƯỚC KHI THÊM CỘT MỚI =====
                dtGioHang.Columns.Clear();
                dtGioHang.Rows.Clear();

                // GIỎ HÀNG
                dtGioHang.Columns.Add("MaDV", typeof(string));
                dtGioHang.Columns.Add("TenDV", typeof(string));
                dtGioHang.Columns.Add("DonGia", typeof(decimal));
                dtGioHang.Columns.Add("SoLuong", typeof(int));
                dtGioHang.Columns.Add("ThanhTien", typeof(decimal), "DonGia * SoLuong");
                dgvGioHang.DataSource = dtGioHang;

                // ===== CẤU HÌNH DATAGRIDVIEW DỊCH VỤ - KHÓA TOÀN BỘ =====
                dgvDichVu.ReadOnly = true;
                dgvDichVu.AllowUserToAddRows = false;
                dgvDichVu.AllowUserToDeleteRows = false;
                dgvDichVu.AllowUserToResizeRows = false;
                dgvDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // ===== CẤU HÌNH DATAGRIDVIEW GIỎ HÀNG - CHỈ CHO CHỈNH SỬA CỘT SOLUONG =====
                dgvGioHang.ReadOnly = false;
                dgvGioHang.AllowUserToAddRows = false;
                dgvGioHang.AllowUserToDeleteRows = false;
                dgvGioHang.AllowUserToResizeRows = false;
                dgvGioHang.SelectionMode = DataGridViewSelectionMode.CellSelect;

                // ===== KHÓA TẤT CẢ CỘT TRỪ SOLUONG =====
                if (dgvGioHang.Columns.Count > 0)
                {
                    dgvGioHang.Columns["MaDV"].ReadOnly = true;
                    dgvGioHang.Columns["TenDV"].ReadOnly = true;
                    dgvGioHang.Columns["DonGia"].ReadOnly = true;
                    dgvGioHang.Columns["ThanhTien"].ReadOnly = true;
                    
                    // ===== CHỈ CẮP NHẬT CỘT SOLUONG =====
                    if (dgvGioHang.Columns.Contains("SoLuong"))
                    {
                        dgvGioHang.Columns["SoLuong"].ReadOnly = false;
                    }
                }

                // ===== THÊM SỰ KIỆN CẬP NHẬT GIÁ THÀNH KHI CHỈNH SỬA SỐ LƯỢNG =====
                dgvGioHang.CellEndEdit += DgvGioHang_CellEndEdit;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khởi tạo bảng: " + ex.Message);
            }
        }

        // ===== SỰ KIỆN CẬP NHẬT GIÁ THÀNH TIỀN KHI CHỈNH SỬA SỐ LƯỢNG =====
        private void DgvGioHang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // ===== CHỈ XỬ LÝ KHI CHỈNH SỬA CỘT SOLUONG (CỘT INDEX 3) =====
                if (e.ColumnIndex != 3)
                    return;

                if (e.RowIndex < 0 || e.RowIndex >= dtGioHang.Rows.Count)
                    return;

                // ===== LẤY HÀNG TỪ DATATABLE =====
                DataRow row = dtGioHang.Rows[e.RowIndex];

                // ===== LẤY GIÁ TIỀN VÀ SỐ LƯỢNG =====
                decimal donGia = Convert.ToDecimal(row["DonGia"]);
                int soLuong = 0;

                // ===== KIỂM TRA NHẬP CÓ HỢP LỆ KHÔNG =====
                if (!int.TryParse(dgvGioHang.Rows[e.RowIndex].Cells[3].Value?.ToString() ?? "0", out soLuong))
                {
                    soLuong = 1;
                    dgvGioHang.Rows[e.RowIndex].Cells[3].Value = 1;
                }

                // ===== KIỂM TRA SỐ LƯỢNG PHẢI > 0 =====
                if (soLuong <= 0)
                {
                    soLuong = 1;
                    dgvGioHang.Rows[e.RowIndex].Cells[3].Value = 1;
                }

                // ===== CẬP NHẬT SỐ LƯỢNG VÀO DATATABLE =====
                row["SoLuong"] = soLuong;

                // ===== TÍNH THÀNH TIỀN = GIÁ * SỐ LƯỢNG =====
                decimal thanhTien = donGia * soLuong;
                row["ThanhTien"] = thanhTien;

                // ===== CẬP NHẬT CELL THÀNH TIỀN =====
                dgvGioHang.Rows[e.RowIndex].Cells[4].Value = thanhTien;
            }
            catch
            {
                // ===== IM LẶNG NẾU CÓ LỖI =====
            }
        }

        private void LoadDichVu()
        {
            try
            {
                dtDichVu.Rows.Clear();
                var listDichVu = busDichVu.GetAllDichVu();

                if (listDichVu != null && listDichVu.Count > 0)
                {
                    foreach (var dv in listDichVu)
                    {
                        dtDichVu.Rows.Add(dv.MaDV, dv.TenDV, dv.DonGia ?? 0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi tải dịch vụ:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= THÊM VÀO GIỎ =================
        private void btnThemDV_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDichVu.CurrentRow == null)
                {
                    MessageBox.Show("❌ Vui lòng chọn dịch vụ cần thêm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string ma = dgvDichVu.CurrentRow.Cells["MaDV"].Value?.ToString();
                string ten = dgvDichVu.CurrentRow.Cells["TenDV"].Value?.ToString();

                if (!decimal.TryParse(dgvDichVu.CurrentRow.Cells["DonGia"].Value?.ToString() ?? "0", out decimal gia))
                {
                    gia = 0;
                }

                var row = dtGioHang.AsEnumerable().FirstOrDefault(x => x["MaDV"].ToString() == ma);

                if (row == null)
                {
                    // ===== THÊM MỚI - KHÔNG CÓ THÔNG BÁO =====
                    dtGioHang.Rows.Add(ma, ten, gia, 1);
                }
                else
                {
                    // ===== TĂNG SỐ LƯỢNG - KHÔNG CÓ THÔNG BÁO =====
                    row["SoLuong"] = (int)row["SoLuong"] + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thêm dịch vụ:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaDV_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvGioHang.CurrentRow == null)
                {
                    MessageBox.Show("❌ Vui lòng chọn dịch vụ cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dgvGioHang.Rows.RemoveAt(dgvGioHang.CurrentRow.Index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi xóa dịch vụ:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= THANH TOÁN =================
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                // ===== KIỂM TRA GIỎ HÀNG KHÔNG TRỐNG =====
                if (dtGioHang.Rows.Count == 0)
                {
                    MessageBox.Show("❌ Giỏ hàng trống! Vui lòng thêm dịch vụ trước khi thanh toán.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ===== KIỂM TRA THEO LOẠI KHÁCH =====
                string tenKH = txtTenKH.Text.Trim();
                string sdtKH = txtSDT.Text.Trim();
                string loaiKhach = "";
                string maLich = "";

                if (rdoKhachDatSan.Checked)
                {
                    // ===== KHÁCH ĐẶT SÂN: PHẢI CHỌN LỊCH =====
                    if (cbxLichDat.SelectedIndex < 0)
                    {
                        MessageBox.Show("❌ Vui lòng chọn lịch đặt sân!",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cbxLichDat.Focus();
                        return;
                    }

                    loaiKhach = "DatSan";
                    maLich = cbxLichDat.SelectedValue?.ToString() ?? "";

                    // ===== ĐÃ ĐƯỢC ĐIỀN TỰ ĐỘNG TỪ LỊCH ĐẶT =====
                    if (string.IsNullOrWhiteSpace(tenKH) || string.IsNullOrWhiteSpace(sdtKH))
                    {
                        MessageBox.Show("❌ Thông tin khách hàng từ lịch đặt không hợp lệ!",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    // ===== KHÁCH VÃNG LAI: TÊN VÀ SĐT CÓ THỂ RỖNG =====
                    loaiKhach = "VangLai";
                    maLich = "";

                    // ===== KHÔNG BẮT BUỘC NHẬP =====
                }

                // ===== CHUẨN BỊ DỮ LIỆU CHI TIẾT DỊCH VỤ =====
                List<DAL.CT_HoaDon_DichVu> listChiTiet = new List<DAL.CT_HoaDon_DichVu>();
                decimal tongTienDichVu = 0;

                foreach (DataRow row in dtGioHang.Rows)
                {
                    decimal thanhTien = (decimal)row["ThanhTien"];
                    listChiTiet.Add(new DAL.CT_HoaDon_DichVu
                    {
                        MaDV = row["MaDV"].ToString(),
                        SoLuong = (int)row["SoLuong"],
                        ThanhTien = thanhTien
                    });
                    tongTienDichVu += thanhTien;
                }

                // ===== KHỞI TẠO FORM CHI TIẾT THANH TOÁN =====
                CT_HoaDon_DichVu frmThanhToan = new CT_HoaDon_DichVu();

                // ===== GỬI THÔNG TIN KHÁCH HÀNG =====
                frmThanhToan.SetKhachHang(tenKH, sdtKH);

                // ===== GỬI LOẠI KHÁCH HÀNG & LỊCH ĐẶT =====
                frmThanhToan.SetLoaiKhach(loaiKhach);
                if (!string.IsNullOrWhiteSpace(maLich))
                {
                    frmThanhToan.SetMaLich(maLich);
                }

                // ===== GỬI CHI TIẾT DỊCH VỤ =====
                frmThanhToan.SetChiTietDichVu(listChiTiet, tongTienDichVu);

                // ===== HIỂN THỊ FORM CHI TIẾT THANH TOÁN =====
                this.Hide();
                frmThanhToan.ShowDialog();
                this.Show();

                // ===== NẾU THANH TOÁN THÀNH CÔNG, XÓA GIỎ HÀNG =====
                if (frmThanhToan.IsThanhToanThanhCong)
                {
                    dtGioHang.Rows.Clear();
                    txtTenKH.Clear();
                    txtSDT.Clear();
                    rdoKhachLe.Checked = true;
                    cbxLichDat.SelectedIndex = -1;

                    MessageBox.Show("✔ Thanh toán thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thanh toán:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}