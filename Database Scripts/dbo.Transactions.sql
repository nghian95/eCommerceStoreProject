USE [eCommerceDB]
GO

/****** Object: Table [dbo].[Transactions] Script Date: 5/1/2021 1:10:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transactions] (
    [TransactionID]    INT             IDENTITY (1, 1) NOT NULL,
    [SKU]              VARCHAR (20)    NULL,
    [Quantity]         INT             NULL,
    [TotalPrice]       DECIMAL (18, 2) NULL,
    [TransactionGroup] INT             NULL,
    [UserName]         VARCHAR (20)    NULL,
    [Status]           INT             NULL
);


