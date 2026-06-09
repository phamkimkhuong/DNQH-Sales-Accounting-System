/*================================================================================
   HỆ THỐNG THÔNG TIN KẾ TOÁN - KẾ TOÁN BÁN HÀNG (DNQH)
   FILE 3: SCRIPT TẠO DATABASE KIỂM THỬ TỰ ĐỘNG (AUTOMATED TEST ISOLATION)
================================================================================
   1. MỤC ĐÍCH:
      - Tạo ra một Database riêng biệt có tên "DNQH_KeToanBanHang_Test" từ bản sao
        của Database chính "DNQH_KeToanBanHang".
      - MỤC TIÊU CỐT LÕI: Cách ly hoàn toàn môi trường kiểm thử tự động (Automated Tests)
        với môi trường hoạt động thực tế. Khi bộ test (trong scripts/run_tests.bat)
        chạy kiểm tra các giao dịch, toàn bộ dữ liệu thử nghiệm chỉ tác động trên
        database "_Test", giúp Database chính của khách hàng luôn sạch sẽ 100%.

   2. THỨ TỰ THỰC THI:
      - BƯỚC 3 (Tùy chọn: Chỉ cần chạy khi bạn hoặc hội đồng nghiệm thu muốn thực thi
        bộ kiểm thử tự động toàn diện scripts/run_tests.bat).

   3. HƯỚNG DẪN SỬ DỤNG:
      - Chạy sau khi đã thực thi xong File 1 và File 2.
      - Mở file này trong SQL Server Management Studio (SSMS) dưới quyền sysadmin / sa.
      - Bấm nút "Execute" (hoặc phím F5).

   4. CƠ CHẾ AN TOÀN:
      - Tự động lấy đường dẫn lưu trữ file (.mdf, .ldf, .bak) mặc định của SQL Server.
      - Nếu database "DNQH_KeToanBanHang_Test" đã tồn tại, script sẽ dừng lại an toàn
        và không tự ý ghi đè dữ liệu.
================================================================================*/

USE [master];
SET NOCOUNT ON;

DECLARE @SourceDatabase sysname = N'DNQH_KeToanBanHang';
DECLARE @TestDatabase sysname = N'DNQH_KeToanBanHang_Test';
DECLARE @BackupRoot nvarchar(4000) = CONVERT(nvarchar(4000), SERVERPROPERTY('InstanceDefaultBackupPath'));
DECLARE @DataRoot nvarchar(4000) = CONVERT(nvarchar(4000), SERVERPROPERTY('InstanceDefaultDataPath'));
DECLARE @SourceDataLogical sysname;
DECLARE @SourceLogLogical sysname;
DECLARE @BackupFile nvarchar(4000);
DECLARE @TestDataFile nvarchar(4000);
DECLARE @TestLogFile nvarchar(4000);
DECLARE @Sql nvarchar(max);

IF DB_ID(@SourceDatabase) IS NULL
    THROW 51000, N'Không tìm thấy database nguồn DNQH_KeToanBanHang.', 1;

IF RIGHT(@BackupRoot, 1) NOT IN (N'\', N'/') SET @BackupRoot += N'\';
IF RIGHT(@DataRoot, 1) NOT IN (N'\', N'/') SET @DataRoot += N'\';

SET @BackupFile = @BackupRoot + N'DNQH_KeToanBanHang_TestSeed.bak';
SET @TestDataFile = @DataRoot + N'DNQH_KeToanBanHang_Test.mdf';
SET @TestLogFile = @DataRoot + N'DNQH_KeToanBanHang_Test_log.ldf';

SELECT TOP (1) @SourceDataLogical = name
FROM sys.master_files
WHERE database_id = DB_ID(@SourceDatabase) AND type = 0
ORDER BY file_id;

SELECT TOP (1) @SourceLogLogical = name
FROM sys.master_files
WHERE database_id = DB_ID(@SourceDatabase) AND type = 1
ORDER BY file_id;

IF DB_ID(@TestDatabase) IS NOT NULL
    THROW 51001, N'Database test đã tồn tại. Script an toàn không tự xóa hoặc ghi đè database hiện có.', 1;

SET @Sql = N'BACKUP DATABASE ' + QUOTENAME(@SourceDatabase) +
           N' TO DISK = @BackupFile WITH COPY_ONLY, INIT, CHECKSUM;';
EXEC sys.sp_executesql @Sql, N'@BackupFile nvarchar(4000)', @BackupFile;

SET @Sql = N'RESTORE DATABASE ' + QUOTENAME(@TestDatabase) +
           N' FROM DISK = @BackupFile WITH RECOVERY, ' +
           N'MOVE @SourceDataLogical TO @TestDataFile, ' +
           N'MOVE @SourceLogLogical TO @TestLogFile;';
EXEC sys.sp_executesql @Sql,
    N'@BackupFile nvarchar(4000), @SourceDataLogical sysname, @TestDataFile nvarchar(4000), @SourceLogLogical sysname, @TestLogFile nvarchar(4000)',
    @BackupFile, @SourceDataLogical, @TestDataFile, @SourceLogLogical, @TestLogFile;

PRINT N'Đã tạo database DNQH_KeToanBanHang_Test từ bản sao cục bộ.';
