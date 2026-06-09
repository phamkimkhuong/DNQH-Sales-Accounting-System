/*================================================================================
   HỆ THỐNG THÔNG TIN KẾ TOÁN - KẾ TOÁN BÁN HÀNG (DNQH)
   FILE 1: SCRIPT KHỞI TẠO CƠ SỞ DỮ LIỆU & RÀNG BUỘC TOÀN VẸN (SCHEMA DDL)
================================================================================
   1. MỤC ĐÍCH:
      - Định nghĩa cấu trúc khung xương (Schema) cho toàn bộ hệ thống gồm 18 bảng.
      - Thiết lập các khóa chính (Primary Key), chỉ mục (Index), khóa ngoại (Foreign Key)
        và ràng buộc duy nhất (Unique Constraint) đảm bảo tính toàn vẹn dữ liệu kế toán.

   2. THỨ TỰ THỰC THI TRÊN HỆ THỐNG:
      - BƯỚC 1 (Bắt buộc chạy đầu tiên khi triển khai trên máy tính / server mới).

   3. HƯỚNG DẪN SỬ DỤNG CHO KHÁCH HÀNG / QUẢN TRỊ VIÊN:
      - Bước 1.1: Mở SQL Server Management Studio (SSMS) và kết nối tới SQL Server.
      - Bước 1.2: Nếu Database chưa được tạo, bỏ comment 3 dòng bên dưới để tạo:
            CREATE DATABASE DNQH_KeToanBanHang;
            GO
      - Bước 1.3: Mở file này trong SSMS và bấm nút "Execute" (hoặc phím F5).

   4. DANH SÁCH 18 BẢNG ĐƯỢC TẠO:
      - Danh mục gốc: KHACHHANG, NHACUNGCAP, LOAISANPHAM, SANPHAM, KHO, NHANVIEN, TAIKHOAN
      - Nghiệp vụ bán hàng: DONDATHANG, CHITIETDONDATHANG, HOADONBAN, CHITIETHOADONBAN
      - Quản lý kho: PHIEUXUATKHO, CHITIETPHIEUXUATKHO, TONKHO
      - Nghiệp vụ kế toán: PHIEUTHU, PHIEUCHI, CHUNGTU, CHITIETCHUNGTU
================================================================================*/

-- Kiểm tra và tạo database nếu chưa có
IF DB_ID('DNQH_KeToanBanHang') IS NULL
BEGIN
    CREATE DATABASE DNQH_KeToanBanHang;
END
GO

USE DNQH_KeToanBanHang;
GO

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIETCHUNGTU') and o.name = 'FK_CHITIETC_GOM_CTCT_CHUNGTU')
alter table CHITIETCHUNGTU
   drop constraint FK_CHITIETC_GOM_CTCT_CHUNGTU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIETDONDATHANG') and o.name = 'FK_CHITIETD_CO_SP_DAT_SANPHAM')
alter table CHITIETDONDATHANG
   drop constraint FK_CHITIETD_CO_SP_DAT_SANPHAM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIETDONDATHANG') and o.name = 'FK_CHITIETD_GOM_CTDAT_DONDATHA')
alter table CHITIETDONDATHANG
   drop constraint FK_CHITIETD_GOM_CTDAT_DONDATHA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIETHOADONBAN') and o.name = 'FK_CHITIETH_CO_SANPHA_SANPHAM')
alter table CHITIETHOADONBAN
   drop constraint FK_CHITIETH_CO_SANPHA_SANPHAM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIETHOADONBAN') and o.name = 'FK_CHITIETH_GOM_CHITI_HOADONBA')
alter table CHITIETHOADONBAN
   drop constraint FK_CHITIETH_GOM_CHITI_HOADONBA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIETPHIEUXUATKHO') and o.name = 'FK_CHITIETP_GOM_CTPXK_PHIEUXUA')
alter table CHITIETPHIEUXUATKHO
   drop constraint FK_CHITIETP_GOM_CTPXK_PHIEUXUA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIETPHIEUXUATKHO') and o.name = 'FK_CHITIETP_XUAT_SANP_SANPHAM')
