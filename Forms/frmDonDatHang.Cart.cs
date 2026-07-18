using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Forms
{
    public partial class frmDonDatHang
    {
        private void InitChiTietTable()
        {
            _dtChiTiet = new DataTable();
            _dtChiTiet.Columns.Add("MaSP", typeof(string));
            _dtChiTiet.Columns.Add("TenSP", typeof(string));
            _dtChiTiet.Columns.Add("DonViTinh", typeof(string));
            _dtChiTiet.Columns.Add("SoLuong", typeof(int));
            _dtChiTiet.Columns.Add("DonGia", typeof(decimal));
            _dtChiTiet.Columns.Add("GiamGia", typeof(decimal));
            _dtChiTiet.Columns.Add("ThanhTien", typeof(decimal));
        }

        private void UpdateSanPhamInfo()
        {
            if (_isBinding || cboSanPham.SelectedValue == null)
            {
                return;
            }

            try
            {
                string maSP = cboSanPham.SelectedValue.ToString().Trim();
                SanPham sp = _sanPhamDAL.GetById(maSP);
                if (sp != null)
                {
                    txtDonViTinh.Text = sp.DonViTinh ?? "";
                    txtDonGiaBan.Text = sp.DonGiaBan.ToString("N0");

                    // Tra cứu tồn kho trực tiếp từ TonKhoDAL
                    int tonKho = _tonKhoDAL.GetTongTonKho(maSP);
                    lblTonKhoKhaDung.Text = string.Format("Tồn kho khả dụng: {0:N0} {1}", tonKho, sp.DonViTinh);
                    lblTonKhoKhaDung.ForeColor = tonKho > 0 ? Color.FromArgb(13, 110, 253) : Color.Crimson;

                    UpdateDetailCalculation();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi cập nhật SP: " + ex.Message);
            }
        }

        private void UpdateDetailCalculation()
        {
            decimal donGia = 0;
            string donGiaStr = txtDonGiaBan.Text.Replace(",", "").Replace(".", "").Trim();
            decimal.TryParse(donGiaStr, out donGia);

            int soLuong = (int)nudSoLuong.Value;
            decimal giamGia = nudGiamGia.Value;

            decimal rate = 1m - (giamGia / 100m);
            if (rate < 0) rate = 0;

            decimal thanhTien = Math.Round(soLuong * donGia * rate, 0);
            txtThanhTienPreview.Text = thanhTien.ToString("N0");
        }

        private void RecalculateGrandTotal()
        {
            decimal tongTien = 0;
            foreach (DataRow row in _dtChiTiet.Rows)
            {
                if (row["ThanhTien"] != DBNull.Value)
                {
                    tongTien += Convert.ToDecimal(row["ThanhTien"]);
                }
            }

            lblTongTien.Text = string.Format("TỔNG CỘNG: {0:N0} VNĐ", tongTien);
        }

        private void cboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSanPhamInfo();
        }

        private void DetailCalculation_Changed(object sender, EventArgs e)
        {
            UpdateDetailCalculation();
        }

        private void btnThemChiTiet_Click(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null)
            {
                ShowValidation(cboSanPham, "Vui lòng chọn sản phẩm cần đặt.");
                return;
            }

            string maSP = cboSanPham.SelectedValue.ToString().Trim();
            string tenSP = cboSanPham.Text;
            string donViTinh = txtDonViTinh.Text.Trim();

            decimal donGia = 0;
            string donGiaStr = txtDonGiaBan.Text.Replace(",", "").Replace(".", "").Trim();
            if (!decimal.TryParse(donGiaStr, out donGia) || donGia < 0)
            {
                ShowValidation(txtDonGiaBan, "Đơn giá sản phẩm không hợp lệ.");
                return;
            }

            int soLuong = (int)nudSoLuong.Value;
            if (soLuong <= 0)
            {
                ShowValidation(nudSoLuong, "Số lượng đặt phải lớn hơn 0.");
                return;
            }

            decimal giamGia = nudGiamGia.Value;

            // Tính tổng số lượng yêu cầu bao gồm cả các dòng đã có trong đơn
            int currentQtyInList = 0;
            DataRow existingRow = null;
            foreach (DataRow row in _dtChiTiet.Rows)
            {
                if (row["MaSP"].ToString().Trim() == maSP)
                {
                    currentQtyInList = Convert.ToInt32(row["SoLuong"]);
                    existingRow = row;
                    break;
                }
            }

            int tongSoLuongYeuCau = currentQtyInList + soLuong;

            // Kiểm tra tồn kho khả dụng - Bắt buộc không được vượt quá tồn kho
            int tonKho = _tonKhoDAL.GetTongTonKho(maSP);
            if (tongSoLuongYeuCau > tonKho)
            {
                ShowValidation(
                    nudSoLuong,
                    string.Format("Tồn khả dụng chỉ còn {0} {1}; tổng số lượng yêu cầu là {2} {1}.",
                        tonKho, donViTinh, tongSoLuongYeuCau));
                return;
            }

            decimal rate = 1m - (giamGia / 100m);
            if (rate < 0) rate = 0;

            if (existingRow != null)
            {
                existingRow["SoLuong"] = tongSoLuongYeuCau;
                existingRow["DonGia"] = donGia;
                existingRow["GiamGia"] = giamGia;
                existingRow["ThanhTien"] = Math.Round(tongSoLuongYeuCau * donGia * rate, 0);
            }
            else
            {
                decimal thanhTien = Math.Round(soLuong * donGia * rate, 0);
                DataRow newRow = _dtChiTiet.NewRow();
                newRow["MaSP"] = maSP;
                newRow["TenSP"] = tenSP;
                newRow["DonViTinh"] = donViTinh;
                newRow["SoLuong"] = soLuong;
                newRow["DonGia"] = donGia;
                newRow["GiamGia"] = giamGia;
                newRow["ThanhTien"] = thanhTien;
                _dtChiTiet.Rows.Add(newRow);
            }

            RecalculateGrandTotal();
        }

        private void btnXoaChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.SelectedRows.Count == 0)
            {
                ShowValidation(dgvChiTiet, "Vui lòng chọn dòng chi tiết cần xóa.");
                return;
            }

            int index = dgvChiTiet.SelectedRows[0].Index;
            if (index >= 0 && index < _dtChiTiet.Rows.Count)
            {
                _dtChiTiet.Rows.RemoveAt(index);
                RecalculateGrandTotal();
            }
        }
    }
}
