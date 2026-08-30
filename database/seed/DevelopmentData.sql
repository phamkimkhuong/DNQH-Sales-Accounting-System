/*================================================================================
   HỆ THỐNG THÔNG TIN KẾ TOÁN - KẾ TOÁN BÁN HÀNG (DNQH)
   FILE 2: SCRIPT NẠP DỮ LIỆU MẪU CHUẨN DOANH NGHIỆP (ENTERPRISE SEED DATA)
================================================================================
   1. MỤC ĐÍCH:
      - Cung cấp bộ dữ liệu mẫu chuẩn hóa, chân thực và chuyên nghiệp phục vụ báo
        cáo nghiệm thu đồ án, thuyết minh với giảng viên và khách hàng:
        + 6 Nhà cung cấp uy tín hàng đầu (Sunhouse, Samsung Vina, CMC, Synnex FPT...)
        + 4 Loại sản phẩm phân loại rõ ràng (Gia dụng, Tin học, Nghe nhìn, Vật tư)
        + 14 Sản phẩm thương mại chi tiết có đơn vị tính, giá niêm yết chuẩn
        + 8 Khách hàng đa dạng (Doanh nghiệp B2B, Trường đại học, Bệnh viện, Siêu thị)
        + 3 Kho hàng chiến lược (Kho Tổng Miền Bắc, Kho Miền Nam, Kho Miền Trung)
        + Số dư tồn kho ban đầu đầy đủ tại bảng TONKHO cho từng sản phẩm tại các kho
      - Giúp hệ thống khi khởi động có sẵn dữ liệu phong phú để thực hiện đầy đủ
        các quy trình: Lập đơn đặt hàng, Phát hành hóa đơn, Phiếu xuất kho, Thu/Chi,
        Sổ cái kế toán và Báo cáo tổng hợp doanh thu - công nợ.

   2. THỨ TỰ THỰC THI TRÊN HỆ THỐNG:
      - BƯỚC 2 (Thực thi ngay sau File 1: DNQH_KeToanBanHang.sql).

   3. TÍNH TOÀN VẸN & AN TOÀN (IDEMPOTENT):
      - Tuân thủ 100% Invariants: Sản phẩm không lưu số lượng tồn, tồn kho quản lý riêng.
      - Có khối kiểm tra "IF NOT EXISTS (...) INSERT ... ELSE UPDATE ...", có thể chạy lại
        nhiều lần mà không gây lỗi trùng khóa chính (Primary Key).
================================================================================*/

USE DNQH_KeToanBanHang;
GO

-- ============================================================================
-- 1. NẠP DANH MỤC NHÀ CUNG CẤP (NHACUNGCAP)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Nhà Cung Cấp...';

-- NCC001
IF NOT EXISTS (SELECT 1 FROM NHACUNGCAP WHERE MaNCC = 'NCC001')
    INSERT INTO NHACUNGCAP (MaNCC, TenNCC, DiaChi, SoDienThoai, Email)
    VALUES ('NCC001', N'Công Ty Cổ Phần Tập Đoàn Sunhouse', N'Tầng 12 Tòa nhà Richy, 35 Mạc Thái Tổ, Yên Hòa, Cầu Giấy, Hà Nội', '02437366688', 'contact@sunhouse.com.vn');
ELSE
    UPDATE NHACUNGCAP SET TenNCC = N'Công Ty Cổ Phần Tập Đoàn Sunhouse', DiaChi = N'Tầng 12 Tòa nhà Richy, 35 Mạc Thái Tổ, Yên Hòa, Cầu Giấy, Hà Nội', SoDienThoai = '02437366688', Email = 'contact@sunhouse.com.vn' WHERE MaNCC = 'NCC001';

-- NCC002
IF NOT EXISTS (SELECT 1 FROM NHACUNGCAP WHERE MaNCC = 'NCC002')
    INSERT INTO NHACUNGCAP (MaNCC, TenNCC, DiaChi, SoDienThoai, Email)
    VALUES ('NCC002', N'Công Ty TNHH Điện Tử Samsung Vina', N'Tầng 25 Tòa nhà Bitexco, 2 Hải Triều, Bến Nghé, Quận 1, TP.HCM', '02839157310', 'b2b.vietnam@samsung.com');
ELSE
    UPDATE NHACUNGCAP SET TenNCC = N'Công Ty TNHH Điện Tử Samsung Vina', DiaChi = N'Tầng 25 Tòa nhà Bitexco, 2 Hải Triều, Bến Nghé, Quận 1, TP.HCM', SoDienThoai = '02839157310', Email = 'b2b.vietnam@samsung.com' WHERE MaNCC = 'NCC002';

-- NCC003
IF NOT EXISTS (SELECT 1 FROM NHACUNGCAP WHERE MaNCC = 'NCC003')
    INSERT INTO NHACUNGCAP (MaNCC, TenNCC, DiaChi, SoDienThoai, Email)
    VALUES ('NCC003', N'Công Ty Cổ Phần Tập Đoàn Công Nghệ CMC', N'Tòa nhà CMC, Phố Duy Tân, Dịch Vọng Hậu, Cầu Giấy, Hà Nội', '02437958668', 'info@cmc.com.vn');
ELSE
    UPDATE NHACUNGCAP SET TenNCC = N'Công Ty Cổ Phần Tập Đoàn Công Nghệ CMC', DiaChi = N'Tòa nhà CMC, Phố Duy Tân, Dịch Vọng Hậu, Cầu Giấy, Hà Nội', SoDienThoai = '02437958668', Email = 'info@cmc.com.vn' WHERE MaNCC = 'NCC003';

-- NCC004
IF NOT EXISTS (SELECT 1 FROM NHACUNGCAP WHERE MaNCC = 'NCC004')
    INSERT INTO NHACUNGCAP (MaNCC, TenNCC, DiaChi, SoDienThoai, Email)
    VALUES ('NCC004', N'Công Ty TNHH Phân Phối Synnex FPT', N'Tòa nhà FPT Cầu Giấy, 10 Phạm Văn Bạch, Cầu Giấy, Hà Nội', '02473006666', 'synnex.sales@fpt.com');
ELSE
    UPDATE NHACUNGCAP SET TenNCC = N'Công Ty TNHH Phân Phối Synnex FPT', DiaChi = N'Tòa nhà FPT Cầu Giấy, 10 Phạm Văn Bạch, Cầu Giấy, Hà Nội', SoDienThoai = '02473006666', Email = 'synnex.sales@fpt.com' WHERE MaNCC = 'NCC004';

-- NCC005
IF NOT EXISTS (SELECT 1 FROM NHACUNGCAP WHERE MaNCC = 'NCC005')
    INSERT INTO NHACUNGCAP (MaNCC, TenNCC, DiaChi, SoDienThoai, Email)
    VALUES ('NCC005', N'Công Ty Cổ Phần Văn Phòng Phẩm Hồng Hà', N'25 Lý Thường Kiệt, Hàng Bài, Hoàn Kiếm, Hà Nội', '02438562116', 'cskh@vpphongha.vn');
ELSE
    UPDATE NHACUNGCAP SET TenNCC = N'Công Ty Cổ Phần Văn Phòng Phẩm Hồng Hà', DiaChi = N'25 Lý Thường Kiệt, Hàng Bài, Hoàn Kiếm, Hà Nội', SoDienThoai = '02438562116', Email = 'cskh@vpphongha.vn' WHERE MaNCC = 'NCC005';

-- NCC006
IF NOT EXISTS (SELECT 1 FROM NHACUNGCAP WHERE MaNCC = 'NCC006')
    INSERT INTO NHACUNGCAP (MaNCC, TenNCC, DiaChi, SoDienThoai, Email)
    VALUES ('NCC006', N'Công Ty TNHH LG Electronics Việt Nam', N'Lô CN-02 KCN Tràng Duệ, Xã Lê Lợi, An Dương, Hải Phòng', '02258831000', 'b2b.lg@lge.com');
ELSE
    UPDATE NHACUNGCAP SET TenNCC = N'Công Ty TNHH LG Electronics Việt Nam', DiaChi = N'Lô CN-02 KCN Tràng Duệ, Xã Lê Lợi, An Dương, Hải Phòng', SoDienThoai = '02258831000', Email = 'b2b.lg@lge.com' WHERE MaNCC = 'NCC006';
GO

-- ============================================================================
-- 2. NẠP DANH MỤC LOẠI SẢN PHẨM (LOAISANPHAM)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Loại Sản Phẩm...';