alter table CHITIETPHIEUXUATKHO
   drop constraint FK_CHITIETP_XUAT_SANP_SANPHAM
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHUNGTU') and o.name = 'FK_CHUNGTU_LAP_CT_NHANVIEN')
alter table CHUNGTU
   drop constraint FK_CHUNGTU_LAP_CT_NHANVIEN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHUNGTU') and o.name = 'FK_CHUNGTU_PHATSINH__HOADONBA')
alter table CHUNGTU
   drop constraint FK_CHUNGTU_PHATSINH__HOADONBA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DONDATHANG') and o.name = 'FK_DONDATHA_DAT_HANG_KHACHHAN')
alter table DONDATHANG
   drop constraint FK_DONDATHA_DAT_HANG_KHACHHAN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DONDATHANG') and o.name = 'FK_DONDATHA_LAP_DATHA_NHANVIEN')
alter table DONDATHANG
   drop constraint FK_DONDATHA_LAP_DATHA_NHANVIEN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADONBAN') and o.name = 'FK_HOADONBA_LAP_HOADO_NHANVIEN')
alter table HOADONBAN
   drop constraint FK_HOADONBA_LAP_HOADO_NHANVIEN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADONBAN') and o.name = 'FK_HOADONBA_MUA_HANG_KHACHHAN')
alter table HOADONBAN
   drop constraint FK_HOADONBA_MUA_HANG_KHACHHAN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADONBAN') and o.name = 'FK_HOADONBA_PHATSINH__DONDATHA')
alter table HOADONBAN
   drop constraint FK_HOADONBA_PHATSINH__DONDATHA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PHIEUCHI') and o.name = 'FK_PHIEUCHI_LAP_PHIEU_NHANVIEN')
alter table PHIEUCHI
   drop constraint FK_PHIEUCHI_LAP_PHIEU_NHANVIEN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PHIEUTHU') and o.name = 'FK_PHIEUTHU_LAP_PHIEU_NHANVIEN')
alter table PHIEUTHU
   drop constraint FK_PHIEUTHU_LAP_PHIEU_NHANVIEN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PHIEUTHU') and o.name = 'FK_PHIEUTHU_THU_THEO__HOADONBA')
alter table PHIEUTHU
   drop constraint FK_PHIEUTHU_THU_THEO__HOADONBA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PHIEUXUATKHO') and o.name = 'FK_PHIEUXUA_LAP_PHIEU_NHANVIEN')
alter table PHIEUXUATKHO
   drop constraint FK_PHIEUXUA_LAP_PHIEU_NHANVIEN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PHIEUXUATKHO') and o.name = 'FK_PHIEUXUA_XUAT_THEO_HOADONBA')
alter table PHIEUXUATKHO
   drop constraint FK_PHIEUXUA_XUAT_THEO_HOADONBA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PHIEUXUATKHO') and o.name = 'FK_PHIEUXUA_XUAT_TUKH_KHO')
alter table PHIEUXUATKHO
   drop constraint FK_PHIEUXUA_XUAT_TUKH_KHO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SANPHAM') and o.name = 'FK_SANPHAM_CUNG_CAP_NHACUNGC')
alter table SANPHAM
   drop constraint FK_SANPHAM_CUNG_CAP_NHACUNGC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SANPHAM') and o.name = 'FK_SANPHAM_THUOC_LOA_LOAISANP')
alter table SANPHAM
   drop constraint FK_SANPHAM_THUOC_LOA_LOAISANP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('TAIKHOAN') and o.name = 'FK_TAIKHOAN_CO_TAIKHO_NHANVIEN')
alter table TAIKHOAN
   drop constraint FK_TAIKHOAN_CO_TAIKHO_NHANVIEN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('TONKHO') and o.name = 'FK_TONKHO_CO_TONKHO_KHO')
alter table TONKHO
   drop constraint FK_TONKHO_CO_TONKHO_KHO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('TONKHO') and o.name = 'FK_TONKHO_TON_SANPH_SANPHAM')
