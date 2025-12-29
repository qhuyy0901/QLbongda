using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TrangChu
{
    public partial class DT_HoaDon : Form
    {
        private HoaDonBUS busHoaDon = new HoaDonBUS();

        public DT_HoaDon()
        {
            InitializeComponent();
            dgvHoaDon.AutoGenerateColumns = false;
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                dtpFromDate.Value = DateTime.Now.AddDays(-30);
                dtpToDate.Value = DateTime.Now;
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                var data = busHoaDon.GetHoaDonWithCustomerInfo();
                
                if (data == null || data.Count == 0)
                {
                    dgvHoaDon.DataSource = null;
                    MessageBox.Show("⚠️ Không có dữ liệu hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dgvHoaDon.DataSource = data;
                }

                TinhTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tuNgay = dtpFromDate.Value.Date;
                DateTime denNgay = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1);

                var data = busHoaDon.GetHoaDonWithCustomerInfo()
                    .Where(hd => 
                        hd.NgayLap >= tuNgay && hd.NgayLap <= denNgay
                    ).ToList();

                if (data == null || data.Count == 0)
                {
                    dgvHoaDon.DataSource = null;
                    MessageBox.Show($"⚠️ Không có hóa đơn trong khoảng thời gian từ {tuNgay:dd/MM/yyyy} đến {denNgay:dd/MM/yyyy}", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dgvHoaDon.DataSource = data;
                }

                TinhTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lọc dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim();

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    MessageBox.Show("❌ Vui lòng nhập từ khóa tìm kiếm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSearch.Focus();
                    return;
                }
        
                var data = busHoaDon.GetHoaDonWithCustomerInfo()
                    .Where(hd =>
                        (hd.MaHD != null && hd.MaHD.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                        (hd.KhachHang?.TenKH != null && hd.KhachHang.TenKH.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                        (hd.KhachHang?.SDT != null && hd.KhachHang.SDT.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    ).ToList();

                if (data == null || data.Count == 0)
                {
                    dgvHoaDon.DataSource = null;
                    MessageBox.Show($"⚠️ Không tìm thấy hóa đơn phù hợp với '{keyword}'", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dgvHoaDon.DataSource = data;
                }

                TinhTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Clear();
                dtpFromDate.Value = DateTime.Now.AddDays(-30);
                dtpToDate.Value = DateTime.Now;
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TinhTongTien()
        {
            try
            {
                decimal tong = 0;

                foreach (DataGridViewRow row in dgvHoaDon.Rows)
                {
                    if (row.Cells["colTongTien"].Value != null && row.Cells["colTongTien"].Value != DBNull.Value)
                    {
                        if (decimal.TryParse(row.Cells["colTongTien"].Value.ToString(), out decimal value))
                        {
                            tong += value;
                        }
                    }
                }

                lblTongDoanhThu.Text = tong.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính tổng tiền: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblTongDoanhThu.Text = "0 VNĐ";
            }
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvHoaDon.CurrentRow == null)
                {
                    MessageBox.Show("❌ Vui lòng chọn hóa đơn cần xem chi tiết!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maHD = dgvHoaDon.CurrentRow.Cells["colMaHD"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(maHD))
                {
                    MessageBox.Show("❌ Mã hóa đơn không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var chiTietList = busHoaDon.GetChiTietHoaDon(maHD);

                if (chiTietList == null || chiTietList.Count == 0)
                {
                    MessageBox.Show($"⚠️ Hóa đơn [{maHD}] không có dịch vụ nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Tạo thông tin hiển thị chi tiết
                string chiTietInfo = $"═══════════════════════════════════\n";
                chiTietInfo += $"CHI TIẾT HÓA ĐƠN: {maHD}\n";
                chiTietInfo += $"═══════════════════════════════════\n\n";

                decimal tongTien = 0;

                foreach (var ct in chiTietList)
                {
                    chiTietInfo += $"🔹 Mã DV: {ct.MaDV}\n";
                    chiTietInfo += $"   Tên DV: {ct.DichVu?.TenDV ?? "N/A"}\n";
                    chiTietInfo += $"   Số lượng: {ct.SoLuong}\n";
                    chiTietInfo += $"   Thành tiền: {ct.ThanhTien:N0} VNĐ\n";
                    chiTietInfo += $"──────────────────────────────────\n";

                    if (ct.ThanhTien.HasValue)
                        tongTien += ct.ThanhTien.Value;
                }

                chiTietInfo += $"\n💰 TỔNG TIỀN DỊCH VỤ: {tongTien:N0} VNĐ\n";

                MessageBox.Show(chiTietInfo, $"Chi Tiết Hóa Đơn {maHD}", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xem chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng in/xuất Excel đang phát triển...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}