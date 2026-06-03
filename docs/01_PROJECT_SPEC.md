# 01_PROJECT_SPEC.md
## HỆ THỐNG THÔNG TIN KẾ TOÁN – KẾ TOÁN BÁN HÀNG

> **WHAT — nguồn sự thật nghiệp vụ.**
> Runtime cố định: **C# WinForms + .NET Framework 4.8 + SQL Server 2022 Express + ADO.NET**.

## 1. Phạm vi
Hệ thống desktop gồm 7 nhóm chức năng:
1. Quản lý hệ thống.
2. Quản lý danh mục.
3. Kế toán bán hàng.
4. Quản lý chứng từ & tiền.
5. Quản lý kho.
6. Kế toán chi tiết.
7. Kế toán tổng hợp.

Người dùng trực tiếp:
- Quản trị viên.
- Nhân viên bán hàng.
- Nhân viên kho.
- Nhân viên kế toán.

Khách hàng là đối tượng nghiệp vụ, không đăng nhập.

### Ngoài phạm vi
Không tự ý thêm: kế toán lương, phiếu nhập kho, hóa đơn mua hàng, công nợ NCC, customer login, web/API/mobile, multi-tenant.

## 2. Công nghệ cố định
- C#.
- Windows Forms.
- .NET Framework 4.8.
- Microsoft SQL Server 2022 Express.
- ADO.NET.
- `System.Data.SqlClient`.
- Visual Studio dùng để build/debug/WinForms Designer.

SQL Server:
- Instance: `localhost\SQLEXPRESS`
- Database: `DNQH_KeToanBanHang`
- Windows Authentication.

Connection string development:
```xml
<connectionStrings>
  <add name="MainDb"
       providerName="System.Data.SqlClient"
       connectionString="Server=localhost\SQLEXPRESS;Database=DNQH_KeToanBanHang;Integrated Security=True;TrustServerCertificate=True;" />
</connectionStrings>
```

## 3. 18 bảng
`TAIKHOAN`, `NHANVIEN`, `KHACHHANG`, `NHACUNGCAP`, `LOAISANPHAM`, `SANPHAM`,
`DONDATHANG`, `CHITIETDONDATHANG`, `HOADONBAN`, `CHITIETHOADONBAN`,
`KHO`, `TONKHO`, `PHIEUXUATKHO`, `CHITIETPHIEUXUATKHO`,
`PHIEUTHU`, `PHIEUCHI`, `CHUNGTU`, `CHITIETCHUNGTU`.

## 4. Quan hệ cốt lõi
```text
NHANVIEN 1 -------- 0..1 TAIKHOAN
KHACHHANG 1 ------- 0..* DONDATHANG
NHANVIEN 1 -------- 0..* DONDATHANG
DONDATHANG 1 ------ 1..* CHITIETDONDATHANG
SANPHAM 1 --------- 0..* CHITIETDONDATHANG
DONDATHANG 1 ------ 0..1 HOADONBAN
NHANVIEN 1 -------- 0..* HOADONBAN
KHACHHANG 1 ------- 0..* HOADONBAN
HOADONBAN 1 ------- 1..* CHITIETHOADONBAN
SANPHAM 1 --------- 0..* CHITIETHOADONBAN
NHACUNGCAP 1 ------ 0..* SANPHAM
LOAISANPHAM 1 ----- 0..* SANPHAM
KHO 1 ------------- 0..* TONKHO
SANPHAM 1 --------- 0..* TONKHO
HOADONBAN 1 ------- 0..* PHIEUTHU
NHANVIEN 1 -------- 0..* PHIEUTHU
NHANVIEN 1 -------- 0..* PHIEUCHI
HOADONBAN 1 ------- 0..* CHUNGTU
NHANVIEN 1 -------- 0..* CHUNGTU
CHUNGTU 1 --------- 1..* CHITIETCHUNGTU
HOADONBAN 1 ------- 0..* PHIEUXUATKHO
NHANVIEN 1 -------- 0..* PHIEUXUATKHO
KHO 1 ------------- 0..* PHIEUXUATKHO
PHIEUXUATKHO 1 ---- 1..* CHITIETPHIEUXUATKHO
SANPHAM 1 --------- 0..* CHITIETPHIEUXUATKHO
```

