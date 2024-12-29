DROP DATABASE IF EXISTS taipeitechiresturant_db;
CREATE DATABASE taipeitechiresturant_db;
use taipeitechiresturant_db;

-- 建立 Users 資料表
CREATE TABLE Users (
    Id int AUTO_INCREMENT primary key, -- 自動遞增的主鍵
    FullName VARCHAR(100) NOT NULL,        -- 使用者名稱
    Email VARCHAR(100) NOT NULL UNIQUE -- 使用者電子郵件，唯一值
);

-- 插入初始資料
INSERT INTO Users (FullName, Email) VALUES ('Alice', 'alice@example.com');
INSERT INTO Users (FullName, Email) VALUES ('Bob', 'bob@example.com');
INSERT INTO Users (FullName, Email) VALUES ('Charlie', 'charlie@example.com');
