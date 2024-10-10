Drop Table [Cart];
Drop Table [Product_Rating];
Drop Table Order_Item;
Drop Table [Order];
Drop Table Delivery;
Drop Table Product_Variation;
Drop Table Product;
Drop Table Payment;
Drop Table [User];


CREATE TABLE [dbo].[User] (
    [id]        INT            IDENTITY (1, 1) NOT NULL,
    [email]     NVARCHAR (50)  NULL,
    [phone_num] NVARCHAR (12)  NULL,
    [address]   NVARCHAR (200) NULL,
    [name]      NVARCHAR (100) NULL,
	[login_name] NVARCHAR(100) NOT NULL,
    [user_type] NVARCHAR (20)  NULL,
    [dob]       DATE           NULL,
    [status]    INT            DEFAULT ((1)) NULL,
    [profile]   NVARCHAR (50)  DEFAULT ('default.jpg') NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[Payment]
(
	[id] INT NOT NULL IDENTITY PRIMARY KEY,
	[payment_datetime] DATETIME,
	[payment_method] NVARCHAR(50),
	[payment_amount] DECIMAL(10,2)
	
);

CREATE TABLE [dbo].[Product]
(
	[id] INT NOT NULL IDENTITY PRIMARY KEY,
	[product_name] NVARCHAR(200),
	[product_category] NVARCHAR(200),
	[product_type] NVARCHAR(200),
	[description] NVARCHAR(3000),
	[stock_status] INT DEFAULT 1,
	[status] INT DEFAULT 1,						-- 1 shows visible, TEMPORARY DELETE, OR PERMANENTLY DELETE
	[product_image_1] NVARCHAR(1000) default 'default_product.png',
	[product_image_2] NVARCHAR(1000) default 'default_product.png',
	[product_image_3] NVARCHAR(1000) default 'default_product.png'
)

CREATE TABLE [dbo].[Product_Variation]
(
	[id] INT NOT NULL IDENTITY PRIMARY KEY,
	[variation_name] NVARCHAR(1000),
	[price] DECIMAL(6,2) DEFAULT 0.00,
	[stock_quantity] INT DEFAULT 0,
	[status] INT DEFAULT 0,                              -- whether to display variation in product page                         
	[product_id] INT,

FOREIGN KEY(product_id) REFERENCES Product(id) --ON DELETE CASCADE
)

CREATE TABLE [dbo].[Delivery] (
    [id]                  INT IDENTITY (1, 1) NOT NULL,
    [address]             NVARCHAR (200) ,
    [estimateDeliverDate] DATE           ,
    [deliveredDatetime]   DATETIME       ,
    [contactPerson]       NVARCHAR (100) ,
    [phone_num]           NVARCHAR (12) ,
    PRIMARY KEY CLUSTERED ([id] ASC)
);
-- 

CREATE TABLE [dbo].[Order] (
    [id]            INT      IDENTITY (1, 1) NOT NULL,
    [payment_id]    INT      NOT NULL,
    [delivery_id]   INT      NOT NULL,
    [user_id]       INT      NOT NULL,
    [status]        NVARCHAR(50),
    [orderDatetime] DATETIME,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([payment_id]) REFERENCES [dbo].[Payment] ([id]), -- ON DELETE CASCADE,
    FOREIGN KEY ([delivery_id]) REFERENCES [dbo].[Delivery] ([id]), -- ON DELETE CASCADE
	FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([id])
);
CREATE TABLE [dbo].[Product_Rating]
(
	[id] INT NOT NULL IDENTITY PRIMARY KEY,
	[rating_num] DECIMAL(3,2) DEFAULT 0.00,
	[comment] NVARCHAR(3000),
	[adminReply] NVARCHAR(3000), --lets say admin reply, refer to which rate id
	[created_at] DATETIME,
	[user_id] INT,
	[variation_id] INT,

	FOREIGN KEY([user_id]) REFERENCES "User"(id),
	FOREIGN KEY(variation_id) REFERENCES [Product_Variation](id),
	CONSTRAINT RatingNum_Ck Check(rating_num BETWEEN 0 AND 5)
)
CREATE TABLE [dbo].[Order_Item] (
    [id]           INT IDENTITY (1, 1) NOT NULL,
    [order_id]     INT NOT NULL,
    [variation_id] INT NOT NULL,
    [purchase_qty] INT DEFAULT 0,
	[rating_id] INT NULL,

	
    PRIMARY KEY CLUSTERED ([order_id] ASC, [variation_id] ASC, [id] ASC),
    FOREIGN KEY ([variation_id]) REFERENCES [dbo].[Product_Variation] ([id]),
	FOREIGN KEY ([rating_id]) REFERENCES [dbo].[Product_Rating] ([id]),
    FOREIGN KEY ([order_id]) REFERENCES [dbo].[Order] ([id])
);

CREATE TABLE [dbo].[Cart] (
    [id]           INT IDENTITY (1, 1) NOT NULL,
    [user_id]      INT NOT NULL,
    [variation_id] INT NOT NULL,
    [purchase_qty] INT DEFAULT 0,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([id]) ON DELETE CASCADE,
    FOREIGN KEY ([variation_id]) REFERENCES [dbo].[Product_Variation] ([id]) ON DELETE CASCADE
);
--product list
SELECT p.id AS product_id, p.product_name,p.product_category,COALESCE(SUM(v.stock_quantity), 0) AS total_stock_quantity,p.product_image_1, p.product_image_2, COALESCE(AVG(r.rating_num), 0) AS avg_rating,COALESCE(MIN(v.price), 0) AS min_price, COALESCE(MAX(v.price), 0) AS max_price FROM Product p JOIN product_variation v ON p.id = v.product_id LEFT JOIN product_rating r ON v.id = r.variation_id WHERE p.status = 1 GROUP BY p.id,p.product_name, p.product_category, p.product_image_1, p.product_image_2;

SELECT p.id, p.product_name, p.product_category, p.product_type, p.description, p.stock_status, p.product_image_1, p.product_image_2, p.product_image_3, v.id, v.variation_name, v.price, v.stock_quantity FROM  Product p INNER JOIN Product_Variation v ON p.id = v.product_id WHERE p.id =9 and v.status =1 GROUP BY  p.id, p.product_name,p.product_category, p.product_type, p.description, p.stock_status,p.product_image_1, p.product_image_2,p.product_image_3,v.id,v.variation_name,v.price, v.stock_quantity



SELECT rating_num, COUNT(*) as total_count FROM Product_Rating R LEFT JOIN Product_Variation V ON R.variation_id = V.id LEFT JOIN Product P ON V.product_id = P.id WHERE rating_num IN (1, 2,3,4,5) and P.id = 9 GROUP BY rating_num
SELECT rating_num, COUNT(*) as total_count, ROUND(CAST(COUNT(*) AS float) / (SELECT COUNT(*) FROM Product_Rating WHERE rating_num IN (1, 2, 3, 4, 5) and variation_id IN (SELECT id FROM Product_Variation WHERE product_id = 9)) * 100, 2) as percentage FROM Product_Rating R LEFT JOIN Product_Variation V ON R.variation_id = V.id LEFT JOIN Product P ON V.product_id = P.id WHERE rating_num IN (1, 2, 3, 4, 5) and P.id = 9 GROUP BY rating_num

Select 
R.rating_num, 
R.comment, 
R.adminReply,
CONVERT(VARCHAR(12), 
R.created_at,107)  as created_at,
U.name, R.variation_id from Product_Rating R Right Join Product_Variation V ON R.variation_id = V.id RIGHT JOIN Product P ON P.id = V.product_id LEFT JOIN [User] U ON R.user_id = U.id  where P.id = 9;

Select COALESCE(AVG(r.rating_num), 0) AS avg_rating From Product_Rating R LEFT JOIN Product_Variation V ON R.variation_id = V.id LEFT JOIN Product P ON V.product_id = P.id Where P.id = 9;


SELECT p.id AS product_id, p.product_name,p.product_category,
COALESCE(SUM(v.stock_quantity), 0) AS total_stock_quantity,p.product_image_1, p.product_image_2, 
COALESCE(AVG(r.rating_num), 0) AS avg_rating,COALESCE(MIN(v.price), 0) AS min_price, 
COALESCE(MAX(v.price), 0) AS max_price FROM Product p JOIN product_variation v 
ON p.id = v.product_id LEFT JOIN product_rating r ON v.id = r.variation_id 
GROUP BY p.id,p.product_name, p.product_category, p.product_image_1, p.product_image_2;