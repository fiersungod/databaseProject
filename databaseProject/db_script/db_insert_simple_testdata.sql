use TaipeiTechIResturant_DB;

INSERT INTO MEMBERS (Member_ID,member_type,phone_Number,member_account,member_password)
VALUES('000000001','member','0900123456','abcdefg@gmail.com','123456789'),
('000000002','member','0900012345','hijklm@gmail.com','qwerty123'),
('000000003','member','0900001234','nopqrst@gmail.com','zxcv0123'),
('000000004','business','0900000123','uvwxyzab@gmail.com','00000'),
('000000005','administrator','0900000012','cdefghi@gmail.com','password');

INSERT INTO ADMINISTRATOR (Administrator_ID,Member_ID)
VALUES('001','000000005');

INSERT INTO CART (Cart_ID,Member_ID)
VALUES('846ef27ab','000000001'),
('1976df157','000000002'),
('defc8763b','000000003');

INSERT INTO SHOP (Shop_ID,image,location,describition,business_time)
VALUES('001','Onion_pancake_Shop.jpg','2F,middle','Onion pancake Shop','monday 7:30 to 19:00');

INSERT INTO PRODUCT(Product_ID,Shop_ID,product_name,image,price,stock)
VALUES('0011','001','Onion_pancake','Onion_pancake.jpg',30,75),
('0012','001','black_tea','black_tea.jpg',20,200);

INSERT INTO BUSINESS (Business_ID,Shop_ID,Member_ID)
VALUES('001','001','000000004');

INSERT INTO IN_CART (In_Cart_ID,Cart_ID,Product_ID,amount)
VALUES('8cf45a6b2','846ef27ab','0011',1),
('ab567f07a','846ef27ab','0012',1);

INSERT INTO ORDERS (Order_ID,Member_ID,Business_ID,order_time,order_states)
VALUES('9ea27530c','000000001','001','10:30:42','Checking');

INSERT INTO IN_ORDER (InOrder_ID,Product_ID,Order_ID,amount)
VALUES('87dca5612','0011','9ea27530c',1),
('12a65cdef','0012','9ea27530c',1);

INSERT INTO RATE (Rate_ID,Member_ID,Shop_ID,Product_ID,describition,stars,rate_time)
VALUES('a1b3479df','000000001','001','0011','good!',5,'2024-12-5');
