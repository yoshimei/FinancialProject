USE FinancialDB;
GO

-- 測試用User資料（王o明的ID完全按照題目給的，並非輸入錯誤）。
IF NOT EXISTS (SELECT 1 FROM dbo.[User] WHERE UserID = N'A1236456789')
BEGIN
    INSERT INTO dbo.[User] (UserID, UserName, Email, Account)
    VALUES (N'A1236456789', N'王o明', N'test1@email.com', N'1111999666');
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.[User] WHERE UserID = N'B987654321')
BEGIN
    INSERT INTO dbo.[User] (UserID, UserName, Email, Account)
    VALUES (N'B987654321', N'陳小華', N'test2@email.com', N'2222888777');
END
GO