alter table TONKHO
   drop constraint FK_TONKHO_TON_SANPH_SANPHAM
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIETCHUNGTU')
            and   name  = 'Gom_CTCT_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIETCHUNGTU.Gom_CTCT_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHITIETCHUNGTU')
            and   type = 'U')
   drop table CHITIETCHUNGTU
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIETDONDATHANG')
            and   name  = 'Co_SP_DatHang_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIETDONDATHANG.Co_SP_DatHang_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIETDONDATHANG')
            and   name  = 'Gom_CTDatHang_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIETDONDATHANG.Gom_CTDatHang_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHITIETDONDATHANG')
            and   type = 'U')
   drop table CHITIETDONDATHANG
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIETHOADONBAN')
            and   name  = 'Co_SanPham_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIETHOADONBAN.Co_SanPham_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIETHOADONBAN')
            and   name  = 'Gom_ChiTiet_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIETHOADONBAN.Gom_ChiTiet_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHITIETHOADONBAN')
            and   type = 'U')
   drop table CHITIETHOADONBAN
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIETPHIEUXUATKHO')
            and   name  = 'Xuat_SanPham_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIETPHIEUXUATKHO.Xuat_SanPham_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIETPHIEUXUATKHO')
            and   name  = 'Gom_CTPXK_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIETPHIEUXUATKHO.Gom_CTPXK_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHITIETPHIEUXUATKHO')
            and   type = 'U')
   drop table CHITIETPHIEUXUATKHO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHUNGTU')
            and   name  = 'PhatSinh_CT_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHUNGTU.PhatSinh_CT_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHUNGTU')
            and   name  = 'Lap_CT_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHUNGTU.Lap_CT_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHUNGTU')
            and   type = 'U')
   drop table CHUNGTU
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('DONDATHANG')
            and   name  = 'Lap_DatHang_FK'
            and   indid > 0
            and   indid < 255)
   drop index DONDATHANG.Lap_DatHang_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('DONDATHANG')
            and   name  = 'Dat_Hang_FK'
            and   indid > 0
            and   indid < 255)
   drop index DONDATHANG.Dat_Hang_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DONDATHANG')
            and   type = 'U')
   drop table DONDATHANG
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOADONBAN')
            and   name  = 'Lap_HoaDon_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOADONBAN.Lap_HoaDon_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOADONBAN')
            and   name  = 'Mua_Hang_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOADONBAN.Mua_Hang_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOADONBAN')
            and   type = 'U')
   drop table HOADONBAN
go

if exists (select 1
            from  sysobjects
           where  id = object_id('KHACHHANG')
            and   type = 'U')
   drop table KHACHHANG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('KHO')
            and   type = 'U')
   drop table KHO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LOAISANPHAM')
            and   type = 'U')
   drop table LOAISANPHAM
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NHACUNGCAP')
            and   type = 'U')
   drop table NHACUNGCAP
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NHANVIEN')
            and   type = 'U')
   drop table NHANVIEN
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PHIEUCHI')
            and   name  = 'Lap_PhieuChi_FK'
            and   indid > 0
            and   indid < 255)
   drop index PHIEUCHI.Lap_PhieuChi_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PHIEUCHI')
            and   type = 'U')
   drop table PHIEUCHI
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PHIEUTHU')
            and   name  = 'Lap_PhieuThu_FK'
            and   indid > 0
            and   indid < 255)
   drop index PHIEUTHU.Lap_PhieuThu_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PHIEUTHU')
            and   name  = 'Thu_Theo_HDB_FK'
            and   indid > 0
            and   indid < 255)
   drop index PHIEUTHU.Thu_Theo_HDB_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PHIEUTHU')
            and   type = 'U')
   drop table PHIEUTHU
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PHIEUXUATKHO')
            and   name  = 'Lap_PhieuXuat_FK'
            and   indid > 0
            and   indid < 255)
   drop index PHIEUXUATKHO.Lap_PhieuXuat_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PHIEUXUATKHO')
            and   name  = 'Xuat_Theo_HDB_FK'
            and   indid > 0
            and   indid < 255)
   drop index PHIEUXUATKHO.Xuat_Theo_HDB_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PHIEUXUATKHO')
            and   name  = 'Xuat_TuKho_FK'
            and   indid > 0
            and   indid < 255)
   drop index PHIEUXUATKHO.Xuat_TuKho_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PHIEUXUATKHO')
            and   type = 'U')
   drop table PHIEUXUATKHO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('SANPHAM')
            and   name  = 'Cung_Cap_FK'
            and   indid > 0
            and   indid < 255)
   drop index SANPHAM.Cung_Cap_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('SANPHAM')
            and   name  = 'Thuoc_Loai_FK'
            and   indid > 0
            and   indid < 255)
   drop index SANPHAM.Thuoc_Loai_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SANPHAM')
            and   type = 'U')
   drop table SANPHAM
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('TAIKHOAN')
            and   name  = 'Co_TaiKhoan_FK'
            and   indid > 0
            and   indid < 255)
   drop index TAIKHOAN.Co_TaiKhoan_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TAIKHOAN')
            and   type = 'U')
   drop table TAIKHOAN
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('TONKHO')
            and   name  = 'Ton_SanPham_FK'
            and   indid > 0
            and   indid < 255)
   drop index TONKHO.Ton_SanPham_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('TONKHO')
            and   name  = 'Co_TonKho_FK'
            and   indid > 0
            and   indid < 255)
   drop index TONKHO.Co_TonKho_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TONKHO')
            and   type = 'U')
   drop table TONKHO
