using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using DNQH_KeToanBanHang.Constants;
using DNQH_KeToanBanHang.DataAccess;
using DNQH_KeToanBanHang.Helpers;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Services
{
    public class AccountingService
    {
        private readonly PhieuThuDAL _phieuThuDAL;
        private readonly PhieuChiDAL _phieuChiDAL;
        private readonly ChungTuDAL _chungTuDAL;
        private readonly HoaDonBanDAL _hoaDonBanDAL;

        public AccountingService()
        {
            _phieuThuDAL = new PhieuThuDAL();
            _phieuChiDAL = new PhieuChiDAL();
            _chungTuDAL = new ChungTuDAL();
            _hoaDonBanDAL = new HoaDonBanDAL();
        }

        #region 1. Quản lý Phiếu Thu

        public string GenerateNewReceiptId()
        {
            return _phieuThuDAL.GetNextMaPT();
        }

        public string CreatePhieuThu(PhieuThu pt)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = (SessionManager.CurrentUser != null) ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = (SessionManager.CurrentUser != null) ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                // 1. Kiểm tra phân quyền: Chỉ Quản trị viên và Kế toán
                if (!SessionManager.IsLoggedIn || SessionManager.CurrentUser == null)
                {
                    throw new UnauthorizedAccessException("Yêu cầu đăng nhập trước khi lập phiếu thu.");
                }

                if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
                {
                    throw new UnauthorizedAccessException("Bạn không có quyền lập Phiếu thu. Chỉ Quản trị viên và Nhân viên kế toán mới có quyền này.");
                }

                if (pt == null)
                {
                    throw new ArgumentException("Dữ liệu phiếu thu không hợp lệ.");
                }

                // 2. Chống giả mạo danh tính: Luôn gán cứng MaNV của phiên đăng nhập
                pt.MaNV = SessionManager.CurrentUser.MaNV;

                // 3. Kiểm tra các tham số đầu vào
                if (string.IsNullOrWhiteSpace(pt.MaHDB))
                {
                    throw new ArgumentException("Vui lòng chọn Hóa đơn bán cần thu tiền.");
                }

                if (pt.SoTien <= 0)
                {
                    throw new ArgumentException("Số tiền thu phải lớn hơn 0 VNĐ.");
                }

                if (string.IsNullOrWhiteSpace(pt.NguoiNop))
                {
                    pt.NguoiNop = "Khách hàng";
                }

                if (pt.NgayThu == null)
                {
                    pt.NgayThu = DateTime.Now;
                }

                // 4. Thực hiện Atomic Transaction: Khóa dòng Hóa đơn (UPDLOCK, HOLDLOCK) ->
                // Kiểm tra lại số tiền còn lại -> Sinh mã mới an toàn -> Insert Phiếu thu -> Cập nhật trạng thái Hóa đơn
                Database.ExecuteTransaction(delegate(SqlTransaction trans)
                {
                    // 4.1: Khóa dòng HOADONBAN để bảo vệ Concurrency (chống 2 phiên cùng thu vượt tổng tiền)
                    string lockSql = "SELECT TongTien, TrangThai FROM HOADONBAN WITH (UPDLOCK, HOLDLOCK) WHERE MaHDB = @MaHDB";
                    decimal tongTienHDB = 0m;
                    string trangThaiHDB = "";

                    using (SqlCommand cmd = new SqlCommand(lockSql, trans.Connection, trans))
                    {
                        cmd.Parameters.Add(new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = pt.MaHDB });
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                throw new InvalidOperationException(
                                    string.Format("Không tìm thấy Hóa đơn bán [{0}] trong hệ thống.", pt.MaHDB));
                            }
                            tongTienHDB = reader["TongTien"] != DBNull.Value ? Convert.ToDecimal(reader["TongTien"]) : 0m;
                            trangThaiHDB = reader["TrangThai"] != DBNull.Value ? reader["TrangThai"].ToString().Trim() : "";
                        }
                    }

                    if (trangThaiHDB == "Hủy" || trangThaiHDB == "Đã hủy")
                    {
                        throw new InvalidOperationException(
                            string.Format("Hóa đơn bán [{0}] đã bị hủy, không thể lập phiếu thu!", pt.MaHDB));
                    }

                    // 4.2: Đọc tổng số tiền đã thu trước đó của hóa đơn trong transaction
                    decimal daThu = _phieuThuDAL.GetTongTienDaThu(pt.MaHDB, trans);
                    decimal conLai = tongTienHDB - daThu;

                    if (conLai <= 0)
                    {
                        throw new InvalidOperationException(string.Format(
                            "Hóa đơn [{0}] đã được thanh toán đủ 100%, không còn tiền phải thu!", pt.MaHDB));
                    }

                    if (pt.SoTien > conLai)
                    {
                        throw new InvalidOperationException(string.Format(
                            "Số tiền thu ({0:N0} VNĐ) vượt quá số tiền còn lại phải thu ({1:N0} VNĐ) của hóa đơn [{2}]!",
                            pt.SoTien, conLai, pt.MaHDB));
                    }

                    // 4.3: Luôn tự sinh mã mới an toàn trong transaction với khóa TABLOCKX, HOLDLOCK
                    pt.MaPT = _phieuThuDAL.GetNextMaPT(trans);

                    // 4.4: Insert PHIEUTHU
                    _phieuThuDAL.Insert(pt, trans);

                    // 4.5: Cập nhật trạng thái Hóa đơn bán
                    decimal tongDaThuMoi = daThu + pt.SoTien;
                    string trangThaiMoi = (tongDaThuMoi >= tongTienHDB) ? InvoiceStatusConstants.DaThanhToan : InvoiceStatusConstants.ThanhToanMotPhan;

                    string updateHdbSql = "UPDATE HOADONBAN SET TrangThai = @TrangThai WHERE MaHDB = @MaHDB";
                    using (SqlCommand updateCmd = new SqlCommand(updateHdbSql, trans.Connection, trans))
                    {
                        updateCmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30) { Value = trangThaiMoi });
                        updateCmd.Parameters.Add(new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = pt.MaHDB });
                        updateCmd.ExecuteNonQuery();
                    }
                });

                sw.Stop();
                AppLogger.Info(
                    "CreatePhieuThu",
                    string.Format("Lập phiếu thu thành công: MaPT={0}, MaHDB={1}, SoTien={2:N0}, NguoiNop={3}",
                        pt.MaPT, pt.MaHDB, pt.SoTien, pt.NguoiNop),
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "PHIEUTHU",
                    pt.MaPT,
                    currentRole,
                    "Success");

                return pt.MaPT;
            }
            catch (Exception ex)
            {
                sw.Stop();
                AppLogger.Error(
                    "CreatePhieuThu",
                    "Thất bại khi lập phiếu thu: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "PHIEUTHU",
                    (pt != null) ? pt.MaPT : null,
                    currentRole,
                    "Failed");

                throw;
            }
        }

        public bool CreatePhieuThu(PhieuThu pt, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                CreatePhieuThu(pt);
                return true;
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    errorMessage = "Đã xảy ra lỗi khi lưu dữ liệu phiếu thu vào cơ sở dữ liệu. Vui lòng thử lại hoặc liên hệ quản trị viên.";
                }
                else
                {
                    errorMessage = ex.Message;
                }
                return false;
            }
        }

        #endregion

        #region 2. Quản lý Phiếu Chi

        public string GenerateNewPaymentId()
        {
            return _phieuChiDAL.GetNextMaPC();
        }

        public string CreatePhieuChi(PhieuChi pc)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = (SessionManager.CurrentUser != null) ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = (SessionManager.CurrentUser != null) ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                // 1. Kiểm tra phân quyền: Chỉ Quản trị viên và Kế toán
                if (!SessionManager.IsLoggedIn || SessionManager.CurrentUser == null)
                {
                    throw new UnauthorizedAccessException("Yêu cầu đăng nhập trước khi lập phiếu chi.");
                }

                if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
                {
                    throw new UnauthorizedAccessException("Bạn không có quyền lập Phiếu chi. Chỉ Quản trị viên và Nhân viên kế toán mới có quyền này.");
                }

                if (pc == null)
                {
                    throw new ArgumentException("Dữ liệu phiếu chi không hợp lệ.");
                }

                // 2. Chống giả mạo danh tính: Luôn gán cứng MaNV của phiên đăng nhập
                pc.MaNV = SessionManager.CurrentUser.MaNV;

                // 3. Kiểm tra các tham số đầu vào
                if (string.IsNullOrWhiteSpace(pc.NguoiNhan))
                {
                    throw new ArgumentException("Vui lòng nhập họ tên người nhận tiền.");
                }

                if (string.IsNullOrWhiteSpace(pc.LyDoChi))
                {
                    throw new ArgumentException("Vui lòng nhập lý do chi tiền.");
                }

                if (pc.SoTien <= 0)
                {
                    throw new ArgumentException("Số tiền chi phải lớn hơn 0 VNĐ.");
                }

                if (pc.NgayChi == null)
                {
                    pc.NgayChi = DateTime.Now;
                }

                // 4. Thực hiện Atomic Transaction:
                // Tự sinh mã mới an toàn (TABLOCKX, HOLDLOCK) -> Insert Phiếu chi
                Database.ExecuteTransaction(delegate(SqlTransaction trans)
                {
                    pc.MaPC = _phieuChiDAL.GetNextMaPC(trans);
                    _phieuChiDAL.Insert(pc, trans);
                });

                sw.Stop();
                AppLogger.Info(
                    "CreatePhieuChi",
                    string.Format("Lập phiếu chi thành công: MaPC={0}, SoTien={1:N0}, NguoiNhan={2}, LyDo={3}",
                        pc.MaPC, pc.SoTien, pc.NguoiNhan, pc.LyDoChi),
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "PHIEUCHI",
                    pc.MaPC,
                    currentRole,
                    "Success");

                return pc.MaPC;
            }
            catch (Exception ex)
            {
                sw.Stop();
                AppLogger.Error(
                    "CreatePhieuChi",
                    "Thất bại khi lập phiếu chi: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "PHIEUCHI",
                    (pc != null) ? pc.MaPC : null,
                    currentRole,
                    "Failed");

                throw;
            }
        }

        public bool CreatePhieuChi(PhieuChi pc, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                CreatePhieuChi(pc);
                return true;
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    errorMessage = "Đã xảy ra lỗi khi lưu dữ liệu phiếu chi vào cơ sở dữ liệu. Vui lòng thử lại hoặc liên hệ quản trị viên.";
                }
                else
                {
                    errorMessage = ex.Message;
                }
                return false;
            }
        }

        #endregion

        #region 3. Quản lý Chứng Từ Kế Toán

        public string GenerateNewDocumentId()
        {
            return _chungTuDAL.GetNextMaCT();
        }

        public string CreateChungTu(ChungTu ct, List<ChiTietChungTu> details)
        {
            string correlationId = Guid.NewGuid().ToString("N");
            string currentUserId = (SessionManager.CurrentUser != null) ? SessionManager.CurrentUser.MaNV : "Unknown";
            string currentRole = (SessionManager.CurrentUser != null) ? SessionManager.CurrentUser.VaiTro : "Unknown";
            Stopwatch sw = Stopwatch.StartNew();

            try
            {
                // 1. Kiểm tra phân quyền: Chỉ Quản trị viên và Kế toán
                if (!SessionManager.IsLoggedIn || SessionManager.CurrentUser == null)
                {
                    throw new UnauthorizedAccessException("Yêu cầu đăng nhập trước khi lập chứng từ kế toán.");
                }

                if (!SessionManager.IsAdmin() && !SessionManager.IsAccountant())
                {
                    throw new UnauthorizedAccessException("Bạn không có quyền lập Chứng từ. Chỉ Quản trị viên và Nhân viên kế toán mới có quyền này.");
                }

                if (ct == null)
                {
                    throw new ArgumentException("Dữ liệu chứng từ không hợp lệ.");
                }

                // 2. Chống giả mạo danh tính: Luôn gán cứng MaNV của phiên đăng nhập
                ct.MaNV = SessionManager.CurrentUser.MaNV;

                // 3. Kiểm tra tham số hóa đơn
                if (string.IsNullOrWhiteSpace(ct.MaHDB))
                {
                    throw new ArgumentException("Vui lòng chọn Hóa đơn bán cần lập chứng từ.");
                }

                if (ct.NgayCT == null)
                {
                    ct.NgayCT = DateTime.Now;
                }

                if (string.IsNullOrWhiteSpace(ct.LoaiCT))
                {
                    ct.LoaiCT = DocumentTypeConstants.ChungTuBanHang;
                }

                // 4. Kiểm tra danh sách chi tiết định khoản
                if (details == null || details.Count == 0)
                {
                    throw new ArgumentException("Chứng từ kế toán phải có ít nhất một dòng định khoản chi tiết.");
                }

                decimal tongTienChiTiet = 0m;
                for (int i = 0; i < details.Count; i++)
                {
                    ChiTietChungTu d = details[i];
                    if (string.IsNullOrWhiteSpace(d.TaiKhoanNo))
                    {
                        throw new ArgumentException(string.Format("Dòng {0}: Chưa nhập Tài khoản Nợ.", i + 1));
                    }
                    if (string.IsNullOrWhiteSpace(d.TaiKhoanCo))
                    {
                        throw new ArgumentException(string.Format("Dòng {0}: Chưa nhập Tài khoản Có.", i + 1));
                    }
                    if (d.SoTien <= 0)
                    {
                        throw new ArgumentException(string.Format("Dòng {0}: Số tiền hạch toán phải lớn hơn 0 VNĐ.", i + 1));
                    }
                    tongTienChiTiet += d.SoTien;
                }

                // 5. Thực hiện Atomic Transaction:
                // Kiểm tra hóa đơn tồn tại và chưa bị hủy -> Tự sinh mã CT (TABLOCKX, HOLDLOCK) ->
                // Insert CHUNGTU -> Insert tất cả CHITIETCHUNGTU
                Database.ExecuteTransaction(delegate(SqlTransaction trans)
                {
                    // Kiểm tra hóa đơn bán tồn tại
                    string checkSql = "SELECT TrangThai FROM HOADONBAN WITH (HOLDLOCK) WHERE MaHDB = @MaHDB";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, trans.Connection, trans))
                    {
                        checkCmd.Parameters.Add(new SqlParameter("@MaHDB", SqlDbType.Char, 12) { Value = ct.MaHDB });
                        using (SqlDataReader reader = checkCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                throw new InvalidOperationException(
                                    string.Format("Không tìm thấy Hóa đơn bán [{0}] trong hệ thống.", ct.MaHDB));
                            }

                            string st = !reader.IsDBNull(0) ? reader.GetString(0).Trim() : "";
                            if (st == "Hủy" || st == "Đã hủy")
                            {
                                throw new InvalidOperationException(
                                    string.Format("Hóa đơn bán [{0}] đã bị hủy, không thể lập chứng từ kế toán!", ct.MaHDB));
                            }
                        }
                    }

                    // Tự sinh mã mới an toàn trong transaction với khóa TABLOCKX, HOLDLOCK
                    ct.MaCT = _chungTuDAL.GetNextMaCT(trans);

                    // Insert Header CHUNGTU
                    _chungTuDAL.Insert(ct, trans);

                    // Insert các dòng chi tiết
                    for (int i = 0; i < details.Count; i++)
                    {
                        ChiTietChungTu d = details[i];
                        d.MaCT = ct.MaCT;
                        d.STT = i + 1;
                        _chungTuDAL.InsertChiTiet(d, trans);
                    }
                });

                sw.Stop();
                AppLogger.Info(
                    "CreateChungTu",
                    string.Format("Lập chứng từ kế toán thành công: MaCT={0}, MaHDB={1}, SoDong={2}, TongTien={3:N0}",
                        ct.MaCT, ct.MaHDB, details.Count, tongTienChiTiet),
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "CHUNGTU",
                    ct.MaCT,
                    currentRole,
                    "Success");

                return ct.MaCT;
            }
            catch (Exception ex)
            {
                sw.Stop();
                AppLogger.Error(
                    "CreateChungTu",
                    "Thất bại khi lập chứng từ: " + ex.Message,
                    ex,
                    correlationId,
                    currentUserId,
                    sw.ElapsedMilliseconds,
                    "CHUNGTU",
                    (ct != null) ? ct.MaCT : null,
                    currentRole,
                    "Failed");

                throw;
            }
        }

        public bool CreateChungTu(ChungTu ct, List<ChiTietChungTu> details, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                CreateChungTu(ct, details);
                return true;
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    errorMessage = "Đã xảy ra lỗi khi lưu dữ liệu chứng từ vào cơ sở dữ liệu. Vui lòng thử lại hoặc liên hệ quản trị viên.";
                }
                else
                {
                    errorMessage = ex.Message;
                }
                return false;
            }
        }

        #endregion
    }
}
