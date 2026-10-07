# HỆ THỐNG THÔNG TIN KẾ TOÁN BÁN HÀNG (DNQH)

---

## 1. Giới Thiệu Dự Án

**DNQH Kế Toán Bán Hàng** là hệ thống phần mềm kế toán quản trị bán hàng chuyên nghiệp dành cho các doanh nghiệp thương mại vừa và nhỏ (SME). Dự án được thiết kế và phát triển theo chuẩn kiến trúc doanh nghiệp nghiêm ngặt, mô phỏng và số hóa khép kín toàn bộ chu trình luân chuyển chứng từ - dòng tiền - hàng hóa thực tế:

$$\text{Đơn Đặt Hàng} \longrightarrow \text{Hóa Đơn Bán Hàng} \longrightarrow \text{Phiếu Xuất Kho} \longrightarrow \text{Phiếu Thu Tiền} \longrightarrow \text{Chứng Từ Sổ Cái} \longrightarrow \text{Báo Cáo Quản Trị}$$

### Mục Tiêu Nghiệp Vụ Cốt Lõi:
- **Tự động hóa luồng nghiệp vụ bán hàng:** Quản lý vòng đời đơn đặt hàng từ khi lập, duyệt đến khi chuyển đổi thành hóa đơn bán hàng thương mại (`1 Đơn hàng → Tối đa 1 Hóa đơn bán`).
- **Quản trị tồn kho đa kho hàng:** Kiểm soát tồn kho chặt chẽ theo từng kho vật lý (Kho Tổng Miền Bắc, Kho Miền Nam, Kho Miền Trung); tự động kiểm tra số dư tồn kho khả dụng trước khi xuất; hỗ trợ xuất hàng nhiều lần (Partial Delivery).
- **Quản lý tài chính & công nợ khách hàng:** Tự động theo dõi đối soát thanh toán theo từng hóa đơn, phân loại trạng thái công nợ minh bạch (*Chưa thanh toán*, *Thanh toán một phần*, *Đã thanh toán*); quản lý dòng tiền chi phí hoạt động doanh nghiệp qua phiếu chi.
- **Hạch toán kế toán & Sổ sách tài chính:** Ghi nhận chứng từ kế toán tự động với đầy đủ các cặp tài khoản định khoản đối ứng Nợ/Có (TK 111, TK 112, TK 131, TK 511, TK 3331, TK 632, TK 156); lập báo cáo tổng hợp doanh thu, công nợ phải thu và sổ chi tiết bán hàng.
- **Trải nghiệm người dùng thông minh (UX):** Tự động gợi ý đề xuất sinh mã kế toán thông minh (`AutoCodeHelper`) khi thêm mới danh mục; giao diện tự co giãn (Responsive UI) tích hợp nhận diện thương hiệu App Icon chuẩn Windows.

---

## 2. Công Nghệ & Ràng Buộc Kỹ Thuật

Dự án tuân thủ tuyệt đối các ràng buộc kỹ thuật cố định theo chuẩn phát triển hệ thống doanh nghiệp:

| Thành Phần | Công Nghệ / Tiêu Chuẩn | Vai Trò & Ràng Buộc Kỹ Thuật |
| :--- | :--- | :--- |
| **Nền tảng & Ngôn ngữ** | **C# / .NET Framework 4.8** | Nền tảng ổn định, tương thích rộng rãi trên môi trường Windows máy tính bàn và máy trạm doanh nghiệp. |
| **Giao diện Người dùng** | **Windows Forms (WinForms)** | Giao diện đồ họa native mượt mà; tích hợp nhận diện thương hiệu `assets/DNQH_App.ico`, xử lý đa độ phân giải màn hình. |
| **Hệ Quản Trị CSDL** | **Microsoft SQL Server 2022 Express** | Quản lý dữ liệu quan hệ ACID; chạy cục bộ trên máy chủ/máy trạm (`localhost\SQLEXPRESS`). |
| **Giao tiếp Dữ liệu** | **ADO.NET thuần (`System.Data.SqlClient`)** | Sử dụng Parameterized Queries 100% (chống SQL Injection); quản lý Transaction nguyên tử; không dùng Entity Framework để đảm bảo hiệu năng và kiểm soát trực tiếp I/O. |
| **Bảo Mật & Mã Hóa** | **PBKDF2-HMAC-SHA256** | Băm mật khẩu an toàn với Salt ngẫu nhiên 16 byte, 100.000 vòng lặp (Rounds); cơ chế xác thực phân quyền dựa trên vai trò (RBAC); từ chối mật khẩu plaintext. |
| **Giám Sát & Nhật Ký** | **Structured Logging (`AppLogger`)** | Ghi log tập trung có cấu trúc tại thư mục `logs/`; tự động sinh mã `CorrelationId` để theo dõi và hỗ trợ kỹ thuật khi có lỗi. |

