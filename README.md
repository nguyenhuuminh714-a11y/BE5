# MiniStore MVC5 – Website bán hàng

Đồ án ASP.NET MVC 5 + Entity Framework 6, dựa trên yêu cầu trong slide Customer Site, Admin Site và cấu trúc CSDL MyStore.

## Chức năng trong bản mẫu
- Trang chủ, danh sách sản phẩm, tìm kiếm theo tên/mô tả/danh mục, sắp xếp và phân trang.
- Chi tiết sản phẩm.
- Giỏ hàng lưu trong Session: thêm, cập nhật số lượng, xóa, làm trống.
- Đăng ký, đăng nhập, đăng xuất khách hàng.
- Đặt hàng COD, lưu Order và OrderDetail, trang xác nhận và lịch sử đơn hàng.
- Khu vực Admin: CRUD danh mục và sản phẩm.
- SQL tạo CSDL và dữ liệu mẫu nằm trong `Database/MyStore.sql`.

## Công nghệ
- ASP.NET MVC 5 (.NET Framework 4.8)
- Entity Framework 6 (Database First/EDMX tương thích)
- SQL Server LocalDB hoặc SQL Server Express
- Bootstrap 4 CDN

## Cách mở/chạy trong Visual Studio
1. Giải nén ZIP.
2. Mở `MiniStore.sln` bằng Visual Studio 2022.
3. Nếu máy chưa có workload ASP.NET and web development, cài workload đó.
4. Mở `Database/MyStore.sql` trong SQL Server Object Explorer hoặc SSMS và chạy script.
5. Mở `MiniStore/Web.config`; chỉnh `DefaultConnection` nếu SQL Server của bạn không dùng `(localdb)\\MSSQLLocalDB`.
6. Restore NuGet packages (Visual Studio thường tự restore). Nếu cần, mở Package Manager Console và chạy:
   `Install-Package EntityFramework -Version 6.4.4`
7. Build Solution rồi nhấn Ctrl+F5.
8. Trang mặc định: `/Home/Index`. Khu vực quản trị: `/Admin`.

## Tài khoản mẫu
- Admin: `admin` / `Admin123!`
- Khách: `customer` / `Customer123!`

**Lưu ý:** Đây là mật khẩu demo để chạy bài tập; không dùng mật khẩu này cho website thật. Đổi mật khẩu trước khi triển khai thật.

## Đẩy lên GitHub
1. Tạo repository mới trên GitHub.
2. Giải nén project, mở terminal tại thư mục có `MiniStore.sln`.
3. Chạy:
   ```bash
   git init
   git add .
   git commit -m "Initial MiniStore MVC5 project"
   git branch -M main
   git remote add origin https://github.com/USERNAME/REPOSITORY.git
   git push -u origin main
   ```
4. Thay URL remote bằng URL repository của bạn.

## Phạm vi / điểm cần biết
- Thanh toán PayPal trong slide là phần tùy chọn, bản này dùng COD để không cần secret/API key.
- Password demo được hash SHA256 để minh họa; với hệ thống thật nên dùng ASP.NET Identity và password hasher chuyên dụng.
- Không commit file `bin/`, `obj/`, `.vs/`, file `.user` lên GitHub.
