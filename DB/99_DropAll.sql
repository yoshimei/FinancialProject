USE FinancialDB;
GO

-- 開發期重置用：移除所有此專案db相關內容，小心使用。

IF OBJECT_ID(N'dbo.usp_LikeList_Delete', N'P') IS NOT NULL DROP PROCEDURE dbo.usp_LikeList_Delete;
IF OBJECT_ID(N'dbo.usp_LikeList_Update', N'P') IS NOT NULL DROP PROCEDURE dbo.usp_LikeList_Update;
IF OBJECT_ID(N'dbo.usp_LikeList_Create', N'P') IS NOT NULL DROP PROCEDURE dbo.usp_LikeList_Create;
IF OBJECT_ID(N'dbo.usp_LikeList_GetById', N'P') IS NOT NULL DROP PROCEDURE dbo.usp_LikeList_GetById;
IF OBJECT_ID(N'dbo.usp_LikeList_GetListByUser', N'P') IS NOT NULL DROP PROCEDURE dbo.usp_LikeList_GetListByUser;
IF OBJECT_ID(N'dbo.usp_User_GetProfile', N'P') IS NOT NULL DROP PROCEDURE dbo.usp_User_GetProfile;
IF OBJECT_ID(N'dbo.usp_User_ValidateLogin', N'P') IS NOT NULL DROP PROCEDURE dbo.usp_User_ValidateLogin;
GO

IF OBJECT_ID(N'dbo.LikeList', N'U') IS NOT NULL DROP TABLE dbo.LikeList;
IF OBJECT_ID(N'dbo.Product', N'U') IS NOT NULL DROP TABLE dbo.Product;
IF OBJECT_ID(N'dbo.[User]', N'U') IS NOT NULL DROP TABLE dbo.[User];
GO
