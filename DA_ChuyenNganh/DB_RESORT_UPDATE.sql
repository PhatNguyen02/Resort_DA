create database DB_Resortf
go
use DB_Resortf
go
-- Bảng Users (Khách hàng và Quản trị viên)
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    PhoneNumber NVARCHAR(15),
    Address NVARCHAR(255),
    Username NVARCHAR(50) UNIQUE,
    Password NVARCHAR(255),
    RegistrationDate DATETIME,
    Role NVARCHAR(50) -- Vai trò (Customer, Admin)
);

-- Bảng Rooms (Phòng)
CREATE TABLE Rooms (
    RoomID INT PRIMARY KEY IDENTITY(1,1),
    RoomType NVARCHAR(50), -- Loại phòng (Single, Double, Suite)
    Price DECIMAL(10,2), -- Giá phòng
    Status NVARCHAR(50), -- Trạng thái phòng (Available, Booked, Maintenance)
    Description NVARCHAR(255),
    ImageRooms NVARCHAR(255)
);

-- Bảng Services (Dịch vụ)
CREATE TABLE Services (
    ServiceID INT PRIMARY KEY IDENTITY(1,1),
    ServiceName NVARCHAR(100),
    ServiceCategory NVARCHAR(50), -- Phân loại dịch vụ (Spa, Food, Tour)
    Price DECIMAL(10,2),
    Description NVARCHAR(255),
    StatusServices NVARCHAR(255),
    ImageServices NVARCHAR(255)
);

-- Bảng Promotions (Khuyến mãi)
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
    UserID INT, -- Sử dụng UserID từ bảng Users
    RoomID INT,
    PromotionID INT NULL, -- Mã khuyến mãi sử dụng
    NumberOfGuests INT, -- Số lượng khách
    BookingDate DATETIME,
    CheckInDate DATETIME,
    CheckOutDate DATETIME,
    TotalPrice DECIMAL(10,2),
    Status NVARCHAR(50), -- Trạng thái (Confirmed, Canceled, Checked-in, Checked-out)
    PaymentType NVARCHAR(50), -- Hình thức thanh toán (Prepaid, Upon Arrival)
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
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

-- Bảng Invoices_Detail (Hóa đơn chi tiết)
CREATE TABLE Invoices_Detail (
    InvoiceDetailID INT PRIMARY KEY IDENTITY(1,1),
    InvoiceID INT,
	UserID INT,
    UsedServiceID INT NULL, 
    RoomID INT NULL, 
    Quantity INT,
    UnitPrice DECIMAL(10,2),
    TotalPrice DECIMAL(10,2),
    FOREIGN KEY (InvoiceID) REFERENCES Invoices(InvoiceID),
	FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (UsedServiceID) REFERENCES UsedServices(UsedServiceID),
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID)
);

-- Bảng Reviews (Đánh giá)
CREATE TABLE Reviews (
    ReviewID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT, -- Sử dụng UserID từ bảng Users
    RoomID INT,
    ServiceID INT NULL, -- Đánh giá dịch vụ
    Rating INT, -- Đánh giá từ 1 đến 5
    Comment NVARCHAR(255),
    ReviewDate DATETIME,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
    FOREIGN KEY (ServiceID) REFERENCES Services(ServiceID)
);

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

-- Chèn dữ liệu vào bảng Users
INSERT INTO Users (FullName, Email, PhoneNumber, Address, Username, Password, RegistrationDate, Role)
VALUES 
('Nguyễn Văn A', 'nguyenvana@gmail.com', '0987654321', '123 Đường ABC, Quận 1, TP.HCM', 'nguyenvana', 'hashed_password_A', GETDATE(), 'Customer'),
('Trần Thị B', 'tranthib@gmail.com', '0976543210', '456 Đường DEF, Quận 2, TP.HCM', 'tranthib', 'hashed_password_B', GETDATE(), 'Customer'),
('Lê Văn C', 'levanc@gmail.com', '0965432109', '789 Đường GHI, Quận 3, TP.HCM', 'levanc', 'hashed_password_C', GETDATE(), 'Admin'),
('Phạm Thị D', 'phamthid@gmail.com', '0954321098', '321 Đường JKL, Quận 4, TP.HCM', 'phamthid', 'hashed_password_D', GETDATE(), 'Admin');

-- Chèn dữ liệu vào bảng Rooms
INSERT INTO Rooms (RoomType, Price, Status, Description, ImageRooms)
VALUES 
('Single', 500000, 'Available', 'Phòng đơn thoải mái cho 1 người.', 'single_room.jpg'),
('Double', 800000, 'Available', 'Phòng đôi thích hợp cho 2 người.', 'double_room.jpg'),
('Suite', 1500000, 'Available', 'Phòng sang trọng với các tiện nghi cao cấp.', 'suite_room.jpg');

-- Chèn dữ liệu vào bảng Services
INSERT INTO Services (ServiceName, ServiceCategory, Price, Description, StatusServices, ImageServices)
VALUES 
('Massage', 'Spa', 300000, 'Dịch vụ massage thư giãn.', 'Available', 'massage.jpg'),
('Bữa sáng', 'Food', 150000, 'Bữa sáng tự chọn tại nhà hàng.', 'Available', 'breakfast.jpg'),
('Tour thành phố', 'Tour', 1200000, 'Chuyến tham quan thành phố trong 1 ngày.', 'Available', 'city_tour.jpg');

-- Chèn dữ liệu vào bảng Promotions
INSERT INTO Promotions (PromotionCode, Description, Discount, IsFlashDeal, StartDate, EndDate)
VALUES 
('DISCOUNT10', 'Giảm giá 10% cho đặt phòng đầu tiên.', 10.00, 0, '2024-01-01', '2024-12-31'),
('FLASH20', 'Ưu đãi chớp nhoáng: Giảm giá 20% cho đặt phòng trong ngày.', 20.00, 1, '2024-01-01', '2024-01-31');

-- Chèn dữ liệu vào bảng Bookings
INSERT INTO Bookings (UserID, RoomID, PromotionID, NumberOfGuests, BookingDate, CheckInDate, CheckOutDate, TotalPrice, Status, PaymentType)
VALUES 
(1, 1, NULL, 1, GETDATE(), '2024-10-15', '2024-10-20', 2500000, 'Confirmed', 'Prepaid'),
(2, 2, 1, 2, GETDATE(), '2024-10-16', '2024-10-18', 1440000, 'Confirmed', 'Upon Arrival');

-- Chèn dữ liệu vào bảng Invoices
INSERT INTO Invoices (BookingID, InvoiceDate, TotalAmount, IsPaid, PaymentStatus, PaymentMethod)
VALUES 
(1, GETDATE(), 2500000, 1, 'Paid', 'Credit Card'),
(2, GETDATE(), 1440000, 0, 'Unpaid', 'Cash');

-- Chèn dữ liệu vào bảng Reviews
INSERT INTO Reviews (UserID, RoomID, ServiceID, Rating, Comment, ReviewDate)
VALUES 
(1, 1, NULL, 5, 'Phòng rất đẹp và sạch sẽ!', GETDATE()),
(2, 2, 1, 4, 'Dịch vụ massage rất thư giãn.', GETDATE());

-- Chèn dữ liệu vào bảng UsedServices
INSERT INTO UsedServices (BookingID, ServiceID, Quantity, TotalPrice)
VALUES 
(1, 1, 1, 300000),
(2, 2, 2, 300000);