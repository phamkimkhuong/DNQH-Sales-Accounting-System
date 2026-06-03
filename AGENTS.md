# AGENTS.md
## Luật bắt buộc cho Antigravity / AI Coding Agent

> Đọc cùng:
> - `01_PROJECT_SPEC.md`
> - `02_ARCHITECTURE.md`
> - `03_ENGINEERING_STANDARDS.md`
> - `04_OPERATIONS_OBSERVABILITY.md`

## 1. Stack cố định
- C#.
- Windows Forms.
- **.NET Framework 4.8**.
- SQL Server 2022 Express.
- ADO.NET.
- `System.Data.SqlClient`.

Không tự chuyển sang:
- .NET 8/10;
- ASP.NET;
- Web/API;
- Entity Framework;
- microservices.

## 2. Trước khi sửa code
Bắt buộc:
1. inspect toàn bộ solution/repository;
2. đọc đủ 4 tài liệu spec;
3. liệt kê project/file hiện có;
4. xác định phase hiện tại;
5. phát hiện mâu thuẫn;
6. báo trước nếu cần schema change.

Không overwrite file quan trọng nếu chưa đọc.

## 3. Dependency rules
- Domain không reference WinForms/Infrastructure/SqlClient.
- Application không chứa SQL/MessageBox.
- Infrastructure implement persistence/logging/security.
- WinForms không chứa transaction/business logic lớn.

## 4. Không SQL trong Form
Không viết raw SQL trực tiếp trong event handler, ngoại trừ smoke-test tạm thời được yêu cầu rõ.

## 5. Transaction
Các use case nhiều bảng phải atomic:
- CreateOrder;
- CreateInvoice;
- IssueInventory;
- CreateAccountingDocument.

Form không sở hữu `SqlTransaction`.

## 6. Database invariants
Không phá:
- `DONDATHANG 1 → 0..1 HOADONBAN`.
- Không `DONDATHANG.MaHDB`.
- `HOADONBAN.MaDDH` FK + UNIQUE.
- Không `SoLuongTon` trong `SANPHAM`.
- Tồn chỉ ở `TONKHO`.
- `PHIEUTHU → HOADONBAN`.
- `PHIEUCHI` độc lập, chỉ gắn `NHANVIEN`.
- `CHUNGTU → HOADONBAN`.
- Không nối `CHUNGTU` với `PHIEUTHU/PHIEUCHI`.

## 7. Không tự thêm module
Không thêm:
- kế toán lương;
- phiếu nhập kho;
- mua hàng;
- công nợ NCC;
- customer login;
- web/API/mobile.

## 8. Security
- parameterized SQL.
- PBKDF2 + salt.
- không log password.
- authorization UI + Application.
- không hard-code credential.

## 9. Logging
Operation quan trọng phải có:
- CorrelationId;
- UserId;
- Operation;
- EntityId;
- Duration;
- Result;
- Exception khi lỗi.

## 10. Async
Ưu tiên async cho DB I/O.
Không dùng `.Result`/`.Wait()` trên UI thread.

## 11. UI
- dùng WinForms Designer khi có thể;
- không phá `.Designer.cs`;
- controls naming đúng convention;
- Form phức tạp dùng Presenter-lite;
- không làm UI freeze.

## 12. Tests
Business rule quan trọng phải có unit test.
Repository/transaction quan trọng nên có integration test.

## 13. Build discipline
Sau mỗi phase:
1. build solution;
2. sửa compile errors;
3. chạy tests;
4. báo file changed;
5. hướng dẫn manual test;
6. không tự sang phase mới nếu phase hiện tại chưa ổn.

## 14. Database migrations
Nếu cần schema change:
- không âm thầm sửa script cũ;
- tạo migration mới;
- giải thích;
- chờ approval nếu ngoài spec.

## 15. Code quality
- method nhỏ;
- tên rõ;
- không giant service;
- không catch rỗng;
- không duplicate logic;
- không magic role/status rải rác;
- không overengineering.

## 16. Thứ tự ưu tiên khi mâu thuẫn
1. Database thực tế đã xác nhận.
2. `01_PROJECT_SPEC.md`.
3. `02_ARCHITECTURE.md`.
4. `03_ENGINEERING_STANDARDS.md`.
5. `04_OPERATIONS_OBSERVABILITY.md`.
6. Code hiện tại.
7. Suy đoán agent.

Không tự quyết định đổi nghiệp vụ.

## 17. Format báo cáo sau mỗi task
```text
Summary
Files created
Files modified
Architecture notes
Build result
Test result
Manual test steps
Known issues
Next recommended task
```

## 18. Phase order
1. Foundation.
2. Authentication.
3. Master Data CRUD.
4. Sales.
5. Warehouse.
6. Cash/Documents.
7. Reporting.
8. Polish/Testing.

Không làm toàn bộ project trong một lần.

## 19. Foundation phase tối thiểu
- solution/project references đúng layer;
- App.config;
- connection factory/config;
- logging bootstrap;
- SessionManager;
- password hasher abstraction;
- models cần cho authentication;
- DB smoke test;
- unit test project;
- build xanh.

## 20. Prompt khởi động chuẩn
```text
Read AGENTS.md and all project specification files completely before changing code.

Treat .NET Framework 4.8 as fixed.

First inspect the current solution and repository. Report:
1. existing projects and references;
2. existing files that can be reused;
3. conflicts with the specification;
4. current implementation phase.

Do not redesign the database.
Do not add modules.
Do not change schema without approval.

Work on the current approved phase only.
Follow the dependency rules in 02_ARCHITECTURE.md and quality rules in 03_ENGINEERING_STANDARDS.md.
Add logging/diagnostics according to 04_OPERATIONS_OBSERVABILITY.md.

After changes:
- build the complete solution;
- run applicable tests;
- fix compile errors;
- report exactly which files changed;
- provide manual testing steps;
- stop before the next phase unless explicitly instructed.
```
