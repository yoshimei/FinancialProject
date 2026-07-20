USE FinancialDB;
GO

-- ============================================================
-- [User] 使用者
-- ============================================================
IF OBJECT_ID(N'dbo.[User]', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.[User]
    (
        UserID      VARCHAR(20)     NOT NULL,
        UserName    NVARCHAR(50)    NOT NULL,
        Email       NVARCHAR(100)   NOT NULL,
        Account     VARCHAR(20)     NOT NULL,
        CreatedDate DATETIME2(3)    NOT NULL CONSTRAINT DF_User_CreatedDate DEFAULT (SYSDATETIME()),
        UpdatedDate DATETIME2(3)    NOT NULL CONSTRAINT DF_User_UpdatedDate DEFAULT (SYSDATETIME()),
        CONSTRAINT PK_User PRIMARY KEY CLUSTERED (UserID)
    );
END
GO

-- ============================================================
-- Product 產品資料
-- ============================================================
IF OBJECT_ID(N'dbo.Product', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Product
    (
        No          INT IDENTITY(1,1)   NOT NULL,
        ProductName NVARCHAR(100)       NOT NULL,
        Price       DECIMAL(18,2)       NOT NULL,
        FeeRate     DECIMAL(9,4)        NOT NULL,
        CreatedDate DATETIME2(3)        NOT NULL CONSTRAINT DF_Product_CreatedDate DEFAULT (SYSDATETIME()),
        UpdatedDate DATETIME2(3)        NOT NULL CONSTRAINT DF_Product_UpdatedDate DEFAULT (SYSDATETIME()),
        CONSTRAINT PK_Product PRIMARY KEY CLUSTERED (No)
    );
END
GO

-- ============================================================
-- LikeList 喜好清單
-- ============================================================
IF OBJECT_ID(N'dbo.LikeList', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.LikeList
    (
        SN          INT IDENTITY(1,1)   NOT NULL,
        UserID      VARCHAR(20)         NOT NULL,
        ProductNo   INT                 NOT NULL,
        Qty         INT                 NOT NULL,
        TotalAmount DECIMAL(18,2)       NOT NULL,
        TotalFee    DECIMAL(18,2)       NOT NULL,
        CreatedDate DATETIME2(3)        NOT NULL CONSTRAINT DF_LikeList_CreatedDate DEFAULT (SYSDATETIME()),
        UpdatedDate DATETIME2(3)        NOT NULL CONSTRAINT DF_LikeList_UpdatedDate DEFAULT (SYSDATETIME()),
        CONSTRAINT PK_LikeList PRIMARY KEY CLUSTERED (SN),
        CONSTRAINT FK_LikeList_User    FOREIGN KEY (UserID)    REFERENCES dbo.[User](UserID),
        CONSTRAINT FK_LikeList_Product FOREIGN KEY (ProductNo) REFERENCES dbo.Product(No),
        CONSTRAINT UQ_LikeList_ProductNo UNIQUE (ProductNo)
    );

    CREATE NONCLUSTERED INDEX IX_LikeList_UserID ON dbo.LikeList(UserID);
END
GO
