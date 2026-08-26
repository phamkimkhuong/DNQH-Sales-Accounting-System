using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.DataAccess
{
    /// <summary>
    /// Data Access Layer xử lý lưu trữ và tra cứu Nhật ký hoạt động doanh nghiệp (Business Audit Trail) tập trung trên SQL Server.
    /// </summary>
    public class NhatKyHoatDongDal
    {
        /// <summary>
        /// Ghi nhận 1 hành động nghiệp vụ vào bảng NHATKYHOATDONG tập trung trên SQL Server.
        /// Được thiết kế an toàn, không bao giờ ném ngoại lệ làm gián đoạn luồng nghiệp vụ chính.
        /// </summary>
        public static bool GhiNhatKy(NhatKyHoatDong entry)
        {
            if (entry == null) return false;

            try
            {
                string sql = @"
INSERT INTO NHATKYHOATDONG 
    (ThoiGian, CapDo, HanhDong, MaNV, TenNV, VaiTro, TenMay, LoaiDoiTuong, MaDoiTuong, KetQua, ThoiGianXuLyMs, CorrelationId, NoiDung)
VALUES 
    (@ThoiGian, @CapDo, @HanhDong, @MaNV, @TenNV, @VaiTro, @TenMay, @LoaiDoiTuong, @MaDoiTuong, @KetQua, @ThoiGianXuLyMs, @CorrelationId, @NoiDung);";

                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@ThoiGian", SqlDbType.DateTime2).Value = entry.ThoiGian;
                    cmd.Parameters.Add("@CapDo", SqlDbType.NVarChar, 20).Value = (object)entry.CapDo ?? "INFO";
                    cmd.Parameters.Add("@HanhDong", SqlDbType.NVarChar, 100).Value = (object)entry.HanhDong ?? "Unknown";
                    cmd.Parameters.Add("@MaNV", SqlDbType.NVarChar, 20).Value = (object)entry.MaNV ?? DBNull.Value;
                    cmd.Parameters.Add("@TenNV", SqlDbType.NVarChar, 100).Value = (object)entry.TenNV ?? DBNull.Value;
                    cmd.Parameters.Add("@VaiTro", SqlDbType.NVarChar, 50).Value = (object)entry.VaiTro ?? DBNull.Value;
                    cmd.Parameters.Add("@TenMay", SqlDbType.NVarChar, 100).Value = (object)entry.TenMay ?? Environment.MachineName;
                    cmd.Parameters.Add("@LoaiDoiTuong", SqlDbType.NVarChar, 50).Value = (object)entry.LoaiDoiTuong ?? DBNull.Value;
                    cmd.Parameters.Add("@MaDoiTuong", SqlDbType.NVarChar, 50).Value = (object)entry.MaDoiTuong ?? DBNull.Value;
                    cmd.Parameters.Add("@KetQua", SqlDbType.NVarChar, 30).Value = (object)entry.KetQua ?? "Thành công";
                    cmd.Parameters.Add("@ThoiGianXuLyMs", SqlDbType.BigInt).Value = entry.ThoiGianXuLyMs;
                    cmd.Parameters.Add("@CorrelationId", SqlDbType.NVarChar, 64).Value = (object)entry.CorrelationId ?? DBNull.Value;
                    cmd.Parameters.Add("@NoiDung", SqlDbType.NVarChar, -1).Value = (object)entry.NoiDung ?? DBNull.Value;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Ghi nhận lỗi chẩn đoán ra console, tuyệt đối không làm crash app
                Console.WriteLine("Lỗi ghi nhật ký hoạt động vào SQL Server: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Ghi nhật ký ngầm không chặn luồng UI/nghiệp vụ (Fire-and-forget).
        /// </summary>
        public static void GhiNhatKyAsync(NhatKyHoatDong entry)
        {
            Task.Run(() => GhiNhatKy(entry));
        }

        /// <summary>
        /// Tra cứu danh sách nhật ký hoạt động với các bộ lọc đa chiều (Thời gian, Nhân viên, Hành động, Cấp độ, Từ khóa).
        /// </summary>
        public static List<NhatKyHoatDong> LayDanhSachNhatKy(
            DateTime tuNgay, 
            DateTime denNgay, 
            string maNV = null, 
            string hanhDong = null, 
            string capDo = null, 
            string tuKhoa = null, 
            int top = 2000)
        {
            List<NhatKyHoatDong> list = new List<NhatKyHoatDong>();

            try
            {
                string sql = string.Format(@"
SELECT TOP ({0})
    Id, ThoiGian, CapDo, HanhDong, MaNV, TenNV, VaiTro, TenMay, LoaiDoiTuong, MaDoiTuong, KetQua, ThoiGianXuLyMs, CorrelationId, NoiDung
FROM NHATKYHOATDONG
WHERE ThoiGian >= @TuNgay AND ThoiGian <= @DenNgay
  AND HanhDong NOT IN ('ApplicationStartup', 'ApplicationShutdown', 'DevStartup', 'LOGIN', 'LOGOUT', 'DANG_NHAP', 'REPORT_KPI', 'LoadStockAlerts')", top > 0 ? top : 2000);

                List<SqlParameter> paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@TuNgay", SqlDbType.DateTime2) { Value = tuNgay });
                paramList.Add(new SqlParameter("@DenNgay", SqlDbType.DateTime2) { Value = denNgay });

                if (!string.IsNullOrWhiteSpace(maNV) && maNV != "ALL")
                {
                    sql += " AND MaNV = @MaNV";
                    paramList.Add(new SqlParameter("@MaNV", SqlDbType.NVarChar, 20) { Value = maNV.Trim() });
                }

                if (!string.IsNullOrWhiteSpace(hanhDong) && hanhDong != "ALL")
                {
                    sql += " AND HanhDong = @HanhDong";
                    paramList.Add(new SqlParameter("@HanhDong", SqlDbType.NVarChar, 100) { Value = hanhDong.Trim() });
                }

                if (!string.IsNullOrWhiteSpace(capDo) && capDo != "ALL")
                {
                    sql += " AND CapDo = @CapDo";
                    paramList.Add(new SqlParameter("@CapDo", SqlDbType.NVarChar, 20) { Value = capDo.Trim() });
                }

                if (!string.IsNullOrWhiteSpace(tuKhoa))
                {
                    sql += @" AND (
                        MaDoiTuong LIKE @TuKhoaPattern 
                        OR CorrelationId LIKE @TuKhoaPattern 
                        OR TenMay LIKE @TuKhoaPattern 
                        OR NoiDung LIKE @TuKhoaPattern 
                        OR TenNV LIKE @TuKhoaPattern
                        OR HanhDong LIKE @TuKhoaPattern
                    )";
                    paramList.Add(new SqlParameter("@TuKhoaPattern", SqlDbType.NVarChar, 200) { Value = "%" + tuKhoa.Trim() + "%" });
                }

                sql += " ORDER BY ThoiGian DESC, Id DESC;";

                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(paramList.ToArray());
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int stt = 1;
                        while (reader.Read())
                        {
                            NhatKyHoatDong item = new NhatKyHoatDong
                            {
                                STT = stt++,
                                Id = reader.GetInt64(reader.GetOrdinal("Id")),
                                ThoiGian = reader.GetDateTime(reader.GetOrdinal("ThoiGian")),
                                CapDo = reader["CapDo"] != DBNull.Value ? reader["CapDo"].ToString() : "INFO",
                                HanhDong = reader["HanhDong"] != DBNull.Value ? reader["HanhDong"].ToString() : string.Empty,
                                MaNV = reader["MaNV"] != DBNull.Value ? reader["MaNV"].ToString() : string.Empty,
                                TenNV = reader["TenNV"] != DBNull.Value ? reader["TenNV"].ToString() : string.Empty,
                                VaiTro = reader["VaiTro"] != DBNull.Value ? reader["VaiTro"].ToString() : string.Empty,
                                TenMay = reader["TenMay"] != DBNull.Value ? reader["TenMay"].ToString() : string.Empty,
                                LoaiDoiTuong = reader["LoaiDoiTuong"] != DBNull.Value ? reader["LoaiDoiTuong"].ToString() : string.Empty,
                                MaDoiTuong = reader["MaDoiTuong"] != DBNull.Value ? reader["MaDoiTuong"].ToString() : string.Empty,
                                KetQua = reader["KetQua"] != DBNull.Value ? reader["KetQua"].ToString() : string.Empty,
                                ThoiGianXuLyMs = reader["ThoiGianXuLyMs"] != DBNull.Value ? Convert.ToInt64(reader["ThoiGianXuLyMs"]) : 0,
                                CorrelationId = reader["CorrelationId"] != DBNull.Value ? reader["CorrelationId"].ToString() : string.Empty,
                                NoiDung = reader["NoiDung"] != DBNull.Value ? reader["NoiDung"].ToString() : string.Empty
                            };
                            list.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi truy vấn danh sách nhật ký hoạt động: " + ex.Message);
            }

            return list;
        }

        /// <summary>
        /// Tra cứu nhật ký hoạt động có phân trang Server-side bằng SQL Server OFFSET/FETCH.
        /// Đồng thời tổng hợp thống kê INFO, WARN, ERROR trong toàn kỳ chỉ qua 1 round-trip.
        /// </summary>
        public static PagedResult<NhatKyHoatDong> LayDanhSachNhatKyPhanTrang(
            DateTime tuNgay,
            DateTime denNgay,
            string maNV,
            string hanhDong,
            string capDo,
            string tuKhoa,
            int pageIndex,
            int pageSize,
            out int countInfo,
            out int countWarn,
            out int countError)
        {
            countInfo = 0;
            countWarn = 0;
            countError = 0;
            int totalRecords = 0;
            List<NhatKyHoatDong> list = new List<NhatKyHoatDong>();

            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 25;
            int offset = (pageIndex - 1) * pageSize;

            try
            {
                string whereClause = @"
WHERE ThoiGian >= @TuNgay AND ThoiGian <= @DenNgay
  AND HanhDong NOT IN ('ApplicationStartup', 'ApplicationShutdown', 'DevStartup', 'LOGIN', 'LOGOUT', 'DANG_NHAP', 'REPORT_KPI', 'LoadStockAlerts')";

                List<SqlParameter> paramList = new List<SqlParameter>();
                paramList.Add(new SqlParameter("@TuNgay", SqlDbType.DateTime2) { Value = tuNgay });
                paramList.Add(new SqlParameter("@DenNgay", SqlDbType.DateTime2) { Value = denNgay });

                if (!string.IsNullOrWhiteSpace(maNV) && maNV != "ALL")
                {
                    whereClause += " AND MaNV = @MaNV";
                    paramList.Add(new SqlParameter("@MaNV", SqlDbType.NVarChar, 20) { Value = maNV.Trim() });
                }

                if (!string.IsNullOrWhiteSpace(hanhDong) && hanhDong != "ALL")
                {
                    whereClause += " AND HanhDong = @HanhDong";
                    paramList.Add(new SqlParameter("@HanhDong", SqlDbType.NVarChar, 100) { Value = hanhDong.Trim() });
                }

                if (!string.IsNullOrWhiteSpace(capDo) && capDo != "ALL")
                {
                    whereClause += " AND CapDo = @CapDo";
                    paramList.Add(new SqlParameter("@CapDo", SqlDbType.NVarChar, 20) { Value = capDo.Trim() });
                }

                if (!string.IsNullOrWhiteSpace(tuKhoa))
                {
                    whereClause += @" AND (
                        MaDoiTuong LIKE @TuKhoaPattern 
                        OR CorrelationId LIKE @TuKhoaPattern 
                        OR TenMay LIKE @TuKhoaPattern 
                        OR NoiDung LIKE @TuKhoaPattern 
                        OR TenNV LIKE @TuKhoaPattern
                        OR HanhDong LIKE @TuKhoaPattern
                    )";
                    paramList.Add(new SqlParameter("@TuKhoaPattern", SqlDbType.NVarChar, 200) { Value = "%" + tuKhoa.Trim() + "%" });
                }

                string sqlCount = @"
SELECT 
    COUNT(1) AS TotalRecords,
    ISNULL(SUM(CASE WHEN CapDo = 'INFO' THEN 1 ELSE 0 END), 0) AS CntInfo,
    ISNULL(SUM(CASE WHEN CapDo = 'WARN' THEN 1 ELSE 0 END), 0) AS CntWarn,
    ISNULL(SUM(CASE WHEN CapDo = 'ERROR' THEN 1 ELSE 0 END), 0) AS CntError
FROM NHATKYHOATDONG " + whereClause + ";";

                string sqlData = @"
SELECT 
    Id, ThoiGian, CapDo, HanhDong, MaNV, TenNV, VaiTro, TenMay, LoaiDoiTuong, MaDoiTuong, KetQua, ThoiGianXuLyMs, CorrelationId, NoiDung
FROM NHATKYHOATDONG " + whereClause + @"
ORDER BY ThoiGian DESC, Id DESC
OFFSET @Offset ROWS
FETCH NEXT @PageSize ROWS ONLY;";

                string combinedSql = sqlCount + "\n" + sqlData;

                paramList.Add(new SqlParameter("@Offset", SqlDbType.Int) { Value = offset });
                paramList.Add(new SqlParameter("@PageSize", SqlDbType.Int) { Value = pageSize });

                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand(combinedSql, conn))
                {
                    cmd.Parameters.AddRange(paramList.ToArray());
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Resultset 1: Count & stats
                        if (reader.Read())
                        {
                            totalRecords = reader["TotalRecords"] != DBNull.Value ? Convert.ToInt32(reader["TotalRecords"]) : 0;
                            countInfo = reader["CntInfo"] != DBNull.Value ? Convert.ToInt32(reader["CntInfo"]) : 0;
                            countWarn = reader["CntWarn"] != DBNull.Value ? Convert.ToInt32(reader["CntWarn"]) : 0;
                            countError = reader["CntError"] != DBNull.Value ? Convert.ToInt32(reader["CntError"]) : 0;
                        }

                        // Resultset 2: Paged data
                        if (reader.NextResult())
                        {
                            int stt = offset + 1;
                            while (reader.Read())
                            {
                                NhatKyHoatDong item = new NhatKyHoatDong
                                {
                                    STT = stt++,
                                    Id = reader.GetInt64(reader.GetOrdinal("Id")),
                                    ThoiGian = reader.GetDateTime(reader.GetOrdinal("ThoiGian")),
                                    CapDo = reader["CapDo"] != DBNull.Value ? reader["CapDo"].ToString() : "INFO",
                                    HanhDong = reader["HanhDong"] != DBNull.Value ? reader["HanhDong"].ToString() : string.Empty,
                                    MaNV = reader["MaNV"] != DBNull.Value ? reader["MaNV"].ToString() : string.Empty,
                                    TenNV = reader["TenNV"] != DBNull.Value ? reader["TenNV"].ToString() : string.Empty,
                                    VaiTro = reader["VaiTro"] != DBNull.Value ? reader["VaiTro"].ToString() : string.Empty,
                                    TenMay = reader["TenMay"] != DBNull.Value ? reader["TenMay"].ToString() : string.Empty,
                                    LoaiDoiTuong = reader["LoaiDoiTuong"] != DBNull.Value ? reader["LoaiDoiTuong"].ToString() : string.Empty,
                                    MaDoiTuong = reader["MaDoiTuong"] != DBNull.Value ? reader["MaDoiTuong"].ToString() : string.Empty,
                                    KetQua = reader["KetQua"] != DBNull.Value ? reader["KetQua"].ToString() : string.Empty,
                                    ThoiGianXuLyMs = reader["ThoiGianXuLyMs"] != DBNull.Value ? Convert.ToInt64(reader["ThoiGianXuLyMs"]) : 0,
                                    CorrelationId = reader["CorrelationId"] != DBNull.Value ? reader["CorrelationId"].ToString() : string.Empty,
                                    NoiDung = reader["NoiDung"] != DBNull.Value ? reader["NoiDung"].ToString() : string.Empty
                                };
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi truy vấn nhật ký hoạt động phân trang: " + ex.Message);
            }

            return new PagedResult<NhatKyHoatDong>(list, totalRecords, pageIndex, pageSize);
        }

        /// <summary>
        /// Gọi Stored Procedure dọn dẹp các dòng log quá hạn (mặc định 30 ngày) trên SQL Server.
        /// </summary>
        public static int DonDepNhatKy(int soNgayLuuTru = 30)
        {
            if (soNgayLuuTru < 1) soNgayLuuTru = 30;

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                using (SqlCommand cmd = new SqlCommand("sp_DonDepNhatKyHoatDong", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SoNgayLuuTru", SqlDbType.Int).Value = soNgayLuuTru;

                    SqlParameter outParam = new SqlParameter("@SoDongDaXoa", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    int deletedRows = outParam.Value != DBNull.Value ? (int)outParam.Value : 0;
                    return deletedRows;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi thực thi thủ tục dọn dẹp log: " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Lấy danh sách các nhân viên đã có hoạt động trong bảng nhật ký.
        /// </summary>
        public static DataTable LayDanhSachNhanVienCoHoatDong()
        {
            string sql = @"
SELECT DISTINCT MaNV, TenNV 
FROM NHATKYHOATDONG 
WHERE MaNV IS NOT NULL AND MaNV <> ''
ORDER BY TenNV, MaNV;";

            try
            {
                return Database.ExecuteQuery(sql);
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// Lấy danh sách các loại hành động đã từng thực hiện trong bảng nhật ký.
        /// </summary>
        public static List<string> LayDanhSachHanhDong()
        {
            List<string> list = new List<string>();
            string sql = @"
SELECT DISTINCT HanhDong 
FROM NHATKYHOATDONG 
WHERE HanhDong IS NOT NULL AND HanhDong <> ''
  AND HanhDong NOT IN ('ApplicationStartup', 'ApplicationShutdown', 'DevStartup', 'LOGIN', 'LOGOUT', 'DANG_NHAP', 'REPORT_KPI', 'LoadStockAlerts')
ORDER BY HanhDong;";

            try
            {
                DataTable dt = Database.ExecuteQuery(sql);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row["HanhDong"].ToString());
                }
            }
            catch { }

            return list;
        }
    }
}