IF NOT EXISTS (SELECT 1 FROM LOAISANPHAM WHERE MaLoai = 'LSP001')
    INSERT INTO LOAISANPHAM (MaLoai, TenLoai, MoTa)
    VALUES ('LSP001', N'Thiết bị gia dụng thông minh', N'Các thiết bị đồ dùng gia đình cao cấp: Nồi chiên, robot hút bụi, máy lọc không khí');
ELSE
    UPDATE LOAISANPHAM SET TenLoai = N'Thiết bị gia dụng thông minh', MoTa = N'Các thiết bị đồ dùng gia đình cao cấp: Nồi chiên, robot hút bụi, máy lọc không khí' WHERE MaLoai = 'LSP001';

IF NOT EXISTS (SELECT 1 FROM LOAISANPHAM WHERE MaLoai = 'LSP002')
    INSERT INTO LOAISANPHAM (MaLoai, TenLoai, MoTa)
    VALUES ('LSP002', N'Thiết bị tin học & văn phòng', N'Máy in laser, máy tính để bàn đồng bộ, laptop, máy quét tài liệu');
ELSE
    UPDATE LOAISANPHAM SET TenLoai = N'Thiết bị tin học & văn phòng', MoTa = N'Máy in laser, máy tính để bàn đồng bộ, laptop, máy quét tài liệu' WHERE MaLoai = 'LSP002';

IF NOT EXISTS (SELECT 1 FROM LOAISANPHAM WHERE MaLoai = 'LSP003')
    INSERT INTO LOAISANPHAM (MaLoai, TenLoai, MoTa)
    VALUES ('LSP003', N'Thiết bị âm thanh & nghe nhìn', N'Màn hình hiển thị chuyên dụng, Smart TV, loa hội nghị, webcam trực tuyến');
ELSE
    UPDATE LOAISANPHAM SET TenLoai = N'Thiết bị âm thanh & nghe nhìn', MoTa = N'Màn hình hiển thị chuyên dụng, Smart TV, loa hội nghị, webcam trực tuyến' WHERE MaLoai = 'LSP003';

IF NOT EXISTS (SELECT 1 FROM LOAISANPHAM WHERE MaLoai = 'LSP004')
    INSERT INTO LOAISANPHAM (MaLoai, TenLoai, MoTa)
    VALUES ('LSP004', N'Vật tư & phụ kiện công nghệ', N'Giấy in photo cao cấp, hộp mực laser chính hãng, bàn phím chuột văn phòng');
ELSE
    UPDATE LOAISANPHAM SET TenLoai = N'Vật tư & phụ kiện công nghệ', MoTa = N'Vật tư & phụ kiện công nghệ' WHERE MaLoai = 'LSP004';
GO

-- ============================================================================
-- 3. NẠP DANH MỤC SẢN PHẨM (SANPHAM)
-- (Không chứa cột SoLuongTon theo đúng Invariant CSDL!)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Sản Phẩm...';

-- SP001
IF NOT EXISTS (SELECT 1 FROM SANPHAM WHERE MaSP = 'SP001')
    INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai)
    VALUES ('SP001', 'NCC001', 'LSP001', N'Nồi chiên không dầu điện tử Lock&Lock 5.2L', N'Cái', 2450000, N'Đang kinh doanh');
ELSE
    UPDATE SANPHAM SET MaNCC = 'NCC001', MaLoai = 'LSP001', TenSP = N'Nồi chiên không dầu điện tử Lock&Lock 5.2L', DonViTinh = N'Cái', DonGiaBan = 2450000, TrangThai = N'Đang kinh doanh' WHERE MaSP = 'SP001';

-- SP002
IF NOT EXISTS (SELECT 1 FROM SANPHAM WHERE MaSP = 'SP002')
    INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai)
    VALUES ('SP002', 'NCC001', 'LSP001', N'Máy lọc không khí Sunhouse SHD-35', N'Chiếc', 3200000, N'Đang kinh doanh');
ELSE
    UPDATE SANPHAM SET MaNCC = 'NCC001', MaLoai = 'LSP001', TenSP = N'Máy lọc không khí Sunhouse SHD-35', DonViTinh = N'Chiếc', DonGiaBan = 3200000, TrangThai = N'Đang kinh doanh' WHERE MaSP = 'SP002';

-- SP003
IF NOT EXISTS (SELECT 1 FROM SANPHAM WHERE MaSP = 'SP003')
    INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai)
    VALUES ('SP003', 'NCC004', 'LSP002', N'Máy in laser đơn năng Canon LBP 2900', N'Máy', 3950000, N'Đang kinh doanh');
ELSE
    UPDATE SANPHAM SET MaNCC = 'NCC004', MaLoai = 'LSP002', TenSP = N'Máy in laser đơn năng Canon LBP 2900', DonViTinh = N'Máy', DonGiaBan = 3950000, TrangThai = N'Đang kinh doanh' WHERE MaSP = 'SP003';

-- SP004
IF NOT EXISTS (SELECT 1 FROM SANPHAM WHERE MaSP = 'SP004')
    INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai)
    VALUES ('SP004', 'NCC003', 'LSP002', N'Máy tính để bàn đồng bộ HP ProTower 280 G9', N'Bộ', 14800000, N'Đang kinh doanh');
ELSE
    UPDATE SANPHAM SET MaNCC = 'NCC003', MaLoai = 'LSP002', TenSP = N'Máy tính để bàn đồng bộ HP ProTower 280 G9', DonViTinh = N'Bộ', DonGiaBan = 14800000, TrangThai = N'Đang kinh doanh' WHERE MaSP = 'SP004';

-- SP005
IF NOT EXISTS (SELECT 1 FROM SANPHAM WHERE MaSP = 'SP005')
    INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai)
    VALUES ('SP005', 'NCC004', 'LSP002', N'Máy in Laser đa chức năng Canon MF244dw', N'Máy', 5650000, N'Đang kinh doanh');
ELSE
    UPDATE SANPHAM SET MaNCC = 'NCC004', MaLoai = 'LSP002', TenSP = N'Máy in Laser đa chức năng Canon MF244dw', DonViTinh = N'Máy', DonGiaBan = 5650000, TrangThai = N'Đang kinh doanh' WHERE MaSP = 'SP005';

-- SP006
IF NOT EXISTS (SELECT 1 FROM SANPHAM WHERE MaSP = 'SP006')
    INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai)
    VALUES ('SP006', 'NCC003', 'LSP002', N'Máy tính xách tay Dell Vostro 3520 Core i5', N'Chiếc', 15490000, N'Đang kinh doanh');
ELSE
    UPDATE SANPHAM SET MaNCC = 'NCC003', MaLoai = 'LSP002', TenSP = N'Máy tính xách tay Dell Vostro 3520 Core i5', DonViTinh = N'Chiếc', DonGiaBan = 15490000, TrangThai = N'Đang kinh doanh' WHERE MaSP = 'SP006';

-- SP007
IF NOT EXISTS (SELECT 1 FROM SANPHAM WHERE MaSP = 'SP007')
    INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai)
    VALUES ('SP007', 'NCC001', 'LSP001', N'Robot hút bụi lau nhà Ecovacs Deebot T9', N'Chiếc', 7890000, N'Đang kinh doanh');
ELSE
    UPDATE SANPHAM SET MaNCC = 'NCC001', MaLoai = 'LSP001', TenSP = N'Robot hút bụi lau nhà Ecovacs Deebot T9', DonViTinh = N'Chiếc', DonGiaBan = 7890000, TrangThai = N'Đang kinh doanh' WHERE MaSP = 'SP007';

-- SP008
IF NOT EXISTS (SELECT 1 FROM SANPHAM WHERE MaSP = 'SP008')
    INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai)
    VALUES ('SP008', 'NCC001', 'LSP001', N'Máy xay sinh tố đa năng Philips ProBlend', N'Bộ', 1350000, N'Đang kinh doanh');
ELSE
    UPDATE SANPHAM SET MaNCC = 'NCC001', MaLoai = 'LSP001', TenSP = N'Máy xay sinh tố đa năng Philips ProBlend', DonViTinh = N'Bộ', DonGiaBan = 1350000, TrangThai = N'Đang kinh doanh' WHERE MaSP = 'SP008';

-- SP009
IF NOT EXISTS (SELECT 1 FROM SANPHAM WHERE MaSP = 'SP009')
    INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai)
    VALUES ('SP009', 'NCC002', 'LSP003', N'Màn hình vi tính Samsung 27 inch IPS 75Hz', N'Chiếc', 3650000, N'Đang kinh doanh');