---

## 3. Kiến Trúc Hệ Thống (System Architecture)

Dự án áp dụng mô hình **Kiến trúc phân tầng (Layered Architecture)** kết hợp tư tưởng **MVP-Lite (Model-View-Presenter Lite)** nhằm đảm bảo tính độc lập, khả năng mở rộng và dễ bảo trì:

```text
┌─────────────────────────────────────────────────────────────┐
│                    PRESENTATION LAYER                       │
│      Forms (WinForms UI, Controls, User Interactions)       │
└──────────────────────────────┬──────────────────────────────┘
                               │ (gọi nghiệp vụ, không chứa SQL)
                               ▼
┌─────────────────────────────────────────────────────────────┐
│                    APPLICATION LAYER                        │
│   Services (OrderService, InvoiceService, Warehouse...)     │
│       - Xác thực nghiệp vụ & phân quyền (RBAC)             │
│       - Điều phối giao dịch nguyên tử (SqlTransaction)      │
└──────────────────────────────┬──────────────────────────────┘
                               │ (gọi xử lý dữ liệu & mapper)
                               ▼
┌─────────────────────────────────────────────────────────────┐
│                 INFRASTRUCTURE & DATA ACCESS                │
│    DataAccess (DAL) & Helpers (Database, Logger, Security)  │
│       - Thực thi Parameterized SQL / ADO.NET                │
│       - Quản lý kết nối & Structured Logging                │
└──────────────────────────────┬──────────────────────────────┘
                               │
                               ▼
┌─────────────────────────────────────────────────────────────┐
│                        DOMAIN LAYER                         │
│     Models / Entities (DonDatHang, HoaDonBan, TonKho...)    │
│  (Thực thể nghiệp vụ thuần túy, không phụ thuộc UI hay SQL) │
└─────────────────────────────────────────────────────────────┘
```

### Các Nguyên Tắc Phụ Thuộc (Dependency Rules) Nghiêm Ngặt:
1. **Domain / Models độc lập tuyệt đối:** Không tham chiếu đến `System.Windows.Forms`, `System.Data.SqlClient` hay tầng Infrastructure.
2. **WinForms UI không sở hữu SQL:** Toàn bộ form giao diện chỉ gọi xuống tầng `Services` hoặc các hàm đọc danh mục qua `DAL`; cấm viết câu lệnh SQL trực tiếp trong các sự kiện click chuột (`event handlers`).
3. **Toàn vẹn giao dịch (Atomic Transactions):** Các nghiệp vụ ghi nhiều bảng (Lập đơn hàng, Lập hóa đơn bán, Xuất kho trừ tồn kho, Ghi sổ chứng từ kế toán) bắt buộc phải đóng gói trong `SqlTransaction` tại tầng Service. Nếu có bất kỳ lỗi nào xảy ra, toàn bộ giao dịch sẽ tự động Rollback.

---

## 4. Cấu Trúc Thư Mục & Chức Năng Chi Tiết

Toàn bộ giải pháp được tổ chức khoa học, phân chia rõ ràng theo từng phân tầng trách nhiệm:

```text
DNQH_KeToanBanHang/
├── assets/                    # Tài nguyên nhận diện thương hiệu ứng dụng
├── database/                  # Kịch bản cơ sở dữ liệu SQL Server & Dữ liệu mẫu
│   ├── scripts/               # Kịch bản hỗ trợ khởi tạo database kiểm thử cách ly
│   └── seed/                  # Kịch bản nạp dữ liệu mẫu doanh nghiệp chuẩn
├── DataAccess/                # Tầng truy xuất dữ liệu (Data Access Layer - DAL)
├── docs/                      # Bộ tài liệu đặc tả kỹ thuật và kiến trúc đồ án
├── Forms/                     # Tầng giao diện người dùng Windows Forms (UI)
├── Helpers/                   # Các thư viện tiện ích dùng chung toàn hệ thống
├── Models/                    # Tầng mô hình thực thể nghiệp vụ (Domain Entities & DTOs)
├── scripts/                   # Các kịch bản thực thi tự động (.bat) phục vụ vận hành
├── Services/                  # Tầng nghiệp vụ ứng dụng & Điều phối giao dịch (BLL)
├── .editorconfig              # Quy chuẩn định dạng mã nguồn nhất quán
├── .gitignore                 # Cấu hình bỏ qua các tệp tin tạm thời khi làm việc
├── AGENTS.md                  # Quy tắc và chuẩn mực phát triển phần mềm cố định
├── App.config                 # Tệp cấu hình ứng dụng (Connection String & Runtime)
├── DNQH_KeToanBanHang.csproj  # Tệp định nghĩa dự án MSBuild .NET 4.8
├── DNQH_KeToanBanHang.sln     # Tệp Solution quản lý toàn bộ giải pháp Visual Studio
├── Program.cs                 # Điểm khởi chạy ứng dụng (Main Entry Point & Global Exception Handler)
└── README.md                  # Tài liệu giới thiệu, hướng dẫn sử dụng và bàn giao hệ thống
```

### Bảng Mô Tả Chi Tiết Nhiệm Vụ Của Từng Thư Mục:

