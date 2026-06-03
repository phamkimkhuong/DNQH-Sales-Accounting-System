# 04_OPERATIONS_OBSERVABILITY.md
## Vận hành, logging, giám sát và khả năng điều tra lỗi

> **RUN — cách hệ thống được theo dõi, chẩn đoán, backup và vận hành ổn định.**

## 1. Mục tiêu
Khi có lỗi phải trả lời được:
- lỗi xảy ra lúc nào;
- user nào thao tác;
- thao tác nào;
- entity nào liên quan;
- mất bao lâu;
- exception gì;
- transaction commit hay rollback;
- database có reachable hay không.

## 2. Logging strategy
Dùng abstraction `IAppLogger` hoặc `ILogger` tương thích .NET Framework 4.8.

Implementation khuyến nghị:
- Serilog hoặc NLog;
- rolling log file theo ngày.

Thư mục:
```text
logs/
app-2026-09-06.log
error-2026-09-06.log
```

## 3. Log levels
- TRACE: chi tiết rất sâu, mặc định off.
- DEBUG: debug dev.
- INFO: lifecycle/nghiệp vụ thành công.
- WARN: bất thường recover được.
- ERROR: operation thất bại.
- FATAL: ứng dụng không thể tiếp tục an toàn.

## 4. Structured context
Mỗi operation quan trọng nên log:
- Timestamp;
- Level;
- OperationName;
- UserId;
- Role;
- EntityType;
- EntityId;
- CorrelationId;
- MachineName;
- DurationMs;
- Result;
- Exception.

Ví dụ:
```text
INFO Operation=CreateInvoice UserId=NV001 Role=Sales
OrderId=DDH001 InvoiceId=HDB001 DurationMs=84
Result=Success CorrelationId=5ca9...
```

## 5. Correlation ID
Mỗi nghiệp vụ lớn tạo một `Guid`.

```csharp
var correlationId = Guid.NewGuid().ToString("N");
```

Truyền qua:
```text
Form/Presenter
→ Application Service
→ Repository
→ Log
```

Áp dụng cho:
- CreateOrder;
- CreateInvoice;
- IssueInventory;
- CreateReceipt;
- CreatePayment;
- CreateAccountingDocument;
- GenerateReport.

## 6. Technical log và business audit
### Technical log
- DB connection error.
- timeout.
- `SqlException`.
- unhandled exception.
- slow report.
- transaction rollback.

### Business audit
- ai login/logout;
- ai tạo/hủy đơn;
- ai lập hóa đơn;
- ai xuất kho;
- ai lập phiếu thu/chi;
- ai thay đổi role;
- ai tạo chứng từ.

Phiên bản đồ án **không bắt buộc thêm bảng AUDITLOG**.
Structured file log đủ cho version hiện tại.
Nếu sau này được phép đổi schema, có thể bổ sung Audit table qua migration.

## 7. Global exception handling
WinForms startup đăng ký:
```csharp
Application.ThreadException += OnThreadException;
AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
```

Handler:
1. log full exception;
2. hiển thị message thân thiện;
3. nếu trạng thái app không còn đáng tin → shutdown/restart an toàn.

Không nuốt lỗi để app tiếp tục giả vờ bình thường.

## 8. Database health
Tạo `SystemHealthService` tối thiểu:
```text
TestDatabaseConnectionAsync()
GetDatabaseName()
GetServerVersion()
CheckLogDirectoryWritable()
```

Có thể hiển thị ở `frmMain`:
```text
Database: Connected
Server: localhost\SQLEXPRESS
Database: DNQH_KeToanBanHang
```

## 9. Startup diagnostics
Khi app start:
- kiểm config tồn tại;
- kiểm log folder writable;
- log application version;
- lỗi DB phải phân biệt với sai username/password.

## 10. Performance timing
Đo duration cho operation quan trọng.

Ngưỡng gợi ý:
- search > 1 giây → WARN;
- save transaction > 2 giây → WARN;
- report > 3 giây → WARN.

