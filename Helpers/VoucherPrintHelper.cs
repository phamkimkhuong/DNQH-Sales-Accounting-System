using System;
using System.Data;
using System.Globalization;
using System.Text;
using DNQH_KeToanBanHang.Models;

namespace DNQH_KeToanBanHang.Helpers
{
    /// <summary>
    /// Lớp tiện ích phát sinh mẫu in tài liệu, chứng từ kế toán theo quy chuẩn Bộ Tài chính (HTML/CSS in A4).
    /// </summary>
    public static class VoucherPrintHelper
    {
        public const string FullCompanyName = "CÔNG TY CỔ PHẦN THƯƠNG MẠI DNQH";
        public const string CompanyName = "CÔNG TY CỔ PHẦN THƯƠNG MẠI DNQH";
        public const string CompanyShortName = "DNQH";
        public const string CompanyAddress = "Số 123 Đường Trần Phú, Quận Hà Đông, TP. Hà Nội";
        public const string CompanyTaxCode = "0102030405";
        public const string CompanyPhone = "024.3888.9999";

        private static string GetHtmlHead(string title)
        {
            return string.Format(@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <title>{0}</title>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-size: 13px;
            color: #1a1a1a;
            margin: 0;
            padding: 20px;
            background-color: #fff;
        }}
        .voucher-container {{
            max-width: 800px;
            margin: 0 auto;
            border: 1px solid #ddd;
            padding: 30px;
            background: #fff;
            box-shadow: 0 2px 10px rgba(0,0,0,0.05);
        }}
        .header-table {{
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }}
        .company-name {{
            font-size: 15px;
            font-weight: bold;
            color: #1e293b;
            text-transform: uppercase;
        }}
        .company-info {{
            font-size: 12px;
            color: #64748b;
            line-height: 1.4;
        }}
        .form-code {{
            text-align: right;
            font-size: 11px;
            color: #475569;
            font-weight: bold;
        }}
        .voucher-title {{
            text-align: center;
            font-size: 22px;
            font-weight: bold;
            color: #0f172a;
            margin-top: 10px;
            margin-bottom: 4px;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }}
        .voucher-subtitle {{
            text-align: center;
            font-size: 12px;
            font-style: italic;
            color: #64748b;
            margin-bottom: 20px;
        }}
        .info-table {{
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 15px;
        }}
        .info-table td {{
            padding: 4px 6px;
            font-size: 13px;
            vertical-align: top;
        }}
        .info-label {{
            width: 140px;
            color: #475569;
        }}
        .info-val {{
            font-weight: 500;
            color: #0f172a;
        }}
        .items-table {{
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
            margin-bottom: 15px;
        }}
        .items-table th {{
            background-color: #f1f5f9;
            border: 1px solid #cbd5e1;
            padding: 8px 6px;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            color: #334155;
        }}
        .items-table td {{
            border: 1px solid #cbd5e1;
            padding: 6px 8px;
            font-size: 12px;
        }}
        .text-center {{ text-align: center; }}
        .text-right {{ text-align: right; }}
        .font-bold {{ font-weight: bold; }}
        .amount-words {{
            margin: 10px 0 20px 0;
            font-size: 13px;
            font-style: italic;
            color: #1e293b;
        }}
        .signature-table {{
            width: 100%;
            border-collapse: collapse;
            margin-top: 25px;
            page-break-inside: avoid;
        }}
        .signature-title {{
            font-weight: bold;
            font-size: 13px;
            text-align: center;
            color: #0f172a;
        }}
        .signature-sub {{
            font-size: 11px;
            font-style: italic;
            text-align: center;
            color: #64748b;
            margin-bottom: 60px;
        }}
        .signature-name {{
            font-weight: bold;
            text-align: center;
            font-size: 12px;
            color: #1e293b;
        }}
        @media print {{
            body {{
                padding: 0;
                background: #fff;
            }}
            .voucher-container {{
                border: none;
                box-shadow: none;
                padding: 0;
                max-width: 100%;
            }}
            @page {{
                size: A4;
                margin: 15mm 15mm 15mm 15mm;
            }}
        }}
    </style>
</head>
<body>
<div class=""voucher-container"">", title);
        }

        private const string HtmlTail = @"
</div>
</body>
</html>";