ELSE
    UPDATE SANPHAM SET MaNCC = 'NCC002', MaLoai = 'LSP003', TenSP = N'Màn hình vi tính Samsung 27 inch IPS 75Hz', DonViTinh = N'Chiếc', DonGiaBan = 3650000, TrangThai = N'Đang kinh doanh' WHERE MaSP = 'SP009';

-- SP010
IF NOT EXISTS (SELECT 1 FROM SANPHAM WHERE MaSP = 'SP010')
    INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai)
    VALUES ('SP010', 'NCC002', 'LSP003', N'Smart Tivi Samsung 4K 55 inch Crystal UHD', N'Chiếc', 11200000, N'Đang kinh doanh');
ELSE
    UPDATE SANPHAM SET MaNCC = 'NCC002', MaLoai = 'LSP003', TenSP = N'Smart Tivi Samsung 4K 55 inch Crystal UHD', DonViTinh = N'Chiếc', DonGiaBan = 11200000, TrangThai = N'Đang kinh doanh' WHERE MaSP = 'SP010';

-- SP011
IF NOT EXISTS (SELECT 1 FROM SANPHAM WHERE MaSP = 'SP011')
    INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai)
    VALUES ('SP011', 'NCC004', 'LSP003', N'Loa hội nghị không dây Jabra Speak 510', N'Chiếc', 3100000, N'Đang kinh doanh');
ELSE
    UPDATE SANPHAM SET MaNCC = 'NCC004', MaLoai = 'LSP003', TenSP = N'Loa hội nghị không dây Jabra Speak 510', DonViTinh = N'Chiếc', DonGiaBan = 3100000, TrangThai = N'Đang kinh doanh' WHERE MaSP = 'SP011';

-- SP012
IF NOT EXISTS (SELECT 1 FROM SANPHAM WHERE MaSP = 'SP012')
    INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai)
    VALUES ('SP012', 'NCC005', 'LSP004', N'Thùng giấy in Double A A4 70gsm (5 ram)', N'Thùng', 385000, N'Đang kinh doanh');
ELSE
    UPDATE SANPHAM SET MaNCC = 'NCC005', MaLoai = 'LSP004', TenSP = N'Thùng giấy in Double A A4 70gsm (5 ram)', DonViTinh = N'Thùng', DonGiaBan = 385000, TrangThai = N'Đang kinh doanh' WHERE MaSP = 'SP012';

-- SP013
IF NOT EXISTS (SELECT 1 FROM SANPHAM WHERE MaSP = 'SP013')
    INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai)
    VALUES ('SP013', 'NCC004', 'LSP004', N'Hộp mực in Laser HP 85A chính hãng', N'Hộp', 1250000, N'Đang kinh doanh');
ELSE
    UPDATE SANPHAM SET MaNCC = 'NCC004', MaLoai = 'LSP004', TenSP = N'Hộp mực in Laser HP 85A chính hãng', DonViTinh = N'Hộp', DonGiaBan = 1250000, TrangThai = N'Đang kinh doanh' WHERE MaSP = 'SP013';

-- SP014
IF NOT EXISTS (SELECT 1 FROM SANPHAM WHERE MaSP = 'SP014')
    INSERT INTO SANPHAM (MaSP, MaNCC, MaLoai, TenSP, DonViTinh, DonGiaBan, TrangThai)
    VALUES ('SP014', 'NCC003', 'LSP004', N'Bộ bàn phím chuột không dây Logitech MK295', N'Bộ', 620000, N'Đang kinh doanh');
ELSE
    UPDATE SANPHAM SET MaNCC = 'NCC003', MaLoai = 'LSP004', TenSP = N'Bộ bàn phím chuột không dây Logitech MK295', DonViTinh = N'Bộ', DonGiaBan = 620000, TrangThai = N'Đang kinh doanh' WHERE MaSP = 'SP014';
GO

-- ============================================================================
-- 4. NẠP DANH MỤC KHÁCH HÀNG (KHACHHANG)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Khách Hàng...';

-- KH001
IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE MaKH = 'KH001')
    INSERT INTO KHACHHANG (MaKH, TenKH, DiaChi, SoDienThoai, Email)
    VALUES ('KH001', N'Công Ty Cổ Phần Công Nghệ & Truyền Thông Minh Khang', N'Tầng 5 Tòa nhà Geleximco, 36 Hoàng Cầu, Đống Đa, Hà Nội', '02435123456', 'contact@minhkhang.com.vn');
ELSE
    UPDATE KHACHHANG SET TenKH = N'Công Ty Cổ Phần Công Nghệ & Truyền Thông Minh Khang', DiaChi = N'Tầng 5 Tòa nhà Geleximco, 36 Hoàng Cầu, Đống Đa, Hà Nội', SoDienThoai = '02435123456', Email = 'contact@minhkhang.com.vn' WHERE MaKH = 'KH001';

-- KH002
IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE MaKH = 'KH002')
    INSERT INTO KHACHHANG (MaKH, TenKH, DiaChi, SoDienThoai, Email)
    VALUES ('KH002', N'Tập Đoàn Thương Mại & Xuất Nhập Khẩu Á Châu', N'45 Nguyễn Văn Linh, Phường Nam Dương, Hải Châu, Đà Nẵng', '02363889988', 'info@achaugroup.vn');
ELSE
    UPDATE KHACHHANG SET TenKH = N'Tập Đoàn Thương Mại & Xuất Nhập Khẩu Á Châu', DiaChi = N'45 Nguyễn Văn Linh, Phường Nam Dương, Hải Châu, Đà Nẵng', SoDienThoai = '02363889988', Email = 'info@achaugroup.vn' WHERE MaKH = 'KH002';

-- KH003
IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE MaKH = 'KH003')
    INSERT INTO KHACHHANG (MaKH, TenKH, DiaChi, SoDienThoai, Email)
    VALUES ('KH003', N'Công Ty Cổ Phần Bán Lẻ & Chuỗi Cửa Hàng Toàn Cầu', N'78 Lê Văn Sỹ, Phường 11, Quận Phú Nhuận, TP.HCM', '02838445566', 'procurement@toancau.vn');
ELSE
    UPDATE KHACHHANG SET TenKH = N'Công Ty Cổ Phần Bán Lẻ & Chuỗi Cửa Hàng Toàn Cầu', DiaChi = N'78 Lê Văn Sỹ, Phường 11, Quận Phú Nhuận, TP.HCM', SoDienThoai = '02838445566', Email = 'procurement@toancau.vn' WHERE MaKH = 'KH003';

-- KH004
IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE MaKH = 'KH004')
    INSERT INTO KHACHHANG (MaKH, TenKH, DiaChi, SoDienThoai, Email)
    VALUES ('KH004', N'Trường Đại Học Kinh Tế Quốc Dân', N'207 Giải Phóng, Phường Đồng Tâm, Hai Bà Trưng, Hà Nội', '02436280280', 'phongketoan@neu.edu.vn');
ELSE
    UPDATE KHACHHANG SET TenKH = N'Trường Đại Học Kinh Tế Quốc Dân', DiaChi = N'207 Giải Phóng, Phường Đồng Tâm, Hai Bà Trưng, Hà Nội', SoDienThoai = '02436280280', Email = 'phongketoan@neu.edu.vn' WHERE MaKH = 'KH004';

-- KH005
IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE MaKH = 'KH005')
    INSERT INTO KHACHHANG (MaKH, TenKH, DiaChi, SoDienThoai, Email)
    VALUES ('KH005', N'Bệnh Viện Đa Khoa Quốc Tế Vinmec', N'458 Minh Khai, Phường Vĩnh Tuy, Hai Bà Trưng, Hà Nội', '02439743556', 'info@vinmec.com');
ELSE
    UPDATE KHACHHANG SET TenKH = N'Bệnh Viện Đa Khoa Quốc Tế Vinmec', DiaChi = N'458 Minh Khai, Phường Vĩnh Tuy, Hai Bà Trưng, Hà Nội', SoDienThoai = '02439743556', Email = 'info@vinmec.com' WHERE MaKH = 'KH005';

-- KH006
IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE MaKH = 'KH006')
    INSERT INTO KHACHHANG (MaKH, TenKH, DiaChi, SoDienThoai, Email)
    VALUES ('KH006', N'Công Ty TNHH Giải Pháp Công Nghệ Phương Nam', N'Tòa nhà Pax Sky, 123 Nguyễn Đình Chiểu, Phường 6, Quận 3, TP.HCM', '02873099999', 'phuongnam.solution@gmail.com');
