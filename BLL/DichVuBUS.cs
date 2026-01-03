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

                var isUsed = db.CT_HoaDon_DichVu.Any(x => x.MaDV == maDV);
                if (isUsed)
                {
                    throw new Exception("Dịch vụ này đã được sử dụng trong hóa đơn và không thể xóa.");
                }

                db.DichVus.Remove(dv);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi DeleteDichVu: {ex.Message}");
                throw new Exception($"Lỗi xóa dịch vụ: {ex.Message}");
            }
        }
        public bool UpdateDichVu(DichVu dichVu)
        {
            var existingDV = db.DichVus.FirstOrDefault(d => d.MaDV == dichVu.MaDV);
            if (existingDV == null)
                return false;

            existingDV.TenDV = dichVu.TenDV;
            existingDV.DonGia = dichVu.DonGia;

            db.SaveChanges();
            return true;
        }


        public bool InsertDichVu(DichVu dichVu)
        {
            try
            {
                db.DichVus.Add(dichVu);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<CT_HoaDon_DichVu> LoadChiTietWithDichVu(List<CT_HoaDon_DichVu> listChiTiet)
        {
            try
            {
                if (listChiTiet == null || listChiTiet.Count == 0)
                    return listChiTiet;

                var maDichVuList = listChiTiet.Select(x => x.MaDV).Distinct().ToList();

                if (maDichVuList.Count == 0)
                    return listChiTiet;

                var dichVuFromDb = db.DichVus
                    .Where(x => maDichVuList.Contains(x.MaDV))
                    .ToList();

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