        /// <summary>
        /// Tạo mẫu in Hóa đơn bán hàng (Mẫu số 02-BH).
        /// </summary>
        public static string GenerateHoaDonBanHtml(HoaDonBan hdb, DataTable dtChiTiet)
        {
            if (hdb == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append(GetHtmlHead("Hóa Đơn Bán Hàng - " + hdb.MaHDB));

            // Header Doanh nghiệp & Mẫu số
            sb.AppendFormat(@"
    <table class=""header-table"">
        <tr>
            <td style=""width: 65%;"">
                <div class=""company-name"">{0}</div>
                <div class=""company-info"">Địa chỉ: {1}<br/>Mã số thuế: {2} - ĐT: {3}</div>
            </td>
            <td class=""form-code"">
                Mẫu số: 02 - BH<br/>
                (Ban hành theo TT số 200/2014/TT-BTC<br/>của Bộ Tài chính)
            </td>
        </tr>
    </table>", CompanyName, CompanyAddress, CompanyTaxCode, CompanyPhone);

            // Tiêu đề & Ngày lập
            sb.AppendFormat(@"
    <div class=""voucher-title"">HÓA ĐƠN BÁN HÀNG</div>
    <div class=""voucher-subtitle"">Ngày {0:dd} tháng {0:MM} năm {0:yyyy} &bull; Số: <b>{1}</b> &bull; (Liên 2: Giao khách hàng)</div>",
                hdb.NgayLap, hdb.MaHDB);

            // Thông tin người mua
            sb.AppendFormat(@"
    <table class=""info-table"">
        <tr>
            <td class=""info-label"">Họ tên khách hàng:</td>
            <td class=""info-val""><b>{0}</b> (Mã: {1})</td>
        </tr>
        <tr>
            <td class=""info-label"">Đơn đặt hàng gốc:</td>
            <td class=""info-val"">{2}</td>
        </tr>
        <tr>
            <td class=""info-label"">Nhân viên bán hàng:</td>
            <td class=""info-val"">{3}</td>
        </tr>
        <tr>
            <td class=""info-label"">Trạng thái thanh toán:</td>
            <td class=""info-val"">{4}</td>
        </tr>
        <tr>
            <td class=""info-label"">Ghi chú:</td>
            <td class=""info-val"">{5}</td>
        </tr>
    </table>",
                Escape(hdb.TenKH), Escape(hdb.MaKH),
                string.IsNullOrEmpty(hdb.MaDDH) ? "Bán lẻ trực tiếp" : Escape(hdb.MaDDH),
                Escape(hdb.TenNV), Escape(hdb.TrangThai),
                string.IsNullOrEmpty(hdb.GhiChu) ? "Không có" : Escape(hdb.GhiChu));

            // Bảng danh mục sản phẩm
            sb.Append(@"
    <table class=""items-table"">
        <thead>
            <tr>
                <th style=""width: 35px;"">STT</th>
                <th>Tên mặt hàng / Quy cách</th>
                <th style=""width: 60px;"">ĐVT</th>
                <th style=""width: 65px;"">Số lượng</th>
                <th style=""width: 100px;"">Đơn giá</th>
                <th style=""width: 70px;"">Giảm giá</th>
                <th style=""width: 115px;"">Thành tiền (VNĐ)</th>
            </tr>
        </thead>
        <tbody>");

            int stt = 1;
            decimal tongTien = 0;
            if (dtChiTiet != null && dtChiTiet.Rows.Count > 0)
            {
                foreach (DataRow r in dtChiTiet.Rows)
                {
                    string tenSP = r["TenSP"] != null ? r["TenSP"].ToString() : "";
                    string dvt = r.Table.Columns.Contains("DonViTinh") && r["DonViTinh"] != null ? r["DonViTinh"].ToString() : "";
                    int soLuong = r["SoLuong"] != DBNull.Value ? Convert.ToInt32(r["SoLuong"]) : 0;
                    decimal donGia = r["DonGia"] != DBNull.Value ? Convert.ToDecimal(r["DonGia"]) : 0;
                    decimal giamGia = r.Table.Columns.Contains("GiamGia") && r["GiamGia"] != DBNull.Value ? Convert.ToDecimal(r["GiamGia"]) : 0;
                    decimal thanhTien = r["ThanhTien"] != DBNull.Value ? Convert.ToDecimal(r["ThanhTien"]) : 0;
                    tongTien += thanhTien;

                    sb.AppendFormat(@"
            <tr>
                <td class=""text-center"">{0}</td>
                <td><b>{1}</b></td>
                <td class=""text-center"">{2}</td>
                <td class=""text-right"">{3:N0}</td>
                <td class=""text-right"">{4:N0}</td>
                <td class=""text-right"">{5:N0}%</td>
                <td class=""text-right font-bold"">{6:N0}</td>
            </tr>", stt++, Escape(tenSP), Escape(dvt), soLuong, donGia, giamGia, thanhTien);
                }
            }
            else
            {
                tongTien = hdb.TongTien;
                sb.Append(@"<tr><td colspan=""7"" class=""text-center"" style=""padding: 12px;"">Không có dòng chi tiết mặt hàng.</td></tr>");
            }

            // Dòng tổng cộng
            sb.AppendFormat(@"
            <tr>
                <td colspan=""6"" class=""text-right font-bold"" style=""font-size: 13px;"">TỔNG CỘNG TIỀN THANH TOÁN:</td>
                <td class=""text-right font-bold"" style=""font-size: 14px; color: #b91c1c;"">{0:N0} VNĐ</td>
            </tr>
        </tbody>
    </table>", tongTien);

            // Đọc số tiền thành chữ
            string bangChu = VietnameseNumberReader.ReadMoney(tongTien);
            sb.AppendFormat(@"<div class=""amount-words""><b>Số tiền viết bằng chữ:</b> {0}</div>", bangChu);

            // Khối chữ ký 4 cột
            sb.Append(@"
    <table class=""signature-table"">
        <tr>
            <td style=""width: 25%;"">
                <div class=""signature-title"">Người mua hàng</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name""></div>
            </td>
            <td style=""width: 25%;"">
                <div class=""signature-title"">Người bán hàng</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name"">" + Escape(hdb.TenNV) + @"</div>
            </td>
            <td style=""width: 25%;"">
                <div class=""signature-title"">Thủ kho</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name""></div>
            </td>
            <td style=""width: 25%;"">
                <div class=""signature-title"">Giám đốc</div>
                <div class=""signature-sub"">(Ký, đóng dấu, họ tên)</div>
                <div class=""signature-name""></div>
            </td>
        </tr>
    </table>");

            sb.Append(HtmlTail);
            return sb.ToString();
        }

        /// <summary>
        /// Tạo mẫu in Phiếu Thu (Mẫu số 01-TT).
        /// </summary>
        public static string GeneratePhieuThuHtml(PhieuThu pt)
        {
            if (pt == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append(GetHtmlHead("Phiếu Thu - " + pt.MaPT));

            sb.AppendFormat(@"
    <table class=""header-table"">
        <tr>
            <td style=""width: 65%;"">
                <div class=""company-name"">{0}</div>
                <div class=""company-info"">Địa chỉ: {1}<br/>Mã số thuế: {2} - ĐT: {3}</div>
            </td>
            <td class=""form-code"">
                Mẫu số: 01 - TT<br/>
                (Ban hành theo TT số 200/2014/TT-BTC<br/>của Bộ Tài chính)
            </td>
        </tr>
    </table>", CompanyName, CompanyAddress, CompanyTaxCode, CompanyPhone);

            sb.AppendFormat(@"
    <div class=""voucher-title"">PHIẾU THU</div>
    <div class=""voucher-subtitle"">Ngày {0:dd} tháng {0:MM} năm {0:yyyy} &bull; Số: <b>{1}</b> &bull; Quyển số: 01</div>",
                pt.NgayThu ?? DateTime.Now, pt.MaPT);

            sb.AppendFormat(@"
    <table class=""info-table"" style=""margin-bottom: 25px;"">
        <tr>
            <td class=""info-label"">Họ tên người nộp tiền:</td>
            <td class=""info-val""><b>{0}</b></td>
        </tr>
        <tr>
            <td class=""info-label"">Khách hàng / Đơn vị:</td>
            <td class=""info-val"">{1}</td>
        </tr>
        <tr>
            <td class=""info-label"">Lý do thu tiền:</td>
            <td class=""info-val"">{2}</td>
        </tr>
        <tr>
            <td class=""info-label"">Số tiền thu:</td>
            <td class=""info-val"" style=""font-size: 16px; color: #047857; font-weight: bold;"">{3:N0} VNĐ</td>
        </tr>
        <tr>
            <td class=""info-label"">Viết bằng chữ:</td>
            <td class=""info-val"" style=""font-style: italic;""><b>{4}</b></td>
        </tr>
        <tr>
            <td class=""info-label"">Hình thức thu:</td>
            <td class=""info-val"">{5}</td>
        </tr>
        <tr>
            <td class=""info-label"">Kèm theo chứng từ:</td>
            <td class=""info-val"">{6}</td>
        </tr>
    </table>",
                Escape(pt.NguoiNop),
                string.IsNullOrEmpty(pt.TenKH) ? "Khách hàng vãng lai" : Escape(pt.TenKH),
                Escape(pt.LyDoThu),
                pt.SoTien,
                VietnameseNumberReader.ReadMoney(pt.SoTien),
                Escape(pt.HinhThuc),
                string.IsNullOrEmpty(pt.MaHDB) ? "Không có" : "Hóa đơn bán hàng số " + Escape(pt.MaHDB));

            sb.Append(@"
    <table class=""signature-table"">
        <tr>
            <td style=""width: 20%;"">
                <div class=""signature-title"">Giám đốc</div>
                <div class=""signature-sub"">(Ký, họ tên, đóng dấu)</div>
                <div class=""signature-name""></div>
            </td>
            <td style=""width: 20%;"">
                <div class=""signature-title"">Kế toán trưởng</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name""></div>
            </td>
            <td style=""width: 20%;"">
                <div class=""signature-title"">Người nộp tiền</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name"">" + Escape(pt.NguoiNop) + @"</div>
            </td>
            <td style=""width: 20%;"">
                <div class=""signature-title"">Người lập phiếu</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name"">" + Escape(pt.TenNV) + @"</div>
            </td>
            <td style=""width: 20%;"">
                <div class=""signature-title"">Thủ quỹ</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name""></div>
            </td>
        </tr>
    </table>");

            sb.Append(HtmlTail);
            return sb.ToString();
        }

        /// <summary>
        /// Tạo mẫu in Phiếu Chi (Mẫu số 02-TT).
        /// </summary>
        public static string GeneratePhieuChiHtml(PhieuChi pc)
        {
            if (pc == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append(GetHtmlHead("Phiếu Chi - " + pc.MaPC));

            sb.AppendFormat(@"
    <table class=""header-table"">
        <tr>
            <td style=""width: 65%;"">
                <div class=""company-name"">{0}</div>
                <div class=""company-info"">Địa chỉ: {1}<br/>Mã số thuế: {2} - ĐT: {3}</div>
            </td>
            <td class=""form-code"">
                Mẫu số: 02 - TT<br/>
                (Ban hành theo TT số 200/2014/TT-BTC<br/>của Bộ Tài chính)
            </td>
        </tr>
    </table>", CompanyName, CompanyAddress, CompanyTaxCode, CompanyPhone);

            sb.AppendFormat(@"
    <div class=""voucher-title"">PHIẾU CHI</div>
    <div class=""voucher-subtitle"">Ngày {0:dd} tháng {0:MM} năm {0:yyyy} &bull; Số: <b>{1}</b> &bull; Quyển số: 01</div>",
                pc.NgayChi ?? DateTime.Now, pc.MaPC);

            sb.AppendFormat(@"
    <table class=""info-table"" style=""margin-bottom: 25px;"">
        <tr>
            <td class=""info-label"">Họ tên người nhận tiền:</td>
            <td class=""info-val""><b>{0}</b></td>
        </tr>
        <tr>
            <td class=""info-label"">Lý do chi tiền:</td>
            <td class=""info-val"">{1}</td>
        </tr>
        <tr>
            <td class=""info-label"">Số tiền chi:</td>
            <td class=""info-val"" style=""font-size: 16px; color: #b91c1c; font-weight: bold;"">{2:N0} VNĐ</td>
        </tr>
        <tr>
            <td class=""info-label"">Viết bằng chữ:</td>
            <td class=""info-val"" style=""font-style: italic;""><b>{3}</b></td>
        </tr>
        <tr>
            <td class=""info-label"">Hình thức chi:</td>
            <td class=""info-val"">{4}</td>
        </tr>
        <tr>
            <td class=""info-label"">Nhân viên lập phiếu:</td>
            <td class=""info-val"">{5} (Mã: {6})</td>
        </tr>
    </table>",
                Escape(pc.NguoiNhan),
                Escape(pc.LyDoChi),
                pc.SoTien,
                VietnameseNumberReader.ReadMoney(pc.SoTien),
                Escape(pc.HinhThuc),
                Escape(pc.TenNV), Escape(pc.MaNV));

            sb.Append(@"
    <table class=""signature-table"">
        <tr>
            <td style=""width: 20%;"">
                <div class=""signature-title"">Giám đốc</div>
                <div class=""signature-sub"">(Ký, họ tên, đóng dấu)</div>
                <div class=""signature-name""></div>
            </td>
            <td style=""width: 20%;"">
                <div class=""signature-title"">Kế toán trưởng</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name""></div>
            </td>
            <td style=""width: 20%;"">
                <div class=""signature-title"">Người nhận tiền</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name"">" + Escape(pc.NguoiNhan) + @"</div>
            </td>
            <td style=""width: 20%;"">
                <div class=""signature-title"">Người lập phiếu</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name"">" + Escape(pc.TenNV) + @"</div>
            </td>
            <td style=""width: 20%;"">
                <div class=""signature-title"">Thủ quỹ</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name""></div>
            </td>
        </tr>
    </table>");

            sb.Append(HtmlTail);
            return sb.ToString();
        }

        /// <summary>
        /// Tạo mẫu in Phiếu Xuất Kho (Mẫu số 02-VT).
        /// </summary>
        public static string GeneratePhieuXuatKhoHtml(PhieuXuatKho pxk, DataTable dtChiTiet)
        {
            if (pxk == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append(GetHtmlHead("Phiếu Xuất Kho - " + pxk.MaPXK));

            sb.AppendFormat(@"
    <table class=""header-table"">
        <tr>
            <td style=""width: 65%;"">
                <div class=""company-name"">{0}</div>
                <div class=""company-info"">Địa chỉ: {1}<br/>Mã số thuế: {2} - ĐT: {3}</div>
            </td>
            <td class=""form-code"">
                Mẫu số: 02 - VT<br/>
                (Ban hành theo TT số 200/2014/TT-BTC<br/>của Bộ Tài chính)
            </td>
        </tr>
    </table>", CompanyName, CompanyAddress, CompanyTaxCode, CompanyPhone);

            sb.AppendFormat(@"
    <div class=""voucher-title"">PHIẾU XUẤT KHO</div>
    <div class=""voucher-subtitle"">Ngày {0:dd} tháng {0:MM} năm {0:yyyy} &bull; Số: <b>{1}</b></div>",
                pxk.NgayXuat ?? DateTime.Now, pxk.MaPXK);

            sb.AppendFormat(@"
    <table class=""info-table"">
        <tr>
            <td class=""info-label"">Khách hàng nhận:</td>
            <td class=""info-val""><b>{0}</b></td>
        </tr>
        <tr>
            <td class=""info-label"">Xuất tại kho:</td>
            <td class=""info-val""><b>{1}</b> (Mã kho: {2})</td>
        </tr>
        <tr>
            <td class=""info-label"">Theo hóa đơn bán:</td>
            <td class=""info-val"">{3}</td>
        </tr>
        <tr>
            <td class=""info-label"">Lý do xuất kho:</td>
            <td class=""info-val"">{4}</td>
        </tr>
        <tr>
            <td class=""info-label"">Thủ kho thực hiện:</td>
            <td class=""info-val"">{5}</td>
        </tr>
    </table>",
                string.IsNullOrEmpty(pxk.TenKH) ? "Giao theo hóa đơn" : Escape(pxk.TenKH),
                Escape(pxk.TenKho), Escape(pxk.MaKho),
                string.IsNullOrEmpty(pxk.MaHDB) ? "Không gắn HDB" : Escape(pxk.MaHDB),
                Escape(pxk.LyDoXuat),
                Escape(pxk.TenNV));

            sb.Append(@"
    <table class=""items-table"">
        <thead>
            <tr>
                <th style=""width: 35px;"">STT</th>
                <th style=""width: 90px;"">Mã SP</th>
                <th>Tên sản phẩm / Quy cách</th>
                <th style=""width: 60px;"">ĐVT</th>
                <th style=""width: 80px;"">Số lượng xuất</th>
            </tr>
        </thead>
        <tbody>");

            int stt = 1;
            int tongSL = 0;
            if (dtChiTiet != null && dtChiTiet.Rows.Count > 0)
            {
                foreach (DataRow r in dtChiTiet.Rows)
                {
                    string maSP = r["MaSP"] != null ? r["MaSP"].ToString() : "";
                    string tenSP = r.Table.Columns.Contains("TenSP") && r["TenSP"] != null ? r["TenSP"].ToString() : "";
                    string dvt = r.Table.Columns.Contains("DonViTinh") && r["DonViTinh"] != null ? r["DonViTinh"].ToString() : "";
                    int sl = r["SoLuongXuat"] != DBNull.Value ? Convert.ToInt32(r["SoLuongXuat"]) : 0;
                    tongSL += sl;

                    sb.AppendFormat(@"
            <tr>
                <td class=""text-center"">{0}</td>
                <td class=""text-center"">{1}</td>
                <td><b>{2}</b></td>
                <td class=""text-center"">{3}</td>
                <td class=""text-right font-bold"">{4:N0}</td>
            </tr>", stt++, Escape(maSP), Escape(tenSP), Escape(dvt), sl);
                }
            }
            else
            {
                sb.Append(@"<tr><td colspan=""5"" class=""text-center"" style=""padding: 12px;"">Không có dòng chi tiết mặt hàng xuất.</td></tr>");
            }

            sb.AppendFormat(@"
            <tr>
                <td colspan=""4"" class=""text-right font-bold"">TỔNG SỐ LƯỢNG XUẤT:</td>
                <td class=""text-right font-bold"" style=""color: #0369a1;"">{0:N0}</td>
            </tr>
        </tbody>
    </table>
    <div class=""amount-words"">Tổng số lượng xuất (viết bằng chữ): <b>{1} ({0:N0} sản phẩm).</b></div>",
                tongSL, VietnameseNumberReader.CapitalizeFirst(VietnameseNumberReader.ReadNumber(tongSL)));

            sb.Append(@"
    <table class=""signature-table"">
        <tr>
            <td style=""width: 25%;"">
                <div class=""signature-title"">Người lập phiếu</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name"">" + Escape(pxk.TenNV) + @"</div>
            </td>
            <td style=""width: 25%;"">
                <div class=""signature-title"">Người nhận hàng</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name""></div>
            </td>
            <td style=""width: 25%;"">
                <div class=""signature-title"">Thủ kho</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name""></div>
            </td>
            <td style=""width: 25%;"">
                <div class=""signature-title"">Giám đốc</div>
                <div class=""signature-sub"">(Ký, đóng dấu, họ tên)</div>
                <div class=""signature-name""></div>
            </td>
        </tr>
    </table>");

            sb.Append(HtmlTail);
            return sb.ToString();
        }

        /// <summary>
        /// Tạo mẫu in Đơn Đặt Hàng.
        /// </summary>
        public static string GenerateDonDatHangHtml(DonDatHang ddh, DataTable dtChiTiet)
        {
            if (ddh == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append(GetHtmlHead("Đơn Đặt Hàng - " + ddh.MaDDH));

            sb.AppendFormat(@"
    <table class=""header-table"">
        <tr>
            <td style=""width: 65%;"">
                <div class=""company-name"">{0}</div>
                <div class=""company-info"">Địa chỉ: {1}<br/>Mã số thuế: {2} - ĐT: {3}</div>
            </td>
            <td class=""form-code"">
                Bộ phận bán hàng<br/>
                Hệ thống kế toán bán hàng DNQH
            </td>
        </tr>
    </table>", CompanyName, CompanyAddress, CompanyTaxCode, CompanyPhone);

            sb.AppendFormat(@"
    <div class=""voucher-title"">ĐƠN ĐẶT HÀNG</div>
    <div class=""voucher-subtitle"">Ngày đặt: {0:dd/MM/yyyy HH:mm} &bull; Số đơn: <b>{1}</b> &bull; Trạng thái: <b>{2}</b></div>",
                ddh.NgayDat, ddh.MaDDH, Escape(ddh.TrangThai));

            sb.AppendFormat(@"
    <table class=""info-table"">
        <tr>
            <td class=""info-label"">Khách hàng:</td>
            <td class=""info-val""><b>{0}</b> (Mã: {1})</td>
        </tr>
        <tr>
            <td class=""info-label"">Ngày hẹn giao:</td>
            <td class=""info-val"">{2}</td>
        </tr>
        <tr>
            <td class=""info-label"">Nhân viên tiếp nhận:</td>
            <td class=""info-val"">{3} (Mã: {4})</td>
        </tr>
        <tr>
            <td class=""info-label"">Ghi chú đơn hàng:</td>
            <td class=""info-val"">{5}</td>
        </tr>
    </table>",
                Escape(ddh.TenKH), Escape(ddh.MaKH),
                ddh.NgayGiaoDuKien.HasValue ? ddh.NgayGiaoDuKien.Value.ToString("dd/MM/yyyy") : "Chưa xác định",
                Escape(ddh.TenNV), Escape(ddh.MaNV),
                string.IsNullOrEmpty(ddh.GhiChu) ? "Không có" : Escape(ddh.GhiChu));

            sb.Append(@"
    <table class=""items-table"">
        <thead>
            <tr>
                <th style=""width: 35px;"">STT</th>
                <th>Tên mặt hàng</th>
                <th style=""width: 60px;"">ĐVT</th>
                <th style=""width: 65px;"">Số lượng</th>
                <th style=""width: 100px;"">Đơn giá</th>
                <th style=""width: 70px;"">Giảm giá</th>
                <th style=""width: 115px;"">Thành tiền (VNĐ)</th>
            </tr>
        </thead>
        <tbody>");

            int stt = 1;
            decimal tongTien = 0;
            if (dtChiTiet != null && dtChiTiet.Rows.Count > 0)
            {
                foreach (DataRow r in dtChiTiet.Rows)
                {
                    string tenSP = r["TenSP"] != null ? r["TenSP"].ToString() : "";
                    string dvt = r.Table.Columns.Contains("DonViTinh") && r["DonViTinh"] != null ? r["DonViTinh"].ToString() : "";
                    int soLuong = r["SoLuong"] != DBNull.Value ? Convert.ToInt32(r["SoLuong"]) : 0;
                    decimal donGia = r["DonGia"] != DBNull.Value ? Convert.ToDecimal(r["DonGia"]) : 0;
                    decimal giamGia = r.Table.Columns.Contains("GiamGia") && r["GiamGia"] != DBNull.Value ? Convert.ToDecimal(r["GiamGia"]) : 0;
                    decimal thanhTien = r["ThanhTien"] != DBNull.Value ? Convert.ToDecimal(r["ThanhTien"]) : 0;
                    tongTien += thanhTien;

                    sb.AppendFormat(@"
            <tr>
                <td class=""text-center"">{0}</td>
                <td><b>{1}</b></td>
                <td class=""text-center"">{2}</td>
                <td class=""text-right"">{3:N0}</td>
                <td class=""text-right"">{4:N0}</td>
                <td class=""text-right"">{5:N0}%</td>
                <td class=""text-right font-bold"">{6:N0}</td>
            </tr>", stt++, Escape(tenSP), Escape(dvt), soLuong, donGia, giamGia, thanhTien);
                }
            }
            else
            {
                tongTien = ddh.TongTien;
                sb.Append(@"<tr><td colspan=""7"" class=""text-center"" style=""padding: 12px;"">Không có chi tiết mặt hàng.</td></tr>");
            }

            sb.AppendFormat(@"
            <tr>
                <td colspan=""6"" class=""text-right font-bold"" style=""font-size: 13px;"">TỔNG GIÁ TRỊ ĐƠN HÀNG:</td>
                <td class=""text-right font-bold"" style=""font-size: 14px; color: #b91c1c;"">{0:N0} VNĐ</td>
            </tr>
        </tbody>
    </table>", tongTien);

            sb.AppendFormat(@"<div class=""amount-words""><b>Số tiền viết bằng chữ:</b> {0}</div>",
                VietnameseNumberReader.ReadMoney(tongTien));

            sb.Append(@"
    <table class=""signature-table"">
        <tr>
            <td style=""width: 33%;"">
                <div class=""signature-title"">Khách hàng</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name"">" + Escape(ddh.TenKH) + @"</div>
            </td>
            <td style=""width: 33%;"">
                <div class=""signature-title"">Nhân viên nhận đơn</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name"">" + Escape(ddh.TenNV) + @"</div>
            </td>
            <td style=""width: 33%;"">
                <div class=""signature-title"">Phụ trách bán hàng</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name""></div>
            </td>
        </tr>
    </table>");

            sb.Append(HtmlTail);
            return sb.ToString();
        }

        /// <summary>
        /// Tạo mẫu in Chứng từ Kế toán (Mẫu số 01-ĐK).
        /// </summary>
        public static string GenerateChungTuHtml(ChungTu ct, DataTable dtChiTiet)
        {
            if (ct == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append(GetHtmlHead("Chứng Từ Kế Toán - " + ct.MaCT));

            sb.AppendFormat(@"
    <table class=""header-table"">
        <tr>
            <td style=""width: 65%;"">
                <div class=""company-name"">{0}</div>
                <div class=""company-info"">Địa chỉ: {1}<br/>Mã số thuế: {2} - ĐT: {3}</div>
            </td>
            <td class=""form-code"">
                Mẫu số: 01 - ĐK<br/>
                (Ban hành theo TT số 200/2014/TT-BTC<br/>của Bộ Tài chính)
            </td>
        </tr>
    </table>", CompanyName, CompanyAddress, CompanyTaxCode, CompanyPhone);

            sb.AppendFormat(@"
    <div class=""voucher-title"">CHỨNG TỪ KẾ TOÁN</div>
    <div class=""voucher-subtitle"">Ngày {0:dd} tháng {0:MM} năm {0:yyyy} &bull; Số: <b>{1}</b> &bull; Loại: <b>{2}</b></div>",
                ct.NgayCT ?? DateTime.Now, ct.MaCT, Escape(ct.LoaiCT));

            sb.AppendFormat(@"
    <table class=""info-table"">
        <tr>
            <td class=""info-label"">Nội dung / Diễn giải:</td>
            <td class=""info-val""><b>{0}</b></td>
        </tr>
        <tr>
            <td class=""info-label"">Hóa đơn bán kèm theo:</td>
            <td class=""info-val"">{1}</td>
        </tr>
        <tr>
            <td class=""info-label"">Nhân viên hạch toán:</td>
            <td class=""info-val"">{2} (Mã: {3})</td>
        </tr>
    </table>",
                Escape(ct.DienGiai),
                string.IsNullOrEmpty(ct.MaHDB) ? "Không kèm HDB" : Escape(ct.MaHDB),
                Escape(ct.TenNV), Escape(ct.MaNV));

            sb.Append(@"
    <table class=""items-table"">
        <thead>
            <tr>
                <th style=""width: 35px;"">STT</th>
                <th>Diễn giải chi tiết nghiệp vụ</th>
                <th style=""width: 110px;"">Tài khoản Nợ</th>
                <th style=""width: 110px;"">Tài khoản Có</th>
                <th style=""width: 140px;"">Số tiền (VNĐ)</th>
            </tr>
        </thead>
        <tbody>");

            int stt = 1;
            decimal tongTien = 0;
            if (dtChiTiet != null && dtChiTiet.Rows.Count > 0)
            {
                foreach (DataRow r in dtChiTiet.Rows)
                {
                    string tkNo = r["TaiKhoanNo"] != null ? r["TaiKhoanNo"].ToString() : "";
                    string tkCo = r["TaiKhoanCo"] != null ? r["TaiKhoanCo"].ToString() : "";
                    decimal soTien = r["SoTien"] != DBNull.Value ? Convert.ToDecimal(r["SoTien"]) : 0;
                    tongTien += soTien;

                    sb.AppendFormat(@"
            <tr>
                <td class=""text-center"">{0}</td>
                <td>{1}</td>
                <td class=""text-center font-bold"">{2}</td>
                <td class=""text-center font-bold"">{3}</td>
                <td class=""text-right font-bold"">{4:N0}</td>
            </tr>", stt++, Escape(ct.DienGiai), Escape(tkNo), Escape(tkCo), soTien);
                }
            }
            else
            {
                sb.Append(@"<tr><td colspan=""5"" class=""text-center"" style=""padding: 12px;"">Không có dòng định khoản chi tiết.</td></tr>");
            }

            sb.AppendFormat(@"
            <tr>
                <td colspan=""4"" class=""text-right font-bold"" style=""font-size: 13px;"">TỔNG CỘNG PHÁT SINH:</td>
                <td class=""text-right font-bold"" style=""font-size: 14px; color: #0369a1;"">{0:N0} VNĐ</td>
            </tr>
        </tbody>
    </table>", tongTien);

            sb.AppendFormat(@"<div class=""amount-words""><b>Số tiền viết bằng chữ:</b> {0}</div>",
                VietnameseNumberReader.ReadMoney(tongTien));

            sb.Append(@"
    <table class=""signature-table"">
        <tr>
            <td style=""width: 33%;"">
                <div class=""signature-title"">Người lập biểu</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name"">" + Escape(ct.TenNV) + @"</div>
            </td>
            <td style=""width: 33%;"">
                <div class=""signature-title"">Kế toán trưởng</div>
                <div class=""signature-sub"">(Ký, họ tên)</div>
                <div class=""signature-name""></div>
            </td>
            <td style=""width: 33%;"">
                <div class=""signature-title"">Thủ trưởng đơn vị</div>
                <div class=""signature-sub"">(Ký, đóng dấu, họ tên)</div>
                <div class=""signature-name""></div>
            </td>
        </tr>
    </table>");

            sb.Append(HtmlTail);
            return sb.ToString();
        }

        private static string Escape(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            return text.Replace("&", "&amp;")
                       .Replace("<", "&lt;")
                       .Replace(">", "&gt;")
                       .Replace("\"", "&quot;");
        }
    }
}
