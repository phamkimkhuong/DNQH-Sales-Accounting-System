# 02_ARCHITECTURE.md
## Kiến trúc kỹ thuật – C# WinForms trên .NET Framework 4.8

> **HOW — cách hệ thống được tổ chức để dễ bảo trì, mở rộng, test và giám sát.**

## 1. Kiến trúc tổng thể
Dùng **Layered Architecture + Clean Architecture principles + MVP-lite**.

```text
┌──────────────────────────────┐
│ Presentation                 │
│ WinForms + Presenter-lite    │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│ Application                  │
│ Use Cases / Services / DTOs  │
│ Authorization / Validation   │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│ Domain                       │
│ Entities / Rules / Enums     │
└──────────────────────────────┘
               ▲
               │
┌──────────────┴───────────────┐
│ Infrastructure               │
│ ADO.NET / SQL / Logging      │
│ Repository implementations   │
└──────────────────────────────┘
```

Mục tiêu:
- Form không chứa SQL lớn.
- Form không quản lý transaction.
- Domain không biết SQL Server/WinForms.
- Application mô tả use case.
- Infrastructure chịu trách nhiệm persistence.
- Các màn hình phức tạp có Presenter để tránh “Fat Form”.

## 2. Solution structure
```text
DNQH.KeToanBanHang.sln
│
├── src/
│   ├── DNQH.KeToanBanHang.Domain
│   ├── DNQH.KeToanBanHang.Application
│   ├── DNQH.KeToanBanHang.Infrastructure
│   └── DNQH.KeToanBanHang.WinForms
│
├── tests/
│   ├── DNQH.KeToanBanHang.UnitTests
│   └── DNQH.KeToanBanHang.IntegrationTests
│
├── database/
│   ├── schema/
│   ├── migrations/
│   ├── seed/
│   └── scripts/
│
├── docs/
│   ├── architecture/
│   ├── adr/
│   └── runbooks/
│
├── .editorconfig
├── .gitignore
├── README.md
└── Directory.Build.props
```

## 3. Dependency rules

### Domain
Được chứa:
- entities;
- value/domain rules;
- enums;
- domain exceptions.

Không được reference:
- WinForms;
- `System.Data.SqlClient`;
- `MessageBox`;
- `DataGridView`;
- Infrastructure.

### Application
Reference Domain.

Chứa:
- use case services;
- repository interfaces;
- DTOs;
- validators;
- authorization abstractions;
- orchestration.

Không chứa:
- raw SQL;
- WinForms controls;
- MessageBox.

### Infrastructure
Reference Application + Domain.

Chứa:
- `System.Data.SqlClient`;
- repository implementations;
- transaction handling;
- logging;
- password hashing;
- configuration access.

### WinForms
Reference Application và các DTO/domain types cần thiết.

Không chứa:
- transaction;
- SQL lớn;
- business rule quan trọng.

## 4. MVP-lite
Không bắt buộc Presenter cho mọi Form.

Presenter bắt buộc/khuyến nghị cho:
- `frmDangNhap`;
- `frmDonDatHang`;
- `frmHoaDonBan`;
- `frmPhieuXuatKho`;
- `frmChungTu`;
- `frmBaoCaoTongHop`.

Ví dụ:
```text
frmHoaDonBan
    ↓
HoaDonPresenter
    ↓
CreateInvoiceService
    ↓
IOrderRepository
IInvoiceRepository
IInventoryRepository
```

Event handler lý tưởng:
```csharp
private async void btnLapHoaDon_Click(object sender, EventArgs e)
{
    await _presenter.CreateInvoiceAsync();
}
```

CRUD đơn giản có thể gọi Application Service trực tiếp.

## 5. Application tổ chức theo use case
Tránh một `BanHangService` khổng lồ.

```text
Application/
├── Authentication/
│   ├── LoginService.cs
│   └── ChangePasswordService.cs
├── Customers/
│   ├── CreateCustomerService.cs
│   ├── UpdateCustomerService.cs
│   └── SearchCustomerService.cs
├── Orders/
│   ├── CreateOrderService.cs
│   ├── CancelOrderService.cs
│   └── GetOrderDetailService.cs
├── Invoices/
│   ├── CreateInvoiceService.cs
│   └── SearchInvoiceService.cs
├── Inventory/
│   ├── CheckInventoryService.cs
│   └── IssueInventoryService.cs
└── Reporting/
    ├── DetailAccountingService.cs
    └── SummaryReportService.cs
```

## 6. Repository boundaries
Ưu tiên theo aggregate/nghiệp vụ:
- `ITaiKhoanRepository`
- `INhanVienRepository`
- `ICustomerRepository`
- `IProductRepository`
- `IOrderRepository`
- `IInvoiceRepository`
- `IInventoryRepository`
- `ICashRepository`
- `IAccountingDocumentRepository`
- `IReportRepository`

Không bắt buộc “1 bảng = 1 repository”.
Ví dụ `IOrderRepository` có thể thao tác cả `DONDATHANG` và `CHITIETDONDATHANG`.

## 7. Dependency Injection
Trên .NET Framework 4.8 có thể dùng `Microsoft.Extensions.DependencyInjection` nếu solution chấp nhận package đó.

