using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TrangChu
{
    public enum UserRole
    {
        admin,      // Quản lý
        nhanvien    // Nhân viên
    }

    public partial class chat : Form
    {
        private UserRole CurrentRole;
        // API Key
        private const string API_KEY = "";
        private readonly HttpClient httpClient = new HttpClient();

        private LichDatBUS busLichDat = new LichDatBUS();
        private HoaDonBUS busHoaDon = new HoaDonBUS();

        private readonly string[] dbKeywords = {
            "lịch", "sân", "trống", "đặt", "full", "kín",
            "doanh thu", "tiền", "lợi nhuận", "thống kê", "báo cáo", "ngày", "tháng", "hôm nay"
        };

        private readonly List<string> forbiddenKeywords = new List<string>
        {
            "doanh thu", "lợi nhuận", "bao nhiêu tiền", "thống kê",
            "báo cáo tài chính", "lương", "lãi", "lỗ", "revenue", "profit"
        };

        public chat(UserRole role)
        {
            InitializeComponent();
            CurrentRole = role;
            SetupUIByRole();

            // Cấu hình RichTextBox
            txtChat.BackColor = Color.White;
            txtChat.ReadOnly = true;
            txtChat.Font = new Font("Segoe UI", 10);

            this.txtMessage.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    btnSend.PerformClick();
                }
            };
        }

        private void SetupUIByRole()
        {
            lblRole.Text = $"Đang chat với vai trò: {CurrentRole.ToString().ToUpper()}";
            lblRole.ForeColor = (CurrentRole == UserRole.admin) ? Color.Red : Color.Blue;

            string greeting = "Chat Hi: Xin chào 👋 Tôi là trợ lý ảo của sân bóng. Tôi có thể giúp gì cho bạn?";
            AppendChatLog(greeting, isUser: false);
        }

        // Logic xử lý nút XÓA
        private void button1_Click(object sender, EventArgs e)
        {
            txtChat.Clear(); // Xóa sạch nội dung chat
            string greeting = "Gemini: Đã xóa lịch sử chat. Tôi có thể giúp gì tiếp theo?";
            AppendChatLog(greeting, isUser: false);
        }

        // Logic xử lý nút THOÁT
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form chat
        }

        // ... (CÁC HÀM IsAllowedQuestion, NeedDatabase, GetRealTimeData, BuildFullPrompt GIỮ NGUYÊN) ...
        // ... (Bạn copy lại từ code trước đó để tránh dài dòng nhé) ...

        private bool IsAllowedQuestion(string question)
        {
            if (CurrentRole == UserRole.admin) return true;
            string lowerQuestion = question.ToLower();
            foreach (var keyword in forbiddenKeywords)
            {
                if (lowerQuestion.Contains(keyword)) return false;
            }
            return true;
        }

        private bool NeedDatabase(string question)
        {
            string q = question.ToLower();
            return dbKeywords.Any(k => q.Contains(k));
        }

        private string GetRealTimeData()
        {
            StringBuilder sb = new StringBuilder();
            DateTime today = DateTime.Now.Date;

            try
            {
                var listLich = busLichDat.GetAll();
                var lichTuongLai = listLich.Where(x => x.NgayDat.HasValue && x.NgayDat.Value.Date >= today)
                                           .OrderBy(x => x.NgayDat).ThenBy(x => x.GioBD)
                                           .Take(15).ToList();

                sb.AppendLine($"=== DỮ LIỆU HỆ THỐNG (Cập nhật lúc {DateTime.Now:HH:mm}) ===");

                if (lichTuongLai.Count == 0)
                {
                    sb.AppendLine("- Hiện tại không có lịch đặt nào sắp tới.");
                }
                else
                {
                    foreach (var l in lichTuongLai)
                    {
                        string ngay = l.NgayDat.Value.ToString("dd/MM");
                        sb.AppendLine($"- Ngày {ngay}: Sân {l.MaSan} ({l.GioBD}h-{l.GioKT}h) - Khách: {l.TenKH} - Trạng thái: {l.TrangThai}");
                    }
                }

                if (CurrentRole == UserRole.admin)
                {
                    int currentMonth = DateTime.Now.Month;
                    int currentYear = DateTime.Now.Year;
                    decimal doanhThuDV = busHoaDon.GetTongDoanhThuDichVu(currentYear, currentMonth);
                    var lichDaTT = listLich.Where(x => x.TrangThai == "Đã thanh toán" &&
                                                       x.NgayDat.Value.Month == currentMonth &&
                                                       x.NgayDat.Value.Year == currentYear);
                    decimal tienSan = lichDaTT.Sum(x => x.DonGiaThucTe ?? 0);

                    sb.AppendLine($"\n=== TÀI CHÍNH THÁNG {currentMonth}/{currentYear} ===");
                    sb.AppendLine($"- Tổng doanh thu: {(doanhThuDV + tienSan):N0} VNĐ");
                }
            }
            catch (Exception ex) { sb.AppendLine("Lỗi đọc DB: " + ex.Message); }

            return sb.ToString();
        }

        private string BuildFullPrompt(string userMessage)
        {
            string dbContext = "";
            if (NeedDatabase(userMessage))
            {
                dbContext = "\n" + GetRealTimeData();
            }

            string role = (CurrentRole == UserRole.admin) ? "Admin" : "Nhân viên";
            string prompt = $@"
Bạn là Trợ lý ảo quản lý sân bóng.
Vai trò người dùng: {role}.
QUY TẮC:
1. Chỉ trả lời câu hỏi liên quan đến sân bóng.
2. Từ chối câu hỏi ngoài lề (thời tiết, chính trị...).
3. Dữ liệu: Dựa trên thông tin bên dưới.
4. Phong cách: Ngắn gọn.
{dbContext}
Câu hỏi: {userMessage}";
            return prompt;
        }

        private async Task<string> CallGemini(string message)
        {
            try
            {
                string url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key=" + API_KEY;
                var requestBody = new
                {
                    contents = new[] { new { parts = new[] { new { text = BuildFullPrompt(message) } } } }
                };

                string jsonBody = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return $"Lỗi API: {(int)response.StatusCode} {response.StatusCode} - {responseString}";

                dynamic data = JsonConvert.DeserializeObject(responseString);
                if (data?.candidates != null && data.candidates.Count > 0)
                    return data.candidates[0].content.parts[0].text;

                return "AI không phản hồi.";
            }
            catch (Exception ex)
            {
                return $"Lỗi hệ thống: {ex.Message}";
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string msg = txtMessage.Text.Trim();
            if (string.IsNullOrEmpty(msg)) return;

            AppendChatLog($"Bạn: {msg}", isUser: true);
            txtMessage.Clear();
            btnSend.Enabled = false;

            if (!IsAllowedQuestion(msg))
            {
                await Task.Delay(300);
                AppendChatLog("Gemini: ⛔ Bạn không có quyền truy cập thông tin này.", isUser: false);
                btnSend.Enabled = true;
                return;
            }

            string aiReply = await CallGemini(msg);
            AppendChatLog($"Gemini: {aiReply}", isUser: false);
            btnSend.Enabled = true;
        }

        private void AppendChatLog(string message, bool isUser)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => AppendChatLog(message, isUser)));
                return;
            }

            txtChat.SelectionStart = txtChat.TextLength;
            txtChat.SelectionLength = 0;

            if (isUser)
            {
                txtChat.SelectionColor = Color.Blue;
                txtChat.SelectionFont = new Font(txtChat.Font, FontStyle.Bold);
                txtChat.AppendText(message + "\r\n");
            }
            else
            {
                txtChat.SelectionColor = Color.Black;
                txtChat.SelectionFont = new Font(txtChat.Font, FontStyle.Regular);
                txtChat.AppendText(message + "\r\n\r\n");
            }

            txtChat.ScrollToCaret();
        }
    }
}