ELSE
    UPDATE KHACHHANG SET TenKH = N'Công Ty TNHH Giải Pháp Công Nghệ Phương Nam', DiaChi = N'Tòa nhà Pax Sky, 123 Nguyễn Đình Chiểu, Phường 6, Quận 3, TP.HCM', SoDienThoai = '02873099999', Email = 'phuongnam.solution@gmail.com' WHERE MaKH = 'KH006';

-- KH007
IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE MaKH = 'KH007')
    INSERT INTO KHACHHANG (MaKH, TenKH, DiaChi, SoDienThoai, Email)
    VALUES ('KH007', N'Công Ty Cổ Phần Tập Đoàn Xây Dựng Hòa Bình', N'Tòa nhà Pax Sky, 123 Võ Văn Tần, Phường 6, Quận 3, TP.HCM', '02839325030', 'ketoan@hbcr.vn');
ELSE
    UPDATE KHACHHANG SET TenKH = N'Công Ty Cổ Phần Tập Đoàn Xây Dựng Hòa Bình', DiaChi = N'Tòa nhà Pax Sky, 123 Võ Văn Tần, Phường 6, Quận 3, TP.HCM', SoDienThoai = '02839325030', Email = 'ketoan@hbcr.vn' WHERE MaKH = 'KH007';

-- KH008
IF NOT EXISTS (SELECT 1 FROM KHACHHANG WHERE MaKH = 'KH008')
    INSERT INTO KHACHHANG (MaKH, TenKH, DiaChi, SoDienThoai, Email)
    VALUES ('KH008', N'Chuỗi Hệ Thống Bán Lẻ Điện Máy Xanh Miền Bắc', N'Số 128 Trần Phú, Phường Mộ Lao, Quận Hà Đông, Hà Nội', '02433556677', 'dmx.mienbac@thegioididong.com');
ELSE
    UPDATE KHACHHANG SET TenKH = N'Chuỗi Hệ Thống Bán Lẻ Điện Máy Xanh Miền Bắc', DiaChi = N'Số 128 Trần Phú, Phường Mộ Lao, Quận Hà Đông, Hà Nội', SoDienThoai = '02433556677', Email = 'dmx.mienbac@thegioididong.com' WHERE MaKH = 'KH008';
GO

-- ============================================================================
-- 5. NẠP DANH MỤC KHO HÀNG (KHO)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Kho Hàng...';

IF NOT EXISTS (SELECT 1 FROM KHO WHERE MaKho = 'KHO01')
    INSERT INTO KHO (MaKho, TenKho, DiaChi, TrangThai)
    VALUES ('KHO01', N'Kho Tổng Miền Bắc', N'Lô CN-05 KCN Nam Từ Liêm, Phường Tây Mỗ, Quận Nam Từ Liêm, Hà Nội', N'Hoạt động');
ELSE
    UPDATE KHO SET TenKho = N'Kho Tổng Miền Bắc', DiaChi = N'Lô CN-05 KCN Nam Từ Liêm, Phường Tây Mỗ, Quận Nam Từ Liêm, Hà Nội', TrangThai = N'Hoạt động' WHERE MaKho = 'KHO01';

IF NOT EXISTS (SELECT 1 FROM KHO WHERE MaKho = 'KHO02')
    INSERT INTO KHO (MaKho, TenKho, DiaChi, TrangThai)
    VALUES ('KHO02', N'Kho Phân Phối Miền Nam', N'Lô 12 Đường số 2, KCN Tân Bình, Phường Tây Thạnh, Quận Tân Phú, TP.HCM', N'Hoạt động');
ELSE
    UPDATE KHO SET TenKho = N'Kho Phân Phối Miền Nam', DiaChi = N'Lô 12 Đường số 2, KCN Tân Bình, Phường Tây Thạnh, Quận Tân Phú, TP.HCM', TrangThai = N'Hoạt động' WHERE MaKho = 'KHO02';

IF NOT EXISTS (SELECT 1 FROM KHO WHERE MaKho = 'KHO03')
    INSERT INTO KHO (MaKho, TenKho, DiaChi, TrangThai)
    VALUES ('KHO03', N'Kho Trung Chuyển Miền Trung', N'Số 18 Đường số 3, KCN Hòa Cầm, Phường Hòa Thọ Tây, Quận Cẩm Lệ, Đà Nẵng', N'Hoạt động');
ELSE
    UPDATE KHO SET TenKho = N'Kho Trung Chuyển Miền Trung', DiaChi = N'Số 18 Đường số 3, KCN Hòa Cầm, Phường Hòa Thọ Tây, Quận Cẩm Lệ, Đà Nẵng', TrangThai = N'Hoạt động' WHERE MaKho = 'KHO03';
GO

-- ============================================================================
-- 6. NẠP SỐ LƯỢNG TỒN KHO BAN ĐẦU (TONKHO)
-- (Tồn kho quản lý độc lập tại bảng TONKHO theo đúng Invariant kiến trúc!)
-- ============================================================================
PRINT N'==> Đang nạp dữ liệu Tồn Kho ban đầu cho 14 sản phẩm tại các kho...';

-- Bảng tạm chứa cấu hình tồn kho ban đầu
DECLARE @TonKhoInit TABLE (
    MaKho char(10),
    MaSP char(10),
    SoLuong int
);

-- Phân bổ tồn kho tại KHO01 (Kho Tổng Hà Nội)
INSERT INTO @TonKhoInit VALUES 
('KHO01', 'SP001', 85),
('KHO01', 'SP002', 60),
('KHO01', 'SP003', 45),
('KHO01', 'SP004', 35),
('KHO01', 'SP005', 28),
('KHO01', 'SP006', 40),
('KHO01', 'SP007', 30),
('KHO01', 'SP008', 75),
('KHO01', 'SP009', 55),
('KHO01', 'SP010', 25),
('KHO01', 'SP011', 50),
('KHO01', 'SP012', 350),
('KHO01', 'SP013', 120),
('KHO01', 'SP014', 180);

-- Phân bổ tồn kho tại KHO02 (Kho TP.HCM)
INSERT INTO @TonKhoInit VALUES 
('KHO02', 'SP001', 65),
('KHO02', 'SP002', 45),
('KHO02', 'SP003', 30),
('KHO02', 'SP004', 25),
('KHO02', 'SP005', 20),
('KHO02', 'SP006', 30),
('KHO02', 'SP007', 22),
('KHO02', 'SP008', 50),
('KHO02', 'SP009', 40),
('KHO02', 'SP010', 18),
('KHO02', 'SP011', 35),
('KHO02', 'SP012', 280),
('KHO02', 'SP013', 95),
('KHO02', 'SP014', 140);

-- Phân bổ tồn kho tại KHO03 (Kho Đà Nẵng)
INSERT INTO @TonKhoInit VALUES 
('KHO03', 'SP001', 30),
('KHO03', 'SP002', 25),
('KHO03', 'SP003', 20),
('KHO03', 'SP004', 15),
('KHO03', 'SP005', 12),
('KHO03', 'SP006', 18),
('KHO03', 'SP007', 10),
('KHO03', 'SP008', 35),
('KHO03', 'SP009', 25),
('KHO03', 'SP010', 10),
('KHO03', 'SP011', 20),
('KHO03', 'SP012', 150),
('KHO03', 'SP013', 60),
('KHO03', 'SP014', 80);

-- Cập nhật vào bảng TONKHO
MERGE INTO TONKHO AS target
USING @TonKhoInit AS source
ON (target.MaKho = source.MaKho AND target.MaSP = source.MaSP)
WHEN MATCHED THEN
    UPDATE SET target.SoLuongTon = source.SoLuong, target.NgayCapNhat = GETDATE()
WHEN NOT MATCHED THEN
    INSERT (MaKho, MaSP, SoLuongTon, NgayCapNhat)
    VALUES (source.MaKho, source.MaSP, source.SoLuong, GETDATE());

GO

-- ============================================================================
-- 7. NẠP DANH MỤC NHÂN VIÊN (NHANVIEN)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Nhân Viên...';

-- NV001
IF NOT EXISTS (SELECT 1 FROM NHANVIEN WHERE MaNV = 'NV001')
    INSERT INTO NHANVIEN (MaNV, HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, ChucVu, TrangThai)
    VALUES ('NV001', N'Nguyễn Văn Quản Trị', '1988-05-15', N'Nam', '0912345678', N'Số 15 Phố Tràng Tiền, Hoàn Kiếm, Hà Nội', N'Trưởng Phòng CNTT & Quản Trị', N'Đang làm việc');
