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
-- Số liệu giao dịch chuẩn mẫu Tháng 10 & Tháng 11/2026
-- ============================================================================
PRINT N'==> Đang nạp danh mục Đơn Đặt Hàng (Tháng 10 & Tháng 11/2026)...';

-- DDH0000001: Tháng 10 - Cty Minh Khang (Đã lập HĐ & Xuất kho, Đã thanh toán)
IF NOT EXISTS (SELECT 1 FROM DONDATHANG WHERE MaDDH = 'DDH0000001')
    INSERT INTO DONDATHANG (MaDDH, MaNV, MaKH, NgayDat, NgayGiaoDuKien, TongTien, TrangThai, GhiChu)
    VALUES ('DDH0000001', 'NV002', 'KH001', '2026-10-01 09:30:00', '2026-10-04', 89300000, N'Đã lập hóa đơn', N'Hợp đồng cung cấp thiết bị tin học đợt 1');
ELSE
    UPDATE DONDATHANG SET MaNV = 'NV002', MaKH = 'KH001', NgayDat = '2026-10-01 09:30:00', NgayGiaoDuKien = '2026-10-04', TongTien = 89300000, TrangThai = N'Đã lập hóa đơn', GhiChu = N'Hợp đồng cung cấp thiết bị tin học đợt 1' WHERE MaDDH = 'DDH0000001';

