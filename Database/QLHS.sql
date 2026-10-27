-- Chạy trong SQL Server Management Studio hoặc sqlcmd.
IF DB_ID(N'QuanLyHocSinh') IS NULL CREATE DATABASE QuanLyHocSinh;
GO
USE QuanLyHocSinh;
GO
IF OBJECT_ID(N'dbo.Lop', N'U') IS NULL
CREATE TABLE dbo.Lop (
    MaLop nvarchar(20) NOT NULL PRIMARY KEY,
    TenLop nvarchar(50) NOT NULL
);
IF OBJECT_ID(N'dbo.HocSinh', N'U') IS NULL
CREATE TABLE dbo.HocSinh (
    MaHS nvarchar(20) NOT NULL PRIMARY KEY,
    TenHS nvarchar(100) NOT NULL,
    NgaySinh date NOT NULL,
    DiaChi nvarchar(250) NOT NULL,
    MaLop nvarchar(20) NOT NULL REFERENCES dbo.Lop(MaLop),
    DTB real NOT NULL CHECK (DTB >= 0 AND DTB <= 10)
);
GO
IF NOT EXISTS (SELECT 1 FROM dbo.Lop WHERE MaLop = N'10A1')
INSERT dbo.Lop VALUES (N'10A1', N'Lớp 10A1');
IF NOT EXISTS (SELECT 1 FROM dbo.Lop WHERE MaLop = N'10A2')
INSERT dbo.Lop VALUES (N'10A2', N'Lớp 10A2');
IF NOT EXISTS (SELECT 1 FROM dbo.HocSinh WHERE MaHS = N'HS001')
INSERT dbo.HocSinh VALUES (N'HS001', N'Nguyễn Văn An', '20100115', N'TP. Hồ Chí Minh', N'10A1', 8.5);
IF NOT EXISTS (SELECT 1 FROM dbo.HocSinh WHERE MaHS = N'HS002')
INSERT dbo.HocSinh VALUES (N'HS002', N'Trần Thị Bình', '20100520', N'Đồng Nai', N'10A1', 9.0);
IF NOT EXISTS (SELECT 1 FROM dbo.HocSinh WHERE MaHS = N'HS003')
INSERT dbo.HocSinh VALUES (N'HS003', N'Lê Minh Châu', '20100910', N'Long An', N'10A2', 7.5);
GO