## 5. Database invariants
- Một nhân viên có tối đa một tài khoản: `TAIKHOAN.MaNV UNIQUE`.
- Username duy nhất.
- `DONDATHANG` **không có** `MaHDB`.
- `HOADONBAN.MaDDH` là FK + UNIQUE.
- `SANPHAM` **không có** `SoLuongTon`.
- Tồn kho chỉ ở `TONKHO(MaKho, MaSP)`.
- `PHIEUTHU` tham chiếu `HOADONBAN`.
- `PHIEUCHI` độc lập, chỉ gắn `NHANVIEN` trong scope hiện tại.
- `CHUNGTU` phát sinh từ `HOADONBAN`.
- Không nối `CHUNGTU` với `PHIEUTHU`/`PHIEUCHI`.

## 6. Luồng bán hàng chuẩn
```text
Khách hàng
→ tiếp nhận yêu cầu
→ khách hàng/sản phẩm
→ kiểm tra tồn kho
→ đơn đặt hàng + chi tiết
→ hóa đơn bán + chi tiết
→ phiếu xuất kho + chi tiết
→ cập nhật tồn kho
```

Nhánh:
```text
Hóa đơn bán → Phiếu thu
Hóa đơn bán → Chứng từ → Chi tiết chứng từ
Nhân viên → Phiếu chi độc lập
```

**Không giảm tồn khi chỉ lập đơn hoặc hóa đơn.** Chỉ giảm tồn trong nghiệp vụ xuất kho thành công.

## 7. Quy tắc nghiệp vụ
### Đơn đặt hàng
- Có khách hàng và nhân viên lập.
- Có ít nhất một chi tiết.
- `SoLuong > 0`, `DonGia >= 0`, `0 <= GiamGia <= 100`.
- `ThanhTien = SoLuong * DonGia * (1 - GiamGia / 100)`.
- `TongTien = SUM(ThanhTien)`.
- Kiểm tra tồn trước khi hoàn tất.

### Hóa đơn
- Bắt buộc từ một đơn.
- Một đơn tối đa một hóa đơn.
- Có ít nhất một chi tiết.
- Tổng tiền tính từ chi tiết.

### Kho
- Không cho tồn âm.
- Không xuất quá tồn.
- Lập phiếu xuất + chi tiết + trừ tồn phải atomic transaction.

### Phiếu thu
- Phải tham chiếu hóa đơn.
- Một hóa đơn có thể nhiều phiếu thu.
- `SoTien > 0`.

### Phiếu chi
- Do nhân viên lập.
- Độc lập trong scope hiện tại.
- `SoTien > 0`.

### Chứng từ
- Phát sinh từ hóa đơn.
- Chi tiết chứa TK Nợ, TK Có, số tiền.
- `SoTien > 0`.

## 8. Actor / phân quyền
### Người dùng chung
- Đăng nhập, đăng xuất, đổi mật khẩu.

### Quản trị viên
- Quản lý tài khoản.
- Phân quyền.
- Quản lý nhân viên.
- Báo cáo tổng hợp.

### Nhân viên bán hàng
- Khách hàng/NCC/loại SP/SP.
- Đơn đặt hàng.
- Kiểm tra tồn.
- Hóa đơn.
- Tra cứu.

### Nhân viên kho
- Kho/tồn.
- Phiếu xuất.
- Cập nhật tồn.

### Nhân viên kế toán
- Phiếu thu/chi.
- Chứng từ/hạch toán.
- Kế toán chi tiết.
- Báo cáo tổng hợp.

## 9. Form bắt buộc
`frmDangNhap`, `frmMain`, `frmTaiKhoan`, `frmNhanVien`, `frmKhachHang`,
`frmNhaCungCap`, `frmLoaiSanPham`, `frmSanPham`, `frmDonDatHang`,
`frmHoaDonBan`, `frmKho`, `frmTonKho`, `frmPhieuXuatKho`, `frmPhieuThu`,
`frmPhieuChi`, `frmChungTu`, `frmKeToanChiTiet`, `frmBaoCaoTongHop`.

## 10. Acceptance
- Build thành công.
- Login/phân quyền đúng.
- CRUD chạy.
- Đơn/hóa đơn/phiếu xuất/chứng từ dùng transaction đúng.
- Không có hai hóa đơn cho một đơn.
- Không tồn âm.
- Phiếu thu đúng hóa đơn.
- Phiếu chi đúng scope.
- Báo cáo lấy dữ liệu thật từ DB.
