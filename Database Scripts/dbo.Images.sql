USE [eCommerceDB]
GO

/****** Object: Table [dbo].[Images] Script Date: 5/1/2021 1:10:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Images] (
    [ImageID]        INT             IDENTITY (1, 1) NOT NULL,
    [ImageName]      VARCHAR (40)    NOT NULL,
    [OriginalFormat] NVARCHAR (5)    NOT NULL,
    [ImageFile]      VARBINARY (MAX) NOT NULL,
    [SKU]            VARCHAR (20)    NOT NULL
);