ELSE
    UPDATE NHANVIEN SET HoTen = N'Nguyễn Văn Trị', NgaySinh = '1988-05-15', GioiTinh = N'Nam', SoDienThoai = '0912345678', DiaChi = N'Số 15 Phố Tràng Tiền, Hoàn Kiếm, Hà Nội', ChucVu = N'Trưởng Phòng CNTT & Quản Trị', TrangThai = N'Đang làm việc' WHERE MaNV = 'NV001';

-- NV002
IF NOT EXISTS (SELECT 1 FROM NHANVIEN WHERE MaNV = 'NV002')
    INSERT INTO NHANVIEN (MaNV, HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, ChucVu, TrangThai)
    VALUES ('NV002', N'Trần Thị Hàng', '1993-08-20', N'Nữ', '0923456789', N'Số 88 Cầu Giấy, Quan Hoa, Cầu Giấy, Hà Nội', N'Nhân viên Kinh doanh & Bán hàng', N'Đang làm việc');
ELSE
    UPDATE NHANVIEN SET HoTen = N'Trần Thị Bán Hàng', NgaySinh = '1993-08-20', GioiTinh = N'Nữ', SoDienThoai = '0923456789', DiaChi = N'Số 88 Cầu Giấy, Quan Hoa, Cầu Giấy, Hà Nội', ChucVu = N'Nhân viên Kinh doanh & Bán hàng', TrangThai = N'Đang làm việc' WHERE MaNV = 'NV002';

-- NV003
IF NOT EXISTS (SELECT 1 FROM NHANVIEN WHERE MaNV = 'NV003')
    INSERT INTO NHANVIEN (MaNV, HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, ChucVu, TrangThai)
    VALUES ('NV003', N'Lê Văn Kho', '1990-11-10', N'Nam', '0934567890', N'Lô B2 KCN Đài Tư, Long Biên, Hà Nội', N'Thủ kho Quản lý kho vận', N'Đang làm việc');
ELSE
    UPDATE NHANVIEN SET HoTen = N'Lê Văn Kho', NgaySinh = '1990-11-10', GioiTinh = N'Nam', SoDienThoai = '0934567890', DiaChi = N'Lô B2 KCN Đài Tư, Long Biên, Hà Nội', ChucVu = N'Thủ kho Quản lý kho vận', TrangThai = N'Đang làm việc' WHERE MaNV = 'NV003';

-- NV004
IF NOT EXISTS (SELECT 1 FROM NHANVIEN WHERE MaNV = 'NV004')
    INSERT INTO NHANVIEN (MaNV, HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, ChucVu, TrangThai)
    VALUES ('NV004', N'Phạm Thị Toán', '1992-03-25', N'Nữ', '0945678901', N'Số 45 Lê Duẩn, Bến Nghé, Quận 1, TP.HCM', N'Kế toán viên Bán hàng & Công nợ', N'Đang làm việc');
ELSE
    UPDATE NHANVIEN SET HoTen = N'Phạm Thị Kế Toán', NgaySinh = '1992-03-25', GioiTinh = N'Nữ', SoDienThoai = '0945678901', DiaChi = N'Số 45 Lê Duẩn, Bến Nghé, Quận 1, TP.HCM', ChucVu = N'Kế toán viên Bán hàng & Công nợ', TrangThai = N'Đang làm việc' WHERE MaNV = 'NV004';

-- NV005
IF NOT EXISTS (SELECT 1 FROM NHANVIEN WHERE MaNV = 'NV005')
    INSERT INTO NHANVIEN (MaNV, HoTen, NgaySinh, GioiTinh, SoDienThoai, DiaChi, ChucVu, TrangThai)
    VALUES ('NV005', N'Hoàng Minh Đức', '1995-12-05', N'Nam', '0956789012', N'Số 12 Nguyễn Văn Linh, Hải Châu, Đà Nẵng', N'Nhân viên Kinh doanh Miền Trung', N'Đang làm việc');
ELSE
    UPDATE NHANVIEN SET HoTen = N'Hoàng Minh Đức', NgaySinh = '1995-12-05', GioiTinh = N'Nam', SoDienThoai = '0956789012', DiaChi = N'Số 12 Nguyễn Văn Linh, Hải Châu, Đà Nẵng', ChucVu = N'Nhân viên Kinh doanh Miền Trung', TrangThai = N'Đang làm việc' WHERE MaNV = 'NV005';
GO

-- ============================================================================
-- 8. NẠP DANH MỤC TÀI KHOẢN ĐĂNG NHẬP (TAIKHOAN)
-- Mật khẩu mặc định toàn hệ thống: 123456
-- Chuẩn mã hóa an toàn: PBKDF2-HMAC-SHA256 (100.000 rounds)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Tài Khoản Đăng Nhập...';

DECLARE @MatKhau123456 varchar(100) = 'PBKDF2-SHA256:100000:6KuuY9vmEXDWYp/JROHRkQ==:VFs4BMNzaZ4ll8q94xGYwcO8Am+oA72GIX3V4cGgaXY=';

-- TK001 (admin)
IF NOT EXISTS (SELECT 1 FROM TAIKHOAN WHERE MaTK = 'TK001' OR TenDangNhap = 'admin')
    INSERT INTO TAIKHOAN (MaTK, MaNV, TenDangNhap, MatKhau, VaiTro, TrangThai)
    VALUES ('TK001', 'NV001', 'admin', @MatKhau123456, N'Quản trị viên', N'Hoạt động');
ELSE
    UPDATE TAIKHOAN SET MaNV = 'NV001', TenDangNhap = 'admin', MatKhau = @MatKhau123456, VaiTro = N'Quản trị viên', TrangThai = N'Hoạt động' WHERE MaTK = 'TK001' OR TenDangNhap = 'admin';

-- TK002 (banhang)
IF NOT EXISTS (SELECT 1 FROM TAIKHOAN WHERE MaTK = 'TK002' OR TenDangNhap = 'banhang')
    INSERT INTO TAIKHOAN (MaTK, MaNV, TenDangNhap, MatKhau, VaiTro, TrangThai)
    VALUES ('TK002', 'NV002', 'banhang', @MatKhau123456, N'Nhân viên bán hàng', N'Hoạt động');
ELSE
    UPDATE TAIKHOAN SET MaNV = 'NV002', TenDangNhap = 'banhang', MatKhau = @MatKhau123456, VaiTro = N'Nhân viên bán hàng', TrangThai = N'Hoạt động' WHERE MaTK = 'TK002' OR TenDangNhap = 'banhang';

-- TK003 (kho)
IF NOT EXISTS (SELECT 1 FROM TAIKHOAN WHERE MaTK = 'TK003' OR TenDangNhap = 'kho')
    INSERT INTO TAIKHOAN (MaTK, MaNV, TenDangNhap, MatKhau, VaiTro, TrangThai)
    VALUES ('TK003', 'NV003', 'kho', @MatKhau123456, N'Nhân viên kho', N'Hoạt động');
ELSE
    UPDATE TAIKHOAN SET MaNV = 'NV003', TenDangNhap = 'kho', MatKhau = @MatKhau123456, VaiTro = N'Nhân viên kho', TrangThai = N'Hoạt động' WHERE MaTK = 'TK003' OR TenDangNhap = 'kho';

-- TK004 (ketoan)
IF NOT EXISTS (SELECT 1 FROM TAIKHOAN WHERE MaTK = 'TK004' OR TenDangNhap = 'ketoan')
    INSERT INTO TAIKHOAN (MaTK, MaNV, TenDangNhap, MatKhau, VaiTro, TrangThai)
    VALUES ('TK004', 'NV004', 'ketoan', @MatKhau123456, N'Nhân viên kế toán', N'Hoạt động');
ELSE
    UPDATE TAIKHOAN SET MaNV = 'NV004', TenDangNhap = 'ketoan', MatKhau = @MatKhau123456, VaiTro = N'Nhân viên kế toán', TrangThai = N'Hoạt động' WHERE MaTK = 'TK004' OR TenDangNhap = 'ketoan';

-- TK005 (banhang2)
IF NOT EXISTS (SELECT 1 FROM TAIKHOAN WHERE MaTK = 'TK005' OR TenDangNhap = 'banhang2')
    INSERT INTO TAIKHOAN (MaTK, MaNV, TenDangNhap, MatKhau, VaiTro, TrangThai)
    VALUES ('TK005', 'NV005', 'banhang2', @MatKhau123456, N'Nhân viên bán hàng', N'Hoạt động');