go

/*==============================================================*/
/* Table: CHITIETCHUNGTU                                        */
/*==============================================================*/
create table CHITIETCHUNGTU (
   MaCT                 char(12)             not null,
   STT                  int                  not null,
   TaiKhoanNo           varchar(20)          null,
   TaiKhoanCo           varchar(20)          null,
   SoTien               decimal(18,2)        null,
   DienGiai             nvarchar(200)        null,
   constraint PK_CHITIETCHUNGTU primary key (MaCT, STT)
)
go

/*==============================================================*/
/* Index: Gom_CTCT_FK                                           */
/*==============================================================*/




create nonclustered index Gom_CTCT_FK on CHITIETCHUNGTU (MaCT ASC)
go

/*==============================================================*/
/* Table: CHITIETDONDATHANG                                     */
/*==============================================================*/
create table CHITIETDONDATHANG (
   MaDDH                char(10)             not null,
   MaSP                 char(10)             not null,
   SoLuong              int                  null,
   DonGia               decimal(18,2)        null,
   GiamGia              decimal(5,2)         null,
   ThanhTien            decimal(18,2)        null,
   constraint PK_CHITIETDONDATHANG primary key (MaDDH, MaSP)
)
go

/*==============================================================*/
/* Index: Gom_CTDatHang_FK                                      */
/*==============================================================*/




create nonclustered index Gom_CTDatHang_FK on CHITIETDONDATHANG (MaDDH ASC)
go

/*==============================================================*/
/* Index: Co_SP_DatHang_FK                                      */
/*==============================================================*/




create nonclustered index Co_SP_DatHang_FK on CHITIETDONDATHANG (MaSP ASC)
go

/*==============================================================*/
/* Table: CHITIETHOADONBAN                                      */
/*==============================================================*/
create table CHITIETHOADONBAN (
   MaHDB                char(12)             not null,
   MaSP                 char(10)             not null,
   SoLuong              int                  null,
   DonGia               decimal(18,2)        null,
   GiamGia              decimal(5,2)         null,
   ThanhTien            decimal(18,2)        null,
   constraint PK_CHITIETHOADONBAN primary key (MaHDB, MaSP)
)
go

/*==============================================================*/
/* Index: Gom_ChiTiet_FK                                        */
/*==============================================================*/




create nonclustered index Gom_ChiTiet_FK on CHITIETHOADONBAN (MaHDB ASC)
go

/*==============================================================*/
/* Index: Co_SanPham_FK                                         */
/*==============================================================*/




create nonclustered index Co_SanPham_FK on CHITIETHOADONBAN (MaSP ASC)
go

