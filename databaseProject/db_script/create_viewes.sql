use TaipeiTechIResturant_DB;

CREATE VIEW MEMBER_CART AS
SELECT MEMBERS.Member_ID, Cart_ID, member_type,member_account,member_password
FROM MEMBERS LEFT JOIN CART ON MEMBERS.Member_ID = CART.Member_ID;

CREATE VIEW Who_Product AS
SELECT Product_ID,Business_ID
FROM PRODUCT RIGHT JOIN BUSINESS ON PRODUCT.Shop_ID = BUSINESS.Shop_ID;

CREATE VIEW Order_Product AS
SELECT Order_ID,product_name,amount,price*discount*amount AS price
FROM IN_ORDER LEFT JOIN PRODUCT ON IN_ORDER.Product_ID = PRODUCT.Product_ID;

CREATE VIEW Order_Who AS
SELECT Business_ID,Order_ID,ORDERS.Member_ID,phone_Number,order_time,order_states
FROM ORDERS LEFT JOIN MEMBERS ON ORDERS.Member_ID = MEMBERS.Member_ID;

CREATE VIEW Business_Product AS
SELECT Business_ID,Product_ID,product_name,describition,image,price,stock,discount
FROM BUSINESS RIGHT JOIN PRODUCT ON PRODUCT.Shop_ID = BUSINESS.Shop_ID;