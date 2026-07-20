-- 建立資料庫（若不存在）
IF DB_ID(N'FinancialDB') IS NULL
BEGIN
    CREATE DATABASE FinancialDB;
END
GO
