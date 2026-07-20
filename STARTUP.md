## 事前需求

- .NET 10 SDK
- MSSQL（自行安裝、docker）

## Step 1：準備 SQL Server

有MSSQL的話可直接使用MSSQL往下一步執行。
若沒有，可以使用docker來起，我自己手邊因為不是windows，所以是使用docker搭配Vscode的SQL Server (mssql)套件，這裡提供參考指令：

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<自訂密碼>" \
  -p 1433:1433 --name financial-project-mssql \
  -d mcr.microsoft.com/mssql/server:2025-latest
```
密碼至少8碼含大小寫+數字/符號

## Step 2：建立資料庫結構

`DB/` 資料夾內有 6 支依序命名的 SQL 腳本，根據編號執行前 5 支（`99_DropAll.sql` 是開發期重置用，正常啟動不需要）：

```
DB/01_CreateDatabase.sql
DB/02_Tables.sql
DB/03_StoredProcedures_User.sql
DB/04_StoredProcedures_LikeList.sql
DB/05_SeedData.sql
```

## Step 3：設定連線字串

複製 `src/FinancialProject.Web/appsettings.Development.json.example` 並改名為 `appsettings.Development.json`（連線字串不進版控，僅提供範例協助啟動專案）：

打開複製出來的 `appsettings.Development.json`，把 `ConnectionStrings:FinancialDb` 裡的 `<host>`、`<port>`、`<帳號>`、`<密碼>` 換成 Step 1 準備好的資訊。

## Step 4：執行專案

回到專案根目錄，執行：

```bash
dotnet run --project src/FinancialProject.Web
```

## 測試帳號

`05_SeedData.sql` 已有兩個測試使用者，登入頁僅需輸入 UserID：

| UserID | UserName | Email | Account |
|---|---|---|---|
| A1236456789 | 王o明 | test1@email.com | 1111999666 |
| B987654321 | 陳小華 | test2@email.com | 2222888777 |

## 專案結構簡介

```
src/
├─ FinancialProject.Common/     共用層：DTO、Repository/Service 介面、自訂例外
├─ FinancialProject.Data/       資料層：連線工廠、Repository（呼叫 Stored Procedure）
├─ FinancialProject.Business/   業務層：金額計算、商業規則驗證
└─ FinancialProject.Web/        展示層：Controller、ViewModel、View（ASP.NET MVC + Bootstrap）
```