ELSE
    UPDATE TAIKHOAN SET MaNV = 'NV005', TenDangNhap = 'banhang2', MatKhau = @MatKhau123456, VaiTro = N'Nhân viên bán hàng', TrangThai = N'Hoạt động' WHERE MaTK = 'TK005' OR TenDangNhap = 'banhang2';
GO

-- ============================================================================
-- 9. NẠP DANH MỤC ĐƠN ĐẶT HÀNG & CHI TIẾT (DONDATHANG, CHITIETDONDATHANG)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Đơn Đặt Hàng...';

-- DDH0000001: Đã lập HĐ & Giao hàng (Cty Minh Khang)
IF NOT EXISTS (SELECT 1 FROM DONDATHANG WHERE MaDDH = 'DDH0000001')
    INSERT INTO DONDATHANG (MaDDH, MaNV, MaKH, NgayDat, NgayGiaoDuKien, TongTien, TrangThai, GhiChu)
    VALUES ('DDH0000001', 'NV002', 'KH001', DATEADD(DAY, -5, GETDATE()), CAST(DATEADD(DAY, -3, GETDATE()) AS date), 89300000, N'Đã lập hóa đơn', N'Hợp đồng cung cấp thiết bị tin học đợt 1');
ELSE
    UPDATE DONDATHANG SET MaNV = 'NV002', MaKH = 'KH001', NgayDat = DATEADD(DAY, -5, GETDATE()), NgayGiaoDuKien = CAST(DATEADD(DAY, -3, GETDATE()) AS date), TongTien = 89300000, TrangThai = N'Đã lập hóa đơn', GhiChu = N'Hợp đồng cung cấp thiết bị tin học đợt 1' WHERE MaDDH = 'DDH0000001';

