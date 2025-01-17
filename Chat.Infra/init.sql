-- 创建数据库
CREATE DATABASE IF NOT EXISTS ChatDb;
USE ChatDb;

-- 创建联系人表
CREATE TABLE IF NOT EXISTS contacts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId CHAR(36) NOT NULL,
    ContactUserId CHAR(36) NOT NULL
) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;

-- 创建消息表
CREATE TABLE IF NOT EXISTS messages (
    Id CHAR(36) PRIMARY KEY,
    SenderId CHAR(36) NOT NULL,
    ReceiverId CHAR(36) NOT NULL,
    Content LONGTEXT NOT NULL,
    MessageType VARCHAR(50) NOT NULL,
    Status VARCHAR(50) NOT NULL,
    CreatedTime DATETIME(6) NOT NULL,
    ReadTime DATETIME(6)
) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;

-- 创建客户端请求表
CREATE TABLE IF NOT EXISTS client_requests (
    Id CHAR(36) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Time DATETIME(6) NOT NULL
) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci; 