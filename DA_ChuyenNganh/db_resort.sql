-- Sử dụng database DB_Resort
USE DB_Resort
GO

-- Bảng Customers (Khách hàng)
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    PhoneNumber NVARCHAR(15),
    Address NVARCHAR(255),
    Username NVARCHAR(50) UNIQUE,
    Password NVARCHAR(255),
    RegistrationDate DATETIME
);

-- Bảng Rooms (Phòng)
CREATE TABLE Rooms (
    RoomID INT PRIMARY KEY IDENTITY(1,1),
    RoomType NVARCHAR(50), -- Loại phòng (Single, Double, Suite)
    Price DECIMAL(10,2), -- Giá phòng
    Status NVARCHAR(50), -- Trạng thái phòng (Available, Booked, Maintenance)
    Description NVARCHAR(255),
	imageRooms NVARCHAR(255)
);

-- Bảng Services (Dịch vụ)
CREATE TABLE Services (
    ServiceID INT PRIMARY KEY IDENTITY(1,1),
    ServiceName NVARCHAR(100),
    ServiceCategory NVARCHAR(50), -- Phân loại dịch vụ (Spa, Food, Tour)
    Price DECIMAL(10,2),
    Description NVARCHAR(255),
	statusServices NVARCHAR(255),
	imageServices NVARCHAR(255)
);
CREATE TABLE Promotions (
    PromotionID INT PRIMARY KEY IDENTITY(1,1),
    PromotionCode NVARCHAR(20) UNIQUE, -- Mã khuyến mãi
    Description NVARCHAR(255), -- Mô tả khuyến mãi
    Discount DECIMAL(5,2), -- Giảm giá tính theo %
    IsFlashDeal BIT DEFAULT 0, -- Có phải là ưu đãi chớp nhoáng không (0: không, 1: có)
    StartDate DATETIME, -- Ngày bắt đầu khuyến mãi
    EndDate DATETIME -- Ngày kết thúc khuyến mãi
);
-- Bảng Bookings (Đặt phòng)
CREATE TABLE Bookings (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    RoomID INT,
    PromotionID INT NULL, -- Mã khuyến mãi sử dụng
    NumberOfGuests INT, -- Số lượng khách
    BookingDate DATETIME,
    CheckInDate DATETIME,
    CheckOutDate DATETIME,
    TotalPrice DECIMAL(10,2),
    Status NVARCHAR(50), -- Trạng thái (Confirmed, Canceled, Checked-in, Checked-out)
    PaymentType NVARCHAR(50), -- Hình thức thanh toán (Prepaid, Upon Arrival)
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
    FOREIGN KEY (PromotionID) REFERENCES Promotions(PromotionID)
);

-- Bảng Invoices (Hóa đơn)
CREATE TABLE Invoices (
    InvoiceID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT,
    InvoiceDate DATETIME,
    TotalAmount DECIMAL(10,2),
    IsPaid BIT DEFAULT 0, -- Đã thanh toán hay chưa
    PaymentStatus NVARCHAR(50), -- Paid, Unpaid
    PaymentMethod NVARCHAR(50), -- Phương thức thanh toán
    FOREIGN KEY (BookingID) REFERENCES Bookings(BookingID)
);

-- Bảng Reviews (Đánh giá)
CREATE TABLE Reviews (
    ReviewID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    RoomID INT,
    ServiceID INT NULL, -- Đánh giá dịch vụ
    Rating INT, -- Đánhd giá từ 1 đến 5
    Comment NVARCHAR(255),
    ReviewDate DATETIME,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
    FOREIGN KEY (ServiceID) REFERENCES Services(ServiceID)
);

-- Bảng Admins (Quản trị viên)
CREATE TABLE Admins (
    AdminID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) UNIQUE,
    Password NVARCHAR(255),
    FullName NVARCHAR(100),
    Role NVARCHAR(50), -- Vai trò quản trị viên (SuperAdmin, Manager)
    LastLogin DATETIME -- Thời gian đăng nhập cuối
);

-- Bảng Promotions (Khuyến mãi) với ưu đãi giờ vàng



-- Bảng UsedServices (Dịch vụ sử dụng)
CREATE TABLE UsedServices (
    UsedServiceID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT,
    ServiceID INT,
    Quantity INT, -- Số lượng dịch vụ
    TotalPrice DECIMAL(10,2), -- Tổng chi phí cho dịch vụ
    FOREIGN KEY (BookingID) REFERENCES Bookings(BookingID),
    FOREIGN KEY (ServiceID) REFERENCES Services(ServiceID)
);

