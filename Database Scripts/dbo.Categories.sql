USE [eCommerceDB]
GO

/****** Object: Table [dbo].[Categories] Script Date: 5/1/2021 1:10:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Categories] (
    [CategoryID] INT          IDENTITY (1, 1) NOT NULL,
    [Name]       VARCHAR (30) NOT NULL
);


