using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BUS; // Gọi BUS của bạn
using DAL; // Gọi Entity Framework Models

namespace TrangChu
{
    public partial class ThongKeDoanhThu : Form
    {
        private LichDatBUS busLichDat = new LichDatBUS();
        private HoaDonBUS busHoaDon = new HoaDonBUS(); // Cần thêm hàm lấy tất cả chi tiết dịch vụ

        public ThongKeDoanhThu()
        {
            InitializeComponent();
            LoadCombobox();
        }

        private void LoadCombobox()
        {
            // Load Năm (5 năm gần nhất)
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear; i >= currentYear - 5; i--)
            {
                cbNam.Items.Add(i);
            }
            cbNam.SelectedIndex = 0; // Chọn năm hiện tại

            // Load Tháng
            cbThang.Items.Add("Tất cả"); // Index 0
            for (int i = 1; i <= 12; i++)
            {
                cbThang.Items.Add("Tháng " + i);
            }
            cbThang.SelectedIndex = DateTime.Now.Month; // Chọn tháng hiện tại
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            int nam = (int)cbNam.SelectedItem;
            int thang = cbThang.SelectedIndex; // 0 là Tất cả, 1-12 là tháng

            // 1. LẤY DỮ LIỆU TỪ BUS
            var listLich = busLichDat.GetAll();
            // var listHoaDonChiTiet = busHoaDon.GetAllChiTiet(); // Bạn cần viết hàm này trong BUS

            // Giả lập dữ liệu nếu chưa có hàm BUS (Để test giao diện)
            // Bạn hãy thay thế đoạn này bằng logic gọi DB thực tế
            decimal tongTienSan = CalculateTongTienSan(listLich, nam, thang);
            decimal tongTienDV = CalculateTongTienDichVu(nam, thang);

            // 2. HIỂN THỊ LÊN LABELS
            lblTongSan.Text = tongTienSan.ToString("N0") + " VNĐ";
            lblTongDichVu.Text = tongTienDV.ToString("N0") + " VNĐ";
            lblTongCong.Text = (tongTienSan + tongTienDV).ToString("N0") + " VNĐ";

            // 3. VẼ BIỂU ĐỒ
            DrawChart(tongTienSan, tongTienDV);
        }

        // Hàm tính tiền Sân
        private decimal CalculateTongTienSan(System.Collections.Generic.List<DAL.LichDat> list, int nam, int thang)
        {
            var query = list.Where(x => x.NgayDat.HasValue &&
                                        x.NgayDat.Value.Year == nam &&
                                        x.TrangThai == "Đã thanh toán"); // Chỉ tính đã thanh toán

            if (thang > 0) // Nếu chọn tháng cụ thể
            {
                query = query.Where(x => x.NgayDat.Value.Month == thang);
            }

            return query.Sum(x => x.DonGiaThucTe ?? 0);
        }

        // Hàm tính tiền Dịch vụ (Cần logic lấy từ DB thật)
        private decimal CalculateTongTienDichVu(int nam, int thang)
        {
            // TODO: Viết hàm trong BUS để lấy danh sách CT_HoaDon_DichVu
            // Sau đó Filter theo ngày tháng của Hóa Đơn cha

            // Code mẫu giả định:
            /*
            var listHD = busHoaDon.GetAll(); // Lấy list hóa đơn
            var query = listHD.Where(x => x.ThoiGianThanhToan.Value.Year == nam);
            if (thang > 0) query = query.Where(x => x.ThoiGianThanhToan.Value.Month == thang);
            
            // Tính tổng tiền dịch vụ (Giả sử TongTien trong HoaDon là bao gồm cả Sân + DV)
            // Thì bạn phải trừ đi tiền sân, hoặc query vào bảng chi tiết.
            */

            return 0; // Tạm thời trả về 0 để không lỗi
        }

        private void DrawChart(decimal san, decimal dichVu)
        {
            // Xóa dữ liệu cũ
            chartDoanhThu.Series["Doanh Thu Sân"].Points.Clear();
            chartDoanhThu.Series["Doanh Thu Dịch Vụ"].Points.Clear();

            // Thêm điểm dữ liệu mới
            // Cột Sân
            var p1 = chartDoanhThu.Series["Doanh Thu Sân"].Points.Add(Convert.ToDouble(san));
            chartDoanhThu.Series["Doanh Thu Sân"].Points[0].AxisLabel = "Sân Bóng";

            // Cột Dịch Vụ
            var p2 = chartDoanhThu.Series["Doanh Thu Dịch Vụ"].Points.Add(Convert.ToDouble(dichVu));
            chartDoanhThu.Series["Doanh Thu Dịch Vụ"].Points[0].AxisLabel = "Dịch Vụ";
        }
    }
}