/*==============================================================*/
/* Table: CHITIETPHIEUXUATKHO                                   */
/*==============================================================*/
create table CHITIETPHIEUXUATKHO (
   MaPXK                char(10)             not null,
   MaSP                 char(10)             not null,
   SoLuongXuat          int                  null,
   constraint PK_CHITIETPHIEUXUATKHO primary key (MaPXK, MaSP)
)
go

/*==============================================================*/
/* Index: Gom_CTPXK_FK                                          */
/*==============================================================*/




create nonclustered index Gom_CTPXK_FK on CHITIETPHIEUXUATKHO (MaPXK ASC)
go

/*==============================================================*/
/* Index: Xuat_SanPham_FK                                       */
/*==============================================================*/




create nonclustered index Xuat_SanPham_FK on CHITIETPHIEUXUATKHO (MaSP ASC)
go

/*==============================================================*/
/* Table: CHUNGTU                                               */
/*==============================================================*/
create table CHUNGTU (
   MaCT                 char(12)             not null,
   MaNV                 char(10)             not null,
   MaHDB                char(12)             not null,
   NgayCT               datetime             null,
   LoaiCT               nvarchar(50)         null,
   DienGiai             nvarchar(200)        null,
   constraint PK_CHUNGTU primary key (MaCT)
)
go

/*==============================================================*/
/* Index: Lap_CT_FK                                             */
/*==============================================================*/




create nonclustered index Lap_CT_FK on CHUNGTU (MaNV ASC)
go

/*==============================================================*/
/* Index: PhatSinh_CT_FK                                        */
/*==============================================================*/




create nonclustered index PhatSinh_CT_FK on CHUNGTU (MaHDB ASC)
go

/*==============================================================*/
/* Table: DONDATHANG                                            */
/*==============================================================*/
create table DONDATHANG (
   MaDDH                char(10)             not null,
   MaNV                 char(10)             not null,
   MaKH                 char(10)             not null,
   NgayDat              datetime             null,
   NgayGiaoDuKien       date            null,
   TongTien             decimal(18,2)        null,
   TrangThai            nvarchar(30)         null,
   GhiChu               nvarchar(200)        null,
   Version              int                  not null default 1,
   constraint PK_DONDATHANG primary key (MaDDH)
)
go

/*==============================================================*/
/* Index: Dat_Hang_FK                                           */
/*==============================================================*/




create nonclustered index Dat_Hang_FK on DONDATHANG (MaKH ASC)
go

/*==============================================================*/
/* Index: Lap_DatHang_FK                                        */
/*==============================================================*/




create nonclustered index Lap_DatHang_FK on DONDATHANG (MaNV ASC)
go

/*==============================================================*/
/* Table: HOADONBAN                                             */
/*==============================================================*/
create table HOADONBAN (
   MaHDB                char(12)             not null,
   MaNV                 char(10)             not null,
   MaDDH                char(10)             not null,
   MaKH                 char(10)             not null,
   NgayLap              datetime             null,
   TongTien             decimal(18,2)        null,
   GhiChu               nvarchar(200)        null,
   TrangThai            nvarchar(30)         null,
   constraint PK_HOADONBAN primary key (MaHDB)
)
go

/*==============================================================*/
/* Index: Mua_Hang_FK                                           */
/*==============================================================*/




create nonclustered index Mua_Hang_FK on HOADONBAN (MaKH ASC)
go

/*==============================================================*/
/* Index: Lap_HoaDon_FK                                         */
/*==============================================================*/




create nonclustered index Lap_HoaDon_FK on HOADONBAN (MaNV ASC)
go

/*==============================================================*/
/* Table: KHACHHANG                                             */
/*==============================================================*/
create table KHACHHANG (
   MaKH                 char(10)             not null,
   TenKH                nvarchar(100)        null,
   SoDienThoai          varchar(15)          null,
   DiaChi               nvarchar(200)        null,
   Email                varchar(100)         null,
   Version              int                  not null default 1,
   constraint PK_KHACHHANG primary key (MaKH)
)
go