Composition root đặt tại `Program.cs`/`Bootstrapper.cs`.

```csharp
var services = new ServiceCollection();

services.AddTransient<IOrderRepository, SqlOrderRepository>();
services.AddTransient<IInvoiceRepository, SqlInvoiceRepository>();
services.AddTransient<CreateInvoiceService>();
services.AddTransient<frmDangNhap>();
services.AddTransient<frmMain>();

var provider = services.BuildServiceProvider();
```

Nếu không dùng package DI, tạo `Bootstrapper` thủ công nhưng không `new` dependency rải rác khắp Forms.

## 8. Configuration
Vì dùng .NET Framework 4.8:
- dùng `App.config`;
- connection string trong `<connectionStrings>`;
- đọc bằng `ConfigurationManager.ConnectionStrings`;
- không hard-code nhiều nơi.

## 9. ADO.NET rules
Dùng `System.Data.SqlClient`.

Bắt buộc:
- `using` cho connection/command/reader/transaction;
- connection sống ngắn;
- parameterized SQL;
- không global-open connection;
- không concat input vào SQL;
- dùng async cho DB I/O có thể chậm.

Ví dụ:
```csharp
using (var connection = new SqlConnection(_connectionString))
using (var command = connection.CreateCommand())
{
    command.CommandText =
        "SELECT MaKH, TenKH FROM KHACHHANG WHERE TenKH LIKE @Keyword";

    command.Parameters.Add("@Keyword", SqlDbType.NVarChar, 100)
                      .Value = "%" + keyword + "%";

    await connection.OpenAsync();
}
```

## 10. Transaction boundaries
Transaction thuộc Application/Infrastructure use case, không thuộc Form.

### CreateOrder
1. Insert `DONDATHANG`.
2. Insert các `CHITIETDONDATHANG`.
3. Update `TongTien`.
4. Commit hoặc rollback.

### CreateInvoice
1. Kiểm tra đơn tồn tại.
2. Kiểm tra đơn chưa có hóa đơn.
3. Kiểm tra tồn.
4. Insert `HOADONBAN`.
5. Insert `CHITIETHOADONBAN`.
6. Update `TongTien`.
7. Commit hoặc rollback.

### IssueInventory
1. Kiểm tra hóa đơn/kho.
2. Kiểm tra tồn.
3. Insert `PHIEUXUATKHO`.
4. Insert `CHITIETPHIEUXUATKHO`.
5. Trừ `TONKHO`.
6. Commit hoặc rollback.

### CreateAccountingDocument
1. Insert `CHUNGTU`.
2. Insert `CHITIETCHUNGTU`.
3. Commit hoặc rollback.

## 11. Concurrency / chống tồn âm
Cập nhật tồn nên atomic:

```sql
UPDATE TONKHO
SET SoLuongTon = SoLuongTon - @Qty,
    NgayCapNhat = GETDATE()
WHERE MaKho = @MaKho
  AND MaSP = @MaSP
  AND SoLuongTon >= @Qty;
```

Nếu affected rows = 0:
- không đủ tồn hoặc record không tồn tại;
- throw `InsufficientStockException`;
- rollback.

Điều này an toàn hơn đọc tồn rồi update ở hai câu lệnh tách rời mà không kiểm soát concurrency.

## 12. Domain/Application rules
Business rule đặt Domain/Application, không lặp ở UI:
- quantity;
- discount;
- total;
- order-to-invoice uniqueness;
- account state;
- authorization;
- stock availability.

## 13. Error model
Các exception có nghĩa:
- `ValidationException`
- `BusinessRuleException`
- `DuplicateInvoiceException`
- `InsufficientStockException`
- `AuthorizationException`
- `DataAccessException`

Infrastructure wrap `SqlException` khi cần.
UI hiển thị message thân thiện, log giữ kỹ thuật đầy đủ.

## 14. Async strategy
.NET Framework 4.8 hỗ trợ `async/await`.

Ưu tiên async cho:
- login;
- search;
- grid load;
- report;
- order/invoice save;
- stock issue.

Không dùng `.Result`/`.Wait()` trên UI thread.

## 15. Security architecture
- Password hash PBKDF2 + random salt.
- Không plaintext.
- UI authorization + Application authorization.
- Không log password.
- Không log secret.
- Không hiển thị raw SQL/stack trace cho end user.

## 16. Session
`SessionManager` tối thiểu:
```text
CurrentUser.MaTK
CurrentUser.MaNV
CurrentUser.TenDangNhap
CurrentUser.VaiTro
CurrentUser.HoTen
```

Logout phải clear session.

## 17. Extensibility workflow
Khi thêm feature:
1. Domain types/rules nếu cần.
2. Application use case/interface.
3. Infrastructure implementation.
4. UI.
5. Tests.
6. Migration nếu schema đổi.
7. ADR nếu quyết định đáng kể.

Không xuyên tắt Form → SQL.

## 18. Architecture acceptance
- Domain không reference Infrastructure/WinForms.
- SQL không nằm trong Form nghiệp vụ.
- Transaction không nằm trong Form.
- Connection string central.
- Repository interface ở Application.
- Implementation ở Infrastructure.
- Presenter cho màn hình phức tạp.
- Các business rule quan trọng có test.
