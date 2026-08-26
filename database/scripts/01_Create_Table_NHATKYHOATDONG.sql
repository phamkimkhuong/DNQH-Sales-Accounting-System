-- ============================================================================
-- Migration: 01_Create_Table_NHATKYHOATDONG.sql
-- Mục đích: Tạo bảng Nhật ký hoạt động doanh nghiệp tập trung (Business Audit Trail)
--           cho mô hình mạng LAN Client-Server đa máy trạm, kèm cơ chế dọn dẹp log quá hạn.
-- Tác giả: Senior Architecture Team
-- Ngày: 2026-09-08
-- ============================================================================

USE DNQH_KeToanBanHang;
GO

-- 1. Tạo bảng NHATKYHOATDONG nếu chưa tồn tại
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'NHATKYHOATDONG')
BEGIN
    CREATE TABLE NHATKYHOATDONG (
        Id BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        ThoiGian DATETIME2(3) NOT NULL CONSTRAINT DF_NHATKYHOATDONG_ThoiGian DEFAULT SYSDATETIME(),
        CapDo NVARCHAR(20) NOT NULL CONSTRAINT DF_NHATKYHOATDONG_CapDo DEFAULT 'INFO',
        HanhDong NVARCHAR(100) NOT NULL,
        MaNV NVARCHAR(20) NULL,
        TenNV NVARCHAR(100) NULL,
        VaiTro NVARCHAR(50) NULL,
        TenMay NVARCHAR(100) NULL,
        LoaiDoiTuong NVARCHAR(50) NULL,
        MaDoiTuong NVARCHAR(50) NULL,
        KetQua NVARCHAR(30) NOT NULL CONSTRAINT DF_NHATKYHOATDONG_KetQua DEFAULT N'Thành công',
        ThoiGianXuLyMs BIGINT NULL CONSTRAINT DF_NHATKYHOATDONG_Duration DEFAULT 0,
        CorrelationId NVARCHAR(64) NULL,
        NoiDung NVARCHAR(MAX) NULL
    );

    PRINT N'Bảng NHATKYHOATDONG đã được tạo thành công.';
END
ELSE
BEGIN
    PRINT N'Bảng NHATKYHOATDONG đã tồn tại.';
END
GO

-- 2. Tạo các Index tối ưu hóa truy vấn tra cứu
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_NHATKYHOATDONG_ThoiGian' AND object_id = OBJECT_ID('NHATKYHOATDONG'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_NHATKYHOATDONG_ThoiGian 
    ON NHATKYHOATDONG(ThoiGian DESC);
    PRINT N'Đã tạo Index IX_NHATKYHOATDONG_ThoiGian.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_NHATKYHOATDONG_MaNV' AND object_id = OBJECT_ID('NHATKYHOATDONG'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_NHATKYHOATDONG_MaNV 
    ON NHATKYHOATDONG(MaNV);
    PRINT N'Đã tạo Index IX_NHATKYHOATDONG_MaNV.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_NHATKYHOATDONG_HanhDong' AND object_id = OBJECT_ID('NHATKYHOATDONG'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_NHATKYHOATDONG_HanhDong 
    ON NHATKYHOATDONG(HanhDong);
    PRINT N'Đã tạo Index IX_NHATKYHOATDONG_HanhDong.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_NHATKYHOATDONG_CorrelationId' AND object_id = OBJECT_ID('NHATKYHOATDONG'))
BEGIN
    CREATE NONCLUSTERED INDEX IX_NHATKYHOATDONG_CorrelationId 
    ON NHATKYHOATDONG(CorrelationId) 
    WHERE CorrelationId IS NOT NULL;
    PRINT N'Đã tạo Index IX_NHATKYHOATDONG_CorrelationId.';
END
GO

-- 3. Stored Procedure tự động dọn dẹp các bản ghi log quá hạn (mặc định 30 ngày)
CREATE OR ALTER PROCEDURE sp_DonDepNhatKyHoatDong
    @SoNgayLuuTru INT = 30,
    @SoDongDaXoa INT = 0 OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @SoNgayLuuTru < 1
    BEGIN
        SET @SoNgayLuuTru = 30;
    END

    DECLARE @CutoffDate DATETIME2(3) = DATEADD(DAY, -@SoNgayLuuTru, SYSDATETIME());
    
    DELETE FROM NHATKYHOATDONG
    WHERE ThoiGian < @CutoffDate;
    
    SET @SoDongDaXoa = @@ROWCOUNT;
    
    PRINT N'Đã dọn dẹp ' + CAST(@SoDongDaXoa AS NVARCHAR(20)) + N' dòng nhật ký cũ hơn ' + CAST(@SoNgayLuuTru AS NVARCHAR(10)) + N' ngày.';
END
GO