| Thư Mục | Chức Năng & Trách Nhiệm Chi Tiết |
| :--- | :--- |
| **`assets/`** | Chứa logo thương hiệu và biểu tượng ứng dụng đa độ phân giải (`DNQH_App.ico` từ 16x16 đến 256x256, `DNQH_App.png`). Được nhúng trực tiếp vào PE resource của file `.exe` và nạp lên Titlebar / Taskbar của các Form. |
| **`database/`** | Quản lý mã nguồn CSDL SQL Server: <br>• `DNQH_KeToanBanHang.sql`: Script DDL tạo cấu trúc 18 bảng, khóa chính, khóa ngoại, chỉ mục và ràng buộc toàn vẹn.<br>• `seed/DevelopmentData.sql`: Script nạp bộ dữ liệu mẫu doanh nghiệp chuẩn (Nhà cung cấp, Sản phẩm, Khách hàng, Kho, Tồn kho, Tài khoản, Đơn hàng, Hóa đơn, Xuất kho, Phiếu thu, Phiếu chi, Chứng từ). |
| **`DataAccess/`** | Chứa các lớp Data Access Object (DAL) phụ trách tương tác với CSDL qua ADO.NET: `SanPhamDAL`, `KhachHangDAL`, `HoaDonBanDAL`, `PhieuXuatKhoDAL`, `PhieuThuDAL`, `PhieuChiDAL`, `ChungTuDAL`, `TonKhoDAL`, `TaiKhoanDAL`, `NhanVienDAL`... |
| **`Forms/`** | Chứa các cửa sổ giao diện người dùng WinForms: <br>• `frmDangNhap.cs`: Cửa sổ đăng nhập hệ thống.<br>• `frmMain.cs`: Bảng điều khiển trung tâm (Dashboard) phân quyền theo vai trò.<br>• `frmDonDatHang.cs`, `frmHoaDonBan.cs`: Quản lý lập đơn hàng và phát hành hóa đơn.<br>• `frmPhieuXuatKho.cs`: Quản lý lập phiếu xuất kho và trừ tồn kho atomic.<br>• `frmPhieuThu.cs`, `frmPhieuChi.cs`: Quản lý thu tiền bán hàng và chi phí.<br>• `frmChungTu.cs`, `frmKeToanChiTiet.cs`: Quản lý hạch toán sổ cái định khoản kế toán.<br>• `frmBaoCaoTongHop.cs`: Màn hình tổng hợp báo cáo doanh thu, công nợ, tồn kho.<br>• Các form danh mục: `frmSanPham.cs`, `frmKhachHang.cs`, `frmKho.cs`, `frmNhaCungCap.cs`, `frmLoaiSanPham.cs`, `frmNhanVien.cs`, `frmTaiKhoan.cs`, `frmDoiMatKhau.cs`. |
| **`Services/`** | Chứa các lớp xử lý nghiệp vụ trung tâm (Business Logic): <br>• `AuthService.cs`: Xác thực đăng nhập, mã hóa và kiểm tra vai trò.<br>• `OrderService.cs`: Lập đơn hàng, tính tổng tiền, kiểm tra trạng thái đơn.<br>• `InvoiceService.cs`: Chuyển đổi đơn hàng sang hóa đơn bán hàng, kiểm soát ràng buộc duy nhất 1-1.<br>• `WarehouseService.cs`: Điều phối xuất kho trừ tồn kho nguyên tử đa kho hàng.<br>• `AccountingService.cs`: Quản lý phiếu thu, phiếu chi và ghi sổ chứng từ kế toán.<br>• `ReportingService.cs`: Truy vấn và tổng hợp dữ liệu báo cáo kinh doanh. |
| **`Models/`** | Định nghĩa các lớp đối tượng thực thể (Domain Entities) ánh xạ trực tiếp với bảng CSDL: `DonDatHang`, `ChiTietDonDatHang`, `HoaDonBan`, `ChiTietHoaDonBan`, `PhieuXuatKho`, `ChiTietPhieuXuatKho`, `PhieuThu`, `PhieuChi`, `ChungTu`, `ChiTietChungTu`, `TonKho`, `SanPham`, `KhachHang`, `NhaCungCap`, `LoaiSanPham`, `NhanVien`, `TaiKhoan` và các DTO báo cáo (`ReportingModels.cs`). |
| **`Helpers/`** | Cung cấp các công cụ tiện ích độc lập dùng chung cho toàn bộ dự án: <br>• `Database.cs`: Quản lý chuỗi kết nối và thực thi truy vấn ADO.NET an toàn.<br>• `SessionManager.cs`: Lưu trữ và quản lý phiên làm việc của người dùng đang đăng nhập.<br>• `SecurityHelper.cs`: Thuật toán băm và xác minh mật khẩu PBKDF2-SHA256.<br>• `AutoCodeHelper.cs`: Thuật toán thông minh tự động đề xuất sinh mã kế toán liên tục.<br>• `UIResponsiveHelper.cs`: Tự động gán App Icon, kích thước dropdown và responsive UI.<br>• `AppLogger.cs`: Ghi log có cấu trúc chuẩn observability kèm CorrelationId.<br>• `ValidationHelper.cs`: Kiểm tra tính hợp lệ dữ liệu nhập liệu (Email, SĐT, Số tiền).<br>• `CsvExportHelper.cs`: Tiện ích xuất dữ liệu DataGridView ra file Excel/CSV an toàn. |
| **`scripts/`** | Chứa kịch bản thực thi (.bat) phục vụ vận hành: <br>• `tao_shortcut_desktop.bat`: Tự động tạo biểu tượng lối tắt ngoài màn hình Desktop. |
| **`docs/`** | Lưu trữ toàn bộ 4 tài liệu đặc tả chuẩn của dự án: `01_PROJECT_SPEC.md` (Đặc tả nghiệp vụ), `02_ARCHITECTURE.md` (Kiến trúc hệ thống), `03_ENGINEERING_STANDARDS.md` (Quy chuẩn kỹ thuật), `04_OPERATIONS_OBSERVABILITY.md` (Vận hành & Giám sát). |

---

## 5. Hướng Dẫn Nạp Cơ Sở Dữ Liệu (Database Setup)

Trước khi khởi động ứng dụng lần đầu tiên, hệ thống cần được khởi tạo cấu trúc 18 bảng và nạp bộ dữ liệu mẫu doanh nghiệp chuẩn trực tiếp qua công cụ **SQL Server Management Studio (SSMS)**:

