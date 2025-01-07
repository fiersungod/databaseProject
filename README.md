# databaseProject
## 環境架設
1. 下載MySQL
   
2. 安裝Visual Stdio 2022
   勾選 ASP.NET與網頁程式開發、Azure開發、Node.js開發、.NET桌面開發、資料儲存和處理
   
3. 運行專案
   使用VS2022啟動databaseProject.sln檔案

4. 建構DB
   在MySQL中依序執行專案中的以下SQL script
  create_db_v2.sql
  create_viewes.sql
  db_insert_simple_testdata.sql

5. 找到專案中的”appsettings.json”,”DBService.cs”，並且將其中的User ID 和Password 改成環境中合法的資料庫帳密。
