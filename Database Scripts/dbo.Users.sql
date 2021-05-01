USE [eCommerceDB]
GO

/****** Object: Table [dbo].[Users] Script Date: 5/1/2021 1:11:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users] (
    [UserName]  VARCHAR (20)  NOT NULL,
    [Password]  VARCHAR (30)  NOT NULL,
    [Access]    INT           NOT NULL,
    [FirstName] VARCHAR (25)  NULL,
    [LastName]  VARCHAR (25)  NULL,
    [Address]   VARCHAR (100) NULL,
    [Phone]     INT           NULL,
    [Email]     VARCHAR (50)  NULL
);


