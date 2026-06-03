# 03_ENGINEERING_STANDARDS.md
## Chuẩn kỹ thuật, bảo trì và chất lượng code

> **QUALITY — tiêu chuẩn để code thể hiện tư duy của một lập trình viên chuyên nghiệp.**

## 1. Thứ tự ưu tiên
1. Correctness.
2. Data integrity.
3. Readability.
4. Maintainability.
5. Testability.
6. Security.
7. Observability.
8. Performance khi có bằng chứng cần tối ưu.

Không dùng công nghệ phức tạp chỉ để trông “hiện đại”.

## 2. SOLID áp dụng thực tế

### SRP
Một class chỉ có một trách nhiệm chính.

Sai:
```text
HoaDonService:
- query SQL
- validate
- MessageBox
- export file
- authorization
- logging
```

Đúng:
```text
CreateInvoiceService
IInvoiceRepository
InvoiceValidator
AuthorizationService
frmHoaDonBan
```

### OCP
Ưu tiên mở rộng qua interface/implementation thay vì sửa nhiều call sites.

### ISP
Không tạo interface khổng lồ như `IEverythingRepository`.

### DIP
Application phụ thuộc abstraction; Infrastructure implement.

## 3. Naming
- Namespace/Class/Public Method/Property: PascalCase.
- Interface: `I` + PascalCase.
- private field: `_camelCase`.
- local/parameter: camelCase.
- async method: hậu tố `Async`.

WinForms:
- Form: `frmXxx`
- Button: `btnXxx`
- TextBox: `txtXxx`
- ComboBox: `cboXxx`
- DataGridView: `dgvXxx`
- Label: `lblXxx`
- DateTimePicker: `dtpXxx`

Ví dụ:
```text
IOrderRepository
CreateInvoiceAsync
_orderRepository
orderId
frmHoaDonBan
btnLapHoaDon
```

## 4. Quy mô class/method
Không phải giới hạn cứng, nhưng phải review khi:
- Form code-behind > 500–700 dòng.
- Service > 400–500 dòng.
- Method > 50–80 dòng.
- Nested > 4 tầng.
- Constructor có quá nhiều dependency.

Khi vượt: extract responsibility trước khi tiếp tục thêm logic.

## 5. Event handler
Event handler chỉ nên:
1. đọc input UI;
2. gọi Presenter/Application Service;
3. cập nhật UI/hiển thị kết quả.

Không được:
- chứa transaction;
- raw SQL;
- tính toán nghiệp vụ lớn;
- logic permission phức tạp.

## 6. Validation
- `Trim()` text.
- required field.
- `decimal.TryParse`.
- `int.TryParse`.
- DateTimePicker cho ngày.
- không dùng exception làm flow bình thường.

Quy tắc:
- `SoLuong > 0`.
- `SoLuongXuat > 0`.
- `SoLuongTon >= 0`.
- `DonGia >= 0`.
- `SoTien > 0`.
- `0 <= GiamGia <= 100`.

## 7. Tiền tệ
- Dùng `decimal`.
- Không dùng `float`/`double`.
- Format UI nhất quán `N0` hoặc `N2`.

## 8. SQL coding standard
Bắt buộc:
- parameterized SQL;
- column list explicit;
- không `SELECT *` nếu có thể tránh;
- kiểm affected rows;
- transaction cho multi-table write;
- query report có timeout phù hợp;
- không concat input user.

Không:
```csharp
"SELECT * FROM TAIKHOAN WHERE TenDangNhap='" + user + "'"
```

## 9. Password security
- PBKDF2.
- random salt.
- không lưu plaintext.
- không có chức năng “giải mã mật khẩu”.
- không log password/hash đầy đủ.

Nên có:
```csharp
public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string storedHash);
}
```

Stored format nên chứa version + iterations + salt + hash để có thể nâng thuật toán sau này.

## 10. Error handling
Cấm:
```csharp
catch (Exception) { }
```

Cấm hiển thị:
```csharp
MessageBox.Show(ex.ToString());
```

Chuẩn:
- catch tại boundary có thể xử lý;
- log full exception;
- UI hiển thị message tiếng Việt thân thiện;
- Infrastructure wrap lỗi DB khi cần.

## 11. Structured logging
Log phải có context.

Tốt:
```text
Operation=CreateInvoice
UserId=NV001
OrderId=DDH001
InvoiceId=HDB001
DurationMs=82
Result=Success
```

Không tốt:
```text
Đã xong
```

Không log:
- password;
- secrets;
- credential;
- raw sensitive connection string.

## 12. .editorconfig
Repository phải có `.editorconfig`.

