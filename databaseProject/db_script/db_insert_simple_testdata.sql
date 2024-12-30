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
VALUES('001','Onion_pancake_Shop.jpg','2F,middle','Onion pancake Shop','Monday to Friday 7:30 to 19:00'),
('002','Xuan_Fang.jpg','2F,left','Xuan Fang','Monday to Friday 8:00 to 19:30'),
('003','Sun_Cuisine.jpg','2F,left','Sun Cuisine','Monday to Friday 11:00 to 19:00'),
('004','Wenhua_Canteen.jpg','2F,left','Wenhua Canteen','Monday to Friday 8:00 to 19:00');

INSERT INTO PRODUCT(Product_ID,Shop_ID,product_name,image,price,stock)
VALUES('0011','001','Onion_pancake','Onion_pancake.jpg',30,75),
('0012','001','Egg_onion_pancake','Egg_Onion_pancake.jpg',40,60),
('0013','001','Nine_egg_onion_pancake','Nine_Egg_Onion_pancake.jpg',45,80),
('0014','001','black_tea','black_tea.jpg',20,200),
('0015','001','soy_milk','soy_milk.jpg',20,200),
('0016','001','soy_milk_black_tea','soy_milk_black_tea.jpg',20,200),
('0017','001','traditional_tofu_pudding','traditional_tofu_pudding.jpg',45,100),
('0018','001','soy_milk_tofu_pudding','soy_milk_tofu_pudding.jpg',45,100),
('0019','001','brown_sugar_shaved_ice','brown_sugar_shaved_ice.jpg',45,100),
('0021','002','larb_pork','larb_pork.jpg',90,100),
('0022','002','larb_pork_combo','larb_pork_combo.jpg',100,100),
('0023','002','larb_chicken','larb_chicken.jpg',105,100),
('0024','002','Hainanese_chicken_rice','Hainanese_chicken_rice.jpg',110,100),
('0025','002','BBQ_pork_rice','BBQ_pork_rice.jpg',110,100),
('0026','002','Thai_style_spicy_chicken','Thai_style_spicy_chicken.jpg',120,100),
('0031','003','deep_fried_chicken_rice','deep_fried_chicken_rice.jpg',110,100),
('0032','003','sweet_spicy_deep_fried_chicken_rice','sweet_spicy_deep_fried_chicken_rice.jpg',120,100),
('0033','003','beef_rice','beef_rice.jpg',120,100),
('0034','003','kimchi_pork_rice','kimchi_prok_rice.jpg',120,100),
('0035','003','Korean_spicy_fried_chicken_rice','Korean_spicy_fried_chicken_rice.jpg',130,100),
('0036','003','kimchi_beef_rice','kimchi_beef_rice.jpg',130,100),
('0037','003','egg_beef_rice','egg_beef_rice.jpg',130,100),
('0038','003','two_selected_rice','two_selected_rice.jpg',130,100),
('0039','003','Korean_spicy_rice_cake_kimchi_pork_rice','Korean_spicy_rice_cake_kimchi_pork_rice.jpg',130,100),
('0041','004','beef_over_rice','two_selected_rice.jpg',95,100),
('0042','004','spicy_entury_egg_rice','spicy_entury_egg_rice.jpg',80,100),
('0043','004','braised_beef_rice','braised_beef_rice.jpg',70,100),
('0044','004','chicken_over_rice','chicken_over_rice.jpg',85,100),
('0045','004','mushroom_braised_pork_rice','mushroom_braised_pork_rice.jpg',60,100),
('0046','004','larb_pork_rice','two_selected_rice.jpg',60,100),
('0047','004','fried_sauce_rice','fried_sauce_rice.jpg',60,100),
('0048','004','pork_thick_soup_rice','pork_thick_soup_rice.jpg',75,100),
('0049','004','squid_thick_soup_rice','squid_thick_soup_rice.jpg',75,100);

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