/*==============================================================*/
/* Table: KHO                                                   */
/*==============================================================*/
create table KHO (
   MaKho                char(10)             not null,
   TenKho               nvarchar(100)        null,
   DiaChi               nvarchar(200)        null,
   TrangThai            nvarchar(30)          null,
   Version              int                  not null default 1,
   constraint PK_KHO primary key (MaKho)
)
go

/*==============================================================*/
/* Table: LOAISANPHAM                                           */
/*==============================================================*/
create table LOAISANPHAM (
   MaLoai               char(10)             not null,
   TenLoai              nvarchar(100)        null,
   MoTa                 nvarchar(200)        null,
   Version              int                  not null default 1,
   constraint PK_LOAISANPHAM primary key (MaLoai)
)
go

/*==============================================================*/
/* Table: NHACUNGCAP                                            */
/*==============================================================*/
create table NHACUNGCAP (
   MaNCC                char(10)             not null,
   TenNCC               nvarchar(100)        null,
   DiaChi               nvarchar(200)        null,
   SoDienThoai          varchar(15)          null,
   Email                varchar(100)         null,
   Version              int                  not null default 1,
   constraint PK_NHACUNGCAP primary key (MaNCC)
)
go

/*==============================================================*/
/* Table: NHANVIEN                                              */
/*==============================================================*/
create table NHANVIEN (
   MaNV                 char(10)             not null,
   HoTen                nvarchar(100)        null,
   NgaySinh             date                 null,
   GioiTinh             nvarchar(10)         null,
   SoDienThoai          varchar(15)          null,
   DiaChi               nvarchar(200)        null,
   ChucVu               nvarchar(50)         null,
   TrangThai            nvarchar(30)         null,
   Version              int                  not null default 1,
   constraint PK_NHANVIEN primary key (MaNV)
)
go

/*==============================================================*/
/* Table: PHIEUCHI                                              */
/*==============================================================*/
create table PHIEUCHI (
   MaPC                 char(10)             not null,
   MaNV                 char(10)             not null,
   NgayChi              datetime             null,
   NguoiNhan            nvarchar(100)        null,
   LyDoChi              nvarchar(200)        null,
   SoTien               decimal(18,2)        null,
   HinhThuc             nvarchar(20)         null,
   GhiChu               nvarchar(200)        null,
   constraint PK_PHIEUCHI primary key (MaPC)
)
go

/*==============================================================*/
/* Index: Lap_PhieuChi_FK                                       */
/*==============================================================*/




create nonclustered index Lap_PhieuChi_FK on PHIEUCHI (MaNV ASC)
go

/*==============================================================*/
/* Table: PHIEUTHU                                              */
/*==============================================================*/
create table PHIEUTHU (
   MaPT                 char(10)             not null,
   MaNV                 char(10)             not null,
   MaHDB                char(12)             not null,
   NgayThu              datetime             null,
   NguoiNop             nvarchar(100)        null,
   LyDoThu              nvarchar(200)         null,
   SoTien               decimal(18,2)        null,
   HinhThuc             nvarchar(20)         null,
   GhiChu               nvarchar(200)        null,
   constraint PK_PHIEUTHU primary key (MaPT)
)
go

/*==============================================================*/
/* Index: Thu_Theo_HDB_FK                                       */
/*==============================================================*/




create nonclustered index Thu_Theo_HDB_FK on PHIEUTHU (MaHDB ASC)
go

/*==============================================================*/
/* Index: Lap_PhieuThu_FK                                       */
/*==============================================================*/




create nonclustered index Lap_PhieuThu_FK on PHIEUTHU (MaNV ASC)
go

/*==============================================================*/
/* Table: PHIEUXUATKHO                                          */
/*==============================================================*/
create table PHIEUXUATKHO (
   MaPXK                char(10)             not null,
   MaNV                 char(10)             not null,
   MaHDB                char(12)             not null,
   MaKho                char(10)             not null,
   NgayXuat             datetime             null,
   LyDoXuat             nvarchar(200)        null,
   TrangThai            nvarchar(30)         null,
   constraint PK_PHIEUXUATKHO primary key (MaPXK)
)
go

