IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'LspShapesDb')
BEGIN
	CREATE DATABASE [LspShapesDb]
END
GO

USE [LspShapesDb]
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='product' and xtype='U')
BEGIN
	CREATE TABLE product (
		product_id INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
		name VARCHAR(100) NOT NULL
	); 
	INSERT INTO product (name) VALUES ('T-Shirt'); 
	INSERT INTO product (name) VALUES ('Socks'); 
	INSERT INTO product (name) VALUES ('Towel'); 
	INSERT INTO product (name) VALUES ('Backpack'); 
	INSERT INTO product (name) VALUES ('Toothbrush'); 
	INSERT INTO product (name) VALUES ('Toothpaste'); 
	INSERT INTO product (name) VALUES ('Ring'); 
	INSERT INTO product (name) VALUES ('Watches'); 
	INSERT INTO product (name) VALUES ('Laptop'); 
	INSERT INTO product (name) VALUES ('Chair'); 
	INSERT INTO product (name) VALUES ('Teapot'); 
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='category' and xtype='U')
BEGIN
	CREATE TABLE category (
		category_id INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
		name VARCHAR(100) NOT NULL
	); 
	INSERT INTO category (name) VALUES ('Hygien'); 
	INSERT INTO category (name) VALUES ('Containers'); 
	INSERT INTO category (name) VALUES ('Accessories'); 
	INSERT INTO category (name) VALUES ('Clothes'); 
	INSERT INTO category (name) VALUES ('Textile'); 
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='product_category' and xtype='U')
BEGIN
	CREATE TABLE product_category (
		product_category_id INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
		product_id INT NOT NULL,
		category_id INT
	); 
	INSERT INTO product_category (product_id, category_id) VALUES (1,4);
	INSERT INTO product_category (product_id, category_id) VALUES (1,5); 
	INSERT INTO product_category (product_id, category_id) VALUES (2,4);
	INSERT INTO product_category (product_id, category_id) VALUES (2,5); 
	INSERT INTO product_category (product_id, category_id) VALUES (3,1); 
	INSERT INTO product_category (product_id, category_id) VALUES (3,5); 
	INSERT INTO product_category (product_id, category_id) VALUES (4,2); 
	INSERT INTO product_category (product_id, category_id) VALUES (4,3); 
	INSERT INTO product_category (product_id, category_id) VALUES (5,1); 
	INSERT INTO product_category (product_id, category_id) VALUES (6,1); 
	INSERT INTO product_category (product_id, category_id) VALUES (7,3); 
	INSERT INTO product_category (product_id, category_id) VALUES (8,3); 
END
