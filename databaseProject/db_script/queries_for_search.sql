use TaipeiTechIResturant_DB;

-- general
	
-- for main page
	SELECT *
	FROM SHOP;

-- for login page
	-- Check if account exsit and if password is right, return member_id cart_id member_type
	SELECT Member_ID, Cart_ID, member_type
	FROM MEMBER_CART
	WHERE member_account = {account} AND member_password = {password};

-- for Create account page
	-- if user don't have phone number, leave that attribute with NULL
	INSERT INTO MEMBERS (Member_ID,member_type,phone_Number,member_account,member_password)
	VALUES({new member id},"Member",{phone number},{account},{password});
	
	INSERT INTO CART (Cart_ID,Member_ID)
	VALUES({new member id},{new cart id});

-- for Shop page
	-- get all products information
	SELECT Product_ID,product_name,describition,discount,image,price,stock
    FROM PRODUCT
    WHERE Shop_ID = {shop_id};
    
    -- add new item into shopping cart
    INSERT INTO IN_CART(In_Cart_ID,Cart_ID,Product_ID,amount)
    VALUES({new in_cart_id},{user cart id},{product id},{amount});
    
-- for Shopping cart page
	-- get all items already in cart
	SELECT In_Cart_ID,Product_ID,amount
    FROM IN_CART
    WHERE Cart_ID = {user cart_id};
    
    -- finding product and order relation
    SELECT *
    FROM Who_Product
    WHERE Product_ID = {product_id};
    
    -- create a new order
    INSERT INTO ORDERS(Order_ID,Member_ID,Business_ID,order_time,order_states)
    VALUES ({new order_id},{user Member_id},{business_id},CURRENT_TIME(),"Checking");
    
    -- add products into order
    INSERT INTO IN_ORDER(InOrder_ID,Product_ID,Order_ID,amount)
    VALUES({new inorder_id},{product_id},{order id},{int amount});
-- for Order page for member(1):
	-- get orders that users have
    SELECT Order_ID,order_time,order_states
    FROM ORDERS
    WHERE Member_ID = {user Member_id};
    
    -- get order details by order id
    SELECT product_name, amount, price
    FROM Order_Product
    WHERE Order_ID = {order_id};
    
-- for Order page for business(2):
	-- get orders that business have
    SELECT Order_ID,Member_ID,phone_Number,order_time,order_states
    FROM Order_Who
    WHERE Business_ID = {user Business_iD};
    
	-- get orders that business have and state:{'Checking','Preparing','Ready'} only
    SELECT Order_ID,Member_ID,phone_Number,order_time,order_states
    FROM Order_Who
    WHERE Business_ID = {user Business_iD} AND order_states = {'Checking','Preparing','Ready'};
    
    -- get order details by order id
    SELECT product_name, amount, price
    FROM Order_Product
    WHERE Order_ID = {order_id};
    
	-- change state of an order
    UPDATE ORDERS
    SET order_states = {'Checking','Preparing','Ready'}
    WHERE Order_ID = {order_id};
    
-- for Menu management page
	-- get all product and product detail that a business have
	SELECT *
    FROM Business_Product
    WHERE Business_ID = {business_ID};
    
    -- add a new product
    INSERT INTO PRODUCT(Product_ID,Shop_ID,product_name,describition,discount,image,price,stock)
    VALUES ({product info});
    
    -- modify a product by product id
    UPDATE PRODUCT
    SET product_name = {product_name},describition = {describition},discount = {discount}, image = {image} price = {price},stock = {stock}
    WHERE Product_ID = {Product_ID};
    
    -- delete a product by product id
    DELETE FROM PRODUCT
    WHERE Product_ID = {Product_ID};
    
    -- modify shop info
    UPDATE SHOP
    SET image = {image},location = {location},describition = {describition},business_time = {business_time}
    WHERE Shop_ID = {Shop_ID};
    
-- for My shop page
	SELECT Shop_ID,describition
    FROM SHOP
    WHERE Shop_ID = {Shop_ID};