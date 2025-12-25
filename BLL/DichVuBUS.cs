using DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    public class DichVuBUS
    {
        private Model1 db = new Model1();

        public List<DichVu> GetAll()
        {
            return db.DichVus.ToList();
        }

        // Alias GetAllDichVu để tương thích
        public List<DichVu> GetAllDichVu()
        {
            return GetAll();
        }

        // Lấy đơn giá dịch vụ theo mã (MaDV là string)
        public decimal GetDonGia(string maDV)
        {
            var dv = db.DichVus.FirstOrDefault(x => x.MaDV == maDV);
            return dv != null ? (dv.DonGia ?? 0) : 0;
        }

        // Xóa dịch vụ theo mã
        public bool DeleteDichVu(string maDV)
        {
            try
            {
                var dv = db.DichVus.FirstOrDefault(x => x.MaDV == maDV);
                if (dv == null)
                    return false;

                db.DichVus.Remove(dv);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ✅ LOAD THÔNG TIN DỊCH VỤ CHO CHI TIẾT HÓA ĐƠN (EAGER LOADING)
        /// Đảm bảo rằng dữ liệu từ SQL được đồng bộ vào CT_HoaDon_DichVu
        /// </summary>
        public List<CT_HoaDon_DichVu> LoadChiTietWithDichVu(List<CT_HoaDon_DichVu> listChiTiet)
        {
            try
            {
                if (listChiTiet == null || listChiTiet.Count == 0)
                    return listChiTiet;

                // ===== LẤY DANH SÁCH MÃ DỊCH VỤ =====
                var maDichVuList = listChiTiet.Select(x => x.MaDV).Distinct().ToList();

                if (maDichVuList.Count == 0)
                    return listChiTiet;

                // ===== LOAD TỪ DATABASE =====
                var dichVuFromDb = db.DichVus
                    .Where(x => maDichVuList.Contains(x.MaDV))
                    .ToList();

                // ===== CẬP NHẬT NAVIGATION PROPERTY =====
                foreach (var ct in listChiTiet)
                {
                    var dv = dichVuFromDb.FirstOrDefault(x => x.MaDV == ct.MaDV);
                    if (dv != null)
                    {
                        ct.DichVu = dv;
                    }
                }

                return listChiTiet;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi load dịch vụ: {ex.Message}");
            }
        }
    }
}