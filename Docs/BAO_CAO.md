# BÁO CÁO ĐỒ ÁN – WEBSITE BÁN HÀNG MINISTORE

## 1. Mục tiêu
Xây dựng website bán hàng bằng ASP.NET MVC 5 và Entity Framework, áp dụng mô hình Model–View–Controller và SQL Server.

## 2. Chức năng
### Khách hàng
- Xem trang chủ, danh sách sản phẩm và chi tiết sản phẩm.
- Tìm kiếm sản phẩm theo tên, mô tả hoặc danh mục.
- Sắp xếp theo tên/giá và phân trang.
- Đăng ký, đăng nhập, đăng xuất.
- Thêm sản phẩm vào giỏ, sửa số lượng, xóa sản phẩm.
- Đặt hàng COD, xem xác nhận và lịch sử đơn hàng.

### Quản trị
- Đăng nhập bằng tài khoản có vai trò Admin.
- Xem thống kê số danh mục, sản phẩm, đơn hàng.
- Thêm/xóa danh mục.
- Tìm kiếm, thêm/xóa sản phẩm.

## 3. Thiết kế cơ sở dữ liệu
- User(Username PK, Password, UserRole)
- Customer(CustomerID PK, Username FK)
- Category(CategoryID PK)
- Product(ProductID PK, CategoryID FK)
- Order(OrderID PK, CustomerID FK)
- OrderDetail(ID PK, ProductID FK, OrderID FK, TotalPrice tính từ Quantity × UnitPrice)

## 4. Luồng nghiệp vụ đặt hàng
1. Khách hàng đăng nhập.
2. Chọn sản phẩm và thêm vào giỏ hàng.
3. Kiểm tra giỏ hàng và nhập địa chỉ giao hàng.
4. Chọn giao hàng và COD.
5. Hệ thống lưu Order và OrderDetail.
6. Xóa giỏ hàng trong Session và hiển thị xác nhận đơn hàng.

## 5. Công nghệ
ASP.NET MVC 5, C#, Entity Framework 6, SQL Server, Razor View, Bootstrap.

## 6. Hình ảnh minh họa
Sau khi chạy project, chụp màn hình các trang sau và chèn vào báo cáo PDF:
1. Trang chủ.
2. Danh sách sản phẩm có tìm kiếm/sắp xếp.
3. Chi tiết sản phẩm.
4. Giỏ hàng.
5. Đăng ký/đăng nhập.
6. Thanh toán và xác nhận đơn hàng.
7. Trang quản trị danh mục/sản phẩm.

## 7. Hướng phát triển
- Bổ sung sửa sản phẩm/danh mục đầy đủ và upload ảnh.
- Bổ sung phân trang khu vực Admin.
- Bổ sung đổi mật khẩu, cập nhật hồ sơ.
- Tích hợp PayPal Sandbox theo hướng dẫn giảng viên.
- Chuyển sang ASP.NET Identity và bổ sung kiểm thử bảo mật.

## 8. Kết luận
MiniStore minh họa quy trình bán hàng cơ bản, từ xem sản phẩm, quản lý giỏ hàng đến đặt hàng và quản trị dữ liệu.