### 5.1. Cấu Hình Chuỗi Kết Nối CSDL (Connection String)
Hệ thống mặc định kết nối tới SQL Server Express trên máy cục bộ với xác thực Windows Authentication:
- File cấu hình: [App.config](App.config)
```xml
<connectionStrings>
  <add name="DNQH_KeToanBanHang"
       connectionString="Server=localhost\SQLEXPRESS;Database=DNQH_KeToanBanHang;Integrated Security=True;TrustServerCertificate=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```
> **Lưu ý:** Nếu máy tính của người dùng hoặc của hội đồng chấm đồ án đặt tên SQL Server khác (ví dụ: `.\SQLEXPRESS`, `(local)`, hoặc tên máy riêng như `DESKTOP-ABC\SQLEXPRESS`), chỉ cần mở file `App.config` và thay đổi giá trị thuộc tính `Server=...` tương ứng.

---

### 5.2. Các Bước Nạp CSDL Qua SQL Server Management Studio (SSMS)

Hệ thống cung cấp sẵn 2 kịch bản SQL độc lập, chuẩn hóa và tuân thủ đúng thứ tự toàn vẹn khóa ngoại (FK):

1. **Bước 1 - Kết nối CSDL:**
   - Mở công cụ **SQL Server Management Studio (SSMS)**.
   - Đăng nhập vào SQL Server (chọn Server name: `localhost\SQLEXPRESS` hoặc Instance tương ứng trên máy, Authentication: `Windows Authentication`).

2. **Bước 2 - Tạo cấu trúc 18 bảng và ràng buộc toàn vẹn (DDL):**
   - Mở file [database/DNQH_KeToanBanHang.sql](database/DNQH_KeToanBanHang.sql) trong SSMS (kéo thả file vào SSMS hoặc chọn `File` $\rightarrow$ `Open` $\rightarrow$ `File...`).
   - Bấm nút **`Execute`** trên thanh công cụ (hoặc nhấn phím `F5`).
   - *Kết quả:* Database `DNQH_KeToanBanHang` được tạo mới hoàn toàn với đầy đủ 18 bảng, khóa chính (PK), khóa ngoại (FK), chỉ mục (Index) và các ràng buộc toàn vẹn dữ liệu.

3. **Bước 3 - Nạp bộ dữ liệu mẫu doanh nghiệp chuẩn (Seed Data):**
   - Mở file [database/seed/DevelopmentData.sql](database/seed/DevelopmentData.sql) trong SSMS.
   - Bấm nút **`Execute`** trên thanh công cụ (hoặc nhấn phím `F5`).
   - *Kết quả:* Toàn bộ dữ liệu mẫu chuẩn Doanh nghiệp được nạp hoàn tất (Nhà cung cấp, Sản phẩm, Khách hàng, Kho, Tồn kho, Tài khoản phân quyền, Đơn đặt hàng, Hóa đơn bán, Xuất kho, Phiếu thu, Phiếu chi, Chứng từ sổ cái).

---

### 5.3. Dữ Liệu Sẵn Có & Tài Khoản Đăng Nhập Mặc Định

Sau khi nạp database thành công, hệ thống đã có sẵn dữ liệu phong phú để bạn kiểm thử ngay:

#### 👤 Danh sách 4 tài khoản phân quyền chuẩn:
| Tên Đăng Nhập | Mật Khẩu | Vai Trò | Nhân Viên Đại Diện | Quyền Hạn Nghiệp Vụ |
| :--- | :---: | :--- | :--- | :--- |
| **`admin`** | `123456` | Quản trị viên | Nguyễn Văn Trị (NV001) | Toàn quyền: Quản trị danh mục, người dùng, phân quyền, tất cả nghiệp vụ |
| **`banhang`** | `123456` | Nhân viên bán hàng | Trần Thị Bán Hàng (NV002) | Lập Đơn đặt hàng, phát hành Hóa đơn bán hàng, tra cứu |
| **`kho`** | `123456` | Nhân viên kho | Lê Văn Kho (NV003) | Lập Phiếu xuất kho, trừ tồn kho, tra cứu thẻ kho |
| **`ketoan`** | `123456` | Nhân viên kế toán | Phạm Thị Kế Toán (NV004) | Lập Phiếu thu, Phiếu chi, Chứng từ sổ cái kế toán, Báo cáo tổng hợp |

