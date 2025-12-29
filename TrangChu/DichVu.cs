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

        private string defaultTenKH = "";
        private string defaultSDT = "";
        private string defaultMaLich = "";
        private bool isLoadingDefaultData = false;

        public DichVu()
        {
            InitializeComponent();
            this.Load += DichVu_Load;
        }

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

                if (!string.IsNullOrWhiteSpace(defaultMaLich))
                {
                    ApplyDefaultCustomer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khởi tạo form Dịch vụ:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetDefaultCustomer(string tenKH, string sdtKH, string maLich)
        {
            try
            {
                defaultTenKH = tenKH ?? "";
                defaultSDT = sdtKH ?? "";
                defaultMaLich = maLich ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi thiết lập khách hàng mặc định: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyDefaultCustomer()
        {
            try
            {
                isLoadingDefaultData = true;

                // ===== ĐIỀN THÔNG TIN KHÁCH HÀNG =====
                txtTenKH.Text = defaultTenKH;
                txtSDT.Text = defaultSDT;
                
                txtTenKH.ReadOnly = true;
                txtSDT.ReadOnly = true;
                txtTenKH.BackColor = System.Drawing.Color.LightGray;
                txtSDT.BackColor = System.Drawing.Color.LightGray;

                // ===== TÌM VÀ CHỌN LỊCH ĐẶT =====
                if (!string.IsNullOrWhiteSpace(defaultMaLich))
                {
                    // ===== KIỂM TRA DATASOURCE CÓ HỢP LỆ KHÔNG =====
                    if (cbxLichDat.DataSource != null && cbxLichDat.DataSource is List<DAL.LichDat> lichDatList)
                    {
                        var selectedLich = lichDatList.FirstOrDefault(l => l.MaLich == defaultMaLich);
                        
                        if (selectedLich != null)
                        {
                            // ===== ENABLE COMBOBOX ĐỂ CÓ THỂ CHỌN =====
                            cbxLichDat.Enabled = true;
                            cbxLichDat.SelectedItem = selectedLich;
                            // ===== SAU ĐÓ DISABLE LẠI ĐỂ KHÔNG CHO THAY ĐỔI =====
                            cbxLichDat.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show($"⚠️ Không tìm thấy lịch đặt [{defaultMaLich}] trong danh sách!", 
                                "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cbxLichDat.Enabled = false;
                        }
                    }
                    else
                    {
                        cbxLichDat.Enabled = false;
                    }
                }
                else
                {
                    cbxLichDat.Enabled = false;
                }

                isLoadingDefaultData = false;
            }
            catch (Exception ex)
            {
                isLoadingDefaultData = false;
                MessageBox.Show($"❌ Lỗi áp dụng dữ liệu mặc định: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupEventHandlers()
        {
            try
            {
                cbxLichDat.SelectedIndexChanged += CbxLichDat_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thiết lập sự kiện:\n" + ex.Message);
            }
        }

        private void LoadLichDat()
        {
            try
            {
                List<DAL.LichDat> allLichDat = busLichDat.GetAll();
                
                DateTime homNay = DateTime.Now.Date;
                List<DAL.LichDat> lichDatList = allLichDat
                    .Where(l => l.NgayDat.HasValue && 
                                l.NgayDat.Value >= homNay && 
                                l.TrangThai == "Đã đặt")
                    .OrderBy(l => l.NgayDat)
                    .ThenBy(l => l.GioBD)
                    .ToList();


                // ===== CẬP NHẬT DATASOURCE CỦA COMBOBOX =====
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
                    // ===== NẾU KHÔNG CÓ MÃ LỊCH MẶC ĐỊNH, MỞ COMBOBOX =====
                    cbxLichDat.Enabled = string.IsNullOrWhiteSpace(defaultMaLich);
                    cbxLichDat.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi tải danh sách lịch đặt:\n" + ex.Message, 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CbxLichDat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isLoadingDefaultData)
                    return;

                if (cbxLichDat.SelectedIndex < 0)
                {
                    txtTenKH.Clear();
                    txtSDT.Clear();
                    return;
                }

                var selectedLich = cbxLichDat.SelectedItem as DAL.LichDat;

                if (selectedLich != null)
                {
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

        private void InitTable()
        {
            try
            {
                dtDichVu.Columns.Clear();
                dtDichVu.Rows.Clear();

                dtDichVu.Columns.Add("MaDV", typeof(string));
                dtDichVu.Columns.Add("TenDV", typeof(string));
                dtDichVu.Columns.Add("DonGia", typeof(decimal));
                dgvDichVu.DataSource = dtDichVu;

                dtGioHang.Columns.Clear();
                dtGioHang.Rows.Clear();

                dtGioHang.Columns.Add("MaDV", typeof(string));
                dtGioHang.Columns.Add("TenDV", typeof(string));
                dtGioHang.Columns.Add("DonGia", typeof(decimal));
                dtGioHang.Columns.Add("SoLuong", typeof(int));
                dtGioHang.Columns.Add("ThanhTien", typeof(decimal), "DonGia * SoLuong");
                dgvGioHang.DataSource = dtGioHang;

                dgvDichVu.ReadOnly = true;
                dgvDichVu.AllowUserToAddRows = false;
                dgvDichVu.AllowUserToDeleteRows = false;
                dgvDichVu.AllowUserToResizeRows = false;
                dgvDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dgvGioHang.ReadOnly = false;
                dgvGioHang.AllowUserToAddRows = false;
                dgvGioHang.AllowUserToDeleteRows = false;
                dgvGioHang.AllowUserToResizeRows = false;
                dgvGioHang.SelectionMode = DataGridViewSelectionMode.CellSelect;

                if (dgvGioHang.Columns.Count > 0)
                {
                    dgvGioHang.Columns["MaDV"].ReadOnly = true;
                    dgvGioHang.Columns["TenDV"].ReadOnly = true;
                    dgvGioHang.Columns["DonGia"].ReadOnly = true;
                    dgvGioHang.Columns["ThanhTien"].ReadOnly = true;
                    
                    if (dgvGioHang.Columns.Contains("SoLuong"))
                    {
                        dgvGioHang.Columns["SoLuong"].ReadOnly = false;
                    }
                }

                dgvGioHang.CellEndEdit += DgvGioHang_CellEndEdit;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khởi tạo bảng: " + ex.Message);
            }
        }

        private void DgvGioHang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != 3)
                    return;

                if (e.RowIndex < 0 || e.RowIndex >= dtGioHang.Rows.Count)
                    return;

                DataRow row = dtGioHang.Rows[e.RowIndex];

                decimal donGia = Convert.ToDecimal(row["DonGia"]);
                int soLuong = 0;

                if (!int.TryParse(dgvGioHang.Rows[e.RowIndex].Cells[3].Value?.ToString() ?? "0", out soLuong))
                {
                    soLuong = 1;
                    dgvGioHang.Rows[e.RowIndex].Cells[3].Value = 1;
                }

                if (soLuong <= 0)
                {
                    soLuong = 1;
                    dgvGioHang.Rows[e.RowIndex].Cells[3].Value = 1;
                }

                row["SoLuong"] = soLuong;

                decimal thanhTien = donGia * soLuong;
                row["ThanhTien"] = thanhTien;

                dgvGioHang.Rows[e.RowIndex].Cells[4].Value = thanhTien;
            }
            catch
            {
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
                    dtGioHang.Rows.Add(ma, ten, gia, 1);
                }
                else
                {
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

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                // ===== KIỂM TRA GIỎ HÀNG CÓ TRỐNG KHÔNG =====
                if (dtGioHang.Rows.Count == 0)
                {
                    MessageBox.Show("❌ Giỏ hàng trống! Vui lòng thêm dịch vụ trước khi thanh toán.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ===== KIỂM TRA CÓ CHỌN LỊCH ĐẶT KHÔNG =====
                string maLich = "";
                if (cbxLichDat.SelectedIndex >= 0)
                {
                    // ===== LẤY GIÁ TRỊ TỪ VALUEEMBER KHÔNG PHẢI SELECTEDVALUE =====
                    var selectedItem = cbxLichDat.SelectedItem as DAL.LichDat;
                    if (selectedItem != null)
                    {
                        maLich = selectedItem.MaLich;
                    }
                }

                if (string.IsNullOrWhiteSpace(maLich))
                {
                    MessageBox.Show("❌ Vui lòng chọn lịch đặt sân!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbxLichDat.Focus();
                    return;
                }

                // ===== LẤY THÔNG TIN KHÁCH HÀNG =====
                string tenKH = txtTenKH.Text.Trim();
                string sdtKH = txtSDT.Text.Trim();

                if (string.IsNullOrWhiteSpace(tenKH) || string.IsNullOrWhiteSpace(sdtKH))
                {
                    MessageBox.Show("❌ Thông tin khách hàng không đầy đủ!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ===== CHUẨN BỊ DANH SÁCH CHI TIẾT DỊCH VỤ =====
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

                // ===== MỞ FORM CHI TIẾT THANH TOÁN =====
                CT_HoaDon_DichVu frmThanhToan = new CT_HoaDon_DichVu();

                frmThanhToan.SetKhachHang(tenKH, sdtKH);
                frmThanhToan.SetMaLich(maLich);
                frmThanhToan.SetChiTietDichVu(listChiTiet, tongTienDichVu);

                // ===== ẨN FORM HIỆN TẠI VÀ HIỂN THỊ FORM THANH TOÁN =====
                this.Hide();
                frmThanhToan.ShowDialog();
                this.Show();

                // ===== NẾU THANH TOÁN THÀNH CÔNG, RESET FORM =====
                if (frmThanhToan.IsThanhToanThanhCong)
                {
                    dtGioHang.Rows.Clear();
                    txtTenKH.Clear();
                    txtSDT.Clear();
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