create database TaipeiTechIResturant_DB;
use TaipeiTechIResturant_DB;
create table MEMBERS(
	Member_ID			varchar(10) not null,
	member_type			Enum('Member','Business','Administrator') not null,
	phone_Number		varchar(10),
	member_account		varchar(30) not null,
	member_password		varchar(30) not null,
	primary key (Member_ID)
);
create table SHOP(
	Shop_ID				varchar(10) not null,
	image				varchar(50),
	location			varchar(20),
	describition		varchar(50),
	business_time		varchar(200),
	primary key (Shop_ID)
);
create table CART(
	Cart_ID				varchar(10) not null,
    Member_ID			varchar(10) not null,
    primary key (Cart_ID),
    foreign key (Member_ID) references MEMBERS(Member_ID)
);
create table PRODUCT(
	Product_ID			varchar(10) not null,
    Shop_ID				varchar(10) not null,
    product_name		varchar(50) not null,
    describition		varchar(50),
    discount			double default 1 not null,
    image				varchar(50),
    price				int not null,
    stock				int default 0 not null,
    primary key (Product_ID),
    foreign key (Shop_ID) references SHOP(Shop_ID)
);
create table BUSINESS(
	Business_ID			varchar(10) not null,
    Shop_ID				varchar(10),
    Member_ID			varchar(10) not null,
    primary key (Business_ID),
    foreign key (Shop_ID) references SHOP(Shop_ID),
    foreign key (Member_ID) references MEMBERS(Member_ID)
);
create table ADMINISTRATOR(
	Administrator_ID	varchar(10) not null,
    Member_ID			varchar(10) not null,
    primary key (Administrator_ID),
    foreign key (Member_ID) references MEMBERS(Member_ID)
);
create table IN_CART(
	In_Cart_ID			varchar(10) not null,
    Cart_ID				varchar(10) not null,
    Product_ID			varchar(10) not null,
    amount				int not null,
    primary key (In_Cart_ID),
    foreign key (Product_ID) references PRODUCT(Product_ID),
    foreign key (Cart_ID) references CART(Cart_ID)
);
create table ORDERS(
	Order_ID			varchar(10) not null,
    Member_ID			varchar(10) not null,
	Business_ID			varchar(10) not null,
	order_time			time not null,
    order_states		Enum('Checking','Preparing','Ready') not null,
    primary key (Order_ID),
    foreign key (Member_ID) references MEMBERS(Member_ID),
    foreign key (Business_ID) references BUSINESS(Business_ID)
);
create table IN_ORDER(
	InOrder_ID			varchar(10) not null,
    Product_ID			varchar(10) not null,
	Order_ID			varchar(10) not null,
	amount				int not null,
    primary key (InOrder_ID),
    foreign key (Product_ID) references PRODUCT(Product_ID),
    foreign key (Order_ID) references ORDERS(Order_ID)
);
create table PUBLISH_COUPON(
	Coupon_ID			varchar(10) not null,
    Business_ID			varchar(20) not null,
    primary key (Coupon_ID),
    foreign key (Business_ID) references BUSINESS(Business_ID)
);
create table COUPON(
	Coupon_ID			varchar(10) not null,
    Member_ID			varchar(10) not null,
    Order_ID			varchar(10),
    coupon_name			varchar(50) not null,
    time_limit			date,
    describition		varchar(50),
    discount			double not null,
    is_used				bool default false not null,
    primary key (Coupon_ID),
    foreign key (Coupon_ID) references PUBLISH_COUPON(Coupon_ID),
    foreign key (Member_ID) references MEMBERS(Member_ID),
    foreign key (Order_ID) references ORDERS(Order_ID)
);
create table RATE(
	Rate_ID				varchar(10) not null,
    Member_ID			varchar(10) not null,
	Shop_ID				varchar(10) not null,
	Product_ID			varchar(10) not null,
	describition		varchar(200),
    stars				tinyint,
    rate_time			date,
    primary key (Rate_ID),
	foreign key (Member_ID) references MEMBERS(Member_ID),
	foreign key (Shop_ID) references SHOP(Shop_ID),
    foreign key (Product_ID) references PRODUCT(Product_ID)
);

     