#### 📊 Dữ liệu mẫu đã được nạp sẵn trong hệ thống:
- **6 Nhà cung cấp uy tín:** Sunhouse, Samsung Vina, Tập đoàn CMC, Synnex FPT, Văn phòng phẩm Hồng Hà, LG Electronics.
- **4 Loại sản phẩm & 14 Sản phẩm thương mại:** Có đầy đủ quy cách, đơn vị tính chuẩn (Bộ, Chiếc, Máy, Thùng, Hộp) và đơn giá niêm yết.
- **8 Khách hàng đa dạng:** Cty Công nghệ Minh Khang, ĐH Kinh Tế Quốc Dân (NEU), BV Quốc Tế Vinmec, Tập Đoàn Xây Dựng Hòa Bình, Siêu Thị Điện Máy Xanh...
- **3 Kho hàng chiến lược & 42 dòng tồn kho:** Kho Tổng Miền Bắc (Hà Nội), Kho Phân Phối Miền Nam (Bình Dương), Kho Trung Chuyển Miền Trung (Đà Nẵng).
- **Giao dịch mẫu sẵn sàng báo cáo:**
  - `HDB0000001`: Đã xuất kho & thu tiền 100% $\rightarrow$ Sẵn sàng trên Báo cáo doanh thu & Sổ cái kế toán.
  - `HDB0000002`: Chưa xuất kho $\rightarrow$ **Sẵn sàng để demo trực tiếp trên màn hình Phiếu Xuất Kho**.
  - `HDB0000003`: Xuất kho 1 phần & khách nợ 50% $\rightarrow$ Sẵn sàng trên Báo cáo Công nợ phải thu.
  - `DDH0000004`: Đơn đặt hàng mới chưa lập hóa đơn $\rightarrow$ Sẵn sàng để demo Lập hóa đơn bán hàng.

---

## 6. Hướng Dẫn Biên Dịch & Khởi Chạy Ứng Dụng (Build & Run Guide)

Sau khi hoàn tất cấu hình chuỗi kết nối và nạp CSDL ở Mục 5, bạn có thể biên dịch và khởi chạy ứng dụng theo một trong các phương thức thuận tiện sau:

### 6.1. Cách 1: Mở Và Chạy Trực Tiếp Bằng Visual Studio (Khuyên Dùng)
Phương thức chuẩn dành cho người phát triển, chấm mã nguồn hoặc muốn đặt breakpoint gỡ lỗi chi tiết:
1. **Bước 1:** Nhấp đúp mở tệp giải pháp [DNQH_KeToanBanHang.sln](DNQH_KeToanBanHang.sln) bằng Microsoft Visual Studio (tương thích các phiên bản 2017 / 2019 / 2022 / 2026).
2. **Bước 2:** Trên thanh công cụ Solution Configurations, chọn chế độ mong muốn:
   - Configuration: **`Debug`** (khi lập trình, gỡ lỗi) hoặc **`Release`** (khi xuất bản bản chạy tối ưu).
   - Platform: **`Any CPU`** (hoặc `x86` / `x64`).
3. **Bước 3:** Đảm bảo dự án khởi động là `DNQH_KeToanBanHang` (nếu tên dự án chưa được in đậm, nhấp chuột phải vào dự án `DNQH_KeToanBanHang` trong cửa sổ Solution Explorer $\rightarrow$ chọn **`Set as Startup Project`**).
4. **Bước 4:** Nhấn phím **`F5`** (hoặc nút **`Start ▶️`**) để biên dịch và chạy kèm chế độ Debug, hoặc nhấn **`Ctrl + F5`** (Start Without Debugging) để chạy trực tiếp.

---

### 6.2. Cách 2: Chạy Trực Tiếp Từ Tệp Thực Thi (.EXE) Đã Biên Dịch
Trong trường hợp máy tính đã có sẵn bản biên dịch (hoặc sau khi build trong Visual Studio), bạn có thể khởi chạy ứng dụng trực tiếp bằng cách nhấp đúp vào tệp thực thi:
- Phiên bản Debug: `bin\Debug\DNQH_KeToanBanHang.exe`
- Phiên bản Release: `bin\Release\DNQH_KeToanBanHang.exe`