DELETE FROM CHITIETDONDATHANG WHERE MaDDH = 'DDH0000001';
INSERT INTO CHITIETDONDATHANG (MaDDH, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('DDH0000001', 'SP006', 5, 15490000, 0, 77450000),
('DDH0000001', 'SP003', 3, 3950000, 0, 11850000);

-- DDH0000002: Tháng 10 - ĐH Kinh Tế Quốc Dân (Đã lập HĐ, Thanh toán 1 phần)
IF NOT EXISTS (SELECT 1 FROM DONDATHANG WHERE MaDDH = 'DDH0000002')
    INSERT INTO DONDATHANG (MaDDH, MaNV, MaKH, NgayDat, NgayGiaoDuKien, TongTien, TrangThai, GhiChu)
    VALUES ('DDH0000002', 'NV002', 'KH004', '2026-10-03 08:45:00', '2026-10-07', 147600000, N'Đã lập hóa đơn', N'Dự án trang bị phòng thực hành tin học NEU');
ELSE
    UPDATE DONDATHANG SET MaNV = 'NV002', MaKH = 'KH004', NgayDat = '2026-10-03 08:45:00', NgayGiaoDuKien = '2026-10-07', TongTien = 147600000, TrangThai = N'Đã lập hóa đơn', GhiChu = N'Dự án trang bị phòng thực hành tin học NEU' WHERE MaDDH = 'DDH0000002';

DELETE FROM CHITIETDONDATHANG WHERE MaDDH = 'DDH0000002';
INSERT INTO CHITIETDONDATHANG (MaDDH, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('DDH0000002', 'SP004', 8, 14800000, 0, 118400000),
('DDH0000002', 'SP009', 8, 3650000, 0, 29200000);

-- DDH0000003: Tháng 10 - Cty Xây Dựng Hòa Bình (Đã lập HĐ, Đã xuất 1 phần, Chưa thanh toán)
IF NOT EXISTS (SELECT 1 FROM DONDATHANG WHERE MaDDH = 'DDH0000003')
    INSERT INTO DONDATHANG (MaDDH, MaNV, MaKH, NgayDat, NgayGiaoDuKien, TongTien, TrangThai, GhiChu)
    VALUES ('DDH0000003', 'NV002', 'KH007', '2026-10-05 14:00:00', '2026-10-08', 51000000, N'Đã lập hóa đơn', N'Cung cấp TV và phụ kiện phòng họp Ban Giám Đốc');
ELSE
    UPDATE DONDATHANG SET MaNV = 'NV002', MaKH = 'KH007', NgayDat = '2026-10-05 14:00:00', NgayGiaoDuKien = '2026-10-08', TongTien = 51000000, TrangThai = N'Đã lập hóa đơn', GhiChu = N'Cung cấp TV và phụ kiện phòng họp Ban Giám Đốc' WHERE MaDDH = 'DDH0000003';

DELETE FROM CHITIETDONDATHANG WHERE MaDDH = 'DDH0000003';
INSERT INTO CHITIETDONDATHANG (MaDDH, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('DDH0000003', 'SP010', 4, 11200000, 0, 44800000),
('DDH0000003', 'SP014', 10, 620000, 0, 6200000);

-- DDH0000004: Tháng 10 - BV Vinmec (Đơn mới ĐÃ DUYỆT - Demo chức năng Lập Hóa Đơn)
IF NOT EXISTS (SELECT 1 FROM DONDATHANG WHERE MaDDH = 'DDH0000004')
    INSERT INTO DONDATHANG (MaDDH, MaNV, MaKH, NgayDat, NgayGiaoDuKien, TongTien, TrangThai, GhiChu)
    VALUES ('DDH0000004', 'NV002', 'KH005', '2026-10-06 16:30:00', '2026-10-10', 26600000, N'Đã duyệt', N'Trang bị máy lọc không khí và gia dụng cho khu điều trị');
ELSE
    UPDATE DONDATHANG SET MaNV = 'NV002', MaKH = 'KH005', NgayDat = '2026-10-06 16:30:00', NgayGiaoDuKien = '2026-10-10', TongTien = 26600000, TrangThai = N'Đã duyệt', GhiChu = N'Trang bị máy lọc không khí và gia dụng cho khu điều trị' WHERE MaDDH = 'DDH0000004';

DELETE FROM CHITIETDONDATHANG WHERE MaDDH = 'DDH0000004';
INSERT INTO CHITIETDONDATHANG (MaDDH, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('DDH0000004', 'SP002', 6, 3200000, 0, 19200000),
('DDH0000004', 'SP001', 4, 1850000, 0, 7400000);

-- DDH0000005: Tháng 11 - Tập Đoàn Á Châu (Đã lập HĐ, Đã xuất kho, Đã thanh toán)
IF NOT EXISTS (SELECT 1 FROM DONDATHANG WHERE MaDDH = 'DDH0000005')
    INSERT INTO DONDATHANG (MaDDH, MaNV, MaKH, NgayDat, NgayGiaoDuKien, TongTien, TrangThai, GhiChu)
    VALUES ('DDH0000005', 'NV005', 'KH002', '2026-11-02 09:00:00', '2026-11-05', 63530000, N'Đã lập hóa đơn', N'Cung cấp tủ lạnh và máy điều hòa cho văn phòng Á Châu Đà Nẵng');
ELSE
    UPDATE DONDATHANG SET MaNV = 'NV005', MaKH = 'KH002', NgayDat = '2026-11-02 09:00:00', NgayGiaoDuKien = '2026-11-05', TongTien = 63530000, TrangThai = N'Đã lập hóa đơn', GhiChu = N'Cung cấp tủ lạnh và máy điều hòa cho văn phòng Á Châu Đà Nẵng' WHERE MaDDH = 'DDH0000005';

DELETE FROM CHITIETDONDATHANG WHERE MaDDH = 'DDH0000005';
INSERT INTO CHITIETDONDATHANG (MaDDH, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('DDH0000005', 'SP008', 3, 11990000, 0, 35970000),
('DDH0000005', 'SP007', 4, 6890000, 0, 27560000);

-- DDH0000006: Tháng 11 - Chuỗi Cửa Hàng Toàn Cầu (Đã lập HĐ, Thanh toán 1 phần)
IF NOT EXISTS (SELECT 1 FROM DONDATHANG WHERE MaDDH = 'DDH0000006')
    INSERT INTO DONDATHANG (MaDDH, MaNV, MaKH, NgayDat, NgayGiaoDuKien, TongTien, TrangThai, GhiChu)
    VALUES ('DDH0000006', 'NV002', 'KH003', '2026-11-05 08:30:00', '2026-11-08', 89460000, N'Đã lập hóa đơn', N'Cung cấp thiết bị gia dụng và mực in cho chuỗi siêu thị');
ELSE
    UPDATE DONDATHANG SET MaNV = 'NV002', MaKH = 'KH003', NgayDat = '2026-11-05 08:30:00', NgayGiaoDuKien = '2026-11-08', TongTien = 89460000, TrangThai = N'Đã lập hóa đơn', GhiChu = N'Cung cấp thiết bị gia dụng và mực in cho chuỗi siêu thị' WHERE MaDDH = 'DDH0000006';

DELETE FROM CHITIETDONDATHANG WHERE MaDDH = 'DDH0000006';
INSERT INTO CHITIETDONDATHANG (MaDDH, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('DDH0000006', 'SP005', 4, 9490000, 0, 37960000),
('DDH0000006', 'SP011', 5, 7800000, 0, 39000000),
('DDH0000006', 'SP013', 10, 1250000, 0, 12500000);

-- DDH0000007: Tháng 11 - Cty Phương Nam (Đã lập HĐ, Đã xuất kho, Chưa thanh toán)
IF NOT EXISTS (SELECT 1 FROM DONDATHANG WHERE MaDDH = 'DDH0000007')
    INSERT INTO DONDATHANG (MaDDH, MaNV, MaKH, NgayDat, NgayGiaoDuKien, TongTien, TrangThai, GhiChu)
    VALUES ('DDH0000007', 'NV002', 'KH006', '2026-11-10 10:00:00', '2026-11-14', 34580000, N'Đã lập hóa đơn', N'Cung cấp Laptop Dell và giấy in văn phòng Phương Nam');
ELSE
    UPDATE DONDATHANG SET MaNV = 'NV002', MaKH = 'KH006', NgayDat = '2026-11-10 10:00:00', NgayGiaoDuKien = '2026-11-14', TongTien = 34580000, TrangThai = N'Đã lập hóa đơn', GhiChu = N'Cung cấp Laptop Dell và giấy in văn phòng Phương Nam' WHERE MaDDH = 'DDH0000007';

DELETE FROM CHITIETDONDATHANG WHERE MaDDH = 'DDH0000007';
INSERT INTO CHITIETDONDATHANG (MaDDH, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('DDH0000007', 'SP006', 2, 15490000, 0, 30980000),
('DDH0000007', 'SP012', 50, 72000, 0, 3600000);

-- DDH0000008: Tháng 11 - Điện Máy Xanh Miền Bắc (Đơn mới ĐÃ DUYỆT - Demo tháng 11)
IF NOT EXISTS (SELECT 1 FROM DONDATHANG WHERE MaDDH = 'DDH0000008')
    INSERT INTO DONDATHANG (MaDDH, MaNV, MaKH, NgayDat, NgayGiaoDuKien, TongTien, TrangThai, GhiChu)
    VALUES ('DDH0000008', 'NV002', 'KH008', '2026-11-15 14:00:00', '2026-11-20', 34500000, N'Đã duyệt', N'Đơn đặt hàng cung cấp gia dụng đợt 2 cho Điện Máy Xanh');
ELSE
    UPDATE DONDATHANG SET MaNV = 'NV002', MaKH = 'KH008', NgayDat = '2026-11-15 14:00:00', NgayGiaoDuKien = '2026-11-20', TongTien = 34500000, TrangThai = N'Đã duyệt', GhiChu = N'Đơn đặt hàng cung cấp gia dụng đợt 2 cho Điện Máy Xanh' WHERE MaDDH = 'DDH0000008';

DELETE FROM CHITIETDONDATHANG WHERE MaDDH = 'DDH0000008';
INSERT INTO CHITIETDONDATHANG (MaDDH, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('DDH0000008', 'SP001', 10, 1850000, 0, 18500000),
('DDH0000008', 'SP002', 5, 3200000, 0, 16000000);
GO

-- ============================================================================
-- 10. NẠP DANH MỤC HÓA ĐƠN BÁN HÀNG & CHI TIẾT (HOADONBAN, CHITIETHOADONBAN)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Hóa Đơn Bán Hàng (Tháng 10 & Tháng 11/2026)...';

-- HDB0000001 (DDH0000001 - Cty Minh Khang - Đã thanh toán 100%)
IF NOT EXISTS (SELECT 1 FROM HOADONBAN WHERE MaHDB = 'HDB0000001')
    INSERT INTO HOADONBAN (MaHDB, MaNV, MaDDH, MaKH, NgayLap, TongTien, GhiChu, TrangThai)
    VALUES ('HDB0000001', 'NV002', 'DDH0000001', 'KH001', '2026-10-02 10:15:00', 89300000, N'Hóa đơn GTGT điện tử cung cấp thiết bị văn phòng', N'Đã thanh toán');
ELSE
    UPDATE HOADONBAN SET MaNV = 'NV002', MaDDH = 'DDH0000001', MaKH = 'KH001', NgayLap = '2026-10-02 10:15:00', TongTien = 89300000, GhiChu = N'Hóa đơn GTGT điện tử cung cấp thiết bị văn phòng', TrangThai = N'Đã thanh toán' WHERE MaHDB = 'HDB0000001';

DELETE FROM CHITIETHOADONBAN WHERE MaHDB = 'HDB0000001';
INSERT INTO CHITIETHOADONBAN (MaHDB, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('HDB0000001', 'SP006', 5, 15490000, 0, 77450000),
('HDB0000001', 'SP003', 3, 3950000, 0, 11850000);

-- HDB0000002 (DDH0000002 - ĐH Kinh Tế Quốc Dân - Thanh toán một phần 70tr/147.6tr)
IF NOT EXISTS (SELECT 1 FROM HOADONBAN WHERE MaHDB = 'HDB0000002')
    INSERT INTO HOADONBAN (MaHDB, MaNV, MaDDH, MaKH, NgayLap, TongTien, GhiChu, TrangThai)
    VALUES ('HDB0000002', 'NV002', 'DDH0000002', 'KH004', '2026-10-04 09:30:00', 147600000, N'Hóa đơn bán hàng theo dự án phòng lab NEU', N'Thanh toán một phần');
ELSE
    UPDATE HOADONBAN SET MaNV = 'NV002', MaDDH = 'DDH0000002', MaKH = 'KH004', NgayLap = '2026-10-04 09:30:00', TongTien = 147600000, GhiChu = N'Hóa đơn bán hàng theo dự án phòng lab NEU', TrangThai = N'Thanh toán một phần' WHERE MaHDB = 'HDB0000002';

DELETE FROM CHITIETHOADONBAN WHERE MaHDB = 'HDB0000002';
INSERT INTO CHITIETHOADONBAN (MaHDB, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('HDB0000002', 'SP004', 8, 14800000, 0, 118400000),
('HDB0000002', 'SP009', 8, 3650000, 0, 29200000);

-- HDB0000003 (DDH0000003 - Tập Đoàn Hòa Bình - Nợ 100% = 51tr)
IF NOT EXISTS (SELECT 1 FROM HOADONBAN WHERE MaHDB = 'HDB0000003')
    INSERT INTO HOADONBAN (MaHDB, MaNV, MaDDH, MaKH, NgayLap, TongTien, GhiChu, TrangThai)
    VALUES ('HDB0000003', 'NV002', 'DDH0000003', 'KH007', '2026-10-06 11:00:00', 51000000, N'Hóa đơn cung cấp TV & bàn phím chuột', N'Chưa thanh toán');
ELSE
    UPDATE HOADONBAN SET MaNV = 'NV002', MaDDH = 'DDH0000003', MaKH = 'KH007', NgayLap = '2026-10-06 11:00:00', TongTien = 51000000, GhiChu = N'Hóa đơn cung cấp TV & bàn phím chuột', TrangThai = N'Chưa thanh toán' WHERE MaHDB = 'HDB0000003';

DELETE FROM CHITIETHOADONBAN WHERE MaHDB = 'HDB0000003';
INSERT INTO CHITIETHOADONBAN (MaHDB, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('HDB0000003', 'SP010', 4, 11200000, 0, 44800000),
('HDB0000003', 'SP014', 10, 620000, 0, 6200000);

-- HDB0000004 (DDH0000005 - Tập Đoàn Á Châu - Tháng 11 - Đã thanh toán 100%)
IF NOT EXISTS (SELECT 1 FROM HOADONBAN WHERE MaHDB = 'HDB0000004')
    INSERT INTO HOADONBAN (MaHDB, MaNV, MaDDH, MaKH, NgayLap, TongTien, GhiChu, TrangThai)
    VALUES ('HDB0000004', 'NV005', 'DDH0000005', 'KH002', '2026-11-03 10:30:00', 63530000, N'Hóa đơn cung cấp điều hòa và tủ lạnh chi nhánh Đà Nẵng', N'Đã thanh toán');
ELSE
    UPDATE HOADONBAN SET MaNV = 'NV005', MaDDH = 'DDH0000005', MaKH = 'KH002', NgayLap = '2026-11-03 10:30:00', TongTien = 63530000, GhiChu = N'Hóa đơn cung cấp điều hòa và tủ lạnh chi nhánh Đà Nẵng', TrangThai = N'Đã thanh toán' WHERE MaHDB = 'HDB0000004';

DELETE FROM CHITIETHOADONBAN WHERE MaHDB = 'HDB0000004';
INSERT INTO CHITIETHOADONBAN (MaHDB, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('HDB0000004', 'SP008', 3, 11990000, 0, 35970000),
('HDB0000004', 'SP007', 4, 6890000, 0, 27560000);

-- HDB0000005 (DDH0000006 - Chuỗi Toàn Cầu - Tháng 11 - Thanh toán một phần 45tr/89.46tr)
IF NOT EXISTS (SELECT 1 FROM HOADONBAN WHERE MaHDB = 'HDB0000005')
    INSERT INTO HOADONBAN (MaHDB, MaNV, MaDDH, MaKH, NgayLap, TongTien, GhiChu, TrangThai)
    VALUES ('HDB0000005', 'NV002', 'DDH0000006', 'KH003', '2026-11-06 09:15:00', 89460000, N'Hóa đơn bán lẻ cung cấp thiết bị gia dụng Toàn Cầu', N'Thanh toán một phần');
ELSE
    UPDATE HOADONBAN SET MaNV = 'NV002', MaDDH = 'DDH0000006', MaKH = 'KH003', NgayLap = '2026-11-06 09:15:00', TongTien = 89460000, GhiChu = N'Hóa đơn bán lẻ cung cấp thiết bị gia dụng Toàn Cầu', TrangThai = N'Thanh toán một phần' WHERE MaHDB = 'HDB0000005';

DELETE FROM CHITIETHOADONBAN WHERE MaHDB = 'HDB0000005';
INSERT INTO CHITIETHOADONBAN (MaHDB, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('HDB0000005', 'SP005', 4, 9490000, 0, 37960000),
('HDB0000005', 'SP011', 5, 7800000, 0, 39000000),
('HDB0000005', 'SP013', 10, 1250000, 0, 12500000);

-- HDB0000006 (DDH0000007 - Cty Phương Nam - Tháng 11 - Chưa thanh toán 34.58tr)
IF NOT EXISTS (SELECT 1 FROM HOADONBAN WHERE MaHDB = 'HDB0000006')
    INSERT INTO HOADONBAN (MaHDB, MaNV, MaDDH, MaKH, NgayLap, TongTien, GhiChu, TrangThai)
    VALUES ('HDB0000006', 'NV002', 'DDH0000007', 'KH006', '2026-11-12 11:00:00', 34580000, N'Hóa đơn bán Laptop và giấy in công ty Phương Nam', N'Chưa thanh toán');
ELSE
    UPDATE HOADONBAN SET MaNV = 'NV002', MaDDH = 'DDH0000007', MaKH = 'KH006', NgayLap = '2026-11-12 11:00:00', TongTien = 34580000, GhiChu = N'Hóa đơn bán Laptop và giấy in công ty Phương Nam', TrangThai = N'Chưa thanh toán' WHERE MaHDB = 'HDB0000006';

DELETE FROM CHITIETHOADONBAN WHERE MaHDB = 'HDB0000006';
INSERT INTO CHITIETHOADONBAN (MaHDB, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('HDB0000006', 'SP006', 2, 15490000, 0, 30980000),
('HDB0000006', 'SP012', 50, 72000, 0, 3600000);
GO

-- ============================================================================
-- 11. NẠP DANH MỤC PHIẾU XUẤT KHO & CHI TIẾT (PHIEUXUATKHO, CHITIETPHIEUXUATKHO)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Phiếu Xuất Kho (Tháng 10 & Tháng 11/2026)...';

-- PXK0000001: Tháng 10 - Xuất đủ cho HDB0000001 tại KHO01 (Kho Tổng Miền Bắc)
IF NOT EXISTS (SELECT 1 FROM PHIEUXUATKHO WHERE MaPXK = 'PXK0000001')
    INSERT INTO PHIEUXUATKHO (MaPXK, MaNV, MaHDB, MaKho, NgayXuat, LyDoXuat, TrangThai)
    VALUES ('PXK0000001', 'NV003', 'HDB0000001', 'KHO01', '2026-10-02 14:00:00', N'Xuất kho giao hàng theo hóa đơn bán HDB0000001', N'Đã xuất');
ELSE
    UPDATE PHIEUXUATKHO SET MaNV = 'NV003', MaHDB = 'HDB0000001', MaKho = 'KHO01', NgayXuat = '2026-10-02 14:00:00', LyDoXuat = N'Xuất kho giao hàng theo hóa đơn bán HDB0000001', TrangThai = N'Đã xuất' WHERE MaPXK = 'PXK0000001';

DELETE FROM CHITIETPHIEUXUATKHO WHERE MaPXK = 'PXK0000001';
INSERT INTO CHITIETPHIEUXUATKHO (MaPXK, MaSP, SoLuongXuat) VALUES
('PXK0000001', 'SP006', 5),
('PXK0000001', 'SP003', 3);

-- PXK0000002: Tháng 10 - Xuất đủ cho HDB0000002 tại KHO01 (Kho Tổng Miền Bắc)
IF NOT EXISTS (SELECT 1 FROM PHIEUXUATKHO WHERE MaPXK = 'PXK0000002')
    INSERT INTO PHIEUXUATKHO (MaPXK, MaNV, MaHDB, MaKho, NgayXuat, LyDoXuat, TrangThai)
    VALUES ('PXK0000002', 'NV003', 'HDB0000002', 'KHO01', '2026-10-05 10:00:00', N'Xuất kho thiết bị phòng lab NEU theo HDB0000002', N'Đã xuất');
ELSE
    UPDATE PHIEUXUATKHO SET MaNV = 'NV003', MaHDB = 'HDB0000002', MaKho = 'KHO01', NgayXuat = '2026-10-05 10:00:00', LyDoXuat = N'Xuất kho thiết bị phòng lab NEU theo HDB0000002', TrangThai = N'Đã xuất' WHERE MaPXK = 'PXK0000002';

DELETE FROM CHITIETPHIEUXUATKHO WHERE MaPXK = 'PXK0000002';
INSERT INTO CHITIETPHIEUXUATKHO (MaPXK, MaSP, SoLuongXuat) VALUES
('PXK0000002', 'SP004', 8),
('PXK0000002', 'SP009', 8);

-- PXK0000003: Tháng 10 - Xuất đợt 1 cho HDB0000003 tại KHO02 (Kho Miền Nam: Xuất 2 TV, 10 chuột phím)
IF NOT EXISTS (SELECT 1 FROM PHIEUXUATKHO WHERE MaPXK = 'PXK0000003')
    INSERT INTO PHIEUXUATKHO (MaPXK, MaNV, MaHDB, MaKho, NgayXuat, LyDoXuat, TrangThai)
    VALUES ('PXK0000003', 'NV003', 'HDB0000003', 'KHO02', '2026-10-06 15:30:00', N'Xuất kho giao hàng đợt 1 theo hóa đơn bán HDB0000003', N'Đã xuất');
ELSE
    UPDATE PHIEUXUATKHO SET MaNV = 'NV003', MaHDB = 'HDB0000003', MaKho = 'KHO02', NgayXuat = '2026-10-06 15:30:00', LyDoXuat = N'Xuất kho giao hàng đợt 1 theo hóa đơn bán HDB0000003', TrangThai = N'Đã xuất' WHERE MaPXK = 'PXK0000003';

DELETE FROM CHITIETPHIEUXUATKHO WHERE MaPXK = 'PXK0000003';
INSERT INTO CHITIETPHIEUXUATKHO (MaPXK, MaSP, SoLuongXuat) VALUES
('PXK0000003', 'SP010', 2),
('PXK0000003', 'SP014', 10);

-- PXK0000004: Tháng 11 - Xuất đủ cho HDB0000004 tại KHO03 (Kho Miền Trung)
IF NOT EXISTS (SELECT 1 FROM PHIEUXUATKHO WHERE MaPXK = 'PXK0000004')
    INSERT INTO PHIEUXUATKHO (MaPXK, MaNV, MaHDB, MaKho, NgayXuat, LyDoXuat, TrangThai)
    VALUES ('PXK0000004', 'NV003', 'HDB0000004', 'KHO03', '2026-11-03 15:00:00', N'Xuất kho tủ lạnh điều hòa giao Á Châu theo HDB0000004', N'Đã xuất');
ELSE
    UPDATE PHIEUXUATKHO SET MaNV = 'NV003', MaHDB = 'HDB0000004', MaKho = 'KHO03', NgayXuat = '2026-11-03 15:00:00', LyDoXuat = N'Xuất kho tủ lạnh điều hòa giao Á Châu theo HDB0000004', TrangThai = N'Đã xuất' WHERE MaPXK = 'PXK0000004';

DELETE FROM CHITIETPHIEUXUATKHO WHERE MaPXK = 'PXK0000004';
INSERT INTO CHITIETPHIEUXUATKHO (MaPXK, MaSP, SoLuongXuat) VALUES
('PXK0000004', 'SP008', 3),
('PXK0000004', 'SP007', 4);

-- PXK0000005: Tháng 11 - Xuất đủ cho HDB0000005 tại KHO02 (Kho Miền Nam)
IF NOT EXISTS (SELECT 1 FROM PHIEUXUATKHO WHERE MaPXK = 'PXK0000005')
    INSERT INTO PHIEUXUATKHO (MaPXK, MaNV, MaHDB, MaKho, NgayXuat, LyDoXuat, TrangThai)
    VALUES ('PXK0000005', 'NV003', 'HDB0000005', 'KHO02', '2026-11-06 14:30:00', N'Xuất kho gia dụng theo hóa đơn bán HDB0000005', N'Đã xuất');
ELSE
    UPDATE PHIEUXUATKHO SET MaNV = 'NV003', MaHDB = 'HDB0000005', MaKho = 'KHO02', NgayXuat = '2026-11-06 14:30:00', LyDoXuat = N'Xuất kho gia dụng theo hóa đơn bán HDB0000005', TrangThai = N'Đã xuất' WHERE MaPXK = 'PXK0000005';

DELETE FROM CHITIETPHIEUXUATKHO WHERE MaPXK = 'PXK0000005';
INSERT INTO CHITIETPHIEUXUATKHO (MaPXK, MaSP, SoLuongXuat) VALUES
('PXK0000005', 'SP005', 4),
('PXK0000005', 'SP011', 5),
('PXK0000005', 'SP013', 10);

-- PXK0000006: Tháng 11 - Xuất đủ cho HDB0000006 tại KHO02 (Kho Miền Nam)
IF NOT EXISTS (SELECT 1 FROM PHIEUXUATKHO WHERE MaPXK = 'PXK0000006')
    INSERT INTO PHIEUXUATKHO (MaPXK, MaNV, MaHDB, MaKho, NgayXuat, LyDoXuat, TrangThai)
    VALUES ('PXK0000006', 'NV003', 'HDB0000006', 'KHO02', '2026-11-12 15:00:00', N'Xuất kho giao thiết bị cho Cty Phương Nam theo HDB0000006', N'Đã xuất');
ELSE
    UPDATE PHIEUXUATKHO SET MaNV = 'NV003', MaHDB = 'HDB0000006', MaKho = 'KHO02', NgayXuat = '2026-11-12 15:00:00', LyDoXuat = N'Xuất kho giao thiết bị cho Cty Phương Nam theo HDB0000006', TrangThai = N'Đã xuất' WHERE MaPXK = 'PXK0000006';

DELETE FROM CHITIETPHIEUXUATKHO WHERE MaPXK = 'PXK0000006';
INSERT INTO CHITIETPHIEUXUATKHO (MaPXK, MaSP, SoLuongXuat) VALUES
('PXK0000006', 'SP006', 2),
('PXK0000006', 'SP012', 50);
GO

-- ============================================================================
-- 12. NẠP DANH MỤC PHIẾU THU BÁN HÀNG (PHIEUTHU)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Phiếu Thu (Tháng 10 & Tháng 11/2026)...';

-- PT0000001: Tháng 10 - Thu đủ 100% cho HDB0000001 (89.300.000 VNĐ)
IF NOT EXISTS (SELECT 1 FROM PHIEUTHU WHERE MaPT = 'PT0000001')
    INSERT INTO PHIEUTHU (MaPT, MaNV, MaHDB, NgayThu, NguoiNop, LyDoThu, SoTien, HinhThuc, GhiChu)
    VALUES ('PT0000001', 'NV004', 'HDB0000001', '2026-10-03 11:00:00', N'Nguyễn Minh Khang (Giám đốc)', N'Thanh toán toàn bộ tiền hàng theo hóa đơn HDB0000001', 89300000, N'Chuyển khoản', N'Đã đối soát ủy nhiệm chi VCB thành công');
ELSE
    UPDATE PHIEUTHU SET MaNV = 'NV004', MaHDB = 'HDB0000001', NgayThu = '2026-10-03 11:00:00', NguoiNop = N'Nguyễn Minh Khang (Giám đốc)', LyDoThu = N'Thanh toán toàn bộ tiền hàng theo hóa đơn HDB0000001', SoTien = 89300000, HinhThuc = N'Chuyển khoản', GhiChu = N'Đã đối soát ủy nhiệm chi VCB thành công' WHERE MaPT = 'PT0000001';

-- PT0000002: Tháng 10 - Tạm ứng tiền đợt 1 cho HDB0000002 (70.000.000 VNĐ)
IF NOT EXISTS (SELECT 1 FROM PHIEUTHU WHERE MaPT = 'PT0000002')
    INSERT INTO PHIEUTHU (MaPT, MaNV, MaHDB, NgayThu, NguoiNop, LyDoThu, SoTien, HinhThuc, GhiChu)
    VALUES ('PT0000002', 'NV004', 'HDB0000002', '2026-10-06 14:30:00', N'Ban Quản Lý Dự Án NEU', N'Tạm ứng tiền thiết bị phòng lab theo hóa đơn HDB0000002', 70000000, N'Chuyển khoản', N'Số còn lại thanh toán sau nghiệm thu bàn giao');
ELSE
    UPDATE PHIEUTHU SET MaNV = 'NV004', MaHDB = 'HDB0000002', NgayThu = '2026-10-06 14:30:00', NguoiNop = N'Ban Quản Lý Dự Án NEU', LyDoThu = N'Tạm ứng tiền thiết bị phòng lab theo hóa đơn HDB0000002', SoTien = 70000000, HinhThuc = N'Chuyển khoản', GhiChu = N'Số còn lại thanh toán sau nghiệm thu bàn giao' WHERE MaPT = 'PT0000002';

-- PT0000003: Tháng 11 - Thu đủ 100% cho HDB0000004 (63.530.000 VNĐ)
IF NOT EXISTS (SELECT 1 FROM PHIEUTHU WHERE MaPT = 'PT0000003')
    INSERT INTO PHIEUTHU (MaPT, MaNV, MaHDB, NgayThu, NguoiNop, LyDoThu, SoTien, HinhThuc, GhiChu)
    VALUES ('PT0000003', 'NV004', 'HDB0000004', '2026-11-04 11:00:00', N'Trần Á Châu (Phó Giám Đốc)', N'Thanh toán toàn bộ tiền điều hòa tủ lạnh theo hóa đơn HDB0000004', 63530000, N'Chuyển khoản', N'Đã thanh toán qua ngân hàng BIDV');
ELSE
    UPDATE PHIEUTHU SET MaNV = 'NV004', MaHDB = 'HDB0000004', NgayThu = '2026-11-04 11:00:00', NguoiNop = N'Trần Á Châu (Phó Giám Đốc)', LyDoThu = N'Thanh toán toàn bộ tiền điều hòa tủ lạnh theo hóa đơn HDB0000004', SoTien = 63530000, HinhThuc = N'Chuyển khoản', GhiChu = N'Đã thanh toán qua ngân hàng BIDV' WHERE MaPT = 'PT0000003';

-- PT0000004: Tháng 11 - Tạm ứng tiền đợt 1 cho HDB0000005 (45.000.000 VNĐ)
IF NOT EXISTS (SELECT 1 FROM PHIEUTHU WHERE MaPT = 'PT0000004')
    INSERT INTO PHIEUTHU (MaPT, MaNV, MaHDB, NgayThu, NguoiNop, LyDoThu, SoTien, HinhThuc, GhiChu)
    VALUES ('PT0000004', 'NV004', 'HDB0000005', '2026-11-08 10:00:00', N'Kế toán Chuỗi Toàn Cầu', N'Tạm ứng đợt 1 tiền mua gia dụng theo hóa đơn HDB0000005', 45000000, N'Chuyển khoản', N'Còn lại 44.460.000 đ thanh toán cuối tháng 11');
ELSE
    UPDATE PHIEUTHU SET MaNV = 'NV004', MaHDB = 'HDB0000005', NgayThu = '2026-11-08 10:00:00', NguoiNop = N'Kế toán Chuỗi Toàn Cầu', LyDoThu = N'Tạm ứng đợt 1 tiền mua gia dụng theo hóa đơn HDB0000005', SoTien = 45000000, HinhThuc = N'Chuyển khoản', GhiChu = N'Còn lại 44.460.000 đ thanh toán cuối tháng 11' WHERE MaPT = 'PT0000004';
GO

-- ============================================================================
-- 13. NẠP DANH MỤC PHIẾU CHI HOẠT ĐỘNG (PHIEUCHI)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Phiếu Chi (Tháng 10 & Tháng 11/2026)...';

-- PC0000001: Tháng 10 - Chi tiếp khách ký hợp đồng Minh Khang
IF NOT EXISTS (SELECT 1 FROM PHIEUCHI WHERE MaPC = 'PC0000001')
    INSERT INTO PHIEUCHI (MaPC, MaNV, NgayChi, NguoiNhan, LyDoChi, SoTien, HinhThuc, GhiChu)
    VALUES ('PC0000001', 'NV002', '2026-10-02 16:00:00', N'Trần Thị Hàng (Kinh doanh)', N'Chi tiếp khách ký kết hợp đồng cung cấp thiết bị Minh Khang', 2500000, N'Tiền mặt', N'Đã duyệt theo phiếu đề xuất thanh toán số 12/KD');
ELSE
    UPDATE PHIEUCHI SET MaNV = 'NV002', NgayChi = '2026-10-02 16:00:00', NguoiNhan = N'Trần Thị Hàng (Kinh doanh)', LyDoChi = N'Chi tiếp khách ký kết hợp đồng cung cấp thiết bị Minh Khang', SoTien = 2500000, HinhThuc = N'Tiền mặt', GhiChu = N'Đã duyệt theo phiếu đề xuất thanh toán số 12/KD' WHERE MaPC = 'PC0000001';

-- PC0000002: Tháng 10 - Chi vận chuyển hàng hóa NEU
IF NOT EXISTS (SELECT 1 FROM PHIEUCHI WHERE MaPC = 'PC0000002')
    INSERT INTO PHIEUCHI (MaPC, MaNV, NgayChi, NguoiNhan, LyDoChi, SoTien, HinhThuc, GhiChu)
    VALUES ('PC0000002', 'NV003', '2026-10-04 11:30:00', N'Nhà xe vận tải Hưng Thịnh', N'Cước vận chuyển thiết bị phòng lab giao trường ĐH NEU', 1800000, N'Tiền mặt', N'Kèm hóa đơn dịch vụ vận chuyển đường bộ');
ELSE
    UPDATE PHIEUCHI SET MaNV = 'NV003', NgayChi = '2026-10-04 11:30:00', NguoiNhan = N'Nhà xe vận tải Hưng Thịnh', LyDoChi = N'Cước vận chuyển thiết bị phòng lab giao trường ĐH NEU', SoTien = 1800000, HinhThuc = N'Tiền mặt', GhiChu = N'Kèm hóa đơn dịch vụ vận chuyển đường bộ' WHERE MaPC = 'PC0000002';

-- PC0000003: Tháng 10 - Mua văn phòng phẩm
IF NOT EXISTS (SELECT 1 FROM PHIEUCHI WHERE MaPC = 'PC0000003')
    INSERT INTO PHIEUCHI (MaPC, MaNV, NgayChi, NguoiNhan, LyDoChi, SoTien, HinhThuc, GhiChu)
    VALUES ('PC0000003', 'NV004', '2026-10-06 10:15:00', N'Phạm Thị Toán (Kế toán)', N'Mua giấy in A4, bút viết, bìa còng lưu trữ chứng từ bán hàng', 650000, N'Tiền mặt', N'Hóa đơn bán lẻ Công ty CP VPP Hồng Hà');
ELSE
    UPDATE PHIEUCHI SET MaNV = 'NV004', NgayChi = '2026-10-06 10:15:00', NguoiNhan = N'Phạm Thị Toán (Kế toán)', LyDoChi = N'Mua giấy in A4, bút viết, bìa còng lưu trữ chứng từ bán hàng', SoTien = 650000, HinhThuc = N'Tiền mặt', GhiChu = N'Hóa đơn bán lẻ Công ty CP VPP Hồng Hà' WHERE MaPC = 'PC0000003';

-- PC0000004: Tháng 11 - Chi tiếp đối tác Á Châu Đà Nẵng
IF NOT EXISTS (SELECT 1 FROM PHIEUCHI WHERE MaPC = 'PC0000004')
    INSERT INTO PHIEUCHI (MaPC, MaNV, NgayChi, NguoiNhan, LyDoChi, SoTien, HinhThuc, GhiChu)
    VALUES ('PC0000004', 'NV005', '2026-11-03 16:30:00', N'Hoàng Minh Đức (Kinh doanh)', N'Chi phí tiếp đối tác Tập đoàn Á Châu tại Đà Nẵng', 3200000, N'Tiền mặt', N'Theo giấy đề nghị tạm ứng công tác phí miền Trung');
ELSE
    UPDATE PHIEUCHI SET MaNV = 'NV005', NgayChi = '2026-11-03 16:30:00', NguoiNhan = N'Hoàng Minh Đức (Kinh doanh)', LyDoChi = N'Chi phí tiếp đối tác Tập đoàn Á Châu tại Đà Nẵng', SoTien = 3200000, HinhThuc = N'Tiền mặt', GhiChu = N'Theo giấy đề nghị tạm ứng công tác phí miền Trung' WHERE MaPC = 'PC0000004';

-- PC0000005: Tháng 11 - Cước vận chuyển giao hàng Chuỗi Toàn Cầu
IF NOT EXISTS (SELECT 1 FROM PHIEUCHI WHERE MaPC = 'PC0000005')
    INSERT INTO PHIEUCHI (MaPC, MaNV, NgayChi, NguoiNhan, LyDoChi, SoTien, HinhThuc, GhiChu)
    VALUES ('PC0000005', 'NV003', '2026-11-06 17:00:00', N'Đội xe vận tải Viettel Post', N'Cước vận chuyển hàng cho Chuỗi Cửa Hàng Toàn Cầu', 2100000, N'Tiền mặt', N'Phiếu thu tiền cước vận chuyển đường bộ');
ELSE
    UPDATE PHIEUCHI SET MaNV = 'NV003', NgayChi = '2026-11-06 17:00:00', NguoiNhan = N'Đội xe vận tải Viettel Post', LyDoChi = N'Cước vận chuyển hàng cho Chuỗi Cửa Hàng Toàn Cầu', SoTien = 2100000, HinhThuc = N'Tiền mặt', GhiChu = N'Phiếu thu tiền cước vận chuyển đường bộ' WHERE MaPC = 'PC0000005';

-- PC0000006: Tháng 11 - Tiền điện thoại internet văn phòng tháng 11
IF NOT EXISTS (SELECT 1 FROM PHIEUCHI WHERE MaPC = 'PC0000006')
    INSERT INTO PHIEUCHI (MaPC, MaNV, NgayChi, NguoiNhan, LyDoChi, SoTien, HinhThuc, GhiChu)
    VALUES ('PC0000006', 'NV004', '2026-11-10 09:30:00', N'VNPT Vinaphone', N'Thanh toán tiền điện thoại internet văn phòng tháng 11/2026', 1200000, N'Tiền mặt', N'Hóa đơn điện tử cước viễn thông tháng 11');
ELSE
    UPDATE PHIEUCHI SET MaNV = 'NV004', NgayChi = '2026-11-10 09:30:00', NguoiNhan = N'VNPT Vinaphone', LyDoChi = N'Thanh toán tiền điện thoại internet văn phòng tháng 11/2026', SoTien = 1200000, HinhThuc = N'Tiền mặt', GhiChu = N'Hóa đơn điện tử cước viễn thông tháng 11' WHERE MaPC = 'PC0000006';
GO

-- ============================================================================
-- 14. NẠP DANH MỤC CHỨNG TỪ KẾ TOÁN & ĐỊNH KHOẢN (CHUNGTU, CHITIETCHUNGTU)
-- ============================================================================
PRINT N'==> Đang nạp danh mục Chứng Từ Kế Toán (Tháng 10 & Tháng 11/2026)...';

-- CT0000001: Tháng 10 - Hạch toán nghiệp vụ bán hàng HDB0000001 (Cty Minh Khang)
IF NOT EXISTS (SELECT 1 FROM CHUNGTU WHERE MaCT = 'CT0000001')
    INSERT INTO CHUNGTU (MaCT, MaNV, MaHDB, NgayCT, LoaiCT, DienGiai)
    VALUES ('CT0000001', 'NV004', 'HDB0000001', '2026-10-03 11:30:00', N'Hóa đơn bán hàng', N'Hạch toán doanh thu, giá vốn và thu tiền HDB0000001 - Cty Minh Khang');
ELSE
    UPDATE CHUNGTU SET MaNV = 'NV004', MaHDB = 'HDB0000001', NgayCT = '2026-10-03 11:30:00', LoaiCT = N'Hóa đơn bán hàng', DienGiai = N'Hạch toán doanh thu, giá vốn và thu tiền HDB0000001 - Cty Minh Khang' WHERE MaCT = 'CT0000001';

DELETE FROM CHITIETCHUNGTU WHERE MaCT = 'CT0000001';
INSERT INTO CHITIETCHUNGTU (MaCT, STT, TaiKhoanNo, TaiKhoanCo, SoTien, DienGiai) VALUES
('CT0000001', 1, '131', '511', 81181818, N'Ghi nhận doanh thu bán hàng chưa VAT (5 Laptop Dell, 3 Máy in Canon)'),
('CT0000001', 2, '131', '3331', 8118182, N'Thuế GTGT đầu ra phải nộp 10%'),
('CT0000001', 3, '1121', '131', 89300000, N'Khách hàng chuyển khoản thanh toán toàn bộ qua ngân hàng VCB'),
('CT0000001', 4, '632', '156', 71400000, N'Giá vốn hàng bán xuất kho tương ứng theo phiếu PXK0000001');

-- CT0000002: Tháng 10 - Hạch toán nghiệp vụ bán hàng HDB0000002 (ĐH Kinh Tế Quốc Dân)
IF NOT EXISTS (SELECT 1 FROM CHUNGTU WHERE MaCT = 'CT0000002')
    INSERT INTO CHUNGTU (MaCT, MaNV, MaHDB, NgayCT, LoaiCT, DienGiai)
    VALUES ('CT0000002', 'NV004', 'HDB0000002', '2026-10-06 15:00:00', N'Hóa đơn bán hàng', N'Hạch toán doanh thu, thuế, tạm ứng tiền và giá vốn HDB0000002 - ĐH NEU');
ELSE
    UPDATE CHUNGTU SET MaNV = 'NV004', MaHDB = 'HDB0000002', NgayCT = '2026-10-06 15:00:00', LoaiCT = N'Hóa đơn bán hàng', DienGiai = N'Hạch toán doanh thu, thuế, tạm ứng tiền và giá vốn HDB0000002 - ĐH NEU' WHERE MaCT = 'CT0000002';

DELETE FROM CHITIETCHUNGTU WHERE MaCT = 'CT0000002';
INSERT INTO CHITIETCHUNGTU (MaCT, STT, TaiKhoanNo, TaiKhoanCo, SoTien, DienGiai) VALUES
('CT0000002', 1, '131', '511', 134181818, N'Ghi nhận doanh thu bán hàng chưa VAT (8 TV Samsung, 8 Màn hình Dell)'),
('CT0000002', 2, '131', '3331', 13418182, N'Thuế GTGT đầu ra phải nộp 10%'),
('CT0000002', 3, '1121', '131', 70000000, N'Khách hàng tạm ứng tiền đợt 1 qua ngân hàng theo PT0000002'),
('CT0000002', 4, '632', '156', 118000000, N'Giá vốn hàng bán xuất kho theo phiếu PXK0000002');

-- CT0000003: Tháng 10 - Hạch toán nghiệp vụ bán hàng HDB0000003 (Cty Hòa Bình)
IF NOT EXISTS (SELECT 1 FROM CHUNGTU WHERE MaCT = 'CT0000003')
    INSERT INTO CHUNGTU (MaCT, MaNV, MaHDB, NgayCT, LoaiCT, DienGiai)
    VALUES ('CT0000003', 'NV004', 'HDB0000003', '2026-10-06 16:00:00', N'Hóa đơn bán hàng', N'Hạch toán doanh thu, thuế và giá vốn HDB0000003 - Cty Hòa Bình');
ELSE
    UPDATE CHUNGTU SET MaNV = 'NV004', MaHDB = 'HDB0000003', NgayCT = '2026-10-06 16:00:00', LoaiCT = N'Hóa đơn bán hàng', DienGiai = N'Hạch toán doanh thu, thuế và giá vốn HDB0000003 - Cty Hòa Bình' WHERE MaCT = 'CT0000003';

DELETE FROM CHITIETCHUNGTU WHERE MaCT = 'CT0000003';
INSERT INTO CHITIETCHUNGTU (MaCT, STT, TaiKhoanNo, TaiKhoanCo, SoTien, DienGiai) VALUES
('CT0000003', 1, '131', '511', 46363636, N'Ghi nhận doanh thu bán hàng chưa VAT (4 TV LG, 10 Chuột phím Logitech)'),
('CT0000003', 2, '131', '3331', 4636364, N'Thuế GTGT đầu ra phải nộp 10%'),
('CT0000003', 3, '632', '156', 40800000, N'Giá vốn hàng bán xuất kho theo phiếu PXK0000003');

-- CT0000004: Tháng 11 - Hạch toán nghiệp vụ bán hàng HDB0000004 (Tập Đoàn Á Châu)
IF NOT EXISTS (SELECT 1 FROM CHUNGTU WHERE MaCT = 'CT0000004')
    INSERT INTO CHUNGTU (MaCT, MaNV, MaHDB, NgayCT, LoaiCT, DienGiai)
    VALUES ('CT0000004', 'NV004', 'HDB0000004', '2026-11-04 14:00:00', N'Hóa đơn bán hàng', N'Hạch toán doanh thu, giá vốn và thanh toán HDB0000004 - Tập Đoàn Á Châu');
ELSE
    UPDATE CHUNGTU SET MaNV = 'NV004', MaHDB = 'HDB0000004', NgayCT = '2026-11-04 14:00:00', LoaiCT = N'Hóa đơn bán hàng', DienGiai = N'Hạch toán doanh thu, giá vốn và thanh toán HDB0000004 - Tập Đoàn Á Châu' WHERE MaCT = 'CT0000004';

DELETE FROM CHITIETCHUNGTU WHERE MaCT = 'CT0000004';
INSERT INTO CHITIETCHUNGTU (MaCT, STT, TaiKhoanNo, TaiKhoanCo, SoTien, DienGiai) VALUES
('CT0000004', 1, '131', '511', 57754545, N'Ghi nhận doanh thu bán hàng chưa VAT (3 Tủ lạnh Panasonic, 4 Điều hòa Casper)'),
('CT0000004', 2, '131', '3331', 5775455, N'Thuế GTGT đầu ra phải nộp 10%'),
('CT0000004', 3, '1121', '131', 63530000, N'Khách hàng chuyển khoản BIDV thanh toán toàn bộ theo PT0000003'),
('CT0000004', 4, '632', '156', 50800000, N'Giá vốn hàng bán xuất kho theo phiếu PXK0000004');

-- CT0000005: Tháng 11 - Hạch toán nghiệp vụ bán hàng HDB0000005 (Chuỗi Toàn Cầu)
IF NOT EXISTS (SELECT 1 FROM CHUNGTU WHERE MaCT = 'CT0000005')
    INSERT INTO CHUNGTU (MaCT, MaNV, MaHDB, NgayCT, LoaiCT, DienGiai)
    VALUES ('CT0000005', 'NV004', 'HDB0000005', '2026-11-08 10:30:00', N'Hóa đơn bán hàng', N'Hạch toán doanh thu, thuế, tạm ứng và giá vốn HDB0000005 - Chuỗi Toàn Cầu');
ELSE
    UPDATE CHUNGTU SET MaNV = 'NV004', MaHDB = 'HDB0000005', NgayCT = '2026-11-08 10:30:00', LoaiCT = N'Hóa đơn bán hàng', DienGiai = N'Hạch toán doanh thu, thuế, tạm ứng và giá vốn HDB0000005 - Chuỗi Toàn Cầu' WHERE MaCT = 'CT0000005';

DELETE FROM CHITIETCHUNGTU WHERE MaCT = 'CT0000005';
INSERT INTO CHITIETCHUNGTU (MaCT, STT, TaiKhoanNo, TaiKhoanCo, SoTien, DienGiai) VALUES
('CT0000005', 1, '131', '511', 81327273, N'Ghi nhận doanh thu chưa VAT (4 Máy giặt LG, 5 Máy hút bụi Dyson, 10 Hộp mực HP)'),
('CT0000005', 2, '131', '3331', 8132727, N'Thuế GTGT đầu ra phải nộp 10%'),
('CT0000005', 3, '1121', '131', 45000000, N'Khách hàng tạm ứng tiền đợt 1 qua ngân hàng theo PT0000004'),
('CT0000005', 4, '632', '156', 71500000, N'Giá vốn hàng bán xuất kho theo phiếu PXK0000005');

-- CT0000006: Tháng 11 - Hạch toán nghiệp vụ bán hàng HDB0000006 (Cty Phương Nam)
IF NOT EXISTS (SELECT 1 FROM CHUNGTU WHERE MaCT = 'CT0000006')
    INSERT INTO CHUNGTU (MaCT, MaNV, MaHDB, NgayCT, LoaiCT, DienGiai)
    VALUES ('CT0000006', 'NV004', 'HDB0000006', '2026-11-12 16:00:00', N'Hóa đơn bán hàng', N'Hạch toán doanh thu, thuế và giá vốn HDB0000006 - Cty Phương Nam');
ELSE
    UPDATE CHUNGTU SET MaNV = 'NV004', MaHDB = 'HDB0000006', NgayCT = '2026-11-12 16:00:00', LoaiCT = N'Hóa đơn bán hàng', DienGiai = N'Hạch toán doanh thu, thuế và giá vốn HDB0000006 - Cty Phương Nam' WHERE MaCT = 'CT0000006';

DELETE FROM CHITIETCHUNGTU WHERE MaCT = 'CT0000006';
INSERT INTO CHITIETCHUNGTU (MaCT, STT, TaiKhoanNo, TaiKhoanCo, SoTien, DienGiai) VALUES
('CT0000006', 1, '131', '511', 31436364, N'Ghi nhận doanh thu bán hàng chưa VAT (2 Laptop Dell, 50 Ram giấy in A4)'),
('CT0000006', 2, '131', '3331', 3143636, N'Thuế GTGT đầu ra phải nộp 10%'),
('CT0000006', 3, '632', '156', 27600000, N'Giá vốn hàng bán xuất kho theo phiếu PXK0000006');
GO

-- ============================================================================
-- 15. CẬP NHẬT LẠI TỒN KHO THỰC TẾ SAU CÁC ĐỢT XUẤT KHO MẪU (PXK0000001 -> PXK0000006)
-- ============================================================================
PRINT N'==> Cập nhật lại tồn kho thực tế phản ánh chính xác các phiếu xuất đã lập...';

-- KHO01: SP003 xuất 3 máy (45 - 3 = 42), SP004 xuất 8 TV (35 - 8 = 27), SP006 xuất 5 laptop (40 - 5 = 35), SP009 xuất 8 màn hình (55 - 8 = 47)
UPDATE TONKHO SET SoLuongTon = 42, NgayCapNhat = '2026-10-02 14:00:00' WHERE MaKho = 'KHO01' AND MaSP = 'SP003';
UPDATE TONKHO SET SoLuongTon = 27, NgayCapNhat = '2026-10-05 10:00:00' WHERE MaKho = 'KHO01' AND MaSP = 'SP004';
UPDATE TONKHO SET SoLuongTon = 35, NgayCapNhat = '2026-10-02 14:00:00' WHERE MaKho = 'KHO01' AND MaSP = 'SP006';
UPDATE TONKHO SET SoLuongTon = 47, NgayCapNhat = '2026-10-05 10:00:00' WHERE MaKho = 'KHO01' AND MaSP = 'SP009';

-- KHO02: SP005 xuất 4 (20 - 4 = 16), SP006 xuất 2 (30 - 2 = 28), SP010 xuất 2 (18 - 2 = 16), SP011 xuất 5 (35 - 5 = 30), SP012 xuất 50 (280 - 50 = 230), SP013 xuất 10 (95 - 10 = 85), SP014 xuất 10 (140 - 10 = 130)
UPDATE TONKHO SET SoLuongTon = 16, NgayCapNhat = '2026-11-06 14:30:00' WHERE MaKho = 'KHO02' AND MaSP = 'SP005';
UPDATE TONKHO SET SoLuongTon = 28, NgayCapNhat = '2026-11-12 15:00:00' WHERE MaKho = 'KHO02' AND MaSP = 'SP006';
UPDATE TONKHO SET SoLuongTon = 16, NgayCapNhat = '2026-10-06 15:30:00' WHERE MaKho = 'KHO02' AND MaSP = 'SP010';
UPDATE TONKHO SET SoLuongTon = 30, NgayCapNhat = '2026-11-06 14:30:00' WHERE MaKho = 'KHO02' AND MaSP = 'SP011';
UPDATE TONKHO SET SoLuongTon = 230, NgayCapNhat = '2026-11-12 15:00:00' WHERE MaKho = 'KHO02' AND MaSP = 'SP012';
UPDATE TONKHO SET SoLuongTon = 85, NgayCapNhat = '2026-11-06 14:30:00' WHERE MaKho = 'KHO02' AND MaSP = 'SP013';
UPDATE TONKHO SET SoLuongTon = 130, NgayCapNhat = '2026-10-06 15:30:00' WHERE MaKho = 'KHO02' AND MaSP = 'SP014';

-- KHO03: SP007 xuất 4 (10 - 4 = 6, mức tồn an toàn để demo cảnh báo), SP008 xuất 3 (35 - 3 = 32)
UPDATE TONKHO SET SoLuongTon = 6, NgayCapNhat = '2026-11-03 15:00:00' WHERE MaKho = 'KHO03' AND MaSP = 'SP007';
UPDATE TONKHO SET SoLuongTon = 32, NgayCapNhat = '2026-11-03 15:00:00' WHERE MaKho = 'KHO03' AND MaSP = 'SP008';
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
PRINT N'- 8 Đơn đặt hàng mẫu phân bổ Tháng 10 & Tháng 11/2026';
PRINT N'- 6 Hóa đơn bán hàng thương mại thực tế (Doanh thu Tháng 10 & Tháng 11)';
PRINT N'- 6 Phiếu xuất kho theo hóa đơn đã lập';
PRINT N'- 4 Phiếu thu tiền khách hàng (Thu 100% & Tạm ứng đợt 1)';
PRINT N'- 6 Phiếu chi chi phí hoạt động doanh nghiệp Tháng 10 & 11';
PRINT N'- 6 Chứng từ kế toán hoàn chỉnh kèm định khoản Nợ/Có chuẩn VAS';
PRINT N'================================================================================';