/*==============================================================*/
/* Index: Xuat_TuKho_FK                                         */
/*==============================================================*/




create nonclustered index Xuat_TuKho_FK on PHIEUXUATKHO (MaKho ASC)
go

/*==============================================================*/
/* Index: Xuat_Theo_HDB_FK                                      */
/*==============================================================*/




create nonclustered index Xuat_Theo_HDB_FK on PHIEUXUATKHO (MaHDB ASC)
go

/*==============================================================*/
/* Index: Lap_PhieuXuat_FK                                      */
/*==============================================================*/




create nonclustered index Lap_PhieuXuat_FK on PHIEUXUATKHO (MaNV ASC)
go

/*==============================================================*/
/* Table: SANPHAM                                               */
/*==============================================================*/
create table SANPHAM (
   MaSP                 char(10)             not null,
   MaNCC                char(10)             not null,
   MaLoai               char(10)             not null,
   TenSP                nvarchar(150)        null,
   DonViTinh            nvarchar(30)         null,
   DonGiaBan            decimal(18,2)        null,
   TrangThai            nvarchar(30)         null,
   Version              int                  not null default 1,
   constraint PK_SANPHAM primary key (MaSP)
)
go

/*==============================================================*/
/* Index: Thuoc_Loai_FK                                         */
/*==============================================================*/




create nonclustered index Thuoc_Loai_FK on SANPHAM (MaLoai ASC)
go

/*==============================================================*/
/* Index: Cung_Cap_FK                                           */
/*==============================================================*/




create nonclustered index Cung_Cap_FK on SANPHAM (MaNCC ASC)
go

/*==============================================================*/
/* Table: TAIKHOAN                                              */
/*==============================================================*/
create table TAIKHOAN (
   MaTK                 char(10)             not null,
   MaNV                 char(10)             not null,
   TenDangNhap          varchar(50)          null,
   MatKhau              varchar(100)         null,
   VaiTro               nvarchar(20)         null,
   TrangThai            nvarchar(30)         null,
   Version              int                  not null default 1,
   constraint PK_TAIKHOAN primary key (MaTK)
)
go

/*==============================================================*/
/* Index: Co_TaiKhoan_FK                                        */
/*==============================================================*/




create nonclustered index Co_TaiKhoan_FK on TAIKHOAN (MaNV ASC)
go

/*==============================================================*/
/* Table: TONKHO                                                */
/*==============================================================*/
create table TONKHO (
   MaKho                char(10)             not null,
   MaSP                 char(10)             not null,
   SoLuongTon           int                  null,
   NgayCapNhat          datetime             null,
   constraint PK_TONKHO primary key (MaKho, MaSP)
)
go

/*==============================================================*/
/* Index: Co_TonKho_FK                                          */
/*==============================================================*/




create nonclustered index Co_TonKho_FK on TONKHO (MaKho ASC)
go

/*==============================================================*/
/* Index: Ton_SanPham_FK                                        */
/*==============================================================*/




create nonclustered index Ton_SanPham_FK on TONKHO (MaSP ASC)
go

alter table CHITIETCHUNGTU
   add constraint FK_CHITIETC_GOM_CTCT_CHUNGTU foreign key (MaCT)
      references CHUNGTU (MaCT)
go

alter table CHITIETDONDATHANG
   add constraint FK_CHITIETD_CO_SP_DAT_SANPHAM foreign key (MaSP)
      references SANPHAM (MaSP)
go

alter table CHITIETDONDATHANG
   add constraint FK_CHITIETD_GOM_CTDAT_DONDATHA foreign key (MaDDH)
      references DONDATHANG (MaDDH)
go

alter table CHITIETHOADONBAN
   add constraint FK_CHITIETH_CO_SANPHA_SANPHAM foreign key (MaSP)
      references SANPHAM (MaSP)
go

alter table CHITIETHOADONBAN
   add constraint FK_CHITIETH_GOM_CHITI_HOADONBA foreign key (MaHDB)
      references HOADONBAN (MaHDB)
go