Ngưỡng nên config, không hard-code nhiều nơi.

## 11. SQL diagnostics
Khi catch `SqlException`, log:
- Number;
- State;
- Class;
- Procedure;
- LineNumber;
- Operation;
- CorrelationId.

Không hiển thị raw SQL/stack trace cho end-user.
Không log password/secret.

## 12. Audit events tối thiểu
- LoginSuccess / LoginFailure.
- Logout.
- PasswordChanged.
- AccountCreated/Updated/Disabled.
- RoleChanged.
- OrderCreated/Cancelled.
- InvoiceCreated.
- WarehouseIssueCreated.
- InventoryUpdated.
- ReceiptCreated.
- PaymentCreated.
- AccountingDocumentCreated.

## 13. Backup runbook
Trước demo hoặc thay đổi schema lớn:
- backup database;
- lưu `.bak` ở thư mục backup;
- ghi ngày/version.

Ví dụ:
```sql
BACKUP DATABASE DNQH_KeToanBanHang
TO DISK = 'C:\Backup\DNQH_KeToanBanHang.bak'
WITH INIT, STATS = 10;
```

Không commit backup binary lớn vào Git nếu không cần.

## 14. Restore runbook
Trước restore:
1. xác nhận đúng file backup;
2. xác nhận đúng database đích;
3. ưu tiên restore sang DB test nếu cần kiểm tra;
4. không overwrite DB hiện tại khi chưa backup.

## 15. Data integrity diagnostics
Các query kiểm tra manual/định kỳ:

```sql
SELECT * FROM TONKHO WHERE SoLuongTon < 0;
```

Kiểm thêm:
- hóa đơn không có chi tiết;
- đơn không có chi tiết;
- trùng `HOADONBAN.MaDDH`;
- trùng `TAIKHOAN.MaNV`;
- trùng `TenDangNhap`.

Constraints DB phải là lớp phòng thủ chính.

## 16. Operational info trên UI
`frmMain` có thể hiển thị:
- user;
- role;
- ngày/giờ;
- app version;
- DB connection state.

Dashboard quản trị có thể hiển thị:
- doanh thu hôm nay;
- tổng thu;
- tổng chi;
- cảnh báo tồn thấp (optional).

## 17. App versioning
Dùng:
- `AssemblyVersion`;
- `AssemblyFileVersion`.

Log startup:
```text
ApplicationStarted
Version=1.0.0
Machine=DESKTOP-...
User=...
```

Hiển thị version ở About/status bar.

## 18. Environment config
Có thể duy trì:
- Development config;
- Demo/Test config.

Không để integration test trỏ nhầm DB demo.

## 19. Troubleshooting: không đăng nhập được
1. SQL Server service có chạy không.
2. Instance `SQLEXPRESS` đúng không.
3. SSMS có connect được không.
4. Database tồn tại không.
5. Connection string đúng không.
6. Windows Authentication đúng không.
7. `TrustServerCertificate=True` khi cần.
8. Đọc error log.

## 20. Troubleshooting: xuất kho lỗi
1. lấy CorrelationId;
2. đọc log;
3. kiểm `PHIEUXUATKHO`;
4. kiểm `CHITIETPHIEUXUATKHO`;
5. kiểm `TONKHO`;
6. xác nhận transaction atomic.

Nếu transaction chuẩn:
```text
hoặc tất cả commit
hoặc không có thay đổi nào
```

## 21. Monitoring scope
Không cần triển khai hệ thống observability phân tán phức tạp cho đồ án.

Không tự ý thêm:
- Prometheus;
- Grafana;
- OpenTelemetry Collector;
- Jaeger;
- ELK stack.

Kiến trúc chỉ cần đủ abstraction để sau này mở rộng.

## 22. Operations acceptance criteria
- Rolling logs hoạt động.
- Exception quan trọng được log.
- Login/order/invoice/warehouse issue có audit event.
- Operation lớn có CorrelationId.
- Rollback được log.
- App version được log.
- DB connectivity dễ chẩn đoán.
- Log không chứa password.