Khởi điểm:
```ini
root = true

[*.cs]
indent_style = space
indent_size = 4
charset = utf-8-bom
end_of_line = crlf
insert_final_newline = true

dotnet_naming_rule.interfaces_should_start_with_i.severity = warning
dotnet_naming_rule.interfaces_should_start_with_i.symbols = interfaces
dotnet_naming_rule.interfaces_should_start_with_i.style = prefix_i

dotnet_naming_symbols.interfaces.applicable_kinds = interface

dotnet_naming_style.prefix_i.required_prefix = I
dotnet_naming_style.prefix_i.capitalization = pascal_case
```

## 13. Static analysis
- bật compiler warnings;
- không tắt warning khi chưa hiểu nguyên nhân;
- có thể dùng Roslyn analyzer tương thích .NET Framework 4.8;
- StyleCop optional.

## 14. Unit testing
Unit test không phụ thuộc SQL Server.

Test tối thiểu:
- tính `ThanhTien`;
- giảm giá;
- quantity validation;
- duplicate invoice rule;
- insufficient stock;
- authorization;
- password hasher;
- order/invoice totals.

Ví dụ:
```text
CreateInvoice_WhenOrderAlreadyHasInvoice_ShouldThrowDuplicateInvoiceException
IssueInventory_WhenStockIsInsufficient_ShouldFail
CalculateLineTotal_WithTenPercentDiscount_ShouldReturnExpectedAmount
```

## 15. Integration testing
Dùng database test riêng.

Test:
- repository CRUD;
- FK/UNIQUE;
- transaction rollback;
- issue inventory;
- invoice creation.

Không chạy integration tests lên database chứa dữ liệu demo quan trọng.

## 16. Test pattern
Dùng Arrange–Act–Assert.

```text
Arrange
Act
Assert
```

Mỗi test độc lập và deterministic.

## 17. Git workflow
Branches:
```text
main
feature/authentication
feature/customer-crud
feature/order-management
feature/inventory
feature/accounting
fix/stock-validation
```

Commit:
```text
feat: implement order creation transaction
fix: prevent negative inventory
refactor: extract invoice repository
test: add inventory rollback tests
docs: add inventory ADR
```

Không dùng:
```text
update
fix
done
final
```
làm commit message thường xuyên.

## 18. Self-review checklist
Trước merge/hoàn tất task:
- build pass;
- tests pass;
- không debug code bỏ quên;
- không hard-code secret;
- không SQL injection;
- logging đủ;
- authorization đủ;
- transaction đúng;
- không làm UI freeze;
- docs cập nhật;
- schema chỉ đổi qua migration.

## 19. Database versioning
Không lưu:
```text
database-final.sql
database-final-2.sql
database-final-final.sql
```

Dùng:
```text
database/migrations/
V001__Initial_schema.sql
V002__Add_unique_username.sql
V003__Add_constraints.sql
V004__Add_indexes.sql
```

Migration đã áp dụng không được âm thầm sửa; thay đổi mới tạo migration mới.

## 20. Seed data
Tách:
```text
database/seed/DevelopmentData.sql
database/seed/DemoData.sql
```

Không trộn seed vào schema migration chính nếu không cần.

## 21. Architecture Decision Records
`docs/adr/`

Ví dụ:
```text
ADR-001-use-net-framework-48.md
ADR-002-use-winforms.md
ADR-003-use-ado-net.md
ADR-004-use-mvp-lite.md
ADR-005-update-stock-only-on-warehouse-issue.md
```

Template:
```text
Title
Status
Context
Decision
Consequences
Alternatives considered
```

Ví dụ ADR-005:
```text
Decision:
Tồn kho chỉ giảm khi phiếu xuất kho được commit.

Reason:
Đơn đặt hàng/hóa đơn không đồng nghĩa hàng đã rời kho.

Consequence:
IssueInventoryService sở hữu transaction cập nhật TONKHO.
```

## 22. Documentation
README tối thiểu:
- prerequisites;
- cách restore DB;
- connection config;
- build;
- run;
- tài khoản demo;
- cấu trúc solution.

Các rule lớn phải có tài liệu, không chỉ tồn tại trong đầu người viết code.

## 23. Definition of Done
Feature chỉ hoàn thành khi:
- build pass;
- behavior đúng;
- validation đủ;
- permission đúng;
- logging đủ;
- error handling đủ;
- tests hợp lý;
- manual test pass;
- không TODO quan trọng;
- docs/migration cập nhật nếu cần.

## 24. Không overengineering
Không tự ý thêm:
- CQRS framework;
- MediatR;
- event bus;
- microservices;
- Redis;
- Kafka;
- Docker infrastructure;
- distributed system stack.

Chỉ thêm khi có yêu cầu thật.