> **Nhận diện thương hiệu chuẩn:** Tệp `.exe` đã được nhúng sẵn Application Icon nhận diện thương hiệu `DNQH_App.ico`, tự động hiển thị logo chuyên nghiệp trên thanh tiêu đề (Titlebar) và thanh tác vụ Windows (Taskbar).

---

### 6.3. Tiện Ích: Tự Động Tạo Lối Tắt (Shortcut) Ngoài Màn Hình Desktop
Để tạo trải nghiệm như một ứng dụng cài đặt hoàn chỉnh cho người đánh giá hoặc người dùng cuối:
- **Cách thực hiện:** Nhấp đúp chuột vào file:
  ```text
  scripts\tao_shortcut_desktop.bat
  ```
- **Kết quả:** Một biểu tượng lối tắt mang tên **`DNQH - Ke Toan Ban Hang`** sẽ tự động được tạo ra ngoài màn hình Desktop, liên kết chuẩn xác tới file thực thi của dự án và hiển thị logo thương hiệu đồng bộ.

---

## 7. Quy Chuẩn Nghiệp Vụ CRUD & Bảo Vệ Toàn Vẹn Dữ Liệu

Trong hệ thống kế toán, thao tác **Sửa** và **Xóa** trên danh mục dùng chung (**Master Data**) được kiểm soát chặt chẽ nhằm bảo vệ tính toàn vẹn của sổ sách tài chính:

1. **Bảo Toàn Chứng Từ Lịch Sử:** Dữ liệu danh mục đã gắn liền với đơn hàng, hóa đơn, phiếu xuất kho hoặc tồn kho là căn cứ pháp lý, **tuyệt đối không xóa vật lý (Hard Delete)** để tránh làm mất vết kiểm toán và sai lệch báo cáo.
2. **Đóng Băng Đơn Giá (Price Snapshot):** Khi sửa `DonGiaBan` của sản phẩm, mức giá mới chỉ áp dụng cho các giao dịch tương lai; các hóa đơn cũ vẫn giữ nguyên giá lịch sử tại thời điểm bán (`CHITIETHOADONBAN.DonGia`).

---

### Bảng Tổng Hợp Quy Tắc CRUD Master Data:

| Danh Mục | Thêm Mới (Create) | Sửa Đổi (Update) | Xóa Khi Chưa Có Giao Dịch | Xóa Khi Đã Phát Sinh Dữ Liệu / Liên Kết |
| :--- | :--- | :--- | :--- | :--- |
| **Sản Phẩm** (`SANPHAM`) | Sinh mã tự động, đơn giá $\ge 0$, mặc định `Đang kinh doanh`. | Cập nhật tên, giá bán, DVT; kiểm soát xung đột đồng thời `Version`. | **Xóa vật lý** (Hard Delete khỏi CSDL sau xác nhận). | **Soft Delete**: Chặn xóa vật lý $\rightarrow$ Đề xuất chuyển sang `Ngưng kinh doanh` (tự động ẩn khi lập đơn mới). |
| **Loại SP** (`LOAISANPHAM`) | Sinh mã tự động, kiểm tra tên loại. | Cập nhật tên, mô tả; kiểm soát `Version`. | **Xóa vật lý** (Khi chưa có sản phẩm nào thuộc loại). | **Chặn xóa** (Cảnh báo đang có $N$ sản phẩm $\rightarrow$ yêu cầu chuyển loại trước). |
| **Khách Hàng** (`KHACHHANG`) | Sinh mã tự động, validate SĐT & Email. | Cập nhật thông tin liên hệ; không đổi mã `MaKH`. | **Xóa vật lý** (Khi chưa từng phát sinh đơn hàng/hóa đơn). | **Chặn xóa** (Bảo toàn lịch sử giao dịch và đối soát công nợ phải thu). |
| **Nhà Cung Cấp** (`NHACUNGCAP`) | Sinh mã tự động, kiểm tra thông tin đối tác. | Cập nhật địa chỉ, SĐT, email; kiểm soát `Version`. | **Xóa vật lý** (Khi chưa gắn với sản phẩm nào). | **Chặn xóa** (Bảo toàn nguồn gốc cung ứng của danh mục sản phẩm). |

