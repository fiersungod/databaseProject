DROP DATABASE IF EXISTS taipeitechiresturant_db;
CREATE DATABASE taipeitechiresturant_db;
use taipeitechiresturant_db;

-- �إ� Users ��ƪ�
CREATE TABLE Users (
    Id int AUTO_INCREMENT primary key, -- �۰ʻ��W���D��
    FullName VARCHAR(100) NOT NULL,        -- �ϥΪ̦W��
    Email VARCHAR(100) NOT NULL UNIQUE -- �ϥΪ̹q�l�l��A�ߤ@��
);

-- ���J��l���
INSERT INTO Users (FullName, Email) VALUES ('Alice', 'alice@example.com');
INSERT INTO Users (FullName, Email) VALUES ('Bob', 'bob@example.com');
INSERT INTO Users (FullName, Email) VALUES ('Charlie', 'charlie@example.com');