alter table CHITIETPHIEUXUATKHO
   add constraint FK_CHITIETP_GOM_CTPXK_PHIEUXUA foreign key (MaPXK)
      references PHIEUXUATKHO (MaPXK)
go

alter table CHITIETPHIEUXUATKHO
   add constraint FK_CHITIETP_XUAT_SANP_SANPHAM foreign key (MaSP)
      references SANPHAM (MaSP)
go

alter table CHUNGTU
   add constraint FK_CHUNGTU_LAP_CT_NHANVIEN foreign key (MaNV)
      references NHANVIEN (MaNV)
go

alter table CHUNGTU
   add constraint FK_CHUNGTU_PHATSINH__HOADONBA foreign key (MaHDB)
      references HOADONBAN (MaHDB)
go

alter table DONDATHANG
   add constraint FK_DONDATHA_DAT_HANG_KHACHHAN foreign key (MaKH)
      references KHACHHANG (MaKH)
go

alter table DONDATHANG
   add constraint FK_DONDATHA_LAP_DATHA_NHANVIEN foreign key (MaNV)
      references NHANVIEN (MaNV)
go

alter table HOADONBAN
   add constraint FK_HOADONBA_LAP_HOADO_NHANVIEN foreign key (MaNV)
      references NHANVIEN (MaNV)
go

alter table HOADONBAN
   add constraint FK_HOADONBA_MUA_HANG_KHACHHAN foreign key (MaKH)
      references KHACHHANG (MaKH)
go

alter table HOADONBAN
   add constraint FK_HOADONBA_PHATSINH__DONDATHA foreign key (MaDDH)
      references DONDATHANG (MaDDH)
go

alter table PHIEUCHI
   add constraint FK_PHIEUCHI_LAP_PHIEU_NHANVIEN foreign key (MaNV)
      references NHANVIEN (MaNV)
go

alter table PHIEUTHU
   add constraint FK_PHIEUTHU_LAP_PHIEU_NHANVIEN foreign key (MaNV)
      references NHANVIEN (MaNV)
go

alter table PHIEUTHU
   add constraint FK_PHIEUTHU_THU_THEO__HOADONBA foreign key (MaHDB)
      references HOADONBAN (MaHDB)
go

alter table PHIEUXUATKHO
   add constraint FK_PHIEUXUA_LAP_PHIEU_NHANVIEN foreign key (MaNV)
      references NHANVIEN (MaNV)
go

alter table PHIEUXUATKHO
   add constraint FK_PHIEUXUA_XUAT_THEO_HOADONBA foreign key (MaHDB)
      references HOADONBAN (MaHDB)
go

alter table PHIEUXUATKHO
   add constraint FK_PHIEUXUA_XUAT_TUKH_KHO foreign key (MaKho)
      references KHO (MaKho)
go

alter table SANPHAM
   add constraint FK_SANPHAM_CUNG_CAP_NHACUNGC foreign key (MaNCC)
      references NHACUNGCAP (MaNCC)
go

alter table SANPHAM
   add constraint FK_SANPHAM_THUOC_LOA_LOAISANP foreign key (MaLoai)
      references LOAISANPHAM (MaLoai)
go

alter table TAIKHOAN
   add constraint FK_TAIKHOAN_CO_TAIKHO_NHANVIEN foreign key (MaNV)
      references NHANVIEN (MaNV)
go

alter table TONKHO
   add constraint FK_TONKHO_CO_TONKHO_KHO foreign key (MaKho)
      references KHO (MaKho)
go

alter table TONKHO
   add constraint FK_TONKHO_TON_SANPH_SANPHAM foreign key (MaSP)
      references SANPHAM (MaSP)
go

ALTER TABLE HOADONBAN
ADD CONSTRAINT UQ_HOADONBAN_MADDH UNIQUE (MaDDH);
GO

ALTER TABLE TAIKHOAN
ADD CONSTRAINT UQ_TAIKHOAN_MANV UNIQUE (MaNV);
GO

ALTER TABLE TAIKHOAN
ADD CONSTRAINT UQ_TAIKHOAN_TENDANGNHAP UNIQUE (TenDangNhap);
GO

