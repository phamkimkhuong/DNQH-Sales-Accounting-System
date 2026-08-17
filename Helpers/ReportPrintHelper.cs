using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Helpers
{
    /// <summary>
    /// Tiện ích sinh mẫu in Báo cáo Tổng hợp & Sổ Chi Tiết Kế Toán theo chuẩn A4 (HTML/CSS).
    /// </summary>
    public static class ReportPrintHelper
    {
        private static string GetHtmlHead(string title, bool isLandscape = true)
        {
            string pageSize = isLandscape ? "A4 landscape" : "A4 portrait";
            return string.Format(@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <title>{0}</title>
    <style>
        @page {{
            size: {1};
            margin: 10mm 12mm 12mm 12mm;
        }}
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: 12px;
            color: #1e293b;
            margin: 0;
            padding: 15px;
            background-color: #fff;
        }}
        .report-container {{
            max-width: {2};
            margin: 0 auto;
            background: #fff;
        }}
        .header-table {{
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 16px;
        }}
        .company-name {{
            font-size: 14px;
            font-weight: bold;
            color: #0f172a;
            text-transform: uppercase;
        }}
        .company-info {{
            font-size: 11.5px;
            color: #475569;
            line-height: 1.4;
        }}
        .report-standard {{
            text-align: right;
            font-size: 11px;
            color: #64748b;
            font-style: italic;
            vertical-align: top;
        }}
        .report-title {{
            text-align: center;
            font-size: 18px;
            font-weight: bold;
            color: #0f172a;
            text-transform: uppercase;
            margin-top: 5px;
            letter-spacing: 0.5px;
        }}
        .report-subtitle {{
            text-align: center;
            font-size: 12px;
            color: #475569;
            margin-top: 4px;
            margin-bottom: 16px;
            font-style: italic;
        }}
        .filter-badge {{
            text-align: center;
            font-size: 12px;
            color: #334155;
            margin-bottom: 14px;
        }}
        .filter-badge b {{
            color: #1e293b;
        }}
        .data-table {{
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 16px;
            font-size: 11.5px;
        }}
        .data-table th {{
            background-color: #f1f5f9;
            color: #0f172a;
            font-weight: 600;
            border: 1px solid #cbd5e1;
            padding: 7px 6px;
            text-align: center;
        }}
        .data-table td {{
            border: 1px solid #e2e8f0;
            padding: 6px 6px;
            vertical-align: middle;
        }}
        .data-table tr:nth-child(even) {{
            background-color: #f8fafc;
        }}
        .data-table .total-row td {{
            font-weight: bold;
            background-color: #f1f5f9;
            border-top: 2px solid #94a3b8;
            border-bottom: 2px solid #94a3b8;
            color: #0f172a;
        }}
        .text-center {{ text-align: center; }}
        .text-left {{ text-align: left; }}
        .text-right {{ text-align: right; }}
        .money-in-words {{
            font-size: 12px;
            font-style: italic;
            margin: 10px 0 20px 0;
            color: #1e293b;
        }}
        .signature-table {{
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
            page-break-inside: avoid;
        }}
        .signature-cell {{
            text-align: center;
            vertical-align: top;
            width: 33.33%;
        }}
        .signature-title {{
            font-weight: bold;
            font-size: 12px;
            color: #0f172a;
        }}
        .signature-sub {{
            font-size: 11px;
            color: #64748b;
            font-style: italic;
            margin-bottom: 50px;
        }}
        .signature-date {{
            text-align: right;
            font-size: 11.5px;
            font-style: italic;
            color: #475569;
            margin-bottom: 10px;
            padding-right: 15px;
        }}
        @media print {{
            body {{
                padding: 0;
            }}
            .data-table th {{
                background-color: #f1f5f9 !important;
                -webkit-print-color-adjust: exact;
                print-color-adjust: exact;
            }}
            .data-table .total-row td {{
                background-color: #f1f5f9 !important;
                -webkit-print-color-adjust: exact;
                print-color-adjust: exact;
            }}
        }}
    </style>
</head>
<body>
<div class=""report-container"">", title, pageSize, isLandscape ? "1080px" : "800px");
        }

        private static string GetHtmlFoot()
        {
            return @"</div>
</body>
</html>";
        }

        private static string BuildHeaderSection(string reportCode)
        {
            return string.Format(@"
    <table class=""header-table"">
        <tr>
            <td style=""width: 65%;"">
                <div class=""company-name"">{0}</div>
                <div class=""company-info"">Địa chỉ: {1}<br/>Mã số thuế: {2} - ĐT: {3}</div>
            </td>
            <td class=""report-standard"">
                {4}
            </td>
        </tr>
    </table>",
                VoucherPrintHelper.CompanyName,
                VoucherPrintHelper.CompanyAddress,
                VoucherPrintHelper.CompanyTaxCode,
                VoucherPrintHelper.CompanyPhone,
                reportCode);
        }

        private static string BuildSignaturesSection(DateTime printDate)
        {
            return string.Format(@"
    <div class=""signature-date"">Ngày {0:dd} tháng {0:MM} năm {0:yyyy}</div>
    <table class=""signature-table"">
        <tr>
            <td class=""signature-cell"">
                <div class=""signature-title"">NGƯỜI LẬP BIỂU</div>
                <div class=""signature-sub"">(Ký, ghi rõ họ tên)</div>
            </td>
            <td class=""signature-cell"">
                <div class=""signature-title"">KẾ TOÁN TRƯỞNG</div>
                <div class=""signature-sub"">(Ký, ghi rõ họ tên)</div>
            </td>
            <td class=""signature-cell"">
                <div class=""signature-title"">GIÁM ĐỐC / THỦ TRƯỞNG</div>
                <div class=""signature-sub"">(Ký, đóng dấu, ghi rõ họ tên)</div>
            </td>
        </tr>
    </table>", printDate);
        }

        /// <summary>
        /// 1. Báo Cáo Doanh Thu Bán Hàng (Landscape)
        /// </summary>
        public static string GenerateBaoCaoDoanhThuHtml(DateTime tuNgay, DateTime denNgay, List<BaoCaoDoanhThuDTO> list, decimal tongDoanhThu, int soHoaDon)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetHtmlHead("Báo Cáo Doanh Thu Bán Hàng", true));
            sb.Append(BuildHeaderSection("Mẫu biểu: BC-01/BH<br/>(Hệ thống Kế toán Quản trị OLC)"));

            sb.Append(@"
    <div class=""report-title"">BÁO CÁO DOANH THU BÁN HÀNG</div>");
            sb.AppendFormat(@"
    <div class=""report-subtitle"">Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}</div>", tuNgay, denNgay);

            sb.AppendFormat(@"
    <div class=""filter-badge"">Tổng số hóa đơn: <b>{0:N0}</b> &bull; Tổng doanh thu: <b>{1:N0} VND</b></div>", soHoaDon, tongDoanhThu);

            sb.Append(@"
    <table class=""data-table"">
        <thead>
            <tr>
                <th style=""width: 32px;"">STT</th>
                <th style=""width: 95px;"">Số Hóa Đơn</th>
                <th style=""width: 78px;"">Ngày Lập</th>
                <th style=""width: 60px;"">Mã KH</th>
                <th>Tên Khách Hàng</th>
                <th style=""width: 110px;"">Nhân Viên Lập</th>
                <th style=""width: 48px;"">Số MH</th>
                <th style=""width: 110px;"">Doanh Thu (VND)</th>
                <th style=""width: 105px;"">Trạng Thái</th>
            </tr>
        </thead>
        <tbody>");

            if (list == null || list.Count == 0)
            {
                sb.Append(@"<tr><td colspan=""9"" class=""text-center"" style=""padding: 20px; color: #64748b;"">Không có dữ liệu doanh thu trong khoảng thời gian đã chọn.</td></tr>");
            }
            else
            {
                int stt = 1;
                foreach (var item in list)
                {
                    sb.AppendFormat(@"
            <tr>
                <td class=""text-center"">{0}</td>
                <td class=""text-center""><b>{1}</b></td>
                <td class=""text-center"">{2:dd/MM/yyyy}</td>
                <td class=""text-center"">{3}</td>
                <td>{4}</td>
                <td>{5}</td>
                <td class=""text-center"">{6}</td>
                <td class=""text-right"">{7:N0}</td>
                <td class=""text-center"">{8}</td>
            </tr>",
                        stt++,
                        item.MaHDB,
                        item.NgayLap,
                        item.MaKH,
                        item.TenKH,
                        item.TenNV ?? item.MaNV,
                        item.SoMatHang,
                        item.TongTien,
                        item.TrangThai);
                }

                // Dòng tổng cộng
                sb.AppendFormat(@"
            <tr class=""total-row"">
                <td colspan=""6"" class=""text-center"">TỔNG CỘNG ({0} HÓA ĐƠN)</td>
                <td class=""text-center"">-</td>
                <td class=""text-right"">{1:N0}</td>
                <td></td>
            </tr>", list.Count, tongDoanhThu);
            }

            sb.Append(@"
        </tbody>
    </table>");

            string moneyText = VietnameseNumberReader.ReadMoney(tongDoanhThu);
            sb.AppendFormat(@"
    <div class=""money-in-words""><b>Tổng doanh thu bằng chữ:</b> {0}</div>", moneyText);

            sb.Append(BuildSignaturesSection(DateTime.Now));
            sb.Append(GetHtmlFoot());
            return sb.ToString();
        }

        /// <summary>
        /// 2. Báo Cáo Dòng Tiền Thu - Chi (Landscape)
        /// </summary>
        public static string GenerateBaoCaoThuChiHtml(DateTime tuNgay, DateTime denNgay, List<BaoCaoThuChiDTO> list, decimal tongThu, decimal tongChi, decimal chenhLech)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetHtmlHead("Báo Cáo Tổng Hợp Thu - Chi", true));
            sb.Append(BuildHeaderSection("Mẫu biểu: BC-02/TC<br/>(Sổ quỹ dòng tiền OLC)"));

            sb.Append(@"
    <div class=""report-title"">BÁO CÁO TỔNG HỢP THU - CHI DÒNG TIỀN</div>");
            sb.AppendFormat(@"
    <div class=""report-subtitle"">Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}</div>", tuNgay, denNgay);

            sb.AppendFormat(@"
    <div class=""filter-badge"">Tổng thu: <b style=""color: #059669;"">{0:N0} VND</b> &bull; Tổng chi: <b style=""color: #e11d48;"">{1:N0} VND</b> &bull; Chênh lệch thuần: <b>{2:N0} VND</b></div>",
                tongThu, tongChi, chenhLech);

            sb.Append(@"
    <table class=""data-table"">
        <thead>
            <tr>
                <th style=""width: 35px;"">STT</th>
                <th style=""width: 85px;"">Ngày GD</th>
                <th style=""width: 70px;"">Loại</th>
                <th style=""width: 95px;"">Mã Phiếu</th>
                <th>Người Giao Dịch</th>
                <th style=""width: 110px;"">Tiền Thu (VND)</th>
                <th style=""width: 110px;"">Tiền Chi (VND)</th>
                <th style=""width: 85px;"">Hình Thức</th>
                <th style=""width: 110px;"">HĐ Liên Kết</th>
                <th>Lý Do Giao Dịch</th>
            </tr>
        </thead>
        <tbody>");

            if (list == null || list.Count == 0)
            {
                sb.Append(@"<tr><td colspan=""10"" class=""text-center"" style=""padding: 20px; color: #64748b;"">Không có giao dịch thu/chi trong khoảng thời gian đã chọn.</td></tr>");
            }
            else
            {
                int stt = 1;
                foreach (var item in list)
                {
                    string colorStyle = item.LoaiGiaoDich == "Thu" ? "color: #059669; font-weight: bold;" : "color: #e11d48; font-weight: bold;";
                    sb.AppendFormat(@"
            <tr>
                <td class=""text-center"">{0}</td>
                <td class=""text-center"">{1:dd/MM/yyyy}</td>
                <td class=""text-center"" style=""{2}"">{3}</td>
                <td class=""text-center""><b>{4}</b></td>
                <td>{5}</td>
                <td class=""text-right"">{6}</td>
                <td class=""text-right"">{7}</td>
                <td class=""text-center"">{8}</td>
                <td class=""text-center"">{9}</td>
                <td>{10}</td>
            </tr>",
                        stt++,
                        item.NgayGiaoDich,
                        colorStyle,
                        item.LoaiGiaoDich,
                        item.MaChungTu,
                        item.NguoiGiaoDich,
                        item.SoTienThu > 0 ? string.Format("{0:N0}", item.SoTienThu) : "-",
                        item.SoTienChi > 0 ? string.Format("{0:N0}", item.SoTienChi) : "-",
                        item.HinhThuc,
                        item.MaHDBLienKet ?? "-",
                        item.LyDo);
                }

                // Dòng tổng cộng
                sb.AppendFormat(@"
            <tr class=""total-row"">
                <td colspan=""5"" class=""text-center"">TỔNG CỘNG DÒNG TIỀN PHÁT SINH</td>
                <td class=""text-right"" style=""color: #059669;"">{0:N0}</td>
                <td class=""text-right"" style=""color: #e11d48;"">{1:N0}</td>
                <td colspan=""3"">Chênh lệch: <b>{2:N0} VND</b></td>
            </tr>", tongThu, tongChi, chenhLech);
            }

            sb.Append(@"
        </tbody>
    </table>");

            string moneyText = VietnameseNumberReader.ReadMoney(chenhLech >= 0 ? chenhLech : -chenhLech);
            sb.AppendFormat(@"
    <div class=""money-in-words""><b>Chênh lệch thu chi thuần bằng chữ:</b> {0} ({1})</div>",
                moneyText, chenhLech >= 0 ? "Dương dòng tiền" : "Âm dòng tiền");

            sb.Append(BuildSignaturesSection(DateTime.Now));
            sb.Append(GetHtmlFoot());
            return sb.ToString();
        }

        /// <summary>
        /// 3. Báo Cáo Tổng Hợp Tồn Kho (Landscape)
        /// </summary>
        public static string GenerateBaoCaoTonKhoHtml(string tenKho, List<BaoCaoTonKhoDTO> list, int tongSoLuong, decimal tongGiaTri)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetHtmlHead("Báo Cáo Tổng Hợp Tồn Kho", true));
            sb.Append(BuildHeaderSection("Mẫu biểu: BC-03/TK<br/>(Báo cáo Quản trị Kho OLC)"));

            sb.Append(@"
    <div class=""report-title"">BÁO CÁO TỔNG HỢP TỒN KHO HÀNG HÓA</div>");
            sb.AppendFormat(@"
    <div class=""report-subtitle"">Phạm vi: <b>{0}</b> &bull; Thời điểm lập: {1:HH:mm dd/MM/yyyy}</div>",
                string.IsNullOrEmpty(tenKho) ? "Toàn bộ hệ thống kho" : tenKho, DateTime.Now);

            sb.AppendFormat(@"
    <div class=""filter-badge"">Tổng số lượng tồn: <b>{0:N0}</b> &bull; Tổng giá trị tồn kho: <b>{1:N0} VND</b></div>",
                tongSoLuong, tongGiaTri);

            sb.Append(@"
    <table class=""data-table"">
        <thead>
            <tr>
                <th style=""width: 40px;"">STT</th>
                <th style=""width: 140px;"">Kho Hàng</th>
                <th style=""width: 85px;"">Mã SP</th>
                <th>Tên Sản Phẩm</th>
                <th style=""width: 130px;"">Loại Hàng</th>
                <th style=""width: 65px;"">ĐVT</th>
                <th style=""width: 90px;"">Số Lượng</th>
                <th style=""width: 110px;"">Đơn Giá (VND)</th>
                <th style=""width: 130px;"">Giá Trị Tồn (VND)</th>
                <th style=""width: 90px;"">Cập Nhật</th>
            </tr>
        </thead>
        <tbody>");

            if (list == null || list.Count == 0)
            {
                sb.Append(@"<tr><td colspan=""10"" class=""text-center"" style=""padding: 20px; color: #64748b;"">Không có dữ liệu tồn kho.</td></tr>");
            }
            else
            {
                int stt = 1;
                foreach (var item in list)
                {
                    sb.AppendFormat(@"
            <tr>
                <td class=""text-center"">{0}</td>
                <td>{1}</td>
                <td class=""text-center""><b>{2}</b></td>
                <td>{3}</td>
                <td>{4}</td>
                <td class=""text-center"">{5}</td>
                <td class=""text-right""><b>{6:N0}</b></td>
                <td class=""text-right"">{7:N0}</td>
                <td class=""text-right""><b>{8:N0}</b></td>
                <td class=""text-center"">{9:dd/MM/yyyy}</td>
            </tr>",
                        stt++,
                        item.TenKho,
                        item.MaSP,
                        item.TenSP,
                        item.TenLoaiSP,
                        item.DonViTinh,
                        item.SoLuongTon,
                        item.DonGia,
                        item.GiaTriTon,
                        item.NgayCapNhat);
                }

                // Dòng tổng cộng
                sb.AppendFormat(@"
            <tr class=""total-row"">
                <td colspan=""6"" class=""text-center"">TỔNG CỘNG TỒN KHO ({0} MẶT HÀNG)</td>
                <td class=""text-right"">{1:N0}</td>
                <td></td>
                <td class=""text-right"">{2:N0}</td>
                <td></td>
            </tr>", list.Count, tongSoLuong, tongGiaTri);
            }

            sb.Append(@"
        </tbody>
    </table>");

            string moneyText = VietnameseNumberReader.ReadMoney(tongGiaTri);
            sb.AppendFormat(@"
    <div class=""money-in-words""><b>Tổng giá trị tồn kho bằng chữ:</b> {0}</div>", moneyText);

            sb.Append(BuildSignaturesSection(DateTime.Now));
            sb.Append(GetHtmlFoot());
            return sb.ToString();
        }

        /// <summary>
        /// 4. Sổ Chi Tiết Công Nợ Khách Hàng (Landscape)
        /// </summary>
        public static string GenerateSoChiTietKhachHangHtml(string maKH, string tenKH, DateTime tuNgay, DateTime denNgay, List<SoChiTietKhachHangDTO> list, decimal tongNo, decimal tongCo, decimal duCuoiKy)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetHtmlHead("Sổ Chi Tiết Công Nợ Khách Hàng", true));
            sb.Append(BuildHeaderSection("Mẫu biểu: S31-DN<br/>(Ban hành theo TT 200/2014/TT-BTC)"));

            sb.Append(@"
    <div class=""report-title"">SỔ CHI TIẾT THANH TOÁN VỚI NGƯỜI MUA (TK 131)</div>");
            sb.AppendFormat(@"
    <div class=""report-subtitle"">Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}</div>", tuNgay, denNgay);

            sb.AppendFormat(@"
    <div class=""filter-badge"">Khách hàng: <b>{0} - {1}</b></div>", maKH, tenKH);

            sb.Append(@"
    <table class=""data-table"">
        <thead>
            <tr>
                <th style=""width: 40px;"">STT</th>
                <th style=""width: 95px;"">Ngày GD</th>
                <th style=""width: 105px;"">Số Chứng Từ</th>
                <th style=""width: 110px;"">Loại Nghiệp Vụ</th>
                <th>Diễn Giải Giao Dịch</th>
                <th style=""width: 130px;"">Phát Sinh Nợ (VND)<br/><span style=""font-size: 10px; font-weight: normal;"">(Tiền mua hàng)</span></th>
                <th style=""width: 130px;"">Phát Sinh Có (VND)<br/><span style=""font-size: 10px; font-weight: normal;"">(Đã thanh toán)</span></th>
                <th style=""width: 140px;"">Số Dư Nợ Còn Lại (VND)<br/><span style=""font-size: 10px; font-weight: normal;"">(Công nợ cuối kỳ)</span></th>
            </tr>
        </thead>
        <tbody>");

            if (list == null || list.Count == 0)
            {
                sb.Append(@"<tr><td colspan=""8"" class=""text-center"" style=""padding: 20px; color: #64748b;"">Không có giao dịch công nợ của khách hàng trong kỳ đã chọn.</td></tr>");
            }
            else
            {
                int stt = 1;
                foreach (var item in list)
                {
                    sb.AppendFormat(@"
            <tr>
                <td class=""text-center"">{0}</td>
                <td class=""text-center"">{1:dd/MM/yyyy}</td>
                <td class=""text-center""><b>{2}</b></td>
                <td class=""text-center"">{3}</td>
                <td>{4}</td>
                <td class=""text-right"">{5}</td>
                <td class=""text-right"">{6}</td>
                <td class=""text-right""><b>{7:N0}</b></td>
            </tr>",
                        stt++,
                        item.NgayGiaoDich,
                        item.SoChungTu,
                        item.LoaiNghiepVu,
                        item.DienGiai,
                        item.PhatSinhNo > 0 ? string.Format("{0:N0}", item.PhatSinhNo) : "-",
                        item.PhatSinhCo > 0 ? string.Format("{0:N0}", item.PhatSinhCo) : "-",
                        item.SoDuCuoiKy);
                }

                // Dòng tổng cộng
                sb.AppendFormat(@"
            <tr class=""total-row"">
                <td colspan=""5"" class=""text-center"">TỔNG CỘNG PHÁT SINH TRONG KỲ</td>
                <td class=""text-right"">{0:N0}</td>
                <td class=""text-right"">{1:N0}</td>
                <td class=""text-right"" style=""color: #b91c1c;"">{2:N0}</td>
            </tr>", tongNo, tongCo, duCuoiKy);
            }

            sb.Append(@"
        </tbody>
    </table>");

            string moneyText = VietnameseNumberReader.ReadMoney(duCuoiKy >= 0 ? duCuoiKy : -duCuoiKy);
            sb.AppendFormat(@"
    <div class=""money-in-words""><b>Số dư công nợ phải thu cuối kỳ bằng chữ:</b> {0}</div>", moneyText);

            sb.Append(BuildSignaturesSection(DateTime.Now));
            sb.Append(GetHtmlFoot());
            return sb.ToString();
        }

        /// <summary>
        /// 5. Sổ Chi Tiết Bán Hàng Theo Sản Phẩm (Landscape)
        /// </summary>
        public static string GenerateSoChiTietSanPhamHtml(string maSP, string tenSP, DateTime tuNgay, DateTime denNgay, List<SoChiTietSanPhamDTO> list, int tongSoLuong, decimal tongDoanhThu)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetHtmlHead("Sổ Chi Tiết Bán Hàng Theo Mặt Hàng", true));
            sb.Append(BuildHeaderSection("Mẫu biểu: S36-DN<br/>(Sổ chi tiết bán sản phẩm OLC)"));

            sb.Append(@"
    <div class=""report-title"">SỔ CHI TIẾT BÁN HÀNG THEO MẶT HÀNG</div>");
            sb.AppendFormat(@"
    <div class=""report-subtitle"">Từ ngày {0:dd/MM/yyyy} đến ngày {1:dd/MM/yyyy}</div>", tuNgay, denNgay);

            if (!string.IsNullOrEmpty(maSP))
            {
                sb.AppendFormat(@"
    <div class=""filter-badge"">Mặt hàng: <b>{0} - {1}</b></div>", maSP, tenSP);
            }
            else
            {
                sb.Append(@"
    <div class=""filter-badge"">Phạm vi: <b>Tất cả sản phẩm kinh doanh</b></div>");
            }

            sb.Append(@"
    <table class=""data-table"">
        <thead>
            <tr>
                <th style=""width: 40px;"">STT</th>
                <th style=""width: 85px;"">Mã SP</th>
                <th>Tên Sản Phẩm</th>
                <th style=""width: 130px;"">Loại Sản Phẩm</th>
                <th style=""width: 65px;"">ĐVT</th>
                <th style=""width: 100px;"">Số Lượng Bán</th>
                <th style=""width: 110px;"">Đơn Giá TB (VND)</th>
                <th style=""width: 130px;"">Doanh Thu (VND)</th>
                <th style=""width: 80px;"">Số HĐ</th>
            </tr>
        </thead>
        <tbody>");

            if (list == null || list.Count == 0)
            {
                sb.Append(@"<tr><td colspan=""9"" class=""text-center"" style=""padding: 20px; color: #64748b;"">Không có dữ liệu bán hàng trong khoảng thời gian đã chọn.</td></tr>");
            }
            else
            {
                int stt = 1;
                foreach (var item in list)
                {
                    sb.AppendFormat(@"
            <tr>
                <td class=""text-center"">{0}</td>
                <td class=""text-center""><b>{1}</b></td>
                <td>{2}</td>
                <td>{3}</td>
                <td class=""text-center"">{4}</td>
                <td class=""text-right""><b>{5:N0}</b></td>
                <td class=""text-right"">{6:N0}</td>
                <td class=""text-right""><b>{7:N0}</b></td>
                <td class=""text-center"">{8}</td>
            </tr>",
                        stt++,
                        item.MaSP,
                        item.TenSP,
                        item.TenLoaiSP,
                        item.DonViTinh,
                        item.TongSoLuongBan,
                        item.DonGiaTrungBinh,
                        item.TongDoanhThu,
                        item.SoHoaDonPhatSinh);
                }

                // Dòng tổng cộng
                sb.AppendFormat(@"
            <tr class=""total-row"">
                <td colspan=""5"" class=""text-center"">TỔNG CỘNG ({0} MẶT HÀNG)</td>
                <td class=""text-right"">{1:N0}</td>
                <td></td>
                <td class=""text-right"">{2:N0}</td>
                <td></td>
            </tr>", list.Count, tongSoLuong, tongDoanhThu);
            }

            sb.Append(@"
        </tbody>
    </table>");

            string moneyText = VietnameseNumberReader.ReadMoney(tongDoanhThu);
            sb.AppendFormat(@"
    <div class=""money-in-words""><b>Tổng doanh thu tiêu thụ bằng chữ:</b> {0}</div>", moneyText);

            sb.Append(BuildSignaturesSection(DateTime.Now));
            sb.Append(GetHtmlFoot());
            return sb.ToString();
        }

        /// <summary>
        /// 6. Hồ Sơ Theo Dõi Luân Chuyển Hóa Đơn & Chứng Từ (Portrait)
        /// </summary>
        public static string GenerateSoChiTietHoaDonHtml(SoChiTietHoaDonDTO hdb)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetHtmlHead("Hồ Sơ Theo Dõi Hóa Đơn Bán Hàng", false));
            sb.Append(BuildHeaderSection("Mẫu biểu: HS-HDB<br/>(Hồ sơ đối soát nghiệp vụ OLC)"));

            sb.Append(@"
    <div class=""report-title"">HỒ SƠ THEO DÕI LUÂN CHUYỂN HÓA ĐƠN</div>");
            sb.AppendFormat(@"
    <div class=""report-subtitle"">Hóa đơn số: <b>{0}</b> &bull; Ngày lập: {1:dd/MM/yyyy}</div>",
                hdb.MaHDB, hdb.NgayLap);

            // Bảng tóm tắt thông tin hóa đơn
            sb.AppendFormat(@"
    <table class=""data-table"" style=""margin-bottom: 12px;"">
        <tr>
            <td style=""width: 18%; background: #f8fafc; font-weight: 600;"">Khách hàng:</td>
            <td style=""width: 42%;""><b>{0} - {1}</b></td>
            <td style=""width: 18%; background: #f8fafc; font-weight: 600;"">Nhân viên lập:</td>
            <td style=""width: 22%;"">{2} - {3}</td>
        </tr>
        <tr>
            <td style=""background: #f8fafc; font-weight: 600;"">Địa chỉ:</td>
            <td>{4}</td>
            <td style=""background: #f8fafc; font-weight: 600;"">Điện thoại:</td>
            <td>{5}</td>
        </tr>
        <tr>
            <td style=""background: #f8fafc; font-weight: 600;"">Tổng giá trị HĐ:</td>
            <td><b style=""color: #0f172a;"">{6:N0} VND</b></td>
            <td style=""background: #f8fafc; font-weight: 600;"">Trạng thái TT:</td>
            <td><b>{7}</b></td>
        </tr>
        <tr>
            <td style=""background: #f8fafc; font-weight: 600;"">Đã thanh toán:</td>
            <td style=""color: #059669;""><b>{8:N0} VND</b></td>
            <td style=""background: #f8fafc; font-weight: 600;"">Còn phải thu:</td>
            <td style=""color: #e11d48;""><b>{9:N0} VND</b></td>
        </tr>
    </table>",
                hdb.MaKH, hdb.TenKH,
                hdb.MaNV, hdb.TenNV,
                hdb.DiaChi ?? "-", hdb.DienThoai ?? "-",
                hdb.TongTien, hdb.TrangThai,
                hdb.DaThu, hdb.ConLai);

            // 1. Chi tiết mặt hàng
            sb.Append(@"
    <div style=""font-weight: bold; margin: 12px 0 6px 0; color: #0f172a;"">1. Danh mục mặt hàng trên hóa đơn:</div>
    <table class=""data-table"">
        <thead>
            <tr>
                <th style=""width: 35px;"">STT</th>
                <th style=""width: 75px;"">Mã SP</th>
                <th>Tên Mặt Hàng</th>
                <th style=""width: 55px;"">ĐVT</th>
                <th style=""width: 60px;"">SL</th>
                <th style=""width: 90px;"">Đơn Giá</th>
                <th style=""width: 60px;"">CK (%)</th>
                <th style=""width: 100px;"">Thành Tiền (VND)</th>
            </tr>
        </thead>
        <tbody>");

            if (hdb.DanhSachMatHang == null || hdb.DanhSachMatHang.Count == 0)
            {
                sb.Append(@"<tr><td colspan=""8"" class=""text-center"">Không có mặt hàng.</td></tr>");
            }
            else
            {
                int stt = 1;
                foreach (var m in hdb.DanhSachMatHang)
                {
                    sb.AppendFormat(@"
            <tr>
                <td class=""text-center"">{0}</td>
                <td class=""text-center"">{1}</td>
                <td>{2}</td>
                <td class=""text-center"">{3}</td>
                <td class=""text-right"">{4:N0}</td>
                <td class=""text-right"">{5:N0}</td>
                <td class=""text-center"">{6:0.#}%</td>
                <td class=""text-right""><b>{7:N0}</b></td>
            </tr>", stt++, m.MaSP, m.TenSP, m.DonViTinh, m.SoLuong, m.DonGia, m.GiamGia, m.ThanhTien);
                }
            }
            sb.Append(@"</tbody></table>");

            // 2. Phiếu thu tiền
            sb.Append(@"
    <div style=""font-weight: bold; margin: 12px 0 6px 0; color: #0f172a;"">2. Lịch sử thu tiền thanh toán:</div>
    <table class=""data-table"">
        <thead>
            <tr>
                <th style=""width: 35px;"">STT</th>
                <th style=""width: 85px;"">Số Phiếu</th>
                <th style=""width: 85px;"">Ngày Thu</th>
                <th>Người Nộp Tiền</th>
                <th style=""width: 90px;"">Hình Thức</th>
                <th style=""width: 110px;"">Số Tiền (VND)</th>
                <th>Lý Do Thu</th>
            </tr>
        </thead>
        <tbody>");

            if (hdb.DanhSachPhieuThu == null || hdb.DanhSachPhieuThu.Count == 0)
            {
                sb.Append(@"<tr><td colspan=""7"" class=""text-center"" style=""color: #64748b;"">Chưa có phiếu thu nào ghi nhận cho hóa đơn này.</td></tr>");
            }
            else
            {
                int stt = 1;
                foreach (var pt in hdb.DanhSachPhieuThu)
                {
                    sb.AppendFormat(@"
            <tr>
                <td class=""text-center"">{0}</td>
                <td class=""text-center""><b>{1}</b></td>
                <td class=""text-center"">{2:dd/MM/yyyy}</td>
                <td>{3}</td>
                <td class=""text-center"">{4}</td>
                <td class=""text-right"" style=""color: #059669; font-weight: bold;"">{5:N0}</td>
                <td>{6}</td>
            </tr>", stt++, pt.MaPT, pt.NgayThu, pt.NguoiNop, pt.HinhThuc, pt.SoTien, pt.LyDoThu);
                }
            }
            sb.Append(@"</tbody></table>");

            // 3. Định khoản kế toán
            sb.Append(@"
    <div style=""font-weight: bold; margin: 12px 0 6px 0; color: #0f172a;"">3. Định khoản sổ cái kế toán liên kết:</div>
    <table class=""data-table"">
        <thead>
            <tr>
                <th style=""width: 35px;"">STT</th>
                <th style=""width: 85px;"">Số Chứng Từ</th>
                <th style=""width: 85px;"">Ngày Hạch Toán</th>
                <th style=""width: 70px;"">TK Nợ</th>
                <th style=""width: 70px;"">TK Có</th>
                <th style=""width: 110px;"">Số Tiền (VND)</th>
                <th>Diễn Giải Hạch Toán</th>
            </tr>
        </thead>
        <tbody>");

            if (hdb.DanhSachDinhKhoan == null || hdb.DanhSachDinhKhoan.Count == 0)
            {
                sb.Append(@"<tr><td colspan=""7"" class=""text-center"" style=""color: #64748b;"">Chưa phát sinh chứng từ kế toán cho hóa đơn này.</td></tr>");
            }
            else
            {
                int stt = 1;
                foreach (var dk in hdb.DanhSachDinhKhoan)
                {
                    sb.AppendFormat(@"
            <tr>
                <td class=""text-center"">{0}</td>
                <td class=""text-center""><b>{1}</b></td>
                <td class=""text-center"">{2:dd/MM/yyyy}</td>
                <td class=""text-center""><b>{3}</b></td>
                <td class=""text-center""><b>{4}</b></td>
                <td class=""text-right""><b>{5:N0}</b></td>
                <td>{6}</td>
            </tr>", stt++, dk.MaCT, dk.NgayLap, dk.TaiKhoanNo, dk.TaiKhoanCo, dk.SoTien, dk.DienGiai);
                }
            }
            sb.Append(@"</tbody></table>");

            sb.Append(BuildSignaturesSection(DateTime.Now));
            sb.Append(GetHtmlFoot());
            return sb.ToString();
        }

        #region Báo cáo Tuổi nợ Khách hàng

        public static string GenerateBaoCaoTuoiNoTongHopHtml(DateTime ngayChot, List<BaoCaoTuoiNoTongHopDTO> data, string tenKHFilter = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetHtmlHead("BÁO CÁO PHÂN TÍCH TUỔI NỢ KHÁCH HÀNG (TỔNG HỢP)", true));
            sb.Append(BuildHeaderSection("Mẫu biểu: BC-04/TN<br/>(Báo cáo Tuổi nợ Quản trị OLC)"));

            sb.Append(@"<div class=""report-title"">BÁO CÁO PHÂN TÍCH TUỔI NỢ KHÁCH HÀNG</div>");
            sb.AppendFormat(@"<div class=""report-subtitle"">Thời điểm phân tích chốt công nợ: ngày {0:dd} tháng {0:MM} năm {0:yyyy}</div>", ngayChot);

            if (!string.IsNullOrEmpty(tenKHFilter))
            {
                sb.AppendFormat(@"<div class=""filter-badge"">Đối tượng khách hàng: <b>{0}</b></div>", tenKHFilter);
            }

            sb.Append(@"
    <table class=""data-table"">
        <thead>
            <tr>
                <th style=""width: 35px;"">STT</th>
                <th style=""width: 80px;"">Mã KH</th>
                <th>Tên Khách Hàng</th>
                <th style=""width: 95px;"">Điện Thoại</th>
                <th style=""width: 115px;"">Tổng Dư Nợ (VND)</th>
                <th style=""width: 110px;"">Trong Hạn<br>(0 - 30 ngày)</th>
                <th style=""width: 110px;"">Quá Hạn Nhẹ<br>(31 - 60 ngày)</th>
                <th style=""width: 110px;"">Quá Hạn TB<br>(61 - 90 ngày)</th>
                <th style=""width: 115px;"">Nợ Khó Đòi<br>(Trên 90 ngày)</th>
                <th style=""width: 110px;"">Đánh Giá Rủi Ro</th>
            </tr>
        </thead>
        <tbody>");

            decimal tongNo = 0;
            decimal tongTrongHan = 0;
            decimal tongQuaHan3160 = 0;
            decimal tongQuaHan6190 = 0;
            decimal tongKhoDoi = 0;

            if (data == null || data.Count == 0)
            {
                sb.Append(@"<tr><td colspan=""10"" class=""text-center"" style=""color: #64748b; padding: 15px;"">Không có khách hàng nào phát sinh công nợ tại thời điểm này.</td></tr>");
            }
            else
            {
                int stt = 1;
                foreach (var item in data)
                {
                    tongNo += item.TongNo;
                    tongTrongHan += item.TrongHan_0_30;
                    tongQuaHan3160 += item.QuaHanNhe_31_60;
                    tongQuaHan6190 += item.QuaHanTB_61_90;
                    tongKhoDoi += item.KhoDoi_Tren90;

                    string rowStyle = "";
                    string riskBadge = "";
                    if (item.KhoDoi_Tren90 > 0)
                    {
                        rowStyle = @"style=""background-color: #fee2e2;""";
                        riskBadge = @"<span style=""color: #b91c1c; font-weight: bold;"">Nợ khó đòi</span>";
                    }
                    else if (item.QuaHanTB_61_90 > 0)
                    {
                        rowStyle = @"style=""background-color: #ffedd5;""";
                        riskBadge = @"<span style=""color: #c2410c; font-weight: bold;"">Quá hạn TB</span>";
                    }
                    else if (item.QuaHanNhe_31_60 > 0)
                    {
                        rowStyle = @"style=""background-color: #fef9c3;""";
                        riskBadge = @"<span style=""color: #854d0e;"">Quá hạn nhẹ</span>";
                    }
                    else
                    {
                        riskBadge = @"<span style=""color: #15803d;"">Trong hạn</span>";
                    }

                    sb.AppendFormat(@"
            <tr {0}>
                <td class=""text-center"">{1}</td>
                <td class=""text-center""><b>{2}</b></td>
                <td>{3}</td>
                <td class=""text-center"">{4}</td>
                <td class=""text-right""><b>{5:N0}</b></td>
                <td class=""text-right"" style=""color: #15803d;"">{6:N0}</td>
                <td class=""text-right"" style=""color: #a16207;"">{7:N0}</td>
                <td class=""text-right"" style=""color: #c2410c;"">{8:N0}</td>
                <td class=""text-right"" style=""color: #b91c1c; font-weight: bold;"">{9:N0}</td>
                <td class=""text-center"">{10}</td>
            </tr>", rowStyle, stt++, item.MaKH, item.TenKH, item.DienThoai,
                    item.TongNo, item.TrongHan_0_30, item.QuaHanNhe_31_60, item.QuaHanTB_61_90, item.KhoDoi_Tren90, riskBadge);
                }
            }

            sb.AppendFormat(@"
            <tr class=""total-row"">
                <td colspan=""4"" class=""text-center"">TỔNG CỘNG</td>
                <td class=""text-right""><b>{0:N0}</b></td>
                <td class=""text-right"" style=""color: #15803d;""><b>{1:N0}</b></td>
                <td class=""text-right"" style=""color: #a16207;""><b>{2:N0}</b></td>
                <td class=""text-right"" style=""color: #c2410c;""><b>{3:N0}</b></td>
                <td class=""text-right"" style=""color: #b91c1c;""><b>{4:N0}</b></td>
                <td class=""text-center"">-</td>
            </tr>", tongNo, tongTrongHan, tongQuaHan3160, tongQuaHan6190, tongKhoDoi);

            sb.Append(@"</tbody></table>");

            sb.AppendFormat(@"<div class=""money-in-words"">Tổng số tiền dư nợ phải thu bằng chữ: <b>{0}</b>.</div>", VietnameseNumberReader.ReadMoney(tongNo));
            sb.Append(BuildSignaturesSection(DateTime.Now));
            sb.Append(GetHtmlFoot());
            return sb.ToString();
        }

        public static string GenerateBaoCaoTuoiNoChiTietHtml(DateTime ngayChot, List<BaoCaoTuoiNoChiTietDTO> data, string tenKHFilter = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetHtmlHead("BÁO CÁO PHÂN TÍCH TUỔI NỢ KHÁCH HÀNG (CHI TIẾT HÓA ĐƠN)", true));
            sb.Append(BuildHeaderSection("Mẫu biểu: BC-04/TN<br/>(Báo cáo Tuổi nợ Quản trị OLC)"));

            sb.Append(@"<div class=""report-title"">BÁO CÁO CHI TIẾT TUỔI NỢ THEO HÓA ĐƠN BÁN</div>");
            sb.AppendFormat(@"<div class=""report-subtitle"">Thời điểm phân tích chốt công nợ: ngày {0:dd} tháng {0:MM} năm {0:yyyy}</div>", ngayChot);

            if (!string.IsNullOrEmpty(tenKHFilter))
            {
                sb.AppendFormat(@"<div class=""filter-badge"">Đối tượng khách hàng: <b>{0}</b></div>", tenKHFilter);
            }

            sb.Append(@"
    <table class=""data-table"">
        <thead>
            <tr>
                <th style=""width: 35px;"">STT</th>
                <th style=""width: 90px;"">Mã HĐB</th>
                <th style=""width: 85px;"">Ngày Lập</th>
                <th style=""width: 80px;"">Mã KH</th>
                <th>Tên Khách Hàng</th>
                <th style=""width: 105px;"">Tổng Tiền HĐ</th>
                <th style=""width: 105px;"">Đã Thanh Toán</th>
                <th style=""width: 110px;"">Còn Nợ (VND)</th>
                <th style=""width: 80px;"">Tuổi Nợ<br>(Số ngày)</th>
                <th style=""width: 130px;"">Phân Nhóm Tuổi Nợ</th>
            </tr>
        </thead>
        <tbody>");

            decimal tongTien = 0;
            decimal tongDaThu = 0;
            decimal tongConNo = 0;

            if (data == null || data.Count == 0)
            {
                sb.Append(@"<tr><td colspan=""10"" class=""text-center"" style=""color: #64748b; padding: 15px;"">Không có hóa đơn nợ nào tại thời điểm này.</td></tr>");
            }
            else
            {
                int stt = 1;
                foreach (var item in data)
                {
                    tongTien += item.TongTien;
                    tongDaThu += item.DaThu;
                    tongConNo += item.ConNo;

                    string rowStyle = "";
                    string badge = "";
                    if (item.MaNhomTuoiNo == 3)
                    {
                        rowStyle = @"style=""background-color: #fee2e2;""";
                        badge = @"<span style=""color: #b91c1c; font-weight: bold;"">Nợ khó đòi (&gt;90 ngày)</span>";
                    }
                    else if (item.MaNhomTuoiNo == 2)
                    {
                        rowStyle = @"style=""background-color: #ffedd5;""";
                        badge = @"<span style=""color: #c2410c; font-weight: bold;"">Quá hạn TB (61-90 ngày)</span>";
                    }
                    else if (item.MaNhomTuoiNo == 1)
                    {
                        rowStyle = @"style=""background-color: #fef9c3;""";
                        badge = @"<span style=""color: #854d0e;"">Quá hạn nhẹ (31-60 ngày)</span>";
                    }
                    else
                    {
                        badge = @"<span style=""color: #15803d;"">Trong hạn (&le;30 ngày)</span>";
                    }

                    sb.AppendFormat(@"
            <tr {0}>
                <td class=""text-center"">{1}</td>
                <td class=""text-center""><b>{2}</b></td>
                <td class=""text-center"">{3:dd/MM/yyyy}</td>
                <td class=""text-center"">{4}</td>
                <td>{5}</td>
                <td class=""text-right"">{6:N0}</td>
                <td class=""text-right"">{7:N0}</td>
                <td class=""text-right""><b>{8:N0}</b></td>
                <td class=""text-center""><b>{9} ngày</b></td>
                <td class=""text-center"">{10}</td>
            </tr>", rowStyle, stt++, item.MaHDB, item.NgayLap, item.MaKH, item.TenKH,
                    item.TongTien, item.DaThu, item.ConNo, item.SoNgayNo, badge);
                }
            }

            sb.AppendFormat(@"
            <tr class=""total-row"">
                <td colspan=""5"" class=""text-center"">TỔNG CỘNG</td>
                <td class=""text-right""><b>{0:N0}</b></td>
                <td class=""text-right""><b>{1:N0}</b></td>
                <td class=""text-right""><b>{2:N0}</b></td>
                <td colspan=""2"" class=""text-center"">-</td>
            </tr>", tongTien, tongDaThu, tongConNo);

            sb.Append(@"</tbody></table>");

            sb.AppendFormat(@"<div class=""money-in-words"">Tổng số tiền dư nợ hóa đơn bằng chữ: <b>{0}</b>.</div>", VietnameseNumberReader.ReadMoney(tongConNo));
            sb.Append(BuildSignaturesSection(DateTime.Now));
            sb.Append(GetHtmlFoot());
            return sb.ToString();
        }

        #endregion
    }
}
