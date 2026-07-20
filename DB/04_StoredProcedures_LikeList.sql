USE FinancialDB;
GO

-- ============================================================
-- usp_LikeList_GetListByUser
-- 用途：查詢喜好金融商品清單。
-- 回傳每一筆的產品名稱/價格/費率、購買數量、預計扣款總金額、總手續費用、扣款帳號與使用者聯絡Email。
-- ============================================================
CREATE OR ALTER PROCEDURE dbo.usp_LikeList_GetListByUser
    @UserID VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        L.SN,
        L.ProductNo,
        P.ProductName,
        P.Price,
        P.FeeRate,
        L.Qty,
        L.TotalAmount,
        L.TotalFee,
        U.Account,
        U.Email,
        L.CreatedDate,
        L.UpdatedDate
    FROM dbo.LikeList AS L
    INNER JOIN dbo.Product AS P ON P.No = L.ProductNo
    INNER JOIN dbo.[User]  AS U ON U.UserID = L.UserID
    WHERE L.UserID = @UserID
    ORDER BY L.CreatedDate DESC;
END
GO

-- ============================================================
-- usp_LikeList_GetById
-- 用途：取單筆喜好清單。
-- 若 @SN 不存在或不屬於 @UserID，回傳 0 筆。
-- ============================================================
CREATE OR ALTER PROCEDURE dbo.usp_LikeList_GetById
    @SN     INT,
    @UserID VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        L.SN,
        L.ProductNo,
        P.ProductName,
        P.Price,
        P.FeeRate,
        L.Qty,
        L.TotalAmount,
        L.TotalFee,
        U.Account,
        U.Email,
        L.CreatedDate,
        L.UpdatedDate
    FROM dbo.LikeList AS L
    INNER JOIN dbo.Product AS P ON P.No = L.ProductNo
    INNER JOIN dbo.[User]  AS U ON U.UserID = L.UserID
    WHERE L.SN = @SN AND L.UserID = @UserID;
END
GO

-- ============================================================
-- usp_LikeList_Create
-- 用途：新增喜好金融商品。
-- ============================================================
CREATE OR ALTER PROCEDURE dbo.usp_LikeList_Create
    @UserID      VARCHAR(20),
    @ProductName NVARCHAR(100),
    @Price       DECIMAL(18,2),
    @FeeRate     DECIMAL(9,4),
    @Qty         INT,
    @TotalAmount DECIMAL(18,2),
    @TotalFee    DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.[User] WHERE UserID = @UserID)
            THROW 51000, N'使用者不存在', 1;

        BEGIN TRAN;

            INSERT INTO dbo.Product (ProductName, Price, FeeRate)
            VALUES (@ProductName, @Price, @FeeRate);

            DECLARE @NewProductNo INT = SCOPE_IDENTITY();

            INSERT INTO dbo.LikeList (UserID, ProductNo, Qty, TotalAmount, TotalFee)
            VALUES (@UserID, @NewProductNo, @Qty, @TotalAmount, @TotalFee);

            DECLARE @NewSN INT = SCOPE_IDENTITY();

        COMMIT TRAN;

        SELECT @NewSN AS SN, @NewProductNo AS ProductNo;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

-- ============================================================
-- usp_LikeList_Update
-- 用途：更改喜好金融商品資訊。
-- ============================================================
CREATE OR ALTER PROCEDURE dbo.usp_LikeList_Update
    @SN          INT,
    @UserID      VARCHAR(20),
    @ProductName NVARCHAR(100),
    @Price       DECIMAL(18,2),
    @FeeRate     DECIMAL(9,4),
    @Qty         INT,
    @TotalAmount DECIMAL(18,2),
    @TotalFee    DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        DECLARE @ProductNo INT;

        SELECT @ProductNo = ProductNo
        FROM dbo.LikeList
        WHERE SN = @SN AND UserID = @UserID;

        IF @ProductNo IS NULL
            THROW 51001, N'找不到指定的喜好清單，或無權限存取', 1;

        BEGIN TRAN;

            UPDATE dbo.Product
               SET ProductName = @ProductName,
                   Price       = @Price,
                   FeeRate     = @FeeRate,
                   UpdatedDate = SYSDATETIME()
             WHERE No = @ProductNo;

            UPDATE dbo.LikeList
               SET Qty         = @Qty,
                   TotalAmount = @TotalAmount,
                   TotalFee    = @TotalFee,
                   UpdatedDate = SYSDATETIME()
             WHERE SN = @SN;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

-- ============================================================
-- usp_LikeList_Delete
-- 用途：刪除喜好金融商品資訊。
-- ============================================================
CREATE OR ALTER PROCEDURE dbo.usp_LikeList_Delete
    @SN     INT,
    @UserID VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        DECLARE @ProductNo INT;

        SELECT @ProductNo = ProductNo
        FROM dbo.LikeList
        WHERE SN = @SN AND UserID = @UserID;

        IF @ProductNo IS NULL
            THROW 51001, N'找不到指定的喜好清單，或無權限存取', 1;

        BEGIN TRAN;

            DELETE FROM dbo.LikeList WHERE SN = @SN;

            DELETE FROM dbo.Product WHERE No = @ProductNo;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO
