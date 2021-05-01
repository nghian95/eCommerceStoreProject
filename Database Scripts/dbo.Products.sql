USE [eCommerceDB]
GO

/****** Object: Table [dbo].[Products] Script Date: 5/1/2021 1:10:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products] (
    [SKU]         VARCHAR (20)    NOT NULL,
    [Name]        VARCHAR (40)    NOT NULL,
    [Quantity]    INT             NULL,
    [Description] VARCHAR (250)   NULL,
    [Price]       DECIMAL (18, 2) NULL,
    [CategoryID]  INT             NULL,
    [Sale]        FLOAT (53)      NULL
);


