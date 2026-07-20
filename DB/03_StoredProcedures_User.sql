USE FinancialDB;
GO

-- ============================================================
-- usp_User_ValidateLogin
-- 用途：簡易登入驗證，僅確認 UserID 是否存在。
-- 回傳：0 或 1 筆 User 資料。
-- ============================================================
CREATE OR ALTER PROCEDURE dbo.usp_User_ValidateLogin
    @UserID VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT UserID, UserName, Email, Account
    FROM dbo.[User]
    WHERE UserID = @UserID;
END
GO

-- ============================================================
-- usp_User_GetProfile
-- 用途：取得目前使用者資料（如新增表單預帶 Account、畫面顯示使用者資訊卡）。
-- 回傳：0 或 1 筆 User 資料。
-- ============================================================
CREATE OR ALTER PROCEDURE dbo.usp_User_GetProfile
    @UserID VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT UserID, UserName, Email, Account
    FROM dbo.[User]
    WHERE UserID = @UserID;
END
GO
