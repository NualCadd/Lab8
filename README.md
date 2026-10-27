# CSLab08 - Windows Forms và LINQ to SQL

## Mở và chạy
1. Dùng Visual Studio hỗ trợ `.slnx` (Visual Studio 2022 17.12 trở lên), cài workload **.NET desktop development** và **.NET Framework 4.8 targeting pack**.
2. Chạy `Database/QLHS.sql` trong SQL Server Management Studio để tạo CSDL `QuanLyHocSinh` và dữ liệu mẫu. Script không xóa dữ liệu đã có.
3. Sửa `Data Source` trong `QLHocSinh/App.config` theo SQL Server của bạn. Mặc định: `(localdb)\MSSQLLocalDB`. Nếu dùng SQL Express, thay bằng `.\SQLEXPRESS`.
4. Mở `CSLab08_LINQ.slnx`, chọn QLHocSinh làm startup project, nhấn F5.

Nếu Visual Studio cũ chưa mở được slnx, mở trực tiếp `QLHocSinh/QLHocSinh.csproj`.

## Chức năng
- Chọn lớp, tìm gần đúng theo tên, hiển thị danh sách và điểm trung bình bằng LINQ.
- Chọn dòng để sửa/xóa; bấm Làm mới trước khi thêm học sinh khác.
- Thêm, sửa đầy đủ tên, ngày sinh, địa chỉ, điểm và lớp đang chọn; mã là khóa chính, không sửa sau khi chọn.
- Kiểm tra mã trùng, thông tin bắt buộc, ngày sinh và điểm từ 0 đến 10.
- Lớp dữ liệu: QLHSDataContext và các ánh xạ bảng; lớp giao diện: frmQLHocSinh.

## Ghi chú
Dùng .NET Framework 4.8 để có LINQ to SQL (`System.Data.Linq`) đúng công nghệ bài học. Không cần gói NuGet hay file .dbml vì các bảng được ánh xạ bằng thuộc tính trong QLHSDataContext.cs. SQL Server phải được cài và cấu hình riêng; chương trình không tự tạo hoặc thay đổi máy chủ.

Build bằng dòng lệnh: `dotnet build CSLab08_LINQ.slnx`.