DELETE FROM CHITIETDONDATHANG WHERE MaDDH = 'DDH0000001';
INSERT INTO CHITIETDONDATHANG (MaDDH, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('DDH0000001', 'SP006', 5, 15490000, 0, 77450000),
('DDH0000001', 'SP003', 3, 3950000, 0, 11850000);

-- DDH0000002: Đã lập HĐ & ĐANG CHỜ XUẤT KHO (Đại học Kinh Tế Quốc Dân)
IF NOT EXISTS (SELECT 1 FROM DONDATHANG WHERE MaDDH = 'DDH0000002')
    INSERT INTO DONDATHANG (MaDDH, MaNV, MaKH, NgayDat, NgayGiaoDuKien, TongTien, TrangThai, GhiChu)
    VALUES ('DDH0000002', 'NV002', 'KH004', GETDATE(), CAST(DATEADD(DAY, 2, GETDATE()) AS date), 147600000, N'Đã lập hóa đơn', N'Dự án trang bị phòng thực hành tin học NEU');
ELSE
    UPDATE DONDATHANG SET MaNV = 'NV002', MaKH = 'KH004', NgayDat = GETDATE(), NgayGiaoDuKien = CAST(DATEADD(DAY, 2, GETDATE()) AS date), TongTien = 147600000, TrangThai = N'Đã lập hóa đơn', GhiChu = N'Dự án trang bị phòng thực hành tin học NEU' WHERE MaDDH = 'DDH0000002';

DELETE FROM CHITIETDONDATHANG WHERE MaDDH = 'DDH0000002';
INSERT INTO CHITIETDONDATHANG (MaDDH, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('DDH0000002', 'SP004', 8, 14800000, 0, 118400000),
('DDH0000002', 'SP009', 8, 3650000, 0, 29200000);

-- DDH0000003: Đã lập HĐ & ĐÃ XUẤT KHO 1 PHẦN (Tập Đoàn Xây Dựng Hòa Bình)
IF NOT EXISTS (SELECT 1 FROM DONDATHANG WHERE MaDDH = 'DDH0000003')
    INSERT INTO DONDATHANG (MaDDH, MaNV, MaKH, NgayDat, NgayGiaoDuKien, TongTien, TrangThai, GhiChu)
    VALUES ('DDH0000003', 'NV002', 'KH007', DATEADD(DAY, -2, GETDATE()), CAST(DATEADD(DAY, 1, GETDATE()) AS date), 51000000, N'Đã lập hóa đơn', N'Cung cấp TV và phụ kiện phòng họp Ban Giám Đốc');
ELSE
    UPDATE DONDATHANG SET MaNV = 'NV002', MaKH = 'KH007', NgayDat = DATEADD(DAY, -2, GETDATE()), NgayGiaoDuKien = CAST(DATEADD(DAY, 1, GETDATE()) AS date), TongTien = 51000000, TrangThai = N'Đã lập hóa đơn', GhiChu = N'Cung cấp TV và phụ kiện phòng họp Ban Giám Đốc' WHERE MaDDH = 'DDH0000003';

DELETE FROM CHITIETDONDATHANG WHERE MaDDH = 'DDH0000003';
INSERT INTO CHITIETDONDATHANG (MaDDH, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('DDH0000003', 'SP010', 4, 11200000, 0, 44800000),
('DDH0000003', 'SP014', 10, 620000, 0, 6200000);

-- DDH0000004: ĐƠN MỚI CHƯA LẬP HÓA ĐƠN (Bệnh viện Vinmec - Dùng để demo lập hóa đơn)
IF NOT EXISTS (SELECT 1 FROM DONDATHANG WHERE MaDDH = 'DDH0000004')
    INSERT INTO DONDATHANG (MaDDH, MaNV, MaKH, NgayDat, NgayGiaoDuKien, TongTien, TrangThai, GhiChu)
    VALUES ('DDH0000004', 'NV002', 'KH005', DATEADD(DAY, -1, GETDATE()), CAST(DATEADD(DAY, 3, GETDATE()) AS date), 26600000, N'Đã duyệt', N'Trang bị máy lọc không khí và gia dụng cho khu điều trị');
ELSE
    UPDATE DONDATHANG SET MaNV = 'NV002', MaKH = 'KH005', NgayDat = DATEADD(DAY, -1, GETDATE()), NgayGiaoDuKien = CAST(DATEADD(DAY, 3, GETDATE()) AS date), TongTien = 26600000, TrangThai = N'Đã duyệt', GhiChu = N'Trang bị máy lọc không khí và gia dụng cho khu điều trị' WHERE MaDDH = 'DDH0000004';

DELETE FROM CHITIETDONDATHANG WHERE MaDDH = 'DDH0000004';
INSERT INTO CHITIETDONDATHANG (MaDDH, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('DDH0000004', 'SP002', 6, 3200000, 0, 19200000),
('DDH0000004', 'SP001', 4, 1850000, 0, 7400000);
GO

-- ============================================================================
-- 10. NẠP DANH MỤC HÓA ĐƠN BÁN HÀNG & CHI TIẾT (HOADONBAN, CHITIETHOADONBAN)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Hóa Đơn Bán Hàng...';

-- HDB0000001 (Theo DDH0000001 - Cty Minh Khang - Đã thanh toán 100%)
IF NOT EXISTS (SELECT 1 FROM HOADONBAN WHERE MaHDB = 'HDB0000001')
    INSERT INTO HOADONBAN (MaHDB, MaNV, MaDDH, MaKH, NgayLap, TongTien, GhiChu, TrangThai)
    VALUES ('HDB0000001', 'NV002', 'DDH0000001', 'KH001', DATEADD(DAY, -4, GETDATE()), 89300000, N'Hóa đơn GTGT điện tử cung cấp thiết bị văn phòng', N'Đã thanh toán');
ELSE
    UPDATE HOADONBAN SET MaNV = 'NV002', MaDDH = 'DDH0000001', MaKH = 'KH001', NgayLap = DATEADD(DAY, -4, GETDATE()), TongTien = 89300000, GhiChu = N'Hóa đơn GTGT điện tử cung cấp thiết bị văn phòng', TrangThai = N'Đã thanh toán' WHERE MaHDB = 'HDB0000001';

DELETE FROM CHITIETHOADONBAN WHERE MaHDB = 'HDB0000001';
INSERT INTO CHITIETHOADONBAN (MaHDB, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('HDB0000001', 'SP006', 5, 15490000, 0, 77450000),
('HDB0000001', 'SP003', 3, 3950000, 0, 11850000);

-- HDB0000002 (Theo DDH0000002 - ĐH Kinh Tế Quốc Dân - CHỜ XUẤT KHO)
IF NOT EXISTS (SELECT 1 FROM HOADONBAN WHERE MaHDB = 'HDB0000002')
    INSERT INTO HOADONBAN (MaHDB, MaNV, MaDDH, MaKH, NgayLap, TongTien, GhiChu, TrangThai)
    VALUES ('HDB0000002', 'NV002', 'DDH0000002', 'KH004', GETDATE(), 147600000, N'Hóa đơn bán hàng theo dự án phòng lab NEU', N'Chưa thanh toán');
ELSE
    UPDATE HOADONBAN SET MaNV = 'NV002', MaDDH = 'DDH0000002', MaKH = 'KH004', NgayLap = GETDATE(), TongTien = 147600000, GhiChu = N'Hóa đơn bán hàng theo dự án phòng lab NEU', TrangThai = N'Chưa thanh toán' WHERE MaHDB = 'HDB0000002';

DELETE FROM CHITIETHOADONBAN WHERE MaHDB = 'HDB0000002';
INSERT INTO CHITIETHOADONBAN (MaHDB, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('HDB0000002', 'SP004', 8, 14800000, 0, 118400000),
('HDB0000002', 'SP009', 8, 3650000, 0, 29200000);

-- HDB0000003 (Theo DDH0000003 - Tập Đoàn Hòa Bình - ĐÃ XUẤT 1 PHẦN, NỢ 50%)
IF NOT EXISTS (SELECT 1 FROM HOADONBAN WHERE MaHDB = 'HDB0000003')
    INSERT INTO HOADONBAN (MaHDB, MaNV, MaDDH, MaKH, NgayLap, TongTien, GhiChu, TrangThai)
    VALUES ('HDB0000003', 'NV002', 'DDH0000003', 'KH007', DATEADD(DAY, -2, GETDATE()), 51000000, N'Hóa đơn cung cấp TV & bàn phím chuột', N'Thanh toán một phần');
ELSE
    UPDATE HOADONBAN SET MaNV = 'NV002', MaDDH = 'DDH0000003', MaKH = 'KH007', NgayLap = DATEADD(DAY, -2, GETDATE()), TongTien = 51000000, GhiChu = N'Hóa đơn cung cấp TV & bàn phím chuột', TrangThai = N'Thanh toán một phần' WHERE MaHDB = 'HDB0000003';

DELETE FROM CHITIETHOADONBAN WHERE MaHDB = 'HDB0000003';
INSERT INTO CHITIETHOADONBAN (MaHDB, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('HDB0000003', 'SP010', 4, 11200000, 0, 44800000),
('HDB0000003', 'SP014', 10, 620000, 0, 6200000);
GO

-- ============================================================================
-- 11. NẠP DANH MỤC PHIẾU XUẤT KHO & CHI TIẾT (PHIEUXUATKHO, CHITIETPHIEUXUATKHO)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Phiếu Xuất Kho...';

-- PXK0000001: Xuất đủ cho HDB0000001 tại KHO01 (Kho Tổng Miền Bắc)
IF NOT EXISTS (SELECT 1 FROM PHIEUXUATKHO WHERE MaPXK = 'PXK0000001')
    INSERT INTO PHIEUXUATKHO (MaPXK, MaNV, MaHDB, MaKho, NgayXuat, LyDoXuat, TrangThai)
    VALUES ('PXK0000001', 'NV003', 'HDB0000001', 'KHO01', DATEADD(DAY, -4, GETDATE()), N'Xuất kho giao hàng theo hóa đơn bán HDB0000001', N'Đã xuất');
ELSE
    UPDATE PHIEUXUATKHO SET MaNV = 'NV003', MaHDB = 'HDB0000001', MaKho = 'KHO01', NgayXuat = DATEADD(DAY, -4, GETDATE()), LyDoXuat = N'Xuất kho giao hàng theo hóa đơn bán HDB0000001', TrangThai = N'Đã xuất' WHERE MaPXK = 'PXK0000001';

DELETE FROM CHITIETPHIEUXUATKHO WHERE MaPXK = 'PXK0000001';
INSERT INTO CHITIETPHIEUXUATKHO (MaPXK, MaSP, SoLuongXuat) VALUES
('PXK0000001', 'SP006', 5),
('PXK0000001', 'SP003', 3);

-- PXK0000002: Xuất đợt 1 cho HDB0000003 tại KHO02 (Kho Miền Nam: Xuất 2 TV, 10 chuột phím)
IF NOT EXISTS (SELECT 1 FROM PHIEUXUATKHO WHERE MaPXK = 'PXK0000002')
    INSERT INTO PHIEUXUATKHO (MaPXK, MaNV, MaHDB, MaKho, NgayXuat, LyDoXuat, TrangThai)
    VALUES ('PXK0000002', 'NV003', 'HDB0000003', 'KHO02', DATEADD(DAY, -1, GETDATE()), N'Xuất kho giao hàng đợt 1 theo hóa đơn bán HDB0000003', N'Đã xuất');
ELSE
    UPDATE PHIEUXUATKHO SET MaNV = 'NV003', MaHDB = 'HDB0000003', MaKho = 'KHO02', NgayXuat = DATEADD(DAY, -1, GETDATE()), LyDoXuat = N'Xuất kho giao hàng đợt 1 theo hóa đơn bán HDB0000003', TrangThai = N'Đã xuất' WHERE MaPXK = 'PXK0000002';

DELETE FROM CHITIETPHIEUXUATKHO WHERE MaPXK = 'PXK0000002';
INSERT INTO CHITIETPHIEUXUATKHO (MaPXK, MaSP, SoLuongXuat) VALUES
('PXK0000002', 'SP010', 2),
('PXK0000002', 'SP014', 10);
GO

-- ============================================================================
-- 12. NẠP DANH MỤC PHIẾU THU BÁN HÀNG (PHIEUTHU)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Phiếu Thu...';

-- PT0000001: Thu đủ 100% cho HDB0000001 (89.300.000 VNĐ)
IF NOT EXISTS (SELECT 1 FROM PHIEUTHU WHERE MaPT = 'PT0000001')
    INSERT INTO PHIEUTHU (MaPT, MaNV, MaHDB, NgayThu, NguoiNop, LyDoThu, SoTien, HinhThuc, GhiChu)
    VALUES ('PT0000001', 'NV004', 'HDB0000001', DATEADD(DAY, -3, GETDATE()), N'Nguyễn Minh Khang (Giám đốc)', N'Thanh toán toàn bộ tiền hàng theo hóa đơn HDB0000001', 89300000, N'Chuyển khoản', N'Đã đối soát ủy nhiệm chi VCB thành công');
ELSE
    UPDATE PHIEUTHU SET MaNV = 'NV004', MaHDB = 'HDB0000001', NgayThu = DATEADD(DAY, -3, GETDATE()), NguoiNop = N'Nguyễn Minh Khang (Giám đốc)', LyDoThu = N'Thanh toán toàn bộ tiền hàng theo hóa đơn HDB0000001', SoTien = 89300000, HinhThuc = N'Chuyển khoản', GhiChu = N'Đã đối soát ủy nhiệm chi VCB thành công' WHERE MaPT = 'PT0000001';

-- PT0000002: Tạm ứng 50% cho HDB0000003 (25.500.000 VNĐ)
IF NOT EXISTS (SELECT 1 FROM PHIEUTHU WHERE MaPT = 'PT0000002')
    INSERT INTO PHIEUTHU (MaPT, MaNV, MaHDB, NgayThu, NguoiNop, LyDoThu, SoTien, HinhThuc, GhiChu)
    VALUES ('PT0000002', 'NV004', 'HDB0000003', DATEADD(DAY, -1, GETDATE()), N'Lê Minh Tuấn (Mua hàng)', N'Tạm ứng 50% tiền hàng đợt 1 theo hóa đơn HDB0000003', 25500000, N'Chuyển khoản', N'Còn lại 25.500.000 đ thanh toán sau khi nhận đủ hàng');
ELSE
    UPDATE PHIEUTHU SET MaNV = 'NV004', MaHDB = 'HDB0000003', NgayThu = DATEADD(DAY, -1, GETDATE()), NguoiNop = N'Lê Minh Tuấn (Mua hàng)', LyDoThu = N'Tạm ứng 50% tiền hàng đợt 1 theo hóa đơn HDB0000003', SoTien = 25500000, HinhThuc = N'Chuyển khoản', GhiChu = N'Còn lại 25.500.000 đ thanh toán sau khi nhận đủ hàng' WHERE MaPT = 'PT0000002';
GO

-- ============================================================================
-- 13. NẠP DANH MỤC PHIẾU CHI HOẠT ĐỘNG (PHIEUCHI)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Phiếu Chi...';

-- PC0000001: Chi tiếp khách ký hợp đồng
IF NOT EXISTS (SELECT 1 FROM PHIEUCHI WHERE MaPC = 'PC0000001')
    INSERT INTO PHIEUCHI (MaPC, MaNV, NgayChi, NguoiNhan, LyDoChi, SoTien, HinhThuc, GhiChu)
    VALUES ('PC0000001', 'NV002', DATEADD(DAY, -4, GETDATE()), N'Trần Thị Hàng (Kinh doanh)', N'Chi tiếp khách ký kết hợp đồng cung cấp thiết bị Minh Khang', 2500000, N'Tiền mặt', N'Đã duyệt theo phiếu đề xuất thanh toán số 12/KD');
ELSE
    UPDATE PHIEUCHI SET MaNV = 'NV002', NgayChi = DATEADD(DAY, -4, GETDATE()), NguoiNhan = N'Trần Thị Hàng (Kinh doanh)', LyDoChi = N'Chi tiếp khách ký kết hợp đồng cung cấp thiết bị Minh Khang', SoTien = 2500000, HinhThuc = N'Tiền mặt', GhiChu = N'Đã duyệt theo phiếu đề xuất thanh toán số 12/KD' WHERE MaPC = 'PC0000001';

-- PC0000002: Chi vận chuyển hàng hóa
IF NOT EXISTS (SELECT 1 FROM PHIEUCHI WHERE MaPC = 'PC0000002')
    INSERT INTO PHIEUCHI (MaPC, MaNV, NgayChi, NguoiNhan, LyDoChi, SoTien, HinhThuc, GhiChu)
    VALUES ('PC0000002', 'NV003', DATEADD(DAY, -2, GETDATE()), N'Nhà xe vận tải Hưng Thịnh', N'Cước vận chuyển xe tải giao hàng cho đối tác Minh Khang', 1800000, N'Tiền mặt', N'Kèm hóa đơn dịch vụ vận chuyển đường bộ');
ELSE
    UPDATE PHIEUCHI SET MaNV = 'NV003', NgayChi = DATEADD(DAY, -2, GETDATE()), NguoiNhan = N'Nhà xe vận tải Hưng Thịnh', LyDoChi = N'Cước vận chuyển xe tải giao hàng cho đối tác Minh Khang', SoTien = 1800000, HinhThuc = N'Tiền mặt', GhiChu = N'Kèm hóa đơn dịch vụ vận chuyển đường bộ' WHERE MaPC = 'PC0000002';

-- PC0000003: Mua văn phòng phẩm
IF NOT EXISTS (SELECT 1 FROM PHIEUCHI WHERE MaPC = 'PC0000003')
    INSERT INTO PHIEUCHI (MaPC, MaNV, NgayChi, NguoiNhan, LyDoChi, SoTien, HinhThuc, GhiChu)
    VALUES ('PC0000003', 'NV004', DATEADD(DAY, -1, GETDATE()), N'Phạm Thị Toán (Kế toán)', N'Mua giấy in A4, bút viết, bìa còng lưu trữ chứng từ bán hàng', 650000, N'Tiền mặt', N'Hóa đơn bán lẻ Công ty CP VPP Hồng Hà');
ELSE
    UPDATE PHIEUCHI SET MaNV = 'NV004', NgayChi = DATEADD(DAY, -1, GETDATE()), NguoiNhan = N'Phạm Thị Toán (Kế toán)', LyDoChi = N'Mua giấy in A4, bút viết, bìa còng lưu trữ chứng từ bán hàng', SoTien = 650000, HinhThuc = N'Tiền mặt', GhiChu = N'Hóa đơn bán lẻ Công ty CP VPP Hồng Hà' WHERE MaPC = 'PC0000003';
GO

-- ============================================================================
-- 14. NẠP DANH MỤC CHỨNG TỪ KẾ TOÁN & ĐỊNH KHOẢN (CHUNGTU, CHITIETCHUNGTU)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Chứng Từ Kế Toán...';

-- CT0000001: Hạch toán nghiệp vụ bán hàng HDB0000001
IF NOT EXISTS (SELECT 1 FROM CHUNGTU WHERE MaCT = 'CT0000001')
    INSERT INTO CHUNGTU (MaCT, MaNV, MaHDB, NgayCT, LoaiCT, DienGiai)
    VALUES ('CT0000001', 'NV004', 'HDB0000001', DATEADD(DAY, -3, GETDATE()), N'Hóa đơn bán hàng', N'Hạch toán doanh thu, giá vốn và thu tiền HDB0000001 - Cty Minh Khang');
ELSE
    UPDATE CHUNGTU SET MaNV = 'NV004', MaHDB = 'HDB0000001', NgayCT = DATEADD(DAY, -3, GETDATE()), LoaiCT = N'Hóa đơn bán hàng', DienGiai = N'Hạch toán doanh thu, giá vốn và thu tiền HDB0000001 - Cty Minh Khang' WHERE MaCT = 'CT0000001';

DELETE FROM CHITIETCHUNGTU WHERE MaCT = 'CT0000001';
INSERT INTO CHITIETCHUNGTU (MaCT, STT, TaiKhoanNo, TaiKhoanCo, SoTien, DienGiai) VALUES
('CT0000001', 1, '131', '511', 81181818, N'Ghi nhận doanh thu bán hàng chưa VAT (5 Laptop Dell, 3 Máy in Canon)'),
('CT0000001', 2, '131', '3331', 8118182, N'Thuế GTGT đầu ra phải nộp 10%'),
('CT0000001', 3, '1121', '131', 89300000, N'Khách hàng chuyển khoản thanh toán toàn bộ qua ngân hàng VCB'),
('CT0000001', 4, '632', '156', 71400000, N'Giá vốn hàng bán xuất kho tương ứng theo phiếu PXK0000001');
GO

-- ============================================================================
-- 15. CẬP NHẬT LẠI TỒN KHO THỰC TẾ SAU CÁC ĐỢT XUẤT KHO MẪU (PXK0000001, PXK0000002)
-- ============================================================================
PRINT N'==> Cập nhật lại tồn kho thực tế phản ánh chính xác các phiếu xuất đã lập...';

-- KHO01: SP006 xuất 5 chiếc (50 - 5 = 45), SP003 xuất 3 máy (40 - 3 = 37)
UPDATE TONKHO SET SoLuongTon = 45, NgayCapNhat = GETDATE() WHERE MaKho = 'KHO01' AND MaSP = 'SP006';
UPDATE TONKHO SET SoLuongTon = 37, NgayCapNhat = GETDATE() WHERE MaKho = 'KHO01' AND MaSP = 'SP003';

-- KHO02: SP010 xuất 2 chiếc (18 - 2 = 16), SP014 xuất 10 bộ (140 - 10 = 130)
UPDATE TONKHO SET SoLuongTon = 16, NgayCapNhat = GETDATE() WHERE MaKho = 'KHO02' AND MaSP = 'SP010';
UPDATE TONKHO SET SoLuongTon = 130, NgayCapNhat = GETDATE() WHERE MaKho = 'KHO02' AND MaSP = 'SP014';
GO

PRINT N'================================================================================';
PRINT N'[HOÀN TẤT] Nạp thành công toàn bộ dữ liệu mẫu chuẩn Doanh Nghiệp (DNQH)!';
PRINT N'- 6 Nhà cung cấp uy tín';
PRINT N'- 4 Loại sản phẩm';
PRINT N'- 14 Sản phẩm đầy đủ phân loại và niêm yết giá bán';
PRINT N'- 8 Khách hàng B2B & tổ chức giáo dục / y tế';
PRINT N'- 3 Kho hàng Bắc - Trung - Nam';
PRINT N'- 42 Dòng tồn kho phản ánh chính xác số lượng thực tế sau xuất hàng';
PRINT N'- 5 Nhân viên đầy đủ phòng ban (Admin, Bán hàng, Kho, Kế toán)';
PRINT N'- 5 Tài khoản đăng nhập bảo mật PBKDF2 (Mật khẩu: 123456)';
PRINT N'- 4 Đơn đặt hàng mẫu (Đã xuất HĐ, Chờ xuất kho, Xuất 1 phần, Đơn mới)';
PRINT N'- 3 Hóa đơn bán hàng thương mại thực tế';
PRINT N'- 2 Phiếu xuất kho theo hóa đơn đã lập';
PRINT N'- 2 Phiếu thu tiền khách hàng (Thu 100% & Tạm ứng 50%)';
PRINT N'- 3 Phiếu chi chi phí hoạt động doanh nghiệp';
PRINT N'- 1 Chứng từ kế toán hoàn chỉnh kèm 4 dòng định khoản Nợ/Có';
PRINT N'================================================================================';
