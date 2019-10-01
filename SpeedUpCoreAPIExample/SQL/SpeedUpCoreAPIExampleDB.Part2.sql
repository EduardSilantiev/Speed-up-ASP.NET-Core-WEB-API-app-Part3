/*    ==SpeedUpCoreAPIExampleDB.Part2.sql ==

	The final version of the SpeedUpCoreAPIExampleDB database
	create script after Part 2
*/


USE [master]
GO

CREATE DATABASE [SpeedUpCoreAPIExampleDB] 
GO 
 
USE [SpeedUpCoreAPIExampleDB] 
GO 

CREATE TABLE [dbo].[Products] (
    [ProductId] INT         IDENTITY (1, 1) NOT NULL,
    [SKU]       NVARCHAR (50)  NOT NULL,
    [Name]      NVARCHAR (150) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductId] ASC)
);
GO

CREATE TABLE [dbo].[Prices] (
    [PriceId]   INT             IDENTITY (1, 1) NOT NULL,
    [ProductId] INT             NOT NULL,
    [Value]     DECIMAL (18, 2) NOT NULL,
    [Supplier]  NVARCHAR (50)   NOT NULL,
	[xProductId]  AS ('x'+CONVERT([NVARCHAR](10),[ProductId])) PERSISTED NOT NULL,

    CONSTRAINT [PK_Prices] PRIMARY KEY CLUSTERED ([PriceId] ASC)
);
GO

ALTER TABLE [dbo].[Prices]  WITH CHECK ADD  CONSTRAINT [FK_Prices_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prices] CHECK CONSTRAINT [FK_Prices_Products]
GO

CREATE FULLTEXT CATALOG [ProductsFTS] WITH ACCENT_SENSITIVITY = ON
GO

CREATE FULLTEXT INDEX ON [dbo].[Products]
(SKU LANGUAGE 1033)
KEY INDEX PK_Products
ON ProductsFTS
GO

CREATE FULLTEXT CATALOG [PricesFTS] WITH ACCENT_SENSITIVITY = ON
GO

CREATE FULLTEXT INDEX ON [dbo].[Prices]
(xProductId LANGUAGE 1033)
KEY INDEX PK_Prices
ON PricesFTS
GO

CREATE PROCEDURE [dbo].[GetProductsBySKU]
	@sku [varchar] (50) 
AS
BEGIN
	SET NOCOUNT ON;

	Select @sku = '"' + @sku + '*"'

    -- Insert statements for procedure here
	SELECT ProductId, SKU, Name FROM [dbo].Products WHERE CONTAINS(SKU, @sku)
END
GO

CREATE PROCEDURE [dbo].[GetPricesByProductId]
	@productId [int]
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @xProductId [NVARCHAR] (10)
	Select @xProductId = '"x' + CONVERT([nvarchar](10),@productId) + '"'

    -- Insert statements for procedure here
	SELECT PriceId, ProductId, [Value], Supplier FROM [dbo].Prices WHERE CONTAINS(xProductId, @xProductId)
END
GO

INSERT INTO Products ([SKU], [Name]) VALUES ('aaa', 'Product1');
INSERT INTO Products ([SKU], [Name]) VALUES ('aab', 'Product2');
INSERT INTO Products ([SKU], [Name]) VALUES ('abc', 'Product3');

INSERT INTO Prices ([ProductId], [Value], [Supplier]) VALUES (1, 100, 'Bosch');
INSERT INTO Prices ([ProductId], [Value], [Supplier]) VALUES (1, 125, 'LG');
INSERT INTO Prices ([ProductId], [Value], [Supplier]) VALUES (1, 130, 'Garmin');

INSERT INTO Prices ([ProductId], [Value], [Supplier]) VALUES (2, 140, 'Bosch');
INSERT INTO Prices ([ProductId], [Value], [Supplier]) VALUES (2, 145, 'LG');
INSERT INTO Prices ([ProductId], [Value], [Supplier]) VALUES (2, 150, 'Garmin');

INSERT INTO Prices ([ProductId], [Value], [Supplier]) VALUES (3, 160, 'Bosch');
INSERT INTO Prices ([ProductId], [Value], [Supplier]) VALUES (3, 165, 'LG');
INSERT INTO Prices ([ProductId], [Value], [Supplier]) VALUES (3, 170, 'Garmin